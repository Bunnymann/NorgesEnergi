namespace NorgesEnergi_main
{
    partial class login_form
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
            System.Windows.Forms.Label user_IDLabel;
            System.Windows.Forms.Label user_nameLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login_form));
            this.norgesEnergiDataSet1 = new NorgesEnergi_main.NorgesEnergiDataSet1();
            this.user_tableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.user_tableTableAdapter = new NorgesEnergi_main.NorgesEnergiDataSet1TableAdapters.user_tableTableAdapter();
            this.tableAdapterManager = new NorgesEnergi_main.NorgesEnergiDataSet1TableAdapters.TableAdapterManager();
            this.user_tableBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.user_tableBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.user_IDTextBox = new System.Windows.Forms.TextBox();
            this.user_nameTextBox = new System.Windows.Forms.TextBox();
            this.login_btn = new System.Windows.Forms.Button();
            this.new_user_btn = new System.Windows.Forms.Button();
            this.login_help_btn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            user_IDLabel = new System.Windows.Forms.Label();
            user_nameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.norgesEnergiDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_tableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_tableBindingNavigator)).BeginInit();
            this.user_tableBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // user_IDLabel
            // 
            user_IDLabel.AutoSize = true;
            user_IDLabel.Location = new System.Drawing.Point(181, 63);
            user_IDLabel.Name = "user_IDLabel";
            user_IDLabel.Size = new System.Drawing.Size(44, 13);
            user_IDLabel.TabIndex = 1;
            user_IDLabel.Text = "user ID:";
            // 
            // user_nameLabel
            // 
            user_nameLabel.AutoSize = true;
            user_nameLabel.Location = new System.Drawing.Point(181, 114);
            user_nameLabel.Name = "user_nameLabel";
            user_nameLabel.Size = new System.Drawing.Size(59, 13);
            user_nameLabel.TabIndex = 3;
            user_nameLabel.Text = "user name:";
            // 
            // norgesEnergiDataSet1
            // 
            this.norgesEnergiDataSet1.DataSetName = "NorgesEnergiDataSet1";
            this.norgesEnergiDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // user_tableBindingSource
            // 
            this.user_tableBindingSource.DataMember = "user_table";
            this.user_tableBindingSource.DataSource = this.norgesEnergiDataSet1;
            // 
            // user_tableTableAdapter
            // 
            this.user_tableTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.help_tableTableAdapter = null;
            this.tableAdapterManager.helpedit_tableTableAdapter = null;
            this.tableAdapterManager.helppage_tableTableAdapter = null;
            this.tableAdapterManager.page_tableTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = NorgesEnergi_main.NorgesEnergiDataSet1TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.user_tableTableAdapter = this.user_tableTableAdapter;
            this.tableAdapterManager.userpage_tableTableAdapter = null;
            // 
            // user_tableBindingNavigator
            // 
            this.user_tableBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.user_tableBindingNavigator.BindingSource = this.user_tableBindingSource;
            this.user_tableBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.user_tableBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.user_tableBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.user_tableBindingNavigatorSaveItem});
            this.user_tableBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.user_tableBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.user_tableBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.user_tableBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.user_tableBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.user_tableBindingNavigator.Name = "user_tableBindingNavigator";
            this.user_tableBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.user_tableBindingNavigator.Size = new System.Drawing.Size(730, 25);
            this.user_tableBindingNavigator.TabIndex = 0;
            this.user_tableBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // user_tableBindingNavigatorSaveItem
            // 
            this.user_tableBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.user_tableBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("user_tableBindingNavigatorSaveItem.Image")));
            this.user_tableBindingNavigatorSaveItem.Name = "user_tableBindingNavigatorSaveItem";
            this.user_tableBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.user_tableBindingNavigatorSaveItem.Text = "Save Data";
            this.user_tableBindingNavigatorSaveItem.Click += new System.EventHandler(this.user_tableBindingNavigatorSaveItem_Click);
            // 
            // user_IDTextBox
            // 
            this.user_IDTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.user_tableBindingSource, "user_ID", true));
            this.user_IDTextBox.Location = new System.Drawing.Point(260, 60);
            this.user_IDTextBox.Name = "user_IDTextBox";
            this.user_IDTextBox.Size = new System.Drawing.Size(100, 20);
            this.user_IDTextBox.TabIndex = 2;
            // 
            // user_nameTextBox
            // 
            this.user_nameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.user_tableBindingSource, "user_name", true));
            this.user_nameTextBox.Location = new System.Drawing.Point(260, 111);
            this.user_nameTextBox.Name = "user_nameTextBox";
            this.user_nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.user_nameTextBox.TabIndex = 4;
            // 
            // login_btn
            // 
            this.login_btn.Location = new System.Drawing.Point(418, 181);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(75, 23);
            this.login_btn.TabIndex = 5;
            this.login_btn.Text = "Log in";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // new_user_btn
            // 
            this.new_user_btn.Location = new System.Drawing.Point(305, 181);
            this.new_user_btn.Name = "new_user_btn";
            this.new_user_btn.Size = new System.Drawing.Size(75, 23);
            this.new_user_btn.TabIndex = 6;
            this.new_user_btn.Text = "Create New User";
            this.new_user_btn.UseVisualStyleBackColor = true;
            this.new_user_btn.Click += new System.EventHandler(this.new_user_btn_Click);
            // 
            // login_help_btn
            // 
            this.login_help_btn.Location = new System.Drawing.Point(606, 63);
            this.login_help_btn.Name = "login_help_btn";
            this.login_help_btn.Size = new System.Drawing.Size(75, 23);
            this.login_help_btn.TabIndex = 7;
            this.login_help_btn.Text = "Help";
            this.login_help_btn.UseVisualStyleBackColor = true;
            this.login_help_btn.Click += new System.EventHandler(this.login_help_btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(593, 92);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 135);
            this.textBox1.TabIndex = 8;
            // 
            // login_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 261);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.login_help_btn);
            this.Controls.Add(this.new_user_btn);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(user_nameLabel);
            this.Controls.Add(this.user_nameTextBox);
            this.Controls.Add(user_IDLabel);
            this.Controls.Add(this.user_IDTextBox);
            this.Controls.Add(this.user_tableBindingNavigator);
            this.Name = "login_form";
            this.Text = "login_form";
            this.Load += new System.EventHandler(this.login_form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.norgesEnergiDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_tableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.user_tableBindingNavigator)).EndInit();
            this.user_tableBindingNavigator.ResumeLayout(false);
            this.user_tableBindingNavigator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NorgesEnergiDataSet1 norgesEnergiDataSet1;
        private System.Windows.Forms.BindingSource user_tableBindingSource;
        private NorgesEnergiDataSet1TableAdapters.user_tableTableAdapter user_tableTableAdapter;
        private NorgesEnergiDataSet1TableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator user_tableBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton user_tableBindingNavigatorSaveItem;
        private System.Windows.Forms.TextBox user_IDTextBox;
        private System.Windows.Forms.TextBox user_nameTextBox;
        private System.Windows.Forms.Button login_btn;
        private System.Windows.Forms.Button new_user_btn;
        private System.Windows.Forms.Button login_help_btn;
        private System.Windows.Forms.TextBox textBox1;
    }
}