using System;
using System.Collections.Generic;
using System.Configuration;
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

            public ActionResult List()
            {
                var obj = GetAll();
                List<helptexttag> result = new List<helptexttag>();
                if (obj != null)
                {
                    foreach (var row in obj)
                    {
                        helptexttag model = new helptexttag();
                        model.helptexttag_ID = row.helptexttag_ID;
                        model.helptext_ID = row.helptext_ID;
                        model.metatag_ID = row.metatag_ID;
                        result.Add(model);
                    }
                }
                return View(result);
            }
            public List<helptexttag> GetAll()
            {
                var obj = conn.Query<helptexttag>("SELECT * FROM helptexttag").ToList();
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

            [HttpGet]
            public ActionResult Details(int id)
            {
                var obj = conn.Query<helptexttag>("SELECT * FROM helptexttag WHERE helptexttag_ID = @helptag", new { helptag = id });

                foreach (var row in obj)
                {
                    helptexttag model = new helptexttag();
                    model.helptexttag_ID = row.helptexttag_ID;
                    model.helptext_ID = row.helptext_ID;
                    model.metatag_ID = row.metatag_ID;
                    return View(model);
                }
                return View();
            }

            [HttpGet]
            public ActionResult Edit(int id)
            {
                var obj = conn.Query<helptexttag>("SELECT * FROM helptexttag WHERE helptexttag_ID = @helptag", new { helptag = id });

                if (obj != null)
                {
                helptexttag model = new helptexttag();
                model.helptexttag_ID = obj.FirstOrDefault().helptexttag_ID;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                return View(model);
                }
                return View();
            }

            [HttpPost]
            public ActionResult Edit(helptexttag model, int id)
            {
                var obj = conn.Execute("UPDATE helptexttag SET [helptext_ID] = @textID, [metatag_ID] = @tagID  WHERE helptexttag_ID = @helptag", new { helptag = id, tagID = model.metatag_ID, textID = model.helptext_ID });

                return RedirectToAction("list");
            }

            [HttpGet]
            public ActionResult Delete(int id)
            {
                var obj = conn.Query<helptexttag>("SELECT * FROM helptexttag WHERE helptexttag_ID = @helptag", new { helptag = id });

                if (obj != null)
                {
                helptexttag model = new helptexttag();
                    model.helptexttag_ID = obj.FirstOrDefault().helptexttag_ID;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                    return View(model);
                }
                return View();
            }

            [HttpPost]
            public ActionResult Delete(helptexttag model, int id)
            {
                var obj = conn.Execute("DELETE FROM helptexttag WHERE helptexttag_ID = @helptag", new { helptag = id });

                return RedirectToAction("list");
            }
        }
    }