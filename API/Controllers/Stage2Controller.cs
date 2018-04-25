using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Web.Mvc;
using API.Models;
using System.Data.Entity.Infrastructure;

namespace WebApplication .Controllers
{
    public class Stage2Controller : Controller
    {
        /** To view the list, write /dappertest/list after localhost port
         */
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();
        public ActionResult List()
        {
            var obj = GetAll();
            List<stage2> result = new List<stage2>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    stage2 model = new stage2();
                    model.stage2_ID = row.stage2_ID;
                    model.stage2_name = row.stage2_name;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<stage2> GetAll()
        {
            var obj = conn.Query<stage2>("SELECT * FROM Stage2").OrderByDescending(u => u.stage2_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(stage2 model)
        {
            var obj = InsertStage2(model);
            return RedirectToAction("list");
        }
        public bool InsertStage2(stage2 model)
        {
            int rowsAffected = conn.Execute("INSERT INTO Stage2([stage2_name]) VALUES (@name) ", new { name = model.stage2_name });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<stage2>("SELECT * FROM Stage2 WHERE stage2_ID =  @Stage2_ID", new { stage2_ID = id });

            if (obj != null)
            {
                stage2 model = new stage2();
                model.stage2_ID = obj.FirstOrDefault().stage2_ID;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<stage2>("SELECT * from Stage2 WHERE stage2_ID = @stage2_ID", new { stage2_ID = id });

            if (obj != null)
            {
                stage2 model = new stage2();
                model.stage2_ID = obj.FirstOrDefault().stage2_ID;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(stage2 model, int id)
        {
            var obj = conn.Execute("UPDATE Stage2 set [stage2_name] = @stage2_name WHERE stage2_ID = @stage2_ID", new { stage2_ID = id, stage2_name = model.stage2_name });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<stage2>("SELECT * from Stage2 WHERE stage2_ID = @stage2_ID", new { stage2_ID = id });

            if (obj != null)
            {
                stage2 model = new stage2();
                model.stage2_ID = obj.FirstOrDefault().stage2_ID;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(stage2 model, int id)
        {
            var obj = conn.Execute("DELETE from Stage2 WHERE stage2_ID = @stage2_ID", new { stage2_ID = id });

            return RedirectToAction("list");
        }

    }
}