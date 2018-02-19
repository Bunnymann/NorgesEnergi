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
                    model.Info_ID = row.Info_ID;
                    model.Category_name = row.Category_name;
                    model.Parent_ID = row.Parent_ID;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<Category> GetAll()
        {
            var obj = conn.Query<Category>("Select * FROM category").OrderByDescending(u => u.Info_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category model)
        {
            var obj = InsertCategory(model);
            return RedirectToAction("list");
        }
        public bool InsertCategory(Category model)
        {
            int rowsAffected = conn.Execute("INSERT INTO category([category_name]) VALUES (@categoryName)", new { categoryName = model.Category_name });
            if( rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<Category>("select * from category where category_ID =  @categoryID", new { categoryID = id });

            if(obj != null)
            {
                Category model = new Category();
                    model.Info_ID = obj.FirstOrDefault().Info_ID;
                    model.Category_name = obj.FirstOrDefault().Category_name;
                    model.Parent_ID = obj.FirstOrDefault().Parent_ID;
                    return View(model);                
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<Category>("select * from category where category_ID = @categoryID", new { categoryID = id });

            if (obj != null)
            {
                Category model = new Category();
                model.Info_ID = obj.FirstOrDefault().Info_ID;
                model.Category_name = obj.FirstOrDefault().Category_name;
                model.Parent_ID = obj.FirstOrDefault().Parent_ID;
                return View(model);
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult Edit(Category model, int id)
        {
            var obj = conn.Execute("update category set [category_name] = @categoryName where _ID = @categoryID", new { categoryID = id, categoryName = model.Category_name });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<Category>("select * from category where category_ID = @categoryID", new { categoryID = id });

            if (obj != null)
            {
                Category model = new Category();
                model.Info_ID = obj.FirstOrDefault().Info_ID;
                model.Category_name = obj.FirstOrDefault().Category_name;
                model.Parent_ID = obj.FirstOrDefault().Parent_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Category model, int id)
        {
            var obj = conn.Execute("delete from category where category_ID = @categoryID", new { categoryID = id });

            return RedirectToAction("list");
        }

    }
}