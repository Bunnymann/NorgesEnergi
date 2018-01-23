using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace NorgesEnergi_webapp
{
    public partial class form_privat : System.Web.UI.Page
    {
        string ConnectionString = "data source=telos.database.windows.net; database=Norges_Energi; user id=NorgesEnergi; password=Deterhemelig123";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Create the connection object

            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                // Pass the connection to the command object, so the command object knows on which
                // connection to execute the command
                SqlCommand cmd = new SqlCommand("SELECT under_kat.underkategori FROM under_kat INNER JOIN knyttekat ON knyttekat.under_ID = under_kat.under_ID INNER JOIN hoved_kat ON knyttekat.hoved_ID = hoved_kat.hoved_ID WHERE hoved_kat.hovedkategori = 'privat'; ", connection);

                // Open the connection. Otherwise you get a runtime error. An open connection is
                // required to execute the command
                connection.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                // Handle Exceptions, if any
            }
            finally
            {
                // The finally block is guarenteed to execute even if there is an exception. 
                //  This ensures connections are always properly closed.
                connection.Close();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT dbo.hjelpeinfo.hjelpekst FROM dbo.hjelpeinfo INNER JOIN dbo.infokat ON infokat.info_ID = hjelpeinfo.info_ID INNER JOIN dbo.under_kat ON under_kat.under_ID = infokat.under_ID INNER JOIN dbo.knyttekat ON knyttekat.under_ID = under_kat.under_ID INNER JOIN dbo.hoved_kat ON hoved_kat.hoved_ID = knyttekat.hoved_ID WHERE hovedkategori = 'privat'; ", connection);

                connection.Open();
                GridView2.DataSource = cmd.ExecuteReader();
                GridView2.DataBind();
            }
            catch (Exception ex)
            {
                // Handle Exceptions, if any
            }
            finally
            {
                // The finally block is guarenteed to execute even if there is an exception. 
                //  This ensures connections are always properly closed.
                connection.Close();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            String searchkat = TextBox1.Text;
            //Create the connection object

            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                if (searchkat == "")
                {
                    // Pass the connection to the command object, so the command object knows on which
                    // connection to execute the command
                    SqlCommand cmd = new SqlCommand("SELECT dbo.hjelpeinfo.hjelpekst FROM dbo.hjelpeinfo INNER JOIN dbo.infokat ON infokat.info_ID = hjelpeinfo.info_ID INNER JOIN dbo.under_kat ON under_kat.under_ID = infokat.under_ID INNER JOIN dbo.knyttekat ON knyttekat.under_ID = under_kat.under_ID INNER JOIN dbo.hoved_kat ON hoved_kat.hoved_ID = knyttekat.hoved_ID WHERE hovedkategori = 'privat'; ", connection);
                    // Open the connection. Otherwise you get a runtime error. An open connection is
                    // required to execute the command
                    connection.Open();
                    GridView2.DataSource = cmd.ExecuteReader();
                    GridView2.DataBind();
                }
                else
                {
                    // Pass the connection to the command object, so the command object knows on which
                    // connection to execute the command
                    SqlCommand cmd = new SqlCommand("SELECT dbo.hjelpeinfo.hjelpekst, dbo.under_kat.underkategori FROM dbo.hjelpeinfo INNER JOIN dbo.infokat ON infokat.info_ID = hjelpeinfo.info_ID INNER JOIN dbo.under_kat ON under_kat.under_ID = infokat.under_ID INNER JOIN dbo.knyttekat ON dbo.knyttekat.under_ID = dbo.under_kat.under_ID INNER JOIN dbo.hoved_kat ON dbo.knyttekat.hoved_ID = dbo.hoved_kat.hoved_ID WHERE underkategori = '" + searchkat + "' AND hovedkategori = 'privat'", connection);
                    // Open the connection. Otherwise you get a runtime error. An open connection is
                    // required to execute the command
                    connection.Open();
                    GridView2.DataSource = cmd.ExecuteReader();
                    GridView2.DataBind();
                }
            }

            catch (Exception ex)
            {
                // Handle Exceptions, if any
            }
            finally
            {
                // The finally block is guarenteed to execute even if there is an exception. 
                //  This ensures connections are always properly closed.
                connection.Close();
            }
            

        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {

        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}