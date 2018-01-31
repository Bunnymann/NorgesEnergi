﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NorgesEnergi_webapp.Views
{
    public partial class Form_hp2 : System.Web.UI.Page
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
                SqlCommand cmd = new SqlCommand("SELECT hjelpetekst FROM hjelpeinfo", connection);
                // Open the connection. Otherwise you get a runtime error. An open connection is
                // required to execute the command
                connection.Open();
                Text1.DataSource = cmd.ExecuteReader();
                Text1.DataBind();
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

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void Text1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}