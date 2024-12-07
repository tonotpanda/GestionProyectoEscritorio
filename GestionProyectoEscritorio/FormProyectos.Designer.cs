namespace GestionProyectoEscritorio
{
    partial class FormProyectos
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
            this.labelNombreProyecto = new System.Windows.Forms.Label();
            this.labelFechaInicio = new System.Windows.Forms.Label();
            this.labelFechaFin = new System.Windows.Forms.Label();
            this.labelAgregarUsuarios = new System.Windows.Forms.Label();
            this.textBoxNombreProyecto = new System.Windows.Forms.TextBox();
            this.dateTimePickerFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFechaFin = new System.Windows.Forms.DateTimePicker();
            this.listBoxUsuarios = new System.Windows.Forms.ListBox();
            this.labelTareas = new System.Windows.Forms.Label();
            this.labelSubTareas = new System.Windows.Forms.Label();
            this.btnGuardarProyecto = new System.Windows.Forms.Button();
            this.richTextBoxTareas = new System.Windows.Forms.RichTextBox();
            this.richTextBoxSubtareas = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // labelNombreProyecto
            // 
            this.labelNombreProyecto.AutoSize = true;
            this.labelNombreProyecto.Location = new System.Drawing.Point(217, 48);
            this.labelNombreProyecto.Name = "labelNombreProyecto";
            this.labelNombreProyecto.Size = new System.Drawing.Size(112, 16);
            this.labelNombreProyecto.TabIndex = 0;
            this.labelNombreProyecto.Text = "Nombre proyecto";
            // 
            // labelFechaInicio
            // 
            this.labelFechaInicio.AutoSize = true;
            this.labelFechaInicio.Location = new System.Drawing.Point(217, 87);
            this.labelFechaInicio.Name = "labelFechaInicio";
            this.labelFechaInicio.Size = new System.Drawing.Size(79, 16);
            this.labelFechaInicio.TabIndex = 1;
            this.labelFechaInicio.Text = "Fecha inicio";
            // 
            // labelFechaFin
            // 
            this.labelFechaFin.AutoSize = true;
            this.labelFechaFin.Location = new System.Drawing.Point(217, 127);
            this.labelFechaFin.Name = "labelFechaFin";
            this.labelFechaFin.Size = new System.Drawing.Size(61, 16);
            this.labelFechaFin.TabIndex = 2;
            this.labelFechaFin.Text = "Fecha fin";
            // 
            // labelAgregarUsuarios
            // 
            this.labelAgregarUsuarios.AutoSize = true;
            this.labelAgregarUsuarios.Location = new System.Drawing.Point(217, 178);
            this.labelAgregarUsuarios.Name = "labelAgregarUsuarios";
            this.labelAgregarUsuarios.Size = new System.Drawing.Size(61, 16);
            this.labelAgregarUsuarios.TabIndex = 3;
            this.labelAgregarUsuarios.Text = "Usuarios";
            // 
            // textBoxNombreProyecto
            // 
            this.textBoxNombreProyecto.Location = new System.Drawing.Point(365, 41);
            this.textBoxNombreProyecto.Name = "textBoxNombreProyecto";
            this.textBoxNombreProyecto.Size = new System.Drawing.Size(275, 22);
            this.textBoxNombreProyecto.TabIndex = 4;
            // 
            // dateTimePickerFechaInicio
            // 
            this.dateTimePickerFechaInicio.Location = new System.Drawing.Point(365, 87);
            this.dateTimePickerFechaInicio.Name = "dateTimePickerFechaInicio";
            this.dateTimePickerFechaInicio.Size = new System.Drawing.Size(275, 22);
            this.dateTimePickerFechaInicio.TabIndex = 5;
            // 
            // dateTimePickerFechaFin
            // 
            this.dateTimePickerFechaFin.Location = new System.Drawing.Point(365, 127);
            this.dateTimePickerFechaFin.Name = "dateTimePickerFechaFin";
            this.dateTimePickerFechaFin.Size = new System.Drawing.Size(275, 22);
            this.dateTimePickerFechaFin.TabIndex = 6;
            // 
            // listBoxUsuarios
            // 
            this.listBoxUsuarios.FormattingEnabled = true;
            this.listBoxUsuarios.ItemHeight = 16;
            this.listBoxUsuarios.Location = new System.Drawing.Point(365, 178);
            this.listBoxUsuarios.Name = "listBoxUsuarios";
            this.listBoxUsuarios.Size = new System.Drawing.Size(275, 116);
            this.listBoxUsuarios.TabIndex = 7;
            // 
            // labelTareas
            // 
            this.labelTareas.AutoSize = true;
            this.labelTareas.Location = new System.Drawing.Point(220, 322);
            this.labelTareas.Name = "labelTareas";
            this.labelTareas.Size = new System.Drawing.Size(51, 16);
            this.labelTareas.TabIndex = 9;
            this.labelTareas.Text = "Tareas";
            // 
            // labelSubTareas
            // 
            this.labelSubTareas.AutoSize = true;
            this.labelSubTareas.Location = new System.Drawing.Point(220, 470);
            this.labelSubTareas.Name = "labelSubTareas";
            this.labelSubTareas.Size = new System.Drawing.Size(69, 16);
            this.labelSubTareas.TabIndex = 10;
            this.labelSubTareas.Text = "Subtareas";
            // 
            // btnGuardarProyecto
            // 
            this.btnGuardarProyecto.Location = new System.Drawing.Point(338, 628);
            this.btnGuardarProyecto.Name = "btnGuardarProyecto";
            this.btnGuardarProyecto.Size = new System.Drawing.Size(107, 36);
            this.btnGuardarProyecto.TabIndex = 12;
            this.btnGuardarProyecto.Text = "Guardar";
            this.btnGuardarProyecto.UseVisualStyleBackColor = true;
            this.btnGuardarProyecto.Click += new System.EventHandler(this.btnGuardarProyecto_Click);
            // 
            // richTextBoxTareas
            // 
            this.richTextBoxTareas.Location = new System.Drawing.Point(365, 322);
            this.richTextBoxTareas.Name = "richTextBoxTareas";
            this.richTextBoxTareas.Size = new System.Drawing.Size(275, 116);
            this.richTextBoxTareas.TabIndex = 13;
            this.richTextBoxTareas.Text = "";
            // 
            // richTextBoxSubtareas
            // 
            this.richTextBoxSubtareas.Location = new System.Drawing.Point(365, 470);
            this.richTextBoxSubtareas.Name = "richTextBoxSubtareas";
            this.richTextBoxSubtareas.Size = new System.Drawing.Size(275, 116);
            this.richTextBoxSubtareas.TabIndex = 14;
            this.richTextBoxSubtareas.Text = "";
            // 
            // FormProyectos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 736);
            this.Controls.Add(this.richTextBoxSubtareas);
            this.Controls.Add(this.richTextBoxTareas);
            this.Controls.Add(this.btnGuardarProyecto);
            this.Controls.Add(this.labelSubTareas);
            this.Controls.Add(this.labelTareas);
            this.Controls.Add(this.listBoxUsuarios);
            this.Controls.Add(this.dateTimePickerFechaFin);
            this.Controls.Add(this.dateTimePickerFechaInicio);
            this.Controls.Add(this.textBoxNombreProyecto);
            this.Controls.Add(this.labelAgregarUsuarios);
            this.Controls.Add(this.labelFechaFin);
            this.Controls.Add(this.labelFechaInicio);
            this.Controls.Add(this.labelNombreProyecto);
            this.Name = "FormProyectos";
            this.Text = "FormProyectos";
            this.Load += new System.EventHandler(this.FormProyectos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNombreProyecto;
        private System.Windows.Forms.Label labelFechaInicio;
        private System.Windows.Forms.Label labelFechaFin;
        private System.Windows.Forms.Label labelAgregarUsuarios;
        private System.Windows.Forms.TextBox textBoxNombreProyecto;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaInicio;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaFin;
        private System.Windows.Forms.ListBox listBoxUsuarios;
        private System.Windows.Forms.Label labelTareas;
        private System.Windows.Forms.Label labelSubTareas;
        private System.Windows.Forms.Button btnGuardarProyecto;
        private System.Windows.Forms.RichTextBox richTextBoxTareas;
        private System.Windows.Forms.RichTextBox richTextBoxSubtareas;
    }
}