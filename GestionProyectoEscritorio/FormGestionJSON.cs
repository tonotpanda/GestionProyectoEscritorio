using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace GestionProyectoEscritorio
{
    public partial class FormGestionJSON : Form
    {
        private static readonly string ClaveEncriptacion = "0123456789012345"; // Clave de encriptación AES
        private static readonly byte[] IVPersonalizado = Encoding.UTF8.GetBytes("5432109876543210"); // IV de 16 bytes

        private List<Proyecto> listaProyectos = new List<Proyecto>(); // Lista de proyectos
        private string rutaArchivoActual = string.Empty; // Variable para almacenar la ruta del archivo cargado

        public FormGestionJSON()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Cargar los proyectos desde el archivo JSON
        private void btnSeleccionarJSON_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos JSON|*.json",
                Title = "Seleccionar archivo JSON"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    rutaArchivoActual = openFileDialog.FileName; // Guardamos la ruta del archivo seleccionado
                    string jsonEncriptado = File.ReadAllText(rutaArchivoActual); // Leemos el archivo encriptado

                    // Verificamos si el archivo está vacío
                    if (string.IsNullOrWhiteSpace(jsonEncriptado))
                    {
                        MessageBox.Show("El archivo seleccionado está vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Desencriptar el JSON completo si está encriptado
                    string jsonDesencriptado = DesencriptarJson(jsonEncriptado);

                    if (string.IsNullOrEmpty(jsonDesencriptado))
                    {
                        MessageBox.Show("Error al desencriptar el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Intentamos deserializar el JSON
                    var proyectosJson = JsonConvert.DeserializeObject<List<Proyecto>>(jsonDesencriptado);

                    if (proyectosJson != null)
                    {
                        CargarProyectosEnDataGridView(proyectosJson);
                    }
                    else
                    {
                        MessageBox.Show("El archivo no tiene un formato válido de proyectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar el archivo JSON: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Método para desencriptar el JSON completo
        private string DesencriptarJson(string jsonEncriptado)
        {
            try
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

                    using (MemoryStream msDecrypt = new MemoryStream(datosEncriptados, 16, datosEncriptados.Length - 16)) // Excluir los primeros 16 bytes (IV)
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error al desencriptar el JSON: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        // Cargar los proyectos en el DataGridView
        private void CargarProyectosEnDataGridView(List<Proyecto> proyectos)
        {
            // Limpia cualquier contenido previo
            dataGridViewJSON.Rows.Clear();

            // Agrega los proyectos al DataGridView
            foreach (var proyecto in proyectos)
            {
                dataGridViewJSON.Rows.Add(proyecto.NombreProyecto, proyecto.FechaInicio, proyecto.FechaFin, proyecto.Tareas);
            }
        }
    }
}
