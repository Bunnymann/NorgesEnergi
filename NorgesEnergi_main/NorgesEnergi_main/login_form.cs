using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; 

namespace NorgesEnergi_main
{
    public partial class login_form : Form
    {
        string Connectionstring = ("Data Source=neb-server.database.windows.net;Initial Catalog=NorgesEnergi;Persist Security Info=True;User ID=mariusfosseli@hotmail.com@neb-server;Password=ne_bachelor_1");

        public login_form()
        {
            InitializeComponent();
        }


        private void user_tableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.user_tableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.norgesEnergiDataSet1);

        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            using (Form1.sqlCon = new SqlConnection(Connectionstring))
            {
                Form1.sqlCon.Open();
                string userid = user_IDTextBox.Text;
                string username = user_nameTextBox.Text;
                SqlCommand cmd = new SqlCommand("SELECT user_ID,user_name from user_table where user_ID='" + user_IDTextBox.Text + "'and user_name='" + user_nameTextBox.Text + "'", Form1.sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Login sucess");
                    this.Hide();
                    Form main_form = new Form1();
                    main_form.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Login please check username and password");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
