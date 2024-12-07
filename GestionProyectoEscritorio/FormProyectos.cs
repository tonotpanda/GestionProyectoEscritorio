using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace GestionProyectoEscritorio
{
    public partial class FormProyectos : Form
    {
        private List<Usuario> listaUsuarios = new List<Usuario>(); // Lista de usuarios
        private List<Proyecto> listaProyectos = new List<Proyecto>(); // Lista de proyectos

        public FormProyectos()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Cargar los usuarios (solo desarrolladores) al iniciar el formulario
            CargarUsuariosDesdeJson();
            CargarUsuariosDesarrolladores();
        }

        private void FormProyectos_Load(object sender, EventArgs e)
        {
            // Aquí puedes realizar acciones cuando el formulario cargue si es necesario
        }

        // Método para cargar usuarios desde el archivo JSON
        // Método para cargar usuarios desde el archivo JSON
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

        // Método para guardar la lista de proyectos en un archivo JSON
        private void GuardarProyectosEnJson()
        {
            try
            {
                string rutaArchivo = "proyectos.json";
                string json = JsonConvert.SerializeObject(listaProyectos, Formatting.Indented);
                File.WriteAllText(rutaArchivo, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar proyectos en JSON: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

