using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace librarytest
{
    class connection
    {
        public string NewConnection()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
                return conn.ToString();
        }
    }
}
