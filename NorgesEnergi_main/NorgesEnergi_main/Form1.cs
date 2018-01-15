using System;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //test if i can get the information from help_text from the database. 
        private void import_from_db_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection ("Data Source=neb-server.database.windows.net;Initial Catalog=NorgesEnergi;Persist Security Info=True;User ID=mariusfosseli@hotmail.com@neb-server;Password=***********");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM page_table";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();
            // Data is accessible through the DataReader object here.

            sqlConnection1.Close();
        }
    }
}

