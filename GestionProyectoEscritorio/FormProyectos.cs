using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GestionProyectoEscritorio
{
    public partial class FormProyectos : Form
    {
        private List<Usuario> listaUsuarios = new List<Usuario>();
        private List<Proyecto> listaProyectos = new List<Proyecto>();
        private List<Tareas> tareas = new List<Tareas>(); // Lista de tareas
        private List<string> subtareas = new List<string>(); // Lista de subtareas
        private static readonly string ClaveEncriptacion = "0123456789012345";
        private static readonly byte[] IVPersonalizado = Encoding.UTF8.GetBytes("5432109876543210");

        public FormProyectos()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CargarUsuariosDesdeJson(); // Cargar usuarios
            CargarProyectosDesdeJson(); // Cargar proyectos
        }

        private void btnCrearProyecto_Click(object sender, EventArgs e)
        {
            string nombreProyecto = textBoxProyecto.Text;
            DateTime fechaInicio = dateTimePickerFechaInici.Value;
            DateTime fechaFin = dateTimePickerFechaFin.Value;
            List<string> usuariosSeleccionados = listBoxUsuarios.SelectedItems.Cast<Usuario>().Select(u => u.Nombre).ToList();

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

            // Crear el proyecto sin las subtareas
            Proyecto nuevoProyecto = new Proyecto
            {
                NombreProyecto = nombreProyecto,
                FechaInicio = fechaInicio.ToString("yyyy-MM-dd"),
                FechaFin = fechaFin.ToString("yyyy-MM-dd"),
                ListaUsuarios = usuariosSeleccionados,
                Tareas = tareas.Select(t => t.NombreTarea).ToList(), // Guardar solo los nombres de las tareas
                SubTareas = subtareas
            };

            listaProyectos.Add(nuevoProyecto);

            // Guardar el proyecto
            GuardarProyecto(nuevoProyecto);

            MessageBox.Show("Proyecto creado exitosamente.");
            this.Hide();
            Form1 nuevoForm = new Form1();
            nuevoForm.ShowDialog();
        }

        private void CargarUsuariosDesdeJson()
        {
            try
            {
                string rutaArchivo = "usuarios.json";

                if (File.Exists(rutaArchivo))
                {
                    string jsonEncriptado = File.ReadAllText(rutaArchivo);
                    string json = DesencriptarJson(jsonEncriptado);
                    listaUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(json) ?? new List<Usuario>();

                    var usuariosDesarrolladores = listaUsuarios.Where(u => u.EsDesarrollador == true).ToList();

                    listBoxUsuarios.DataSource = usuariosDesarrolladores;
                    listBoxUsuarios.DisplayMember = "Nombre";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void CargarProyectosDesdeJson()
        {
            try
            {
                string rutaArchivo = "proyectos.json";

                if (File.Exists(rutaArchivo))
                {
                    string jsonEncriptado = File.ReadAllText(rutaArchivo);
                    string json = DesencriptarJson(jsonEncriptado);
                    var listaProyectosJson = JsonConvert.DeserializeObject<List<object>>(json);

                    if (listaProyectosJson != null && listaProyectosJson.Count > 0)
                    {
                        listaProyectos = listaProyectosJson.Select(p => JsonConvert.DeserializeObject<Proyecto>(JsonConvert.SerializeObject(p))).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar proyectos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAgregarTarea_Click(object sender, EventArgs e)
        {
            string nombreTarea = textBoxTarea.Text;

            if (!string.IsNullOrWhiteSpace(nombreTarea))
            {
                // Verificar si la tarea ya existe
                if (tareas.Any(t => t.NombreTarea.Equals(nombreTarea, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("La tarea ya existe. Por favor, ingresa un nombre diferente.");
                    return;
                }

                Tareas crearTarea = new Tareas
                {
                    NombreTarea = nombreTarea,
                };

                tareas.Add(crearTarea);
                ListViewItem item = new ListViewItem(nombreTarea);
                listViewTareas.Items.Add(item);
                listViewTareas.View = View.List;
                comboBoxTareas.Items.Add(crearTarea.NombreTarea);
                textBoxTarea.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa una tarea.");
            }
        }

        private void buttonAgregarSubtarea_Click(object sender, EventArgs e)
        {
            string nombreSubtarea = textBoxSubtarea.Text;

            if (!string.IsNullOrWhiteSpace(nombreSubtarea))
            {
                // Verificar si la subtarea ya existe
                if (subtareas.Any(s => s.Equals(nombreSubtarea, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("La subtarea ya existe. Por favor, ingresa un nombre diferente.");
                    return;
                }

                subtareas.Add(nombreSubtarea);
                ListViewItem item = new ListViewItem(nombreSubtarea);
                listViewSubtareas.Items.Add(item);
                listViewSubtareas.View = View.List;
                textBoxSubtarea.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa una subtarea.");
            }
        }

        private void GuardarProyecto(Proyecto proyecto)
        {
            try
            {
                string rutaArchivo = "proyectos.json";
                List<Proyecto> proyectos = new List<Proyecto>();

                if (File.Exists(rutaArchivo))
                {
                    string jsonEncriptado = File.ReadAllText(rutaArchivo);
                    string json = DesencriptarJson(jsonEncriptado);
                    proyectos = JsonConvert.DeserializeObject<List<Proyecto>>(json) ?? new List<Proyecto>();
                }

                proyectos.Add(proyecto);

                string jsonSerializado = JsonConvert.SerializeObject(proyectos, Formatting.Indented);
                string jsonEncriptadoFinal = EncriptarJson(jsonSerializado);
                File.WriteAllText(rutaArchivo, jsonEncriptadoFinal);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar proyecto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string EncriptarJson(string json)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(ClaveEncriptacion);
                aesAlg.IV = IVPersonalizado;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(json);
                        }
                    }

                    byte[] encrypted = msEncrypt.ToArray();
                    return Convert.ToBase64String(encrypted);
                }
            }
        }
    }
}
