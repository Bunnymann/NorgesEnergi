﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.ClientApp.Data;
using Dapper;
using WebApplication1.ClientApp.Model;

namespace WebApplication1.Controllers
{
    public class Stage2Controller : Controller
    {
        /** To view the list, write /dappertest/list after localhost port
         */
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());

        public ActionResult List()
        {
            var obj = GetAll();
            List<Stage2> result = new List<Stage2>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    Stage2 model = new Stage2();
                    model.stage2_ID = row.stage2_ID;
                    model.stage2_name = row.stage2_name;
                    model.helptext_ID = row.helptext_ID;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<Stage2> GetAll()
        {
            var obj = conn.Query<Stage2>("Select * FROM Stage2").OrderByDescending(u => u.stage2_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Stage2 model)
        {
            var obj = InsertStage3(model);
            return RedirectToAction("list");
        }
        public bool InsertStage3(Stage2 model)
        {
            int rowsAffected = conn.Execute("INSERT INTO Stage2([stage2_name]) VALUES (@Stage2Name)", new { categoryName = model.stage2_name });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<Stage2>("select * from Stage2 where stage2_ID =  @Stage2_ID", new { stage2_ID = id });

            if (obj != null)
            {
                Stage2 model = new Stage2();
                model.stage2_ID = obj.FirstOrDefault().stage2_ID;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<Stage2>("select * from Stage2 where stage2_ID = @stage2_ID", new { stage2_ID = id });

            if (obj != null)
            {
                Stage2 model = new Stage2();
                model.stage2_ID = obj.FirstOrDefault().stage2_ID;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Stage2 model, int id)
        {
            var obj = conn.Execute("update Stage2 set [Stage2_name] = @stage2_name where _ID = @stage2_ID", new { categoryID = id, categoryName = model.stage2_name });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<Stage2>("select * from Stage2 where stage2_ID = @stage2_ID", new { stage2_ID = id });

            if (obj != null)
            {
                Stage2 model = new Stage2();
                model.stage2_ID = obj.FirstOrDefault().stage2_ID;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Stage2 model, int id)
        {
            var obj = conn.Execute("delete from Stage2 where stage2_ID = @stage2_ID", new { stage2_ID = id });

            return RedirectToAction("list");
        }

    }
}