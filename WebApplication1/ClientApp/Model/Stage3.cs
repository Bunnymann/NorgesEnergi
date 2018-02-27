using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ClientApp.Data
{
    public class Stage3
    {
        public int Stage3_ID { get; set; }
        public string stage3_name { get; set; }
        public int helptext_ID { get; set; }
    }
}
