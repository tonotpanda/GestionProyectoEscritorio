using System;

namespace GestionProyectoEscritorio
{
    partial class FormGestionJSON
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGestionJSON));
            this.dataGridViewJSON = new System.Windows.Forms.DataGridView();
            this.btnSeleccionarJSON = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelarJSON = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJSON)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewJSON
            // 
            this.dataGridViewJSON.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewJSON.Location = new System.Drawing.Point(37, 34);
            this.dataGridViewJSON.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridViewJSON.Name = "dataGridViewJSON";
            this.dataGridViewJSON.RowHeadersWidth = 51;
            this.dataGridViewJSON.Size = new System.Drawing.Size(1240, 572);
            this.dataGridViewJSON.TabIndex = 0;
            // 
            // btnSeleccionarJSON
            // 
            this.btnSeleccionarJSON.Location = new System.Drawing.Point(37, 643);
            this.btnSeleccionarJSON.Margin = new System.Windows.Forms.Padding(5);
            this.btnSeleccionarJSON.Name = "btnSeleccionarJSON";
            this.btnSeleccionarJSON.Size = new System.Drawing.Size(208, 48);
            this.btnSeleccionarJSON.TabIndex = 1;
            this.btnSeleccionarJSON.Text = "Seleccionar JSON";
            this.btnSeleccionarJSON.UseVisualStyleBackColor = true;
            this.btnSeleccionarJSON.Click += new System.EventHandler(this.btnSeleccionarJSON_Click);
            // 
            // btnBorrar
            // 
            this.btnBorrar.Location = new System.Drawing.Point(268, 643);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(208, 48);
            this.btnBorrar.TabIndex = 2;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = true;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(496, 643);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(208, 48);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelarJSON
            // 
            this.btnCancelarJSON.Location = new System.Drawing.Point(728, 643);
            this.btnCancelarJSON.Name = "btnCancelarJSON";
            this.btnCancelarJSON.Size = new System.Drawing.Size(208, 48);
            this.btnCancelarJSON.TabIndex = 4;
            this.btnCancelarJSON.Text = "Cancelar";
            this.btnCancelarJSON.UseVisualStyleBackColor = true;
            this.btnCancelarJSON.Click += new System.EventHandler(this.btnCancelarJSON_Click);
            // 
            // FormGestionJSON
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::GestionProyectoEscritorio.Properties.Resources.fondo_chsarp_nergo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1333, 727);
            this.Controls.Add(this.btnCancelarJSON);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnBorrar);
            this.Controls.Add(this.btnSeleccionarJSON);
            this.Controls.Add(this.dataGridViewJSON);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormGestionJSON";
            this.Text = "JSON";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJSON)).EndInit();
            this.ResumeLayout(false);

        }

        

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewJSON;
        private System.Windows.Forms.Button btnSeleccionarJSON;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelarJSON;
    }
}