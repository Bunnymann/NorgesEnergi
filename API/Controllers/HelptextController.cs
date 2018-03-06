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
    public class HelptextController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());

        //Get list from function "GetALL" under
        public ActionResult List()
        {
            var obj = GetAll();
            List<Helptext> result = new List<Helptext>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    Helptext model = new Helptext();
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
        public List<Helptext> GetAll()
        {
            var obj = conn.Query<Helptext>("Select * FROM helptext").OrderByDescending(u => u.helptext_header).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Helptext model)
        {
            var obj = InsertHelptext(model);
            return RedirectToAction("list");
        }
        public bool InsertHelptext(Helptext model)
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
            var obj = conn.Query<Helptext>("SELECT * FROM helptext WHERE helptext_ID =  @text_ID", new { text_ID = id });

            if (obj != null)
            {
                Helptext model = new Helptext();
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
            var obj = conn.Query<Helptext>("SELECT * FROM helptext WHERE helptext_ID = @textID", new { textID = id });

            if (obj != null)
            {
                Helptext model = new Helptext();
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                model.helptext_header = obj.FirstOrDefault().helptext_header;
                model.helptext_short = obj.FirstOrDefault().helptext_short;
                model.helptext_long = obj.FirstOrDefault().helptext_long;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Helptext model, int id)
        {
            var obj = conn.Execute("UPDATE helptext SET [helptext_header] = @header, [helptext_short] = @text_short, [helptext_long] = @text_long WHERE helptext_ID = @helpID", new { helpID = id, header = model.helptext_header, text_short = model.helptext_short, text_long = model.helptext_long });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<Helptext>("SELECT * FROM helptext WHERE helptext_ID = @textID", new { textID = id });

            if (obj != null)
            {
                Helptext model = new Helptext();
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                model.helptext_header = obj.FirstOrDefault().helptext_header;
                model.helptext_short = obj.FirstOrDefault().helptext_short;
                model.helptext_long = obj.FirstOrDefault().helptext_long;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Helptext model, int id)
        {
            var obj = conn.Execute("DELETE FROM helptext WHERE helptext_ID = @textID", new { textID = id });

            return RedirectToAction("list");
        }

    }
}