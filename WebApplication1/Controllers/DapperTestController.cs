using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.ClientApp.Data;
using Dapper;

namespace WebApplication1.Controllers
{
    public class DapperTestController : Controller
    {
        /** To view the list, write /dappertest/list after localhost port
         */ 
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());

        public ActionResult List()
        {
            var obj = GetAll();
            List<Category> result = new List<Category>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    Category model = new Category();
                    model.Category_ID = row.Category_ID;
                    model.Category_name = row.Category_name;
                    model.Parent_ID = row.Parent_ID;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<Category> GetAll()
        {
            var obj = conn.Query<Category>("Select * FROM category").OrderByDescending(u => u.Category_ID).Take(10).ToList();
            return obj;
        }
    }
}