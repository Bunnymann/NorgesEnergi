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
        public int stage1_ID { get; set; }
        public string stage1name { get; set; }
        public int Parent_ID { get; set; }
    }
}
