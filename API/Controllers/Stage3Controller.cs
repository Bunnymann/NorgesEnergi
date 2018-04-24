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

namespace NorgesEnergi.Controllers
{
    public class stage3Controller : Controller
    {
        /** To view the list, write /dappertest/list after localhost port
         */ 
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();
        public ActionResult List()
        {
            var obj = GetAll();
            List<stage3> result = new List<stage3>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    stage3 model = new stage3();
                    model.stage3_ID = row.stage3_ID;
                    model.stage3_name = row.stage3_name;
                    model.helptext_ID = row.helptext_ID;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<stage3> GetAll()
        {
            var obj = conn.Query<stage3>("SELECT * FROM stage3").OrderByDescending(u => u.stage3_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(stage3 model)
        {
            var obj = InsertStage3(model);
            return RedirectToAction("list");
        }
        public bool InsertStage3(stage3 model)
        {
            int rowsAffected = conn.Execute("INSERT INTO Stage3([stage3_name], [helptext_ID]) VALUES (@name, @helptextID)", new { name = model.stage3_name, helptextID = model.helptext_ID});
            if( rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<stage3>("SELECT * from Stage3 WHERE Stage3_ID = @stage3ID", new { stage3ID = id });

            if(obj != null)
            {
                stage3 model = new stage3();
                    model.stage3_ID = obj.FirstOrDefault().stage3_ID;
                    model.stage3_name = obj.FirstOrDefault().stage3_name;
                    model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                    return View(model);                
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<stage3>("SELECT * from Stage3 WHERE Stage3_ID = @stage3ID", new { stage3ID = id });

            if (obj != null)
            {
                stage3 model = new stage3();
                model.stage3_ID = obj.FirstOrDefault().stage3_ID;
                model.stage3_name = obj.FirstOrDefault().stage3_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult Edit(stage3 model, int id)
        {
            var obj = conn.Execute("UPDATE stage3 set [Stage3_name] = @stage3_Name WHERE stage3_ID = @stage3ID", new { stage3ID = id, stage3_Name = model.stage3_name });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<stage3>("SELECT * from Stage3 WHERE stage3_ID = @stage3ID", new {stage3ID = id });

            if (obj != null)
            {
                stage3 model = new stage3();
                model.stage3_ID = obj.FirstOrDefault().stage3_ID;
                model.stage3_name = obj.FirstOrDefault().stage3_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(stage3 model, int id)
        {
            var obj = conn.Execute("DELETE from Stage3 WHERE stage3_ID = @stage3ID", new { stage3ID = id });

            return RedirectToAction("list");
        }

        public ActionResult CreateStage3()
        {
            PopulateHelpDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStage3([Bind(Include = "stage3_ID, stage3_name, helptext_ID")] stage3 stage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.stage3.Add(stage);
                    db.SaveChanges();
                    return RedirectToAction("FullList");
                }
            }
            catch (RetryLimitExceededException /*dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateHelpDropDownList(stage.helptext_ID);
            return View(stage);
        }

        private void PopulateHelpDropDownList(object selecthelp = null)
        {
            var helptext = from h in db.helptext
                           orderby h.helptext_header
                           select h;
            ViewBag.helptext_ID = new SelectList(helptext, "helptext_ID", "helptext_header", selecthelp);
        }

    }
}