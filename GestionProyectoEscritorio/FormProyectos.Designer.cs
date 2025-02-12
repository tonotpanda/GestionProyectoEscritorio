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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProyectos));
            this.labelNombreProyecto = new System.Windows.Forms.Label();
            this.labelFechaInicio = new System.Windows.Forms.Label();
            this.labelFechaFin = new System.Windows.Forms.Label();
            this.labelAgregarUsuarios = new System.Windows.Forms.Label();
            this.textBoxProyecto = new System.Windows.Forms.TextBox();
            this.dateTimePickerFechaInici = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFechaFin = new System.Windows.Forms.DateTimePicker();
            this.listBoxUsuarios = new System.Windows.Forms.ListBox();
            this.labelTareas = new System.Windows.Forms.Label();
            this.btnCrearProyecto = new System.Windows.Forms.Button();
            this.btnCancelarProyecto = new System.Windows.Forms.Button();
            this.textBoxTarea = new System.Windows.Forms.TextBox();
            this.buttonAgregarTarea = new System.Windows.Forms.Button();
            this.buttonCancelarTarea = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewTareas = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTareas = new System.Windows.Forms.ComboBox();
            this.textBoxSubtarea = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listViewSubtareas = new System.Windows.Forms.ListView();
            this.buttonConfirmarSubtareas = new System.Windows.Forms.Button();
            this.buttonAgregarSubtarea = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelNombreProyecto
            // 
            this.labelNombreProyecto.AutoSize = true;
            this.labelNombreProyecto.BackColor = System.Drawing.Color.Transparent;
            this.labelNombreProyecto.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNombreProyecto.ForeColor = System.Drawing.Color.White;
            this.labelNombreProyecto.Location = new System.Drawing.Point(38, 41);
            this.labelNombreProyecto.Name = "labelNombreProyecto";
            this.labelNombreProyecto.Size = new System.Drawing.Size(179, 24);
            this.labelNombreProyecto.TabIndex = 0;
            this.labelNombreProyecto.Text = "Nombre proyecto";
            // 
            // labelFechaInicio
            // 
            this.labelFechaInicio.AutoSize = true;
            this.labelFechaInicio.BackColor = System.Drawing.Color.Transparent;
            this.labelFechaInicio.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFechaInicio.ForeColor = System.Drawing.Color.White;
            this.labelFechaInicio.Location = new System.Drawing.Point(38, 115);
            this.labelFechaInicio.Name = "labelFechaInicio";
            this.labelFechaInicio.Size = new System.Drawing.Size(131, 24);
            this.labelFechaInicio.TabIndex = 1;
            this.labelFechaInicio.Text = "Fecha inicio";
            // 
            // labelFechaFin
            // 
            this.labelFechaFin.AutoSize = true;
            this.labelFechaFin.BackColor = System.Drawing.Color.Transparent;
            this.labelFechaFin.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFechaFin.ForeColor = System.Drawing.Color.White;
            this.labelFechaFin.Location = new System.Drawing.Point(38, 190);
            this.labelFechaFin.Name = "labelFechaFin";
            this.labelFechaFin.Size = new System.Drawing.Size(102, 24);
            this.labelFechaFin.TabIndex = 2;
            this.labelFechaFin.Text = "Fecha fin";
            // 
            // labelAgregarUsuarios
            // 
            this.labelAgregarUsuarios.AutoSize = true;
            this.labelAgregarUsuarios.BackColor = System.Drawing.Color.Transparent;
            this.labelAgregarUsuarios.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAgregarUsuarios.ForeColor = System.Drawing.Color.White;
            this.labelAgregarUsuarios.Location = new System.Drawing.Point(38, 267);
            this.labelAgregarUsuarios.Name = "labelAgregarUsuarios";
            this.labelAgregarUsuarios.Size = new System.Drawing.Size(96, 24);
            this.labelAgregarUsuarios.TabIndex = 3;
            this.labelAgregarUsuarios.Text = "Usuarios";
            // 
            // textBoxProyecto
            // 
            this.textBoxProyecto.Location = new System.Drawing.Point(261, 37);
            this.textBoxProyecto.Name = "textBoxProyecto";
            this.textBoxProyecto.Size = new System.Drawing.Size(342, 30);
            this.textBoxProyecto.TabIndex = 4;
            // 
            // dateTimePickerFechaInici
            // 
            this.dateTimePickerFechaInici.Location = new System.Drawing.Point(261, 109);
            this.dateTimePickerFechaInici.Name = "dateTimePickerFechaInici";
            this.dateTimePickerFechaInici.Size = new System.Drawing.Size(342, 30);
            this.dateTimePickerFechaInici.TabIndex = 5;
            // 
            // dateTimePickerFechaFin
            // 
            this.dateTimePickerFechaFin.Location = new System.Drawing.Point(261, 184);
            this.dateTimePickerFechaFin.Name = "dateTimePickerFechaFin";
            this.dateTimePickerFechaFin.Size = new System.Drawing.Size(342, 30);
            this.dateTimePickerFechaFin.TabIndex = 6;
            // 
            // listBoxUsuarios
            // 
            this.listBoxUsuarios.FormattingEnabled = true;
            this.listBoxUsuarios.ItemHeight = 25;
            this.listBoxUsuarios.Location = new System.Drawing.Point(261, 267);
            this.listBoxUsuarios.Name = "listBoxUsuarios";
            this.listBoxUsuarios.Size = new System.Drawing.Size(342, 129);
            this.listBoxUsuarios.TabIndex = 7;
            // 
            // labelTareas
            // 
            this.labelTareas.AutoSize = true;
            this.labelTareas.BackColor = System.Drawing.Color.Transparent;
            this.labelTareas.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTareas.ForeColor = System.Drawing.Color.White;
            this.labelTareas.Location = new System.Drawing.Point(679, 41);
            this.labelTareas.Name = "labelTareas";
            this.labelTareas.Size = new System.Drawing.Size(164, 24);
            this.labelTareas.TabIndex = 9;
            this.labelTareas.Text = "Agregar Tareas";
            // 
            // btnCrearProyecto
            // 
            this.btnCrearProyecto.Location = new System.Drawing.Point(565, 841);
            this.btnCrearProyecto.Name = "btnCrearProyecto";
            this.btnCrearProyecto.Size = new System.Drawing.Size(133, 47);
            this.btnCrearProyecto.TabIndex = 12;
            this.btnCrearProyecto.Text = "Guardar";
            this.btnCrearProyecto.UseVisualStyleBackColor = true;
            this.btnCrearProyecto.Click += new System.EventHandler(this.btnCrearProyecto_Click);
            // 
            // btnCancelarProyecto
            // 
            this.btnCancelarProyecto.Location = new System.Drawing.Point(751, 841);
            this.btnCancelarProyecto.Name = "btnCancelarProyecto";
            this.btnCancelarProyecto.Size = new System.Drawing.Size(133, 47);
            this.btnCancelarProyecto.TabIndex = 15;
            this.btnCancelarProyecto.Text = "Cancelar";
            this.btnCancelarProyecto.UseVisualStyleBackColor = true;
            // 
            // textBoxTarea
            // 
            this.textBoxTarea.Location = new System.Drawing.Point(858, 37);
            this.textBoxTarea.Name = "textBoxTarea";
            this.textBoxTarea.Size = new System.Drawing.Size(342, 30);
            this.textBoxTarea.TabIndex = 16;
            // 
            // buttonAgregarTarea
            // 
            this.buttonAgregarTarea.Location = new System.Drawing.Point(893, 115);
            this.buttonAgregarTarea.Name = "buttonAgregarTarea";
            this.buttonAgregarTarea.Size = new System.Drawing.Size(103, 44);
            this.buttonAgregarTarea.TabIndex = 17;
            this.buttonAgregarTarea.Text = "Agregar";
            this.buttonAgregarTarea.UseVisualStyleBackColor = true;
            // 
            // buttonCancelarTarea
            // 
            this.buttonCancelarTarea.Location = new System.Drawing.Point(1053, 115);
            this.buttonCancelarTarea.Name = "buttonCancelarTarea";
            this.buttonCancelarTarea.Size = new System.Drawing.Size(103, 44);
            this.buttonCancelarTarea.TabIndex = 18;
            this.buttonCancelarTarea.Text = "Cancelar";
            this.buttonCancelarTarea.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(679, 267);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 24);
            this.label1.TabIndex = 19;
            this.label1.Text = "Tareas";
            // 
            // listViewTareas
            // 
            this.listViewTareas.HideSelection = false;
            this.listViewTareas.Location = new System.Drawing.Point(858, 267);
            this.listViewTareas.Name = "listViewTareas";
            this.listViewTareas.Size = new System.Drawing.Size(342, 129);
            this.listViewTareas.TabIndex = 20;
            this.listViewTareas.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(37, 477);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 24);
            this.label2.TabIndex = 21;
            this.label2.Text = "Seleccionar Tarea";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(37, 548);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 24);
            this.label3.TabIndex = 22;
            this.label3.Text = "Agregar Subtarea";
            // 
            // comboBoxTareas
            // 
            this.comboBoxTareas.FormattingEnabled = true;
            this.comboBoxTareas.Location = new System.Drawing.Point(261, 474);
            this.comboBoxTareas.Name = "comboBoxTareas";
            this.comboBoxTareas.Size = new System.Drawing.Size(342, 33);
            this.comboBoxTareas.TabIndex = 23;
            // 
            // textBoxSubtarea
            // 
            this.textBoxSubtarea.Location = new System.Drawing.Point(261, 545);
            this.textBoxSubtarea.Name = "textBoxSubtarea";
            this.textBoxSubtarea.Size = new System.Drawing.Size(342, 30);
            this.textBoxSubtarea.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(679, 474);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 24);
            this.label4.TabIndex = 25;
            this.label4.Text = "Subtareas";
            // 
            // listViewSubtareas
            // 
            this.listViewSubtareas.HideSelection = false;
            this.listViewSubtareas.Location = new System.Drawing.Point(858, 474);
            this.listViewSubtareas.Name = "listViewSubtareas";
            this.listViewSubtareas.Size = new System.Drawing.Size(344, 129);
            this.listViewSubtareas.TabIndex = 26;
            this.listViewSubtareas.UseCompatibleStateImageBehavior = false;
            // 
            // buttonConfirmarSubtareas
            // 
            this.buttonConfirmarSubtareas.Location = new System.Drawing.Point(858, 650);
            this.buttonConfirmarSubtareas.Name = "buttonConfirmarSubtareas";
            this.buttonConfirmarSubtareas.Size = new System.Drawing.Size(138, 43);
            this.buttonConfirmarSubtareas.TabIndex = 27;
            this.buttonConfirmarSubtareas.Text = "Confirmar";
            this.buttonConfirmarSubtareas.UseVisualStyleBackColor = true;
            // 
            // buttonAgregarSubtarea
            // 
            this.buttonAgregarSubtarea.Location = new System.Drawing.Point(465, 650);
            this.buttonAgregarSubtarea.Name = "buttonAgregarSubtarea";
            this.buttonAgregarSubtarea.Size = new System.Drawing.Size(138, 43);
            this.buttonAgregarSubtarea.TabIndex = 28;
            this.buttonAgregarSubtarea.Text = "Agregar";
            this.buttonAgregarSubtarea.UseVisualStyleBackColor = true;
            // 
            // FormProyectos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = global::GestionProyectoEscritorio.Properties.Resources.fondo_chsarp_nergo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1250, 925);
            this.Controls.Add(this.buttonAgregarSubtarea);
            this.Controls.Add(this.buttonConfirmarSubtareas);
            this.Controls.Add(this.listViewSubtareas);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxSubtarea);
            this.Controls.Add(this.comboBoxTareas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listViewTareas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancelarTarea);
            this.Controls.Add(this.buttonAgregarTarea);
            this.Controls.Add(this.textBoxTarea);
            this.Controls.Add(this.btnCancelarProyecto);
            this.Controls.Add(this.btnCrearProyecto);
            this.Controls.Add(this.labelTareas);
            this.Controls.Add(this.listBoxUsuarios);
            this.Controls.Add(this.dateTimePickerFechaFin);
            this.Controls.Add(this.dateTimePickerFechaInici);
            this.Controls.Add(this.textBoxProyecto);
            this.Controls.Add(this.labelAgregarUsuarios);
            this.Controls.Add(this.labelFechaFin);
            this.Controls.Add(this.labelFechaInicio);
            this.Controls.Add(this.labelNombreProyecto);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormProyectos";
            this.Text = "Proyectos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNombreProyecto;
        private System.Windows.Forms.Label labelFechaInicio;
        private System.Windows.Forms.Label labelFechaFin;
        private System.Windows.Forms.Label labelAgregarUsuarios;
        private System.Windows.Forms.TextBox textBoxProyecto;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaInici;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaFin;
        private System.Windows.Forms.ListBox listBoxUsuarios;
        private System.Windows.Forms.Label labelTareas;
        private System.Windows.Forms.Button btnCrearProyecto;
        private System.Windows.Forms.Button btnCancelarProyecto;
        private System.Windows.Forms.TextBox textBoxTarea;
        private System.Windows.Forms.Button buttonAgregarTarea;
        private System.Windows.Forms.Button buttonCancelarTarea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewTareas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxTareas;
        private System.Windows.Forms.TextBox textBoxSubtarea;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listViewSubtareas;
        private System.Windows.Forms.Button buttonConfirmarSubtareas;
        private System.Windows.Forms.Button buttonAgregarSubtarea;
    }
}