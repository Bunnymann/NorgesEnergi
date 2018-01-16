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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.norgesEnergiDataSet11 = new NorgesEnergi_main.NorgesEnergiDataSet1();
            this.helppage_tableTableAdapter1 = new NorgesEnergi_main.NorgesEnergiDataSet1TableAdapters.helppage_tableTableAdapter();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.norgesEnergiDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.goto_login = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.norgesEnergiDataSet11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.norgesEnergiDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // import_from_db
            // 
            this.import_from_db.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.import_from_db.Location = new System.Drawing.Point(36, 22);
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
            this.dataGridView1.Size = new System.Drawing.Size(338, 196);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Location = new System.Drawing.Point(693, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 44);
            this.button1.TabIndex = 2;
            this.button1.Text = "GethelpInfo";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(656, 90);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(199, 196);
            this.textBox1.TabIndex = 3;
            // 
            // norgesEnergiDataSet11
            // 
            this.norgesEnergiDataSet11.DataSetName = "NorgesEnergiDataSet1";
            this.norgesEnergiDataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // helppage_tableTableAdapter1
            // 
            this.helppage_tableTableAdapter1.ClearBeforeFill = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // goto_login
            // 
            this.goto_login.Location = new System.Drawing.Point(511, 39);
            this.goto_login.Name = "goto_login";
            this.goto_login.Size = new System.Drawing.Size(75, 23);
            this.goto_login.TabIndex = 4;
            this.goto_login.Text = "Til salg";
            this.goto_login.UseVisualStyleBackColor = true;
            this.goto_login.Click += new System.EventHandler(this.goto_login_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 451);
            this.Controls.Add(this.goto_login);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.import_from_db);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.norgesEnergiDataSet11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.norgesEnergiDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button import_from_db;
        private System.Windows.Forms.BindingSource norgesEnergiDataSetBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private NorgesEnergiDataSet1 norgesEnergiDataSet11;
        private NorgesEnergiDataSet1TableAdapters.helppage_tableTableAdapter helppage_tableTableAdapter1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button goto_login;
    }
}

