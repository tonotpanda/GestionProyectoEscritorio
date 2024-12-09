using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace GestionProyectoEscritorio
{
    public partial class FormProyectos : Form
    {
        private List<Usuario> listaUsuarios = new List<Usuario>(); // Lista de usuarios
        private List<Proyecto> listaProyectos = new List<Proyecto>(); // Lista de proyectos
        private static readonly string ClaveEncriptacion = "0123456789012345"; // Clave de encriptación AES

        public FormProyectos()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Evento Form_Load: Se ejecuta cuando el formulario se carga
        private void FormProyectos_Load(object sender, EventArgs e)
        {
            // Cargar los usuarios (solo desarrolladores) al iniciar el formulario
            CargarUsuariosDesdeJson();
            CargarUsuariosDesarrolladores();
        }

        // Método para cargar usuarios desde el archivo JSON (desencriptando el archivo)
        private void CargarUsuariosDesdeJson()
        {
            try
            {
                // Ruta donde se guardará el archivo JSON
                string rutaArchivo = "usuarios.json";

                // Verificar si el archivo existe
                if (File.Exists(rutaArchivo))
                {
                    // Leer el contenido del archivo (JSON encriptado)
                    string jsonEncriptado = File.ReadAllText(rutaArchivo);

                    // Desencriptar el JSON
                    string jsonDesencriptado = DesencriptarJson(jsonEncriptado);

                    // Deserializar el JSON en la lista de usuarios
                    listaUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(jsonDesencriptado) ?? new List<Usuario>();
                }
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje en caso de error
                MessageBox.Show($"Error al cargar usuarios desde JSON: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para desencriptar el JSON usando AES
        private string DesencriptarJson(string jsonEncriptado)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(ClaveEncriptacion); // Clave de 16 bytes
                aesAlg.IV = new byte[16]; // Inicialización con un IV de 16 bytes (todo 0s)

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(jsonEncriptado)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd(); // Retornar el JSON desencriptado
                        }
                    }
                }
            }
        }

        // Método para cargar solo los usuarios que son desarrolladores en el ListBox
        private void CargarUsuariosDesarrolladores()
        {
            listBoxUsuarios.Items.Clear(); // Limpiar ListBox
            foreach (var usuario in listaUsuarios.Where(u => u.EsDesarrollador))
            {
                listBoxUsuarios.Items.Add(usuario.Nombre); // Añadir usuarios desarrolladores al ListBox
            }
        }

        // Método para guardar un proyecto
        private void btnGuardarProyecto_Click(object sender, EventArgs e)
        {
            // Capturar los datos ingresados por el usuario
            string nombreProyecto = textBoxNombreProyecto.Text;
            DateTime fechaInicio = dateTimePickerFechaInicio.Value;
            DateTime fechaFin = dateTimePickerFechaFin.Value;
            List<string> usuariosSeleccionados = listBoxUsuarios.SelectedItems.Cast<string>().ToList();
            string tareas = richTextBoxTareas.Text;
            string subtareas = richTextBoxSubtareas.Text;

            // Validar los campos obligatorios
            if (string.IsNullOrEmpty(nombreProyecto) || usuariosSeleccionados.Count == 0)
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Reemplazar saltos de línea por comas en las tareas y subtareas
            tareas = ReemplazarSaltosPorComas(tareas);
            subtareas = ReemplazarSaltosPorComas(subtareas);

            // Crear el objeto del proyecto
            Proyecto nuevoProyecto = new Proyecto
            {
                NombreProyecto = nombreProyecto,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                Usuarios = usuariosSeleccionados,
                Tareas = tareas,
                Subtareas = subtareas
            };

            // Agregar el nuevo proyecto a la lista de proyectos
            listaProyectos.Add(nuevoProyecto);

            // Guardar los proyectos en el archivo JSON
            GuardarProyectosEnJson();

            // Confirmar creación del proyecto
            MessageBox.Show("Proyecto guardado correctamente.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar los campos
            textBoxNombreProyecto.Clear();
            richTextBoxTareas.Clear();
            richTextBoxSubtareas.Clear();
            listBoxUsuarios.ClearSelected();

            // Volver al formulario principal
            this.Hide();
            Form1 formularioPrincipal = new Form1();
            formularioPrincipal.Show();
            this.Close();
        }

        // Método para reemplazar saltos de línea por comas
        private string ReemplazarSaltosPorComas(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return texto;

            // Reemplazar los saltos de línea por comas
            return texto.Replace(Environment.NewLine, ", ").Replace("\n", ", ").Trim();
        }

        // Método para guardar la lista de proyectos en un archivo JSON encriptado
        private void GuardarProyectosEnJson()
        {
            try
            {
                string rutaArchivo = "proyectos.json";
                string json = JsonConvert.SerializeObject(listaProyectos, Formatting.Indented);

                // Encriptar el JSON antes de guardarlo
                string jsonEncriptado = EncriptarJson(json);

                // Guardar el JSON encriptado en el archivo
                File.WriteAllText(rutaArchivo, jsonEncriptado);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar proyectos en JSON: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para encriptar el JSON usando AES
        private string EncriptarJson(string json)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(ClaveEncriptacion); // Clave de 16 bytes
                aesAlg.IV = new byte[16]; // Inicialización con un IV de 16 bytes (todo 0s)

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(json); // Escribir el JSON en el CryptoStream
                        }
                    }
                    // Retornar el JSON encriptado en formato Base64
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
}
