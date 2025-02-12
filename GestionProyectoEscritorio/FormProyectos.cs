﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionProyectoEscritorio;
using Newtonsoft.Json;

namespace GestorDeProyectos
{
    public partial class FormProyectos : Form
    {
        String nomProyecto;
        String tarea;
        private List<string> usuariosSeleccionados = new List<string>();
        private List<Tareas> tareas = new List<Tareas>();
        private List<string> subtareas = new List<string>();

        private static readonly string ClaveEncriptacion = "0123456789012345"; // Clave de 16 caracteres para AES
        private static readonly byte[] IVPersonalizado = Encoding.UTF8.GetBytes("5432109876543210"); // IV invertido (16 bytes)

        public FormProyectos()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CargarUsuarios();
            this.FormClosing += FormProyectos_FormClosing;
            comboBoxTareas.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void FormProyectos_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void CargarUsuarios()
        {
            string rutaArchivo = "usuarios.json"; // Ruta del archivo JSON

            // Verificar si el archivo existe
            if (File.Exists(rutaArchivo))
            {
                try
                {
                    // Leer y desencriptar el contenido del archivo
                    string contenidoEncriptado = File.ReadAllText(rutaArchivo);
                    string contenidoDesencriptado = DesencriptarJson(contenidoEncriptado);

                    // Deserializar los datos a la lista de usuarios
                    var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(contenidoDesencriptado) ?? new List<Usuario>();

                    // Limpiar el ListBox antes de agregar usuarios
                    listBoxUsuarios.Items.Clear();

                    // Agregar solo los nombres de los usuarios al ListBox
                    foreach (var usuario in usuarios)
                    {
                        listBoxUsuarios.Items.Add(usuario.Nombre);
                    }
                }
                catch
                {
                    MessageBox.Show("El archivo de usuarios no se puede desencriptar o está dañado.");
                }
            }
            else
            {
                MessageBox.Show("No hay usuarios registrados.");
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

        private void buttonConfirmarSubtareas_Click(object sender, EventArgs e)
        {
            if (comboBoxTareas.SelectedItem != null)
            {
                string tareaSeleccionada = comboBoxTareas.SelectedItem.ToString();
                var tareaEncontrada = tareas.FirstOrDefault(t => t.NombreTarea.Equals(tareaSeleccionada, StringComparison.OrdinalIgnoreCase));

                if (tareaEncontrada != null)
                {
                    // Verificar si las subtareas ya están asociadas a la tarea
                    if (tareaEncontrada.Subtareas == null)
                    {
                        tareaEncontrada.Subtareas = new List<string>();
                    }

                    foreach (var subtarea in subtareas)
                    {
                        if (tareaEncontrada.Subtareas.Any(s => s.Equals(subtarea, StringComparison.OrdinalIgnoreCase)))
                        {
                            MessageBox.Show($"La subtarea '{subtarea}' ya está asociada a la tarea '{tareaSeleccionada}'.");
                            subtareas.Clear();
                            listViewSubtareas.Clear();
                            return;
                        }
                    }

                    tareaEncontrada.Subtareas.AddRange(subtareas);
                    MessageBox.Show("Subtareas añadidas exitosamente.");
                    listViewSubtareas.Clear();
                    subtareas.Clear();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una tarea.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombreProyecto = textBoxProyecto.Text;

            if (string.IsNullOrWhiteSpace(nombreProyecto))
            {
                MessageBox.Show("Los campos estan vacios, rellenelos antes de aceptar.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (crearJson(nombreProyecto))
                {
                    MessageBox.Show("Proyecto creado exitosamente.");
                    this.Hide();
                    Form1 nuevoForm = new Form1();
                    nuevoForm.ShowDialog();
                }
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

        private string DesencriptarJson(string jsonEncriptado)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(ClaveEncriptacion);
                aesAlg.IV = IVPersonalizado;

                byte[] datosEncriptados = Convert.FromBase64String(jsonEncriptado);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(datosEncriptados))
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

        private bool crearJson(string nombreProyecto)
        {
            string rutaArchivo = "proyecto.json";

            List<Proyecto> proyectos;

            // Verificar si el archivo existe y desencriptarlo
            if (File.Exists(rutaArchivo))
            {
                string contenidoEncriptado = File.ReadAllText(rutaArchivo);
                if (!string.IsNullOrWhiteSpace(contenidoEncriptado))
                {
                    try
                    {
                        string contenidoDesencriptado = DesencriptarJson(contenidoEncriptado);
                        proyectos = JsonConvert.DeserializeObject<List<Proyecto>>(contenidoDesencriptado) ?? new List<Proyecto>();
                    }
                    catch
                    {
                        MessageBox.Show("El archivo JSON está dañado o no se puede desencriptar.");
                        return false;
                    }
                }
                else
                {
                    proyectos = new List<Proyecto>();
                }
            }
            else
            {
                proyectos = new List<Proyecto>();
            }

            // Verificar si el proyecto ya existe
            if (proyectos.Any(u => u.NombreProyecto == nombreProyecto))
            {
                MessageBox.Show("El proyecto ya existe.");
                return false;
            }

            // Recopilar datos del nuevo proyecto
            foreach (var item in listBoxUsuarios.SelectedItems)
            {
                usuariosSeleccionados.Add(item.ToString());
            }

            var nuevoProyecto = new Proyecto
            {
                NombreProyecto = textBoxProyecto.Text,
                Tareas = tareas.Select(t => t.NombreTarea).ToList(),  // Nombres de tareas
                FechaInicio = dateTimePickerFechaInici.Value,  // Verifica que este sea el nombre correcto
                FechaFin = dateTimePickerFechaFin.Value,  // Asegúrate de que este sea el nombre correcto
                Usuarios = usuariosSeleccionados  // Asegúrate de que usuariosSeleccionados sea una lista válida
            };



            proyectos.Add(nuevoProyecto);

            // Serializar y encriptar el JSON
            string jsonSerializado = JsonConvert.SerializeObject(proyectos, Formatting.Indented);
            string jsonEncriptado = EncriptarJson(jsonSerializado);
            File.WriteAllText(rutaArchivo, jsonEncriptado);

            return true;
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 nuevoForm = new Form1();
            nuevoForm.ShowDialog();
        }

        private void FormProyectos_Load(object sender, EventArgs e)
        {

        }
    }
}
