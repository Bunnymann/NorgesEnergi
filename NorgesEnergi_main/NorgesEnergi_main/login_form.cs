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

        /*
         * Validates a user before login
         */
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

        //Error when adding existing primary key
        /*
         * Adds new user to system using @paramaters, parameters defined using input text box
         * Checks if data is added
         */ 
        private void new_user_btn_Click(object sender, EventArgs e)
        {
            using (Form1.sqlCon = new SqlConnection(Connectionstring))
            {
                Form1.sqlCon.Open();
                string new_userid = user_IDTextBox.Text;
                string new_username = user_nameTextBox.Text;
                SqlCommand cmd = new SqlCommand("INSERT INTO user_table (user_ID, user_name) VALUES (@user_ID, @user_name)", Form1.sqlCon);
                cmd.Parameters.AddWithValue("@user_ID", new_userid);
                cmd.Parameters.AddWithValue("@user_name", new_username);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.ExecuteNonQuery();
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("New user created!");   
                }
                else
                {
                    MessageBox.Show("User does allready exist or something else happened");
                }
            }
        }

        private void login_form_Load(object sender, EventArgs e)
        {

        }

        private void login_help_btn_Click(object sender, EventArgs e)
        {
            using (Form1.sqlCon = new SqlConnection(Connectionstring))
            {
                Form1.sqlCon.Open();
                SqlDataAdapter sqlDainfo = new SqlDataAdapter("SELECT help_text FROM help_table ", Form1.sqlCon);
                DataSet dtBlInfo = new DataSet();
                sqlDainfo.Fill(dtBlInfo);

                // Insert btBl into textTable 
                textBox1.Text += dtBlInfo.Tables[0].Rows[0]["help_text"].ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
