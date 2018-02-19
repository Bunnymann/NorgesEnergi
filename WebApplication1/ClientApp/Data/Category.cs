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
        public int Category_ID { get; set; }
        public string Category_name { get; set; }
        public int Parent_ID { get; set; }
    }
}
