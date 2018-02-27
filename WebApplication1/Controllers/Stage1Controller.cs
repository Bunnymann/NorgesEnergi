using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.ClientApp.Data;
using Dapper;
using WebApplication1.ClientApp.Model;

namespace WebApplication1.Controllers
{
    public class Stage1Controller : Controller
    {
        /** To view the list, write /dappertest/list after localhost port
         */
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());

        public ActionResult List()
        {
            var obj = GetAll();
            List<Stage1> result = new List<Stage1>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    Stage1 model = new Stage1();
                    model.stage1_ID = row.stage1_ID;
                    model.stage1_name = row.stage1_name;
                    model.helptext_ID = row.helptext_ID;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<Stage1> GetAll()
        {
            var obj = conn.Query<Stage1>("Select * FROM stage1").OrderByDescending(u => u.stage1_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Stage1 model)
        {
            var obj = InsertCategory(model);
            return RedirectToAction("list");
        }
        public bool InsertCategory(Stage1 model)
        {
            int rowsAffected = conn.Execute("INSERT INTO stage1([stage1_name]) VALUES (@stage1Name)", new { stage1Name = model.stage1_name });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<Stage1>("select * from stage1 where stage1_ID =  @stage1_ID", new { stage1_ID = id });

            if (obj != null)
            {
                Stage1 model = new Stage1();
                model.stage1_ID = obj.FirstOrDefault().stage1_ID;
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<Stage1>("select * from stage1 where stage1_ID = @stage1_ID", new { stage1_ID = id });

            if (obj != null)
            {
                Stage1 model = new Stage1();
                model.stage1_ID = obj.FirstOrDefault().stage1_ID;
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Stage1 model, int id)
        {
            var obj = conn.Execute("update stage1 set [stage1_name] = @stage1Name where stage1_ID = @stage1_ID", new { stage1_ID = id, stage1Name = model.stage1_name });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<Stage1>("select * from stage1 where stage1_ID = @stage1_ID", new { stage1_ID = id });

            if (obj != null)
            {
                Stage1 model = new Stage1();
                model.stage1_ID = obj.FirstOrDefault().stage1_ID;
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Stage1 model, int id)
        {
            var obj = conn.Execute("delete from stage1 where stage1_ID = @stage1_ID", new { stage1_ID = id });

            return RedirectToAction("list");
        }

    }
}