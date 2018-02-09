using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ClientApp.Data
{
    public class Category
    {
        public int category_ID { get; set; }
        public string category_name { get; set; }
        public int parent_ID { get; set; }
    }

    public List<Category> ReadAll()
    {
        using ConfigurationManager.Equals(2);
    }
}
