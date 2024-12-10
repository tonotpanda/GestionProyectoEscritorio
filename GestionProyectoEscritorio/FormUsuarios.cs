using System;
using System.Collections.Generic;
using System.IO; // Para trabajar con archivos
using System.Security.Cryptography; // Para encriptación
using System.Text; // Para trabajar con cadenas y bytes
using System.Windows.Forms;
using Newtonsoft.Json; // Importar la biblioteca Newtonsoft.Json

namespace GestionProyectoEscritorio
{
    public partial class FormUsuarios : Form
    {
        private List<Usuario> listaUsuarios = new List<Usuario>(); // Lista para almacenar los usuarios

        private static readonly string ClaveEncriptacion = "0123456789012345"; // Clave de 16 caracteres para AES
        private static readonly byte[] IVPersonalizado = Encoding.UTF8.GetBytes("5432109876543210"); // IV invertido (16 bytes)

        public FormUsuarios()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Cargar usuarios existentes al iniciar el formulario
            CargarUsuariosDesdeJson();
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            // Capturar los datos ingresados por el usuario
            string nombreUsuario = textBoxNombreUsuario.Text;
            string contrasena = textBoxContrasenaUsuario.Text;
            bool esDesarrollador = checkBoxDesarrollador.Checked;

            // Validar los campos obligatorios
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verificar si el usuario ya existe
            if (listaUsuarios.Exists(u => u.Nombre == nombreUsuario))
            {
                MessageBox.Show("El nombre de usuario ya existe. Por favor, elige otro.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Encriptar la contraseña
            string contrasenaEncriptada = EncriptarContrasena(contrasena);

            // Crear un objeto para almacenar los datos
            Usuario nuevoUsuario = new Usuario
            {
                Nombre = nombreUsuario,
                Contrasena = contrasenaEncriptada,
                EsDesarrollador = esDesarrollador
            };

            // Agregar a la lista de usuarios
            listaUsuarios.Add(nuevoUsuario);

            // Guardar en un archivo JSON encriptado
            GuardarUsuariosEnJson();

            // Confirmar creación
            MessageBox.Show("Usuario guardado correctamente y almacenado en JSON.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar los campos
            textBoxNombreUsuario.Clear();
            textBoxContrasenaUsuario.Clear();
            checkBoxDesarrollador.Checked = false;

            // Volver al formulario principal
            this.Hide();
            Form1 formularioPrincipal = new Form1();
            formularioPrincipal.Show();
            this.Close();
        }

        // Método para guardar el archivo JSON encriptado
        private void GuardarUsuariosEnJson()
        {
            try
            {
                // Ruta donde se guardará el archivo JSON
                string rutaArchivo = "usuarios.json";

                // Serializar la lista de usuarios a JSON usando Newtonsoft.Json
                string json = JsonConvert.SerializeObject(listaUsuarios, Formatting.Indented);

                // Encriptar el JSON antes de guardarlo
                string jsonEncriptado = EncriptarJson(json);

                // Guardar el JSON encriptado en el archivo
                File.WriteAllText(rutaArchivo, jsonEncriptado);
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje en caso de error
                MessageBox.Show($"Error al guardar en JSON: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para encriptar el JSON usando AES
        private string EncriptarJson(string json)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(ClaveEncriptacion); // Clave de 16 bytes
                aesAlg.IV = IVPersonalizado; // Usar el IV personalizado invertido

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Guardamos primero el IV antes de los datos encriptados
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(json); // Escribir el JSON en el flujo cifrado
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray()); // Retornar el JSON encriptado como Base64
                }
            }
        }

        // Método para cargar y desencriptar el archivo JSON
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
                    string json = DesencriptarJson(jsonEncriptado);

                    // Deserializar el JSON en la lista de usuarios
                    listaUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(json) ?? new List<Usuario>();
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
                aesAlg.IV = IVPersonalizado; // Usar el IV personalizado invertido

                byte[] datosEncriptados = Convert.FromBase64String(jsonEncriptado);

                // Extraemos el IV de los primeros 16 bytes (aunque en este caso, estamos usando un IV fijo)
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
                            return srDecrypt.ReadToEnd(); // Retornar el JSON desencriptado
                        }
                    }
                }
            }
        }

        private void FormUsuarios_Load(object sender, EventArgs e)
        {
            this.textBoxContrasenaUsuario.UseSystemPasswordChar = true;
        }

        // Método para encriptar la contraseña
        private string EncriptarContrasena(string contrasena)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(ClaveEncriptacion); // Clave de 16 bytes
                aesAlg.IV = IVPersonalizado; // Usar el IV personalizado invertido

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Guardamos primero el IV antes de los datos encriptados
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(contrasena); // Escribir la contraseña en el flujo cifrado
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray()); // Retornar la contraseña encriptada como Base64
                }
            }
        }
    }
}
