using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GestionProyectoEscritorio
{
    public partial class FormGestionJSON : Form
    {
        private static readonly string ClaveEncriptacion = "0123456789012345"; // Clave de encriptación AES
        private static readonly byte[] IVPersonalizado = Encoding.UTF8.GetBytes("5432109876543210"); // IV de 16 bytes

        private List<Proyecto> listaProyectos = new List<Proyecto>(); // Lista de proyectos

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
                    string rutaArchivo = openFileDialog.FileName;
                    string jsonEncriptado = File.ReadAllText(rutaArchivo);

                    // Desencriptar el JSON completo
                    string json = DesencriptarJson(jsonEncriptado);

                    // Deserializar el JSON en una lista de proyectos
                    var proyectosJson = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json) ?? new List<Dictionary<string, object>>();

                    // Cargar los datos en el DataGridView
                    CargarDatosEnDataGridView(proyectosJson);
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error al desencriptar el JSON: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        // Método para cargar los datos en el DataGridView
        private void CargarDatosEnDataGridView(List<Dictionary<string, object>> proyectosJson)
        {
            dataGridViewJSON.Rows.Clear();
            dataGridViewJSON.Columns.Clear();

            if (proyectosJson == null || proyectosJson.Count == 0)
            {
                MessageBox.Show("No se encontraron proyectos en el archivo JSON.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Crear columnas dinámicas basadas en las claves del primer proyecto
            var primerasClaves = proyectosJson.FirstOrDefault();
            if (primerasClaves != null)
            {
                // Añadir las columnas dinámicamente
                foreach (var clave in primerasClaves.Keys)
                {
                    dataGridViewJSON.Columns.Add(clave, clave); // Usamos las claves del JSON como nombres de las columnas
                }

                // Llenar las filas con los datos de cada proyecto
                foreach (var proyecto in proyectosJson)
                {
                    var valores = proyecto.Values.Select(v => v.ToString()).ToArray();  // Convertir los valores de cada proyecto a cadenas
                    dataGridViewJSON.Rows.Add(valores); // Añadir la fila al DataGridView
                }

                // Ajustar el tamaño de las columnas para que ocupe todo el espacio disponible
                dataGridViewJSON.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // Método para encriptar JSON
        private string EncriptarJson(string json)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(ClaveEncriptacion);
                aesAlg.IV = IVPersonalizado;

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

        // Método para guardar proyectos en JSON (encriptado)
        private void GuardarProyectosEnJson()
        {
            try
            {
                string json = JsonConvert.SerializeObject(listaProyectos, Formatting.Indented);
                string jsonEncriptado = EncriptarJson(json); // Encriptar el JSON
                File.WriteAllText("proyectos.json", jsonEncriptado); // Guardar en el archivo
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar proyectos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para manejar la eliminación de un proyecto
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dataGridViewJSON.SelectedRows.Count > 0)
            {
                try
                {
                    // Obtener el índice de la fila seleccionada
                    int filaSeleccionada = dataGridViewJSON.SelectedRows[0].Index;

                    // Obtener el nombre del proyecto de la fila seleccionada
                    string nombreProyecto = dataGridViewJSON.Rows[filaSeleccionada].Cells["NombreProyecto"].Value.ToString();

                    // Buscar el proyecto correspondiente en la lista
                    var proyectoAEliminar = listaProyectos.FirstOrDefault(p => p.NombreProyecto == nombreProyecto);

                    if (proyectoAEliminar != null)
                    {
                        // Eliminar el proyecto de la lista
                        listaProyectos.Remove(proyectoAEliminar);

                        // Eliminar la fila del DataGridView
                        dataGridViewJSON.Rows.RemoveAt(filaSeleccionada);

                        // Guardar los cambios en el archivo JSON
                        GuardarProyectosEnJson();

                        MessageBox.Show("El proyecto ha sido eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Redirigir a Form1
                        this.Hide();
                        Form1 form1 = new Form1();
                        form1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el proyecto seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el proyecto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un proyecto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
