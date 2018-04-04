using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using API.Models;
using System.Web.Mvc;
using System.Configuration;
using System.Data.Entity.Infrastructure;

namespace WebApplication1.Controllers
{
    public class HelptextController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();
        //Get list from function "GetALL" under
        public ActionResult List()
        {
            var obj = GetAll();
            List<helptext> result = new List<helptext>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    helptext model = new helptext();
                    model.helptext_ID = row.helptext_ID;
                    model.helptext_header = row.helptext_header;
                    model.helptext_short = row.helptext_short;
                    model.helptext_long = row.helptext_long;
                    result.Add(model);
                }
            }
            return View(result);
        }

        //Get all values from table helptext ordered by helptext header name
        public List<helptext> GetAll()
        {
            var obj = conn.Query<helptext>("Select * FROM helptext").OrderByDescending(u => u.helptext_header).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(helptext model)
        {
            var obj = InsertHelptext(model);
            return RedirectToAction("list");
        }
        public bool InsertHelptext(helptext model)
        {
            int rowsAffected = conn.Execute("INSERT INTO helptext([helptext_header], [helptext_short], [helptext_long]) VALUES (@header, @text_short, @text_long)", new { header = model.helptext_header, text_short = model.helptext_short, text_long = model.helptext_long });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<helptext>("SELECT * FROM helptext WHERE helptext_ID =  @text_ID", new { text_ID = id });

            if (obj != null)
            {
                helptext model = new helptext();
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                model.helptext_header = obj.FirstOrDefault().helptext_header;
                model.helptext_short = obj.FirstOrDefault().helptext_short;
                model.helptext_long = obj.FirstOrDefault().helptext_long;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<helptext>("SELECT * FROM helptext WHERE helptext_ID = @textID", new { textID = id });

            if (obj != null)
            {
                helptext model = new helptext();
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                model.helptext_header = obj.FirstOrDefault().helptext_header;
                model.helptext_short = obj.FirstOrDefault().helptext_short;
                model.helptext_long = obj.FirstOrDefault().helptext_long;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(helptext model, int id)
        {
            var obj = conn.Execute("UPDATE helptext SET [helptext_header] = @header, [helptext_short] = @text_short, [helptext_long] = @text_long WHERE helptext_ID = @helpID", new { helpID = id, header = model.helptext_header, text_short = model.helptext_short, text_long = model.helptext_long });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<helptext>("SELECT * FROM helptext WHERE helptext_ID = @textID", new { textID = id });

            if (obj != null)
            {
                helptext model = new helptext();
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                model.helptext_header = obj.FirstOrDefault().helptext_header;
                model.helptext_short = obj.FirstOrDefault().helptext_short;
                model.helptext_long = obj.FirstOrDefault().helptext_long;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(helptext model, int id)
        {
            var obj = conn.Execute("DELETE FROM helptext WHERE helptext_ID = @textID", new { textID = id });

            return RedirectToAction("list");
        }

        public ActionResult CreateHelptext()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHelptext([Bind(Include = "helptext_ID, helptext_header, helptext_short, helptext_long")]helptext help)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.helptext.Add(help);
                    db.SaveChanges();
                    return RedirectToAction("FullList");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(help);
        }

    }
}