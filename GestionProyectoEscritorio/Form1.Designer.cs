namespace GestionProyectoEscritorio
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnProyectos = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGestionJSON = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.Location = new System.Drawing.Point(287, 133);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(229, 44);
            this.btnUsuarios.TabIndex = 0;
            this.btnUsuarios.Text = "Gestión Usuarios";
            this.btnUsuarios.UseVisualStyleBackColor = true;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnProyectos
            // 
            this.btnProyectos.Location = new System.Drawing.Point(287, 206);
            this.btnProyectos.Name = "btnProyectos";
            this.btnProyectos.Size = new System.Drawing.Size(229, 44);
            this.btnProyectos.TabIndex = 1;
            this.btnProyectos.Text = "Gestión Proyectos";
            this.btnProyectos.UseVisualStyleBackColor = true;
            this.btnProyectos.Click += new System.EventHandler(this.btnProyectos_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(287, 347);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(229, 44);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGestionJSON
            // 
            this.btnGestionJSON.Location = new System.Drawing.Point(287, 276);
            this.btnGestionJSON.Name = "btnGestionJSON";
            this.btnGestionJSON.Size = new System.Drawing.Size(229, 44);
            this.btnGestionJSON.TabIndex = 3;
            this.btnGestionJSON.Text = "Gestión JSON";
            this.btnGestionJSON.UseVisualStyleBackColor = true;
            this.btnGestionJSON.Click += new System.EventHandler(this.btnGestionJSON_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GestionProyectoEscritorio.Properties.Resources.fondo_chsarp_nergo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 507);
            this.Controls.Add(this.btnGestionJSON);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnProyectos);
            this.Controls.Add(this.btnUsuarios);
            this.Font = new System.Drawing.Font("Merriweather Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Gestor Proyectos";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnProyectos;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGestionJSON;
    }
}

