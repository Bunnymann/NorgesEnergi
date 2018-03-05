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
    public class InfoController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());

        public ActionResult List()
        {
            var obj = GetAll();
            List<Info> result = new List<Info>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    Info model = new Info();
                    model.info_ID = row.info_ID;
                    model.stage1 = row.stage1;
                    model.stage2 = row.stage2;
                    model.stage3 = row.stage3;
                    model.stage4 = row.stage4;
               
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<Info> GetAll()
        {
            var obj = conn.Query<Info>("Select * FROM info").OrderByDescending(u => u.info_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Info model)
        {
            var obj = InsertInfo(model);
            return RedirectToAction("list");
        }
        public bool InsertInfo(Info model)
        {
            int rowsAffected = conn.Execute("INSERT INTO info ([stage1_ID],[stage2_ID],[stage3_ID],[stage4_ID]) VALUES (@stage01, @stage02, @stage03, @stage04)", new { stage01 = model.stage1, stage02 = model.stage2, stage03 = model.stage3, stage04 = model.stage4});
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<Info>("SELECT * FROM info WHERE info_ID = @infoID", new { infoID = id });

            if (obj != null)
            {
                Info model = new Info();
                model.info_ID = obj.FirstOrDefault().info_ID;
                model.stage1 = obj.FirstOrDefault().stage1;
                model.stage2 = obj.FirstOrDefault().stage2;
                model.stage3 = obj.FirstOrDefault().stage3;
                model.stage4 = obj.FirstOrDefault().stage4;
          
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<Info>("select * FROM info WHERE Info_ID = @InfoID", new { infoID = id });

            if (obj != null)
            {
                Info model = new Info();
                model.stage1 = obj.FirstOrDefault().stage1;
                model.stage2 = obj.FirstOrDefault().stage2;
                model.stage3 = obj.FirstOrDefault().stage3;
                model.stage4 = obj.FirstOrDefault().stage4;
           
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Info model, int id)
        {
            var obj = conn.Execute("update info set [stage1_ID] = @stage01 ,[stage2_ID] = @stage02,[stage3_ID] = @stage03,[stage4_ID] = @stage04 where Info_ID = @InfoID", new { InfoID = id, stage01 = model.stage1, stage02 = model.stage2, stage03 = model.stage3, stage04 = model.stage4 });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<Info>("select * from info where info_Id = @infoID", new { infoID = id });

            if (obj != null)
            {
                Info model = new Info();
                model.info_ID = obj.FirstOrDefault().info_ID;
                model.stage1 = obj.FirstOrDefault().stage1;
                model.stage2 = obj.FirstOrDefault().stage2;
                model.stage3 = obj.FirstOrDefault().stage3;
                model.stage4 = obj.FirstOrDefault().stage4;
          
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Info model, int id)
        {
            var obj = conn.Execute("delete from info where info_ID= @infoID", new { infoID = id });

            return RedirectToAction("list");
        }

    }
}