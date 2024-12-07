using System;
using System.Collections.Generic;
using System.IO; // Para trabajar con archivos
using System.Windows.Forms;
using Newtonsoft.Json; // Importar la biblioteca Newtonsoft.Json

namespace GestionProyectoEscritorio
{
    public partial class FormUsuarios : Form
    {
        private List<Usuario> listaUsuarios = new List<Usuario>(); // Lista para almacenar los usuarios

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

            // Crear un objeto para almacenar los datos
            Usuario nuevoUsuario = new Usuario
            {
                Nombre = nombreUsuario,
                Contrasena = contrasena,
                EsDesarrollador = esDesarrollador
            };

            // Agregar a la lista de usuarios
            listaUsuarios.Add(nuevoUsuario);

            // Guardar en un archivo JSON
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

        private void GuardarUsuariosEnJson()
        {
            try
            {
                // Ruta donde se guardará el archivo JSON
                string rutaArchivo = "usuarios.json";

                // Serializar la lista de usuarios a JSON usando Newtonsoft.Json
                string json = JsonConvert.SerializeObject(listaUsuarios, Formatting.Indented);

                // Guardar el JSON en el archivo
                File.WriteAllText(rutaArchivo, json);
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje en caso de error
                MessageBox.Show($"Error al guardar en JSON: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUsuariosDesdeJson()
        {
            try
            {
                // Ruta donde se guardará el archivo JSON
                string rutaArchivo = "usuarios.json";

                // Verificar si el archivo existe
                if (File.Exists(rutaArchivo))
                {
                    // Leer el contenido del archivo
                    string json = File.ReadAllText(rutaArchivo);

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

        private void FormUsuarios_Load(object sender, EventArgs e)
        {

        }
    }

}
