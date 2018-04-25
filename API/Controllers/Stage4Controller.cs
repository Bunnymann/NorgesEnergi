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
using System.Net;

namespace NorgesEnergi.Controllers
{
    public class Stage4Controller : Controller
    {
        /** To view the list, write /dappertest/list after localhost port
         */
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();
        public ActionResult List()
        {
            var obj = GetAll();
            List<stage4> result = new List<stage4>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    stage4 model = new stage4();
                    model.stage4_ID = row.stage4_ID;
                    model.stage4_name = row.stage4_name;
                    model.helptext_ID = row.helptext_ID;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<stage4> GetAll()
        {
            var obj = conn.Query<stage4>("Select * FROM Stage4").OrderByDescending(u => u.stage4_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(stage4 model)
        {
            var obj = InsertStage4(model);
            return RedirectToAction("list");
        }
        public bool InsertStage4(stage4 model)
        {
            int rowsAffected = conn.Execute("INSERT INTO Stage4([stage4_name], [helptext_ID]) VALUES (@name, @helptextID)", new { name = model.stage4_name, helptextID = model.helptext_ID });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<stage4>("SELECT * from Stage4 WHERE stage4_ID =  @stage4_ID", new { stage4_ID = id });

            if (obj != null)
            {
                stage4 model = new stage4();
                model.stage4_ID = obj.FirstOrDefault().stage4_ID;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<stage4>("SELECT * from Stage4 WHERE stage4_ID = @stage4_ID", new { stage4_ID = id });

            if (obj != null)
            {
                stage4 model = new stage4();
                model.stage4_ID = obj.FirstOrDefault().stage4_ID;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(stage4 model, int id)
        {
            var obj = conn.Execute("UPDATE Stage4 set [stage4_name] = @name, [helptext_ID] = @helpID WHERE stage4_ID = @stage4_ID", new { stage4_ID = id, name = model.stage4_name, helpID = model.helptext_ID });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<stage4>("SELECT * from Stage4 WHERE stage4_ID = @stage4_ID", new { Stage4_ID = id });

            if (obj != null)
            {
                stage4 model = new stage4();
                model.stage4_ID = obj.FirstOrDefault().stage4_ID;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(stage4 model, int id)
        {
            var obj = conn.Execute("DELETE from Stage4 WHERE Stage4_ID = @Stage4_ID", new { Stage4_ID = id });

            return RedirectToAction("list");
        }

        public ActionResult CreateStage4()
        {
            PopulateHelpDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStage4([Bind(Include = "stage4_ID, stage4_name, helptext_ID, helptext_header, helptext_short, helptext_long")] stage4 stage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.stage4.Add(stage);
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stage4 stage = db.stage4
                .Where(s => s.stage4_ID == id)
                .Single();
            if (stage == null)
            {
                return HttpNotFound();
            }
            //PopulateHelpDropDownList(stage.helptext_ID);
            return View(stage);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stage4ToUpdate = db.stage4
                .Where(s => s.stage4_ID == id)
                .Single();
            if (TryUpdateModel(stage4ToUpdate, "",
                new string[] { "stage4_name" }))
            {
                try
                {
                    if(String.IsNullOrWhiteSpace(stage4ToUpdate.stage4_name))
                    {
                        stage4ToUpdate.stage4_name = null;
                    }
                    db.SaveChanges();
                    return RedirectToAction("FullList");
                }
                catch (RetryLimitExceededException  /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            //PopulateHelpDropDownList(stage4ToUpdate.helptext_ID);
            return View(stage4ToUpdate);
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