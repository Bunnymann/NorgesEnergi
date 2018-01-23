using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace NorgesEnergi_webapp
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        string ConnectionString = "data source=telos.database.windows.net; database=Norges_Energi; user id=NorgesEnergi; password=Deterhemelig123";



        protected void Page_Load(object sender, EventArgs e)
        {
            //Create the connection object

            SqlConnection start_connection = new SqlConnection(ConnectionString);
            try
            {
                // Pass the connection to the command object, so the command object knows on which
                // connection to execute the command
                SqlCommand start_cmd = new SqlCommand("SELECT hovedkategori FROM hoved_kat", start_connection);
                // Open the connection. Otherwise you get a runtime error. An open connection is
                // required to execute the command
                start_connection.Open();
                start_grid_view.DataSource = start_cmd.ExecuteReader();
                start_grid_view.DataBind();
            }
            catch (Exception ex)
            {
                // Handle Exceptions, if any
            }
            finally
            {
                // The finally block is guarenteed to execute even if there is an exception. 
                //  This ensures connections are always properly closed.
                start_connection.Close();
            }
        }


        protected void list_Db_list_Click(object sender, EventArgs e)
        {
            SqlConnection list_db_connection = new SqlConnection(ConnectionString);
            try
            {
                // Pass the connection to the command object, so the command object knows on which
                // connection to execute the command
                SqlCommand list_db_cmd = new SqlCommand("SELECT hjelpekst FROM hjelpeinfo", list_db_connection);
                // Open the connection. Otherwise you get a runtime error. An open connection is
                // required to execute the command
                list_db_connection.Open();
                Db_GridView.DataSource = list_db_cmd.ExecuteReader();
                Db_GridView.DataBind();
            }
            catch (Exception ex)
            {
                // Handle Exceptions, if any
            }
            finally
            {
                // The finally block is guarenteed to execute even if there is an exception. 
                //  This ensures connections are always properly closed.
                list_db_connection.Close();
            }
        }

        protected void Kundenummer_Text_KeyDown(object sender, EventArgs e)
        {

            Db_GridView.Rows[0].Cells[1].BorderColor= System.Drawing.Color.Blue; 
        }
    }
    
}