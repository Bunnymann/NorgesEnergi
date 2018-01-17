namespace NorgesEnergi_main
{
    partial class prototypeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.navn_textBox = new System.Windows.Forms.TextBox();
            this.adresse_textBox = new System.Windows.Forms.TextBox();
            this.telefon_textBox = new System.Windows.Forms.TextBox();
            this.salg_navn_help_btn = new System.Windows.Forms.Button();
            this.salg_adr_help_btn = new System.Windows.Forms.Button();
            this.salg_tlf_help_btn = new System.Windows.Forms.Button();
            this.salg_add_cust_btn = new System.Windows.Forms.Button();
            this.salg_add_cust_help_btn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Navn:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Adresse:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Telefon:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Salg";
            // 
            // navn_textBox
            // 
            this.navn_textBox.Location = new System.Drawing.Point(85, 99);
            this.navn_textBox.Name = "navn_textBox";
            this.navn_textBox.Size = new System.Drawing.Size(100, 20);
            this.navn_textBox.TabIndex = 4;
            this.navn_textBox.TextChanged += new System.EventHandler(this.navn_textBox_TextChanged);
            // 
            // adresse_textBox
            // 
            this.adresse_textBox.Location = new System.Drawing.Point(85, 125);
            this.adresse_textBox.Name = "adresse_textBox";
            this.adresse_textBox.Size = new System.Drawing.Size(100, 20);
            this.adresse_textBox.TabIndex = 5;
            this.adresse_textBox.TextChanged += new System.EventHandler(this.adresse_textBox_TextChanged);
            // 
            // telefon_textBox
            // 
            this.telefon_textBox.Location = new System.Drawing.Point(85, 151);
            this.telefon_textBox.Name = "telefon_textBox";
            this.telefon_textBox.Size = new System.Drawing.Size(100, 20);
            this.telefon_textBox.TabIndex = 6;
            this.telefon_textBox.TextChanged += new System.EventHandler(this.telefon_textBox_TextChanged);
            // 
            // salg_navn_help_btn
            // 
            this.salg_navn_help_btn.Location = new System.Drawing.Point(208, 97);
            this.salg_navn_help_btn.Name = "salg_navn_help_btn";
            this.salg_navn_help_btn.Size = new System.Drawing.Size(16, 23);
            this.salg_navn_help_btn.TabIndex = 7;
            this.salg_navn_help_btn.Text = "?";
            this.salg_navn_help_btn.UseVisualStyleBackColor = true;
            this.salg_navn_help_btn.Click += new System.EventHandler(this.salg_navn_help_btn_Click);
            // 
            // salg_adr_help_btn
            // 
            this.salg_adr_help_btn.Location = new System.Drawing.Point(208, 123);
            this.salg_adr_help_btn.Name = "salg_adr_help_btn";
            this.salg_adr_help_btn.Size = new System.Drawing.Size(16, 23);
            this.salg_adr_help_btn.TabIndex = 8;
            this.salg_adr_help_btn.Text = "?";
            this.salg_adr_help_btn.UseVisualStyleBackColor = true;
            this.salg_adr_help_btn.Click += new System.EventHandler(this.salg_adr_help_btn_Click);
            // 
            // salg_tlf_help_btn
            // 
            this.salg_tlf_help_btn.Location = new System.Drawing.Point(208, 151);
            this.salg_tlf_help_btn.Name = "salg_tlf_help_btn";
            this.salg_tlf_help_btn.Size = new System.Drawing.Size(16, 23);
            this.salg_tlf_help_btn.TabIndex = 9;
            this.salg_tlf_help_btn.Text = "?";
            this.salg_tlf_help_btn.UseVisualStyleBackColor = true;
            this.salg_tlf_help_btn.Click += new System.EventHandler(this.salg_tlf_help_btn_Click);
            // 
            // salg_add_cust_btn
            // 
            this.salg_add_cust_btn.Location = new System.Drawing.Point(98, 215);
            this.salg_add_cust_btn.Name = "salg_add_cust_btn";
            this.salg_add_cust_btn.Size = new System.Drawing.Size(87, 23);
            this.salg_add_cust_btn.TabIndex = 10;
            this.salg_add_cust_btn.Text = "Legg til kunde";
            this.salg_add_cust_btn.UseVisualStyleBackColor = true;
            this.salg_add_cust_btn.Click += new System.EventHandler(this.salg_add_cust_btn_Click);
            // 
            // salg_add_cust_help_btn
            // 
            this.salg_add_cust_help_btn.Location = new System.Drawing.Point(208, 215);
            this.salg_add_cust_help_btn.Name = "salg_add_cust_help_btn";
            this.salg_add_cust_help_btn.Size = new System.Drawing.Size(16, 23);
            this.salg_add_cust_help_btn.TabIndex = 11;
            this.salg_add_cust_help_btn.Text = "?";
            this.salg_add_cust_help_btn.UseVisualStyleBackColor = true;
            this.salg_add_cust_help_btn.Click += new System.EventHandler(this.salg_add_cust_help_btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(275, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 86);
            this.textBox1.TabIndex = 12;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // prototypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 329);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.salg_add_cust_help_btn);
            this.Controls.Add(this.salg_add_cust_btn);
            this.Controls.Add(this.salg_tlf_help_btn);
            this.Controls.Add(this.salg_adr_help_btn);
            this.Controls.Add(this.salg_navn_help_btn);
            this.Controls.Add(this.telefon_textBox);
            this.Controls.Add(this.adresse_textBox);
            this.Controls.Add(this.navn_textBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "prototypeForm";
            this.Text = "prototypeForm";
            this.Load += new System.EventHandler(this.prototypeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox navn_textBox;
        private System.Windows.Forms.TextBox adresse_textBox;
        private System.Windows.Forms.TextBox telefon_textBox;
        private System.Windows.Forms.Button salg_navn_help_btn;
        private System.Windows.Forms.Button salg_adr_help_btn;
        private System.Windows.Forms.Button salg_tlf_help_btn;
        private System.Windows.Forms.Button salg_add_cust_btn;
        private System.Windows.Forms.Button salg_add_cust_help_btn;
        private System.Windows.Forms.TextBox textBox1;
    }
}