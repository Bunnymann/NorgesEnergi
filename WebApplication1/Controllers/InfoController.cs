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
                    model.stage1_name = row.stage1_name;
                    model.stage2_name = row.stage2_name;
                    model.stage3_name = row.stage3_name;
                    model.stage4_name = row.stage4_name;
                    model.stage1_ID = row.stage1_ID;
                    model.stage2_ID = row.stage2_ID;
                    model.stage3_ID = row.stage3_ID;
                    model.stage4_ID = row.stage4_ID;

                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<Info> GetAll()
        {
            var obj = conn.Query<Info>("select info_ID, stage1.stage1_name, stage2.stage2_name, stage3.stage3_name, stage4.stage4_name from info inner join stage1 on info.stage1_ID = stage1.stage1_ID inner join stage2 on info.stage2_ID = stage2.stage2_ID inner join stage3 on info.stage3_ID = stage3.stage3_ID inner join stage4 on info.stage4_ID = stage4.stage4_ID; ").OrderByDescending(u => u.info_ID).Take(10).ToList();
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
            int rowsAffected = conn.Execute("INSERT INTO info ([stage1_ID],[stage2_ID],[stage3_ID],[stage4_ID]) VALUES (@stage01, @stage02, @stage03, @stage04)", new { stage01 = model.stage1_ID, stage02 = model.stage2_ID, stage03 = model.stage3_ID, stage04 = model.stage4_ID});
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<Info>("select info_ID, stage1.stage1_name, stage2.stage2_name, stage3.stage3_name, stage4.stage4_name from info inner join stage1 on info.stage1_ID = stage1.stage1_ID inner join stage2 on info.stage2_ID = stage2.stage2_ID inner join stage3 on info.stage3_ID = stage3.stage3_ID inner join stage4 on info.stage4_ID = stage4.stage4_ID WHERE info_ID = @infoID", new { infoID = id });

            if (obj != null)
            {
                Info model = new Info();
                model.info_ID = obj.FirstOrDefault().info_ID;
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                model.stage3_name = obj.FirstOrDefault().stage3_name;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
          
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
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                model.stage3_name = obj.FirstOrDefault().stage3_name;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
           
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Info model, int id)
        {
            var obj = conn.Execute("update info set [stage1_ID] = @stage01 ,[stage2_ID] = @stage02,[stage3_ID] = @stage03,[stage4_ID] = @stage04 where Info_ID = @InfoID", new { InfoID = id, stage01 = model.stage1_ID, stage02 = model.stage2_ID, stage03 = model.stage3_ID, stage04 = model.stage4_ID });

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
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                model.stage3_name = obj.FirstOrDefault().stage3_name;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
          
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