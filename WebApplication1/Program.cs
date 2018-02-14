using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using WebApplication1.ClientApp.Data;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ConnectionString);
            string SqlString = "SELECT * FROM dbo.Category;";


            var ourCategory = (List<Category>)db.Query<Category>(SqlString);

            foreach (var Category in ourCategory)
            {
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("\nCategory ID: " + Category.category_ID.ToString());
                Console.WriteLine("First Name: " + Category.category_name);
                Console.WriteLine(new string('*', 20));
            }

            Console.ReadLine();
        }


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}