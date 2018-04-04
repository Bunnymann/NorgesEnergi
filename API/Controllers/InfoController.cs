using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Web.Mvc;
using API.Models;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace WebApplication1.Controllers
{
    public class InfoController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();

        public ActionResult DDLSearchStage3()
        {
            Norges_EnergiEntities stages = new Norges_EnergiEntities();
            return View(stages.stage3.ToList());
        }

        public ActionResult DDLSearchStage4()
        {
            Norges_EnergiEntities stages = new Norges_EnergiEntities();
            return View(stages.stage4.ToList());
        }

        public ActionResult Stage3DDLSearch(string search)
        {
            Norges_EnergiEntities stages = new Norges_EnergiEntities();
            var stageList = stages.stage3.Where(x => x.stage3_name.Contains(search)).ToList();
            return Json(stageList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult viewAll()
        {
            Norges_EnergiEntities info = new Norges_EnergiEntities();

            return View();
        }

        public ActionResult Index()
        {
            Norges_EnergiEntities db = new Norges_EnergiEntities();

            List<stage1> stage1List = db.stage1.ToList();
            ViewBag.stage1List = new SelectList(stage1List, "stage1_ID", "stage1_name");

            List<stage2> stage2List = db.stage2.ToList();
            ViewBag.stage2List = new SelectList(stage2List, "stage2_ID", "stage2_name");

            List<stage3> stage3List = db.stage3.ToList();
            ViewBag.stage3List = new SelectList(stage3List, "stage3_ID", "stage3_name");

            List<stage4> stage4List = db.stage4.ToList();
            ViewBag.stage4List = new SelectList(stage4List, "stage4_ID", "stage4_name");

            return View();
        }
        [HttpPost]
        public ActionResult Index(InfoViewModel model)
        {
            try
            {
                Norges_EnergiEntities db = new Norges_EnergiEntities();

                List<stage1> stage1List = db.stage1.ToList();
                ViewBag.stage1List = new SelectList(stage1List, "stage1_ID", "stage1_name");

                List<stage2> stage2List = db.stage2.ToList();
                ViewBag.stage2List = new SelectList(stage2List, "stage2_ID", "stage2_name");

                List<stage3> stage3List = db.stage3.ToList();
                ViewBag.stage3List = new SelectList(stage3List, "stage3_ID", "stage3_name");

                List<stage4> stage4List = db.stage4.ToList();
                ViewBag.stage4List = new SelectList(stage4List, "stage4_ID", "stage4_name");

                helptext help = new helptext();
                help.helptext_header = model.helptext_header;
                help.helptext_short = model.helptext_short;
                help.helptext_long = model.helptext_long;

                db.helptext.Add(help);
                db.SaveChanges();
                int latesthelp = help.helptext_ID;

                metatag tag = new metatag();
                tag.metatag_ID = model.metatag_ID;
                tag.tag = model.tag;

                db.metatag.Add(tag);
                db.SaveChanges();
                int latesttag = tag.metatag_ID;

                helptexttag hptag = new helptexttag();
                hptag.helptext_ID = latesthelp;
                hptag.metatag_ID = latesttag;

                db.helptexttag.Add(hptag);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return View(model);
        }

        public ActionResult AddTags()
        {
            Norges_EnergiEntities db = new Norges_EnergiEntities();
            return View();
        }

        [HttpPost]
        public ActionResult AddTags(HelpTagsViewModel model)
        {
            try
            {
                Norges_EnergiEntities db = new Norges_EnergiEntities();

                helptext help = new helptext();
                help.helptext_ID = model.helptext_ID;
                help.helptext_header = model.helptext_header;
                help.helptext_short = model.helptext_short;
                help.helptext_long = model.helptext_long;

                db.helptext.Add(help);
                db.SaveChanges();
                int lasthelp = help.helptext_ID;

                metatag tag = new metatag();
                tag.metatag_ID = model.tag_ID;
                tag.tag = model.tag;

                db.metatag.Add(tag);
                db.SaveChanges();
                int lasttag = tag.metatag_ID;

                helptexttag htag = new helptexttag();
                htag.helptext_ID = lasthelp;
                htag.metatag_ID = lasttag;

                db.helptexttag.Add(htag);
                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            return View(model);
        }

        public List<InfoViewModel> GetFullList()
        {
            //METATAG.TAG IS NOT IN THIS SQLQUERY
            var obj = conn.Query<InfoViewModel>("SELECT info.info_ID, stage1.stage1_name, stage2.stage2_name, stage3.stage3_name, stage4.stage4_name, helptext.helptext_header, helptext.helptext_short, helptext.helptext_long FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID;") /*INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID;")*/.OrderByDescending(u => u.info_ID).ToList();

            return obj;
        }

        public ActionResult FullList()
        {
            var obj = GetFullList();
            List<InfoViewModel> result = new List<InfoViewModel>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    InfoViewModel model = new InfoViewModel();
                    model.info_ID = row.info_ID;
                    model.stage1_name = row.stage1_name;
                    model.stage2_name = row.stage2_name;
                    model.stage3_name = row.stage3_name;
                    model.stage4_name = row.stage4_name;
                    model.helptext_header = row.helptext_header;
                    model.helptext_short = row.helptext_short;
                    model.helptext_long = row.helptext_long;

                    result.Add(model);
                }
            }
            return View(result);
        }

       

        public ActionResult CreateInfo()
        {
            PopulateStage1DropDownList();
            PopulateStage2DropDownList();
            PopulateStage3DropDownList();
            PopulateStage4DropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInfo([Bind(Include = "info_ID, stage1_ID, stage2_ID, stage3_ID, stage4_ID")] info fullinfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.info.Add(fullinfo);
                    db.SaveChanges();
                    return RedirectToAction("FullList");
                }
            }
            catch (RetryLimitExceededException /*dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateStage1DropDownList(fullinfo.stage1_ID);
            PopulateStage2DropDownList(fullinfo.stage2_ID);
            PopulateStage3DropDownList(fullinfo.stage3_ID);
            PopulateStage4DropDownList(fullinfo.stage4_ID);
            return View(fullinfo);
        }

        private void PopulateStage1DropDownList(object stage1 = null)
        {
            var stage = from s in db.stage1
                           orderby s.stage1_name
                           select s;
            ViewBag.stage1_ID = new SelectList(stage, "stage1_ID", "stage1_name", stage1);
        }

        private void PopulateStage2DropDownList(object stage2 = null)
        {
            var stage = from s in db.stage2
                        orderby s.stage2_name
                        select s;
            ViewBag.stage2_ID = new SelectList(stage, "stage2_ID", "stage2_name", stage2);
        }

        private void PopulateStage3DropDownList(object stage3 = null)
        {
            var stage = from s in db.stage3
                        orderby s.stage3_name
                        select s;
            ViewBag.stage3_ID = new SelectList(stage, "stage3_ID", "stage3_name", stage3);
        }

        private void PopulateStage4DropDownList(object stage4 = null)
        {
            var stage = from s in db.stage4
                        orderby s.stage4_name
                        select s;
            ViewBag.stage4_ID = new SelectList(stage, "stage4_ID", "stage4_name", stage4);
        }
    }
}
