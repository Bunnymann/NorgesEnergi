namespace NorgesEnergi_main
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.import_from_db = new System.Windows.Forms.Button();
            this.norgesEnergiDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.norgesEnergiDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // import_from_db
            // 
            this.import_from_db.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.import_from_db.Location = new System.Drawing.Point(188, 21);
            this.import_from_db.Name = "import_from_db";
            this.import_from_db.Size = new System.Drawing.Size(134, 44);
            this.import_from_db.TabIndex = 0;
            this.import_from_db.Text = "Hent fra DB";
            this.import_from_db.UseVisualStyleBackColor = false;
            this.import_from_db.Click += new System.EventHandler(this.import_from_db_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 90);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(487, 196);
            this.dataGridView1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 298);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.import_from_db);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.norgesEnergiDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button import_from_db;
        private System.Windows.Forms.BindingSource norgesEnergiDataSetBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

