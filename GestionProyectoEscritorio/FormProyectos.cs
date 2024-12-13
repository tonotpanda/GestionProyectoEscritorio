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
        private static readonly byte[] IVPersonalizado = Encoding.UTF8.GetBytes("5432109876543210"); // IV de 16 bytes

        public FormProyectos()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CargarProyectosDesdeJson(); // Cargar proyectos desde el archivo JSON encriptado
            CargarUsuariosDesdeJson();  // Cargar usuarios desde el archivo JSON
        }

        private void btnCrearProyecto_Click(object sender, EventArgs e)
        {
            string nombreProyecto = textBoxNombreProyecto.Text;
            DateTime fechaInicio = dateTimePickerFechaInicio.Value;
            DateTime fechaFin = dateTimePickerFechaFin.Value;
            List<string> usuariosSeleccionados = listBoxUsuarios.SelectedItems.Cast<Usuario>().Select(u => u.Nombre).ToList();
            string tareas = richTextBoxTareas.Text;
            string subtareas = richTextBoxSubtareas.Text;

            // Validar si se ha ingresado un nombre de proyecto
            if (string.IsNullOrEmpty(nombreProyecto))
            {
                MessageBox.Show("Por favor, ingrese el nombre del proyecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar fechas
            if (fechaInicio > fechaFin)
            {
                MessageBox.Show("La fecha de inicio no puede ser posterior a la fecha de fin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear el proyecto
            Proyecto nuevoProyecto = new Proyecto
            {
                NombreProyecto = nombreProyecto,
                FechaInicio = fechaInicio.ToString("yyyy-MM-dd"),
                FechaFin = fechaFin.ToString("yyyy-MM-dd"),
                ListaUsuarios = usuariosSeleccionados,
                Tareas = tareas.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList(), // Convertir tareas en lista de strings
                SubTareas = subtareas.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList() // Convertir subtareas en lista de strings
            };

            listaProyectos.Add(nuevoProyecto);
            GuardarProyectosEnJson(); // Guardar los proyectos en el archivo JSON

            MessageBox.Show("Proyecto guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        // Método para guardar proyectos en JSON (encriptado)
        private void GuardarProyectosEnJson()
        {
            try
            {
                // Crear la estructura JSON
                var proyectoJson = listaProyectos.Select(p => new
                {
                    NombreProyecto = p.NombreProyecto,
                    Tareas = p.Tareas,
                    SubTareas = p.SubTareas,
                    ListaUsuarios = p.ListaUsuarios,
                    FechaInicio = p.FechaInicio,
                    FechaFin = p.FechaFin
                }).ToList();

                // Serializar la estructura a JSON
                string json = JsonConvert.SerializeObject(proyectoJson, Formatting.Indented);
                string jsonEncriptado = EncriptarJson(json); // Encriptar el JSON
                File.WriteAllText("proyectos.json", jsonEncriptado); // Guardar en el archivo
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar proyectos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para encriptar JSON (igual que en FormUsuarios)
        private string EncriptarJson(string json)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(ClaveEncriptacion);
                aesAlg.IV = IVPersonalizado; // IV invertido

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length); // Escribir el IV primero

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(json); // Escribir el JSON en el CryptoStream
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        // Método para cargar proyectos desde JSON (desencriptado)
        private void CargarProyectosDesdeJson()
        {
            try
            {
                string rutaArchivo = "proyectos.json";

                if (File.Exists(rutaArchivo))
                {
                    string jsonEncriptado = File.ReadAllText(rutaArchivo);
                    string json = DesencriptarJson(jsonEncriptado); // Desencriptar el JSON
                    var listaProyectosJson = JsonConvert.DeserializeObject<List<object>>(json);

                    // Si es necesario, procesar la lista de proyectos de la estructura
                    if (listaProyectosJson != null && listaProyectosJson.Count > 0)
                    {
                        // Cargar los proyectos
                        listaProyectos = listaProyectosJson.Select(p => JsonConvert.DeserializeObject<Proyecto>(JsonConvert.SerializeObject(p))).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar proyectos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para desencriptar JSON
        private string DesencriptarJson(string jsonEncriptado)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(ClaveEncriptacion);
                aesAlg.IV = IVPersonalizado;

                byte[] datosEncriptados = Convert.FromBase64String(jsonEncriptado);
                byte[] iv = new byte[16];
                Array.Copy(datosEncriptados, 0, iv, 0, iv.Length);

                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(datosEncriptados, 16, datosEncriptados.Length - 16))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        // Método para cargar usuarios desde el archivo JSON (encriptado)
        private void CargarUsuariosDesdeJson()
        {
            try
            {
                string rutaArchivo = "usuarios.json";

                if (File.Exists(rutaArchivo))
                {
                    string jsonEncriptado = File.ReadAllText(rutaArchivo);
                    string json = DesencriptarJson(jsonEncriptado); // Desencriptar el JSON
                    listaUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(json) ?? new List<Usuario>();

                    // Filtrar los usuarios para mostrar solo los que son desarrolladores
                    var usuariosDesarrolladores = listaUsuarios.Where(u => u.EsDesarrollador == true).ToList();

                    // Llenar el ListBox con los usuarios filtrados
                    listBoxUsuarios.DataSource = usuariosDesarrolladores;
                    listBoxUsuarios.DisplayMember = "Nombre"; // Mostrar el nombre de cada usuario
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labelFechaInicio_Click(object sender, EventArgs e) { }

        private void btnCancelarProyecto_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void FormProyectos_Load(object sender, EventArgs e) { }
    }
}
