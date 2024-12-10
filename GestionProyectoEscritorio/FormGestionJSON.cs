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
        private static readonly byte[] IVPersonalizado = Encoding.UTF8.GetBytes("5432109876543210"); // IV invertido de 16 bytes

        public FormGestionJSON()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }



        private void btnSeleccionarJSON_Click(object sender, EventArgs e)
        {
            // Crear un cuadro de diálogo para seleccionar el archivo JSON
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos JSON|*.json",
                Title = "Seleccionar archivo JSON"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Leer el archivo seleccionado
                    string rutaArchivo = openFileDialog.FileName;
                    string jsonEncriptado = File.ReadAllText(rutaArchivo);

                    // Desencriptar el contenido del archivo JSON
                    string json = DesencriptarJson(jsonEncriptado);

                    // Deserializar el JSON en una lista de objetos (ajustar según la estructura de datos)
                    var datos = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

                    // Llenar el DataGridView con los datos
                    CargarDatosEnDataGridView(datos);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar el archivo JSON: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Método para desencriptar el JSON
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
                                return srDecrypt.ReadToEnd(); // Retorna el JSON desencriptado
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

        // Método para cargar los datos desencriptados en el DataGridView
        private void CargarDatosEnDataGridView(List<Dictionary<string, object>> datos)
        {
            // Limpiar el DataGridView antes de llenarlo con nuevos datos
            dataGridViewJSON.Rows.Clear();
            dataGridViewJSON.Columns.Clear();

            if (datos == null || datos.Count == 0)
            {
                MessageBox.Show("No se encontraron datos en el archivo JSON.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Agregar las columnas dinámicamente en función de las claves de los datos
            HashSet<string> allKeys = new HashSet<string>();
            foreach (var item in datos)
            {
                foreach (var key in item.Keys)
                {
                    allKeys.Add(key); // Añadir todas las claves únicas encontradas
                }
            }

            foreach (var key in allKeys)
            {
                dataGridViewJSON.Columns.Add(key, key); // Agrega columnas con el nombre de la clave
            }

            // Agregar las filas con los datos
            foreach (var item in datos)
            {
                // Crear una lista de valores que correspondan a las columnas
                List<object> rowValues = new List<object>();

                // Añadir los valores de las columnas para esta fila
                foreach (var column in dataGridViewJSON.Columns)
                {
                    string columnName = ((DataGridViewColumn)column).Name;
                    Console.WriteLine($"Columna encontrada: {columnName}"); // Imprime el nombre de la columna

                    if (item.ContainsKey(columnName))
                    {
                        object value = item[columnName];

                        // Si la columna es "password" (o el nombre de la clave que almacena la contraseña)
                        if (columnName.ToLower() == "password" && value != null)
                        {
                            // Ya no desencriptamos la contraseña, simplemente la mostramos tal cual
                            rowValues.Add(value.ToString());
                        }
                        else
                        {
                            rowValues.Add(value);
                        }
                    }
                    else
                    {
                        rowValues.Add(null); // Si no está presente, agregar un valor nulo
                    }
                }

                // Añadir la fila al DataGridView
                dataGridViewJSON.Rows.Add(rowValues.ToArray());
            }

            // Ajustar automáticamente el ancho de las columnas para que ocupen todo el espacio disponible
            dataGridViewJSON.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Aumentar la altura de las filas para que haya más espacio entre ellas
            dataGridViewJSON.RowTemplate.Height = 100; // Ajusta este valor para hacer las filas más grandes

            // Hacer que el texto se ajuste automáticamente en las celdas
            foreach (DataGridViewColumn column in dataGridViewJSON.Columns)
            {
                column.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Habilitar ajuste de texto
            }

            // Asegurarse de que el ancho de las columnas se ajusten correctamente para que no haya "..."
            dataGridViewJSON.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {

        }
    }
}
