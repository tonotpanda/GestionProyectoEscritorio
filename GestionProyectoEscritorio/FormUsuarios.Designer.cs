﻿namespace GestionProyectoEscritorio
{
    partial class FormUsuarios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelNombre = new System.Windows.Forms.Label();
            this.textBoxNombreUsuario = new System.Windows.Forms.TextBox();
            this.textBoxContrasenaUsuario = new System.Windows.Forms.TextBox();
            this.labelContrasena = new System.Windows.Forms.Label();
            this.labelDesarrollador = new System.Windows.Forms.Label();
            this.checkBoxDesarrollador = new System.Windows.Forms.CheckBox();
            this.btnCrearUsuario = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(149, 57);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(106, 16);
            this.labelNombre.TabIndex = 0;
            this.labelNombre.Text = "Nombre usuario:";
            // 
            // textBoxNombreUsuario
            // 
            this.textBoxNombreUsuario.Location = new System.Drawing.Point(289, 54);
            this.textBoxNombreUsuario.Name = "textBoxNombreUsuario";
            this.textBoxNombreUsuario.Size = new System.Drawing.Size(229, 22);
            this.textBoxNombreUsuario.TabIndex = 1;
            // 
            // textBoxContrasenaUsuario
            // 
            this.textBoxContrasenaUsuario.Location = new System.Drawing.Point(289, 105);
            this.textBoxContrasenaUsuario.Name = "textBoxContrasenaUsuario";
            this.textBoxContrasenaUsuario.Size = new System.Drawing.Size(229, 22);
            this.textBoxContrasenaUsuario.TabIndex = 2;
            // 
            // labelContrasena
            // 
            this.labelContrasena.AutoSize = true;
            this.labelContrasena.Location = new System.Drawing.Point(149, 108);
            this.labelContrasena.Name = "labelContrasena";
            this.labelContrasena.Size = new System.Drawing.Size(123, 16);
            this.labelContrasena.TabIndex = 3;
            this.labelContrasena.Text = "Contraseña usuario";
            // 
            // labelDesarrollador
            // 
            this.labelDesarrollador.AutoSize = true;
            this.labelDesarrollador.Location = new System.Drawing.Point(149, 162);
            this.labelDesarrollador.Name = "labelDesarrollador";
            this.labelDesarrollador.Size = new System.Drawing.Size(90, 16);
            this.labelDesarrollador.TabIndex = 4;
            this.labelDesarrollador.Text = "Desarrollador";
            // 
            // checkBoxDesarrollador
            // 
            this.checkBoxDesarrollador.AutoSize = true;
            this.checkBoxDesarrollador.Location = new System.Drawing.Point(289, 162);
            this.checkBoxDesarrollador.Name = "checkBoxDesarrollador";
            this.checkBoxDesarrollador.Size = new System.Drawing.Size(112, 20);
            this.checkBoxDesarrollador.TabIndex = 5;
            this.checkBoxDesarrollador.Text = "Desarrollador";
            this.checkBoxDesarrollador.UseVisualStyleBackColor = true;
            // 
            // btnCrearUsuario
            // 
            this.btnCrearUsuario.Location = new System.Drawing.Point(289, 222);
            this.btnCrearUsuario.Name = "btnCrearUsuario";
            this.btnCrearUsuario.Size = new System.Drawing.Size(148, 29);
            this.btnCrearUsuario.TabIndex = 6;
            this.btnCrearUsuario.Text = "Crear Usuario";
            this.btnCrearUsuario.UseVisualStyleBackColor = true;
            this.btnCrearUsuario.Click += new System.EventHandler(this.btnCrearUsuario_Click);
            // 
            // FormUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 674);
            this.Controls.Add(this.btnCrearUsuario);
            this.Controls.Add(this.checkBoxDesarrollador);
            this.Controls.Add(this.labelDesarrollador);
            this.Controls.Add(this.labelContrasena);
            this.Controls.Add(this.textBoxContrasenaUsuario);
            this.Controls.Add(this.textBoxNombreUsuario);
            this.Controls.Add(this.labelNombre);
            this.Name = "FormUsuarios";
            this.Text = "FormUsuarios";
            this.Load += new System.EventHandler(this.FormUsuarios_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.TextBox textBoxNombreUsuario;
        private System.Windows.Forms.TextBox textBoxContrasenaUsuario;
        private System.Windows.Forms.Label labelContrasena;
        private System.Windows.Forms.Label labelDesarrollador;
        private System.Windows.Forms.CheckBox checkBoxDesarrollador;
        private System.Windows.Forms.Button btnCrearUsuario;
    }
}