using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NorgesEnergi_main;
using System.Data.SqlClient;

namespace NorgesEnergi_main
{
    public partial class prototypeForm : Form
    {
        string Connectionstring = ("Data Source=neb-server.database.windows.net;Initial Catalog=NorgesEnergi;Persist Security Info=True;User ID=mariusfosseli@hotmail.com@neb-server;Password=ne_bachelor_1");

        public prototypeForm()
        {
            InitializeComponent();
            using (SqlConnection sqlCon = new SqlConnection(Connectionstring))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDainfo = new SqlDataAdapter("SELECT help_text FROM help_table ", sqlCon);
                DataSet dtBlInfo = new DataSet();
                sqlDainfo.Fill(dtBlInfo);

                // Insert btBl into textTable 
                textBox1.Text += dtBlInfo.Tables[0].Rows[0]["help_text"].ToString();
                textBox1.Text += Environment.NewLine + dtBlInfo.Tables[0].Rows[1]["help_text"].ToString();
                textBox1.Text += Environment.NewLine + dtBlInfo.Tables[0].Rows[2]["help_text"].ToString();
                textBox1.Text += Environment.NewLine + dtBlInfo.Tables[0].Rows[3]["help_text"].ToString();
            }
        }

        private void salg_navn_help_btn_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(Connectionstring))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDainfo = new SqlDataAdapter("SELECT help_text FROM help_table ", sqlCon);
                DataSet dtBlInfo = new DataSet();
                sqlDainfo.Fill(dtBlInfo);

                // Insert btBl into textTable
                textBox1.Text = String.Empty;
                textBox1.Text += dtBlInfo.Tables[0].Rows[0]["help_text"].ToString();

            }

        }
        
        private void salg_adr_help_btn_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(Connectionstring))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDainfo = new SqlDataAdapter("SELECT help_text FROM help_table ", sqlCon);
                DataSet dtBlInfo = new DataSet();
                sqlDainfo.Fill(dtBlInfo);

                // Insert btBl into textTable
                textBox1.Text = String.Empty;
                textBox1.Text += Environment.NewLine + dtBlInfo.Tables[0].Rows[1]["help_text"].ToString();
                textBox1.ForeColor = Color.Red;
            }

        }

        private void salg_tlf_help_btn_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(Connectionstring))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDainfo = new SqlDataAdapter("SELECT help_text FROM help_table ", sqlCon);
                DataSet dtBlInfo = new DataSet();
                sqlDainfo.Fill(dtBlInfo);

                // Insert btBl into textTable
                textBox1.Text = String.Empty;
                textBox1.Text += dtBlInfo.Tables[0].Rows[2]["help_text"].ToString();
            }

        }

        private void salg_add_cust_help_btn_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(Connectionstring))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDainfo = new SqlDataAdapter("SELECT help_text FROM help_table ", sqlCon);
                DataSet dtBlInfo = new DataSet();
                sqlDainfo.Fill(dtBlInfo);

                // Insert btBl into textTable
                textBox1.Text = String.Empty;
                textBox1.Text += dtBlInfo.Tables[0].Rows[3]["help_text"].ToString();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

            private void salg_add_cust_btn_Click(object sender, EventArgs e)
        {

        }


        private void navn_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void adresse_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void telefon_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void prototypeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
