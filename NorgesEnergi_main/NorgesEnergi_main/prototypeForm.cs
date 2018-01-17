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
            loadoutput();
        }
        
        private void loadoutput()
         {
                using (SqlConnection sqlCon = new SqlConnection(Connectionstring))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDainfo = new SqlDataAdapter("SELECT help_text FROM help_table ", sqlCon);
                    DataTable dtBlInfo = new DataTable();
                    sqlDainfo.Fill(dtBlInfo);
                
                    foreach (DataRow row in dtBlInfo.Rows)
                    {
                    listView1.Items.Add(row["help_text"].ToString());
                    } 
                    
                }
         }
        

        private void salg_navn_help_btn_Click(object sender, EventArgs e)
        {

        }
        
        private void salg_adr_help_btn_Click(object sender, EventArgs e)
        {

        }

        private void salg_tlf_help_btn_Click(object sender, EventArgs e)
        {

        }

        private void salg_add_cust_help_btn_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

         
        private void prototypeForm_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /*Higligts text if we add a search button
        private void highligth_help(Object sender, EventArgs e)
        {
            string[] words = richTextBox1.Text.Split(',');
            foreach(string word in words)
            {
                int startIndex = 0;
                while(startIndex < richTextBox1.TextLength)
                {
                    int wordStartIndex = richTextBox1.Find(word, startIndex, RichTextBoxFinds.None);
                    if (wordStartIndex != -1)
                    {
                        richTextBox1.SelectionStart = wordStartIndex;
                        richTextBox1.SelectionLength = word.Length;
                        richTextBox1.SelectionBackColor = Color.Red;
                    }
                    else
                        break;
                    startIndex += wordStartIndex + word.Length;
                }
            }

        }*/
    }
}
