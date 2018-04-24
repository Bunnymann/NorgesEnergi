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
    public class Stage1Controller : Controller
    {
        /** To view the list, write /dappertest/list after localhost port
         */
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();
        public ActionResult List()
        {
            var obj = GetAll();
            List<stage1> result = new List<stage1>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    stage1 model = new stage1();
                    model.stage1_ID = row.stage1_ID;
                    model.stage1_name = row.stage1_name;
                    model.helptext_ID = row.helptext_ID;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<stage1> GetAll()
        {
            var obj = conn.Query<stage1>("Select * FROM stage1").OrderByDescending(u => u.stage1_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(stage1 model)
        {
            var obj = InsertStage1(model);
            return RedirectToAction("list");
        }
        public bool InsertStage1(stage1 model)
        {
            int rowsAffected = conn.Execute("INSERT INTO stage1([stage1_name], [helptext_ID]) VALUES (@name, @helptextID) ", new { name = model.stage1_name, helptextID = model.helptext_ID });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<stage1>("SELECT * from stage1 WHERE stage1_ID =  @stage1_ID", new { stage1_ID = id });

            if (obj != null)
            {
                stage1 model = new stage1();
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
            var obj = conn.Query<stage1>("SELECT * from stage1 WHERE stage1_ID = @stage1_ID", new { stage1_ID = id });

            if (obj != null)
            {
                stage1 model = new stage1();
                model.stage1_ID = obj.FirstOrDefault().stage1_ID;
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(stage1 model, int id)
        {
            var obj = conn.Execute("UPDATE stage1 set [stage1_name] = @stage1Name WHERE stage1_ID = @stage1_ID", new { stage1_ID = id, stage1Name = model.stage1_name });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<stage1>("SELECT * from stage1 WHERE stage1_ID = @stage1_ID", new { stage1_ID = id });

            if (obj != null)
            {
                stage1 model = new stage1();
                model.stage1_ID = obj.FirstOrDefault().stage1_ID;
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(stage1 model, int id)
        {
            var obj = conn.Execute("DELETE from stage1 WHERE stage1_ID = @stage1_ID", new { stage1_ID = id });

            return RedirectToAction("list");
        }

        public ActionResult CreateStage1()
        {
            PopulateHelpDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStage1([Bind(Include = "stage1_ID, stage1_name, helptext_ID")] stage1 stage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.stage1.Add(stage);
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