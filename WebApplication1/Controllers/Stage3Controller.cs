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
    public class stage3Controller : Controller
    {
        /** To view the list, write /dappertest/list after localhost port
         */ 
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());

        public ActionResult List()
        {
            var obj = GetAll();
            List<Stage3> result = new List<Stage3>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    Stage3 model = new Stage3();
                    model.Stage3_ID = row.Stage3_ID;
                    model.stage3_name = row.stage3_name;
                    model.helptext_ID = row.helptext_ID;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<Stage3> GetAll()
        {
            var obj = conn.Query<Stage3>("Select * FROM stage3").OrderByDescending(u => u.Stage3_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Stage3 model)
        {
            var obj = InsertStage3(model);
            return RedirectToAction("list");
        }
        public bool InsertStage3(Stage3 model)
        {
            int rowsAffected = conn.Execute("INSERT INTO Stage3([stage3_name], [helptext_ID]) VALUES (@stage3_name, @helptext_ID)", new { stage3_name = model.stage3_name, helptextID = model.helptext_ID});
            if( rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<Stage3>("select * from Stage3 where Stage3_ID =  @stage3ID", new { stage3ID = id });

            if(obj != null)
            {
                Stage3 model = new Stage3();
                    model.Stage3_ID = obj.FirstOrDefault().Stage3_ID;
                    model.stage3_name = obj.FirstOrDefault().stage3_name;
                    model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                    return View(model);                
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<Stage3>("select * from Stage3 where Stage3_ID = @stage3ID", new { stage3ID = id });

            if (obj != null)
            {
                Stage3 model = new Stage3();
                model.Stage3_ID = obj.FirstOrDefault().Stage3_ID;
                model.stage3_name = obj.FirstOrDefault().stage3_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult Edit(Stage3 model, int id)
        {
            var obj = conn.Execute("update stage3 set [Stage3_name] = @stage3_Name where _ID = @stage3_ID", new { stage3ID = id, stage3_Name = model.stage3_name });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<Stage3>("select * from Stage3 where Stage3_ID = @stage3ID", new {stage3ID = id });

            if (obj != null)
            {
                Stage3 model = new Stage3();
                model.Stage3_ID = obj.FirstOrDefault().Stage3_ID;
                model.stage3_name = obj.FirstOrDefault().stage3_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Stage3 model, int id)
        {
            var obj = conn.Execute("delete from Stage3 where Stage3_ID = @stage3ID", new { stage3ID = id });

            return RedirectToAction("list");
        }

    }
}