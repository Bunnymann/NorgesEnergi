using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Web.Mvc;
using API.Models;
using System.Data.Entity.Validation;

namespace WebApplication1.Controllers
{
    public class MetaTagController : Controller
    {
        /** To view the list, write /dappertest/list after localhost port
         */
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();

        public ActionResult List()
        {
            var obj = GetAll();
            List<metatag> result = new List<metatag>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    metatag model = new metatag();
                    model.metatag_ID = row.metatag_ID;
                    model.tag = row.tag;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<metatag> GetAll()
        {
            var obj = conn.Query<metatag>("SELECT * FROM Metatag").OrderByDescending(u => u.metatag_ID).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(metatag model)
        {
            var obj = InsertMetatag(model);
            return RedirectToAction("List");
        }
        public bool InsertMetatag(metatag model)
        {
            int rowsAffected = conn.Execute("INSERT INTO Metatag([tag]) VALUES (@metatag)", new { metatag = model.tag });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<metatag>("SELECT * FROM Metatag WHERE metatag_ID = @metatag_ID", new { Metatag_ID = id });

            if (obj != null)
            {
                metatag model = new metatag();
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                model.tag = obj.FirstOrDefault().tag;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<metatag>("SELECT * FROM Metatag WHERE metatag_ID = @metatag_ID", new { metatag_ID = id });

            if (obj != null)
            {
                metatag model = new metatag();
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                model.tag = obj.FirstOrDefault().tag;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(metatag model, int id)
        {
            var obj = conn.Execute("UPDATE Metatag set [tag] = @mtag WHERE metatag_ID = @metatag_ID", new { metatag_ID = id, mtag = model.tag });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<metatag>("SELECT * FROM Metatag WHERE metatag_ID = @metatag_ID", new { metatag_ID = id });

            if (obj != null)
            {
                metatag model = new metatag();
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                model.tag = obj.FirstOrDefault().tag;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(metatag model, int id)
        {
            var obj = conn.Execute("DELETE FROM Metatag WHERE metatag_ID = @metatag_ID", new { metatag_ID = id });

            return RedirectToAction("list");
        }
            
        public ActionResult MultipleTags(int id)
        {
            char[] delimiterChars = { ',', '.', ':', };
            var obj = conn.Query<metatag>("select metatag.tag, helptext.helptext_header from metatag inner join helptexttag on metatag.metatag_ID = helptexttag.metatag_ID inner join helptext on helptexttag.helptext_ID = helptext.helptext_ID where helptext.helptext_ID = @ID;", new { ID = id });
            List<String> tags = new List<String>();
            
            if (obj != null)
            {
                foreach(var meta in obj)
                {
                    metatag model = new metatag();
                    model.tag = meta.tag.ToString();
                    tags.Add(model.ToString());
                }
            }

            String text = tags.ToString();

            string[] words = text.Split(delimiterChars);
            System.Console.WriteLine($"{words.Length} words in text:");

            foreach (var word in words)
            {
                System.Console.WriteLine($"<{word}>");
            }

            return View();

        }

        [HttpGet]
        public ActionResult AddTags()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTags(InfoViewModel model)
        {
            char[] delimiterChars = { ',', '.', ':', };
                helptext help = new helptext()
                {
                    helptext_header = model.helptext_header,
                    helptext_short = model.helptext_short,
                    helptext_long = model.helptext_long
                };

                metatag tag = new metatag()
                {
                    metatag_ID = model.metatag_ID,
                    tag = model.tag
                };

                String text = tag.ToString();

                string[] words = text.Split(delimiterChars);

                helptexttag ht = new helptexttag();
                foreach (var word in words)
                {
                    ht.helptext_ID = help.helptext_ID;
                    ht.metatag_ID = tag.metatag_ID;
                };

                db.metatag.Add(tag);
                db.helptext.Add(help);
                db.helptexttag.Add(ht);
            db.SaveChangesAsync();
            

            return RedirectToAction("List");
        }

    }
}