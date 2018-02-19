﻿using System;
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
                    model.stage1 = row.stage1;
                    model.stage2 = row.stage2;
                    model.stage3 = row.stage3;
                    model.stage4 = row.stage4;
                    model.helptext_sum = row.helptext_sum;
                    model.helptext_full = row.helptext_full;
                    model.helptext_header = row.helptext_header;
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
            int rowsAffected = conn.Execute("INSERT INTO info ([stage1]),([stage2]),([stage3]),([stage4]),([Helptext_sum]),([helptext_full]),([helptext_header]) VALUES (@stage01), (@stage02), (@stage03), (@stage04), (@helptextsum), (@helptextfull), (@helptextheader)", new { stage01 = model.stage1, stage02 = model.stage2, stage03 = model.stage3, stage04 = model.stage4, helptextsum = model.helptext_sum, helptextfull = model.helptext_full, helptextheader = model.helptext_header });
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
                model.helptext_sum = obj.FirstOrDefault().helptext_sum;
                model.helptext_full = obj.FirstOrDefault().helptext_full;
                model.helptext_header = obj.FirstOrDefault().helptext_header;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<Info>("select * FROM info WHERE Info_ID = @Info_ID", new { info_id = id });

            if (obj != null)
            {
                Info model = new Info();
                model.info_ID = obj.FirstOrDefault().info_ID;
                model.stage1 = obj.FirstOrDefault().stage1;
                model.stage2 = obj.FirstOrDefault().stage2;
                model.stage3 = obj.FirstOrDefault().stage3;
                model.stage4 = obj.FirstOrDefault().stage4;
                model.helptext_sum = obj.FirstOrDefault().helptext_sum;
                model.helptext_full = obj.FirstOrDefault().helptext_full;
                model.helptext_header = obj.FirstOrDefault().helptext_header;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Info model, int id)
        {
            var obj = conn.Execute("update category set ([stage1]), ([stage2]), ([stage3]), ([stage4]), ([Helptext_sum]), ([helptext_full]), ([helptext_header]) VALUES (@stage01), (@stage02), (@stage03), (@stage04), (@helptextsum), (@helptextfull), (@helptextheader) where Info_ID = @InfoID", new { InfoID = id, stage01 = model.stage1, stage02 = model.stage2, stage03 = model.stage3, stage04 = model.stage4, helptextsum = model.helptext_sum, helptextfull = model.helptext_full, helptextheader = model.helptext_header });

            return RedirectToAction("list");
        }

    }
}