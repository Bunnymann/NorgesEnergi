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
    public class MetainfoController : Controller
    {
        /** To view the list, write /dappertest/list after localhost port
         */
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());

        public ActionResult List()
        {
            var obj = GetAll();
            List<MetaInfo> result = new List<MetaInfo>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    MetaInfo model = new MetaInfo();
                    model.category_ID = row.category_ID;
                    model.metatag_ID = row.metatag_ID;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<MetaInfo> GetAll()
        {
            var obj = conn.Query<MetaInfo>("select category.category_name, metatag.metatag_tag from category inner join metainfo on category.category_ID = metainfo.category_ID inner join metatag on metainfo.metatag_ID = metatag.metatag_ID").ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MetaInfo model)
        {
            var obj = InsertMetaInfo(model);
            return RedirectToAction("list");
        }
        public bool InsertMetaInfo(MetaInfo model)
        {
            int rowsAffected = conn.Execute("INSERT INTO metainfo([category_ID], [metatag_ID]) VALUES (@categoryID, @metatagID)", new { categoryID = model.category_ID, metatagID = model.metatag_ID });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<MetaInfo>("select * from metainfo where category_ID =  @categoryID", new { categoryID = id });

            if (obj != null)
            {
                MetaInfo model = new MetaInfo();
                model.category_ID = obj.FirstOrDefault().category_ID;
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<MetaInfo>("select * from metainfo where category_ID = @categoryID", new { categoryID = id });

            if (obj != null)
            {
                MetaInfo model = new MetaInfo();
                model.category_ID = obj.FirstOrDefault().category_ID;
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(MetaInfo model, int id)
        {
            var obj = conn.Execute("update metainfo set [metatag_ID = @metatagID] where category_ID = @categoryID", new { categoryID = id, metatagID = model.metatag_ID });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<MetaInfo>("select * from metainfo where category_ID = @categoryID", new { categoryID = id });

            if (obj != null)
            {
                MetaInfo model = new MetaInfo();
                model.category_ID = obj.FirstOrDefault().category_ID;
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(MetaInfo model, int id)
        {
            var obj = conn.Execute("delete  from metainfo where metatag_ID = @metatagID", new { metatagID = id });

            return RedirectToAction("list");
        }

    }
}