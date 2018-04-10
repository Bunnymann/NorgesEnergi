using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using API.Models;
using Dapper;

namespace WebApplication1.Controllers
{
    public class HelptexttagController : Controller
    {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();
        

            public ActionResult List()
            {
                var obj = GetAll();
                List<HelpTagsViewModel> result = new List<HelpTagsViewModel>();
                if (obj != null)
                {
                    foreach (var row in obj)
                    {
                        HelpTagsViewModel model = new HelpTagsViewModel();
                        model.helptext_ID = row.helptext_ID;
                        model.helptext_header = row.helptext_header;
                        model.helptext_short = row.helptext_short;
                        model.tag = row.tag;
                        result.Add(model);
                    }
                }
                return View(result);
            }
            public List<HelpTagsViewModel> GetAll()
            {
                var obj = conn.Query<HelpTagsViewModel>("SELECT helptext.helptext_header, helptext.helptext_short, metatag.tag FROM helptext INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID; ").OrderByDescending(h => h.helptext_header).ToList();
                return obj;
            }

            [HttpGet]
            public ActionResult Create()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Create(helptexttag model)
            {
                var obj = InsertHelptexttag(model);
                return RedirectToAction("list");
            }
            public bool InsertHelptexttag(helptexttag model)
            {
                int rowsAffected = conn.Execute("INSERT INTO helptexttag([helptext_ID], [metatag_ID]) VALUES (@textID, @tagID)", new { textID = model.helptext_ID, @tagID = model.metatag_ID });
                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }

        public ActionResult CreateMetahelp()
        {
            PopulateHelptextDropDownList();
            PopulateTagDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMetahelp([Bind(Include = "helptexttag_ID, helptext_ID, metatag_ID")] helptexttag metahelp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.helptexttag.Add(metahelp);
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
            }
            catch (RetryLimitExceededException /*dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateHelptextDropDownList(metahelp.helptext_ID);
            PopulateTagDropDownList(metahelp.metatag_ID);
            return View(metahelp);
        }

        private void PopulateHelptextDropDownList(object helptext = null)
        {
            var help = from h in db.helptext
                        orderby h.helptext_header
                        select h;
            ViewBag.helptext_ID = new SelectList(help, "helptext_ID", "helptext_header", helptext);
        }

        private void PopulateTagDropDownList(object tag = null)
        {
            var metatag = from m in db.metatag
                        orderby m.tag
                        select m;
            ViewBag.metatag_ID = new SelectList(metatag, "metatag_ID", "tag", tag);
        }
    }
}