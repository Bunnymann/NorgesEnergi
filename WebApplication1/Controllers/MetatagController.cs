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
    public class MetaTagController : Controller
    {
        /** To view the list, write /dappertest/list after localhost port
         */
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());

        public ActionResult List()
        {
            var obj = GetAll();
            List<MetaTag> result = new List<MetaTag>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    MetaTag model = new MetaTag();
                    model.metatag_ID = row.metatag_ID;
                    model.metatag_tag = row.metatag_tag;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<MetaTag> GetAll()
        {
            var obj = conn.Query<MetaTag>("Select * FROM MetaTag").OrderByDescending(u => u.metatag_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MetaTag model)
        {
            var obj = InsertMetatag(model);
            return RedirectToAction("list");
        }
        public bool InsertMetatag(MetaTag model)
        {
            int rowsAffected = conn.Execute("INSERT INTO MetaTag([metatag_tag]) VALUES (@tag)", new { tag = model.metatag_tag });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<MetaTag>("select * from MetaTag where metatag_ID = @metatag_ID", new { Metatag_ID = id });

            if (obj != null)
            {
                MetaTag model = new MetaTag();
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                model.metatag_tag = obj.FirstOrDefault().metatag_tag;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<MetaTag>("select * from Metatag where metatag_ID = @metatag_ID", new { metatag_ID = id });

            if (obj != null)
            {
                MetaTag model = new MetaTag();
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                model.metatag_tag = obj.FirstOrDefault().metatag_tag;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(MetaTag model, int id)
        {
            var obj = conn.Execute("update MetaTag set [metatag_tag] = @tag where metatag_ID = @metatag_ID", new { metatag_ID = id, tag = model.metatag_tag });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<MetaTag>("select * from MetaTag where metatag_ID = @metatag_ID", new { metatag_ID = id });

            if (obj != null)
            {
                MetaTag model = new MetaTag();
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                model.metatag_tag = obj.FirstOrDefault().metatag_tag;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(MetaTag model, int id)
        {
            var obj = conn.Execute("delete from MetaTag where metatag_ID = @metatag_ID", new { metatag_ID = id });

            return RedirectToAction("list");
        }

    }
}