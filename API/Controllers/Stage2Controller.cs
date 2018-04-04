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
                    model.helptext_ID = row.helptext_ID;
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
            int rowsAffected = conn.Execute("INSERT INTO Stage2([stage2_name], [helptext_ID]) VALUES (@name, @helptextID) ", new { name = model.stage2_name, helptextID = model.helptext_ID });
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
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
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
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
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
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
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

        public ActionResult CreateStage2()
        {
            PopulateHelpDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStage2([Bind(Include = "stage2_ID, stage2_name, helptext_ID")] stage2 stage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.stage2.Add(stage);
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