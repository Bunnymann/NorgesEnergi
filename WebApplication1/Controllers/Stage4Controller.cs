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
    public class Stage4Controller : Controller
    {
        /** To view the list, write /dappertest/list after localhost port
         */
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());

        public ActionResult List()
        {
            var obj = GetAll();
            List<Stage4> result = new List<Stage4>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    Stage4 model = new Stage4();
                    model.stage4_ID = row.stage4_ID;
                    model.stage4_name = row.stage4_name;
                    model.helptext_ID = row.helptext_ID;
                    result.Add(model);
                }
            }
            return View(result);
        }
        public List<Stage4> GetAll()
        {
            var obj = conn.Query<Stage4>("Select * FROM Stage4").OrderByDescending(u => u.stage4_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Stage4 model)
        {
            var obj = InsertStage4(model);
            return RedirectToAction("list");
        }
        public bool InsertStage4(Stage4 model)
        {
            int rowsAffected = conn.Execute("INSERT INTO Stage4([stage4_name], [helptext_ID]) VALUES (@name, @helptextID)", new { name = model.stage4_name, helptextID = model.helptext_ID });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<Stage4>("SELECT * from Stage4 WHERE stage4_ID =  @stage4_ID", new { stage4_ID = id });

            if (obj != null)
            {
                Stage4 model = new Stage4();
                model.stage4_ID = obj.FirstOrDefault().stage4_ID;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<Stage4>("SELECT * from Stage4 WHERE stage4_ID = @stage4_ID", new { stage4_ID = id });

            if (obj != null)
            {
                Stage4 model = new Stage4();
                model.stage4_ID = obj.FirstOrDefault().stage4_ID;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Stage4 model, int id)
        {
            var obj = conn.Execute("UPDATE Stage4 set [stage4_name] = @name, [helptext_ID] = @helpID WHERE stage4_ID = @stage4_ID", new { stage4_ID = id, name = model.stage4_name, helpID = model.helptext_ID });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<Stage4>("SELECT * from Stage4 WHERE stage4_ID = @stage4_ID", new { Stage4_ID = id });

            if (obj != null)
            {
                Stage4 model = new Stage4();
                model.stage4_ID = obj.FirstOrDefault().stage4_ID;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Stage4 model, int id)
        {
            var obj = conn.Execute("DELETE from Stage4 WHERE Stage4_ID = @Stage4_ID", new { Stage4_ID = id });

            return RedirectToAction("list");
        }

    }
}