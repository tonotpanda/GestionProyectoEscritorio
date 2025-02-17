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
                    // Si existe una clave de "Contrasena", la desencriptamos
                    if (proyecto.ContainsKey("Contrasena"))
                    {
                        string contrasenaEncriptada = proyecto["Contrasena"].ToString();
                        string contrasenaDesencriptada = DesencriptarContrasena(contrasenaEncriptada);
                        proyecto["Contrasena"] = contrasenaDesencriptada; // Actualizamos la contraseña desencriptada
                    }

                    // Convertir los valores de cada proyecto a cadenas
                    var valores = proyecto.Values.Select(v => v.ToString()).ToArray();

                    // Añadir la fila al DataGridView
                    dataGridViewJSON.Rows.Add(valores);
                }

                // Ajustar el tamaño de las columnas para que ocupe todo el espacio disponible
                dataGridViewJSON.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // Método para desencriptar la contraseña
        private string DesencriptarContrasena(string contrasenaEncriptada)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(ClaveEncriptacion);
                    aesAlg.IV = IVPersonalizado;

                    byte[] datosEncriptados = Convert.FromBase64String(contrasenaEncriptada);
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
                                return srDecrypt.ReadToEnd(); // Devolver la contraseña desencriptada
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al desencriptar la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        // Método para encriptar la contraseña
        private string EncriptarContrasena(string contrasena)
        {
            try
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
                                swEncrypt.Write(contrasena); // Escribir la contraseña en el CryptoStream
                            }
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray()); // Retorna la contraseña encriptada en Base64
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al encriptar la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
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
                // Convertir los datos del DataGridView de nuevo a un JSON
                List<Dictionary<string, object>> proyectosJson = new List<Dictionary<string, object>>();

                foreach (DataGridViewRow row in dataGridViewJSON.Rows)
                {
                    if (row.IsNewRow) continue; // Evitar agregar la nueva fila que es solo de entrada

                    Dictionary<string, object> proyecto = new Dictionary<string, object>();
                    foreach (DataGridViewColumn column in dataGridViewJSON.Columns)
                    {
                        // Asegurarse de capturar correctamente los valores de cada celda
                        proyecto[column.Name] = row.Cells[column.Name].Value?.ToString() ?? string.Empty;
                    }

                    // Si existe una clave de "Contrasena", la encriptamos antes de guardar
                    if (proyecto.ContainsKey("Contrasena"))
                    {
                        string contrasena = proyecto["Contrasena"].ToString();
                        proyecto["Contrasena"] = EncriptarContrasena(contrasena); // Encriptar la contraseña
                    }

                    proyectosJson.Add(proyecto);
                }

                // Serializar el JSON en texto
                string json = JsonConvert.SerializeObject(proyectosJson, Formatting.Indented);

                // Encriptar el JSON
                string jsonEncriptado = EncriptarJson(json);

                // Guardar el archivo JSON encriptado en el archivo original
                if (!string.IsNullOrEmpty(rutaArchivoActual)) // Asegurarse de que la ruta esté definida
                {
                    File.WriteAllText(rutaArchivoActual, jsonEncriptado); // Sobrescribir el archivo original con los datos encriptados

                    MessageBox.Show("Los proyectos se han guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se ha cargado ningún archivo para guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Regresar a Form1
                this.Close(); // Cierra FormGestionJSON
                Form1 form1 = new Form1(); // Crear una nueva instancia de Form1
                form1.Show(); // Mostrar Form1
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar proyectos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento del botón Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarProyectosEnJson();
        }

        // Evento del botón Borrar
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewJSON.SelectedRows)
            {
                dataGridViewJSON.Rows.Remove(row); // Eliminar las filas seleccionadas
            }
        }

        private void btnCancelarJSON_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }
    }
}