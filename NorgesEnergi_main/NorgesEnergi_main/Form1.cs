﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace NorgesEnergi_main
{
    public partial class Form1 : Form
    {

        string Connectionstring = ("Data Source=neb-server.database.windows.net;Initial Catalog=NorgesEnergi;Persist Security Info=True;User ID=mariusfosseli@hotmail.com@neb-server;Password=ne_bachelor_1");

        public Form1()
        {
            InitializeComponent();

        }


        //test if i can get the information from help_text from the database. 
        private void import_from_db_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(Connectionstring))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM user_table", sqlCon);
                DataTable dtBl = new DataTable();
                sqlDa.Fill(dtBl);


                // Insert btBl into datagrid1 
                dataGridView1.DataSource = dtBl;


            }
        }

        //finding elements from the database, and insert them into a textbox. through a button.
        private void button1_Click(object sender, EventArgs e)
        {
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
        
    }
}

