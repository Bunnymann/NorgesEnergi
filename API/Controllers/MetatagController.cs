using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Web.Mvc;
using API.Models;

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
            var obj = conn.Query<metatag>("SELECT * FROM Metatag").OrderByDescending(u => u.metatag_ID).Take(10).ToList();
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
            return RedirectToAction("list");
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

    }
}