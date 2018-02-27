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
            List<Metatag> result = new List<Metatag>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    Metatag model = new Metatag();
                    model.metatag_ID = row.metatag_ID;
                    model.tag = row.tag;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<Metatag> GetAll()
        {
            var obj = conn.Query<Metatag>("SELECT * FROM Metatag").OrderByDescending(u => u.metatag_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Metatag model)
        {
            var obj = InsertMetatag(model);
            return RedirectToAction("list");
        }
        public bool InsertMetatag(Metatag model)
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
            var obj = conn.Query<Metatag>("SELECT * FROM Metatag WHERE metatag_ID = @metatag_ID", new { Metatag_ID = id });

            if (obj != null)
            {
                Metatag model = new Metatag();
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                model.tag = obj.FirstOrDefault().tag;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<Metatag>("SELECT * FROM Metatag WHERE metatag_ID = @metatag_ID", new { metatag_ID = id });

            if (obj != null)
            {
                Metatag model = new Metatag();
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                model.tag = obj.FirstOrDefault().tag;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Metatag model, int id)
        {
            var obj = conn.Execute("UPDATE Metatag set [tag] = @tag WHERE metatag_ID = @metatag_ID", new { metatag_ID = id, tag = model.tag });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<Metatag>("SELECT * FROM Metatag WHERE metatag_ID = @metatag_ID", new { metatag_ID = id });

            if (obj != null)
            {
                Metatag model = new Metatag();
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                model.tag = obj.FirstOrDefault().tag;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Metatag model, int id)
        {
            var obj = conn.Execute("DELETE FROM Metatag WHERE metatag_ID = @metatag_ID", new { metatag_ID = id });

            return RedirectToAction("list");
        }

    }
}