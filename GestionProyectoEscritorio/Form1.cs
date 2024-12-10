using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionProyectoEscritorio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormUsuarios usuarios = new FormUsuarios();
            usuarios.ShowDialog();
        }

        private void btnProyectos_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormProyectos proyectos = new FormProyectos();
            proyectos.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGestionJSON_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormGestionJSON gestionJSON = new FormGestionJSON();
            gestionJSON.ShowDialog();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
