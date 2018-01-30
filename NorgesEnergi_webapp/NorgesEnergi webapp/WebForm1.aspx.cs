using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;


namespace NorgesEnergi_webapp
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        string ConnectionString = "data source=telos.database.windows.net; database=Norges_Energi; user id=NorgesEnergi; password=Deterhemelig123";
      


        protected void Page_Load(object sender, EventArgs e)
        {

            
  
        }


        protected void list_Db_list_Click(object sender, EventArgs e)
        {
            SqlConnection list_db_connection = new SqlConnection(ConnectionString);
            try
            {
                // Pass the connection to the command object, so the command object knows on which
                // connection to execute the command
                SqlCommand list_db_cmd = new SqlCommand("SELECT hjelptekst FROM hjelpeinfo", list_db_connection);
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


            protected void update_grid_color_Click(object sender, EventArgs e)
            {
            
            }
    }  
}