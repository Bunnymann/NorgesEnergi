using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Web.Mvc;
using API.Models;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace WebApplication1.Controllers
{
    public class InfoController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();

        public ActionResult DDLSearchStage3()
        {
            Norges_EnergiEntities stages = new Norges_EnergiEntities();
            return View(stages.stage3.ToList());
        }

        public List<InfoViewModel> GetFullList()
        {
            //sql query does NOT ask for metatag.tag
            //method getTags select tags
            var obj = conn.Query<InfoViewModel>("SELECT info.info_ID, stage1.stage1_name, stage2.stage2_name, stage3.stage3_name, stage4.stage4_name, helptext.helptext_header, helptext.helptext_short, helptext.helptext_long FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID;" /*INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID;"*/).OrderByDescending(u => u.stage1_name).ToList();

            return obj;
        }

        public ActionResult FullList()
        {
            var obj = GetFullList();
            List<InfoViewModel> result = new List<InfoViewModel>();
            List<string> metatag = new List<string>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    InfoViewModel model = new InfoViewModel();
                    model.info_ID = row.info_ID;
                    model.stage1_name = row.stage1_name;
                    model.stage2_name = row.stage2_name;
                    model.stage3_name = row.stage3_name;
                    model.stage4_name = row.stage4_name;
                    model.helptext_header = row.helptext_header;
                    model.helptext_short = row.helptext_short;
                    model.helptext_long = row.helptext_long;
                    model.tag = GetTags(row.info_ID);
                    result.Add(model);
                }
            }
            return View(result);
        }
        
        public ActionResult FullList_Admin()
        {
            Norges_EnergiEntities stages = new Norges_EnergiEntities();
            var obj = GetFullList();
            List<InfoViewModel> result = new List<InfoViewModel>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    InfoViewModel model = new InfoViewModel();
                    model.info_ID = row.info_ID;
                    model.stage1_name = row.stage1_name;
                    model.stage2_name = row.stage2_name;
                    model.stage3_name = row.stage3_name;
                    model.stage4_name = row.stage4_name;
                    model.helptext_header = row.helptext_header;
                    model.helptext_short = row.helptext_short;
                    model.helptext_long = row.helptext_long;
                    model.tag = GetTags(row.info_ID);

                    result.Add(model);
                }
            }
            return View(result);
        }

        public ActionResult testCreate()
        {
            PopulateStage1DropDownList();
            PopulateStage2DropDownList();
            PopulateStage3DropDownList();
            PopulateStage4DropDownList();
            return View();
        }
        [HttpPost]
        public ActionResult testcreate(InfoViewModel model)
        {
            char[] delimiterChars = { ',', '.', ':', };

            string text = model.tag;

            string[] words = text.Split(delimiterChars);

            List<metatag> tagList = new List<metatag>();

            helptext help = new helptext
            {
                helptext_ID = model.helptext_ID,
                helptext_header = model.helptext_header,
                helptext_short = model.helptext_short,
                helptext_long = model.helptext_long
            };

            stage4 s4 = new stage4()
            {
                stage4_ID = model.stage4_ID,
                stage4_name = model.stage4_name,
                helptext_ID = model.helptext_ID
            };

            info info = new info
            {
                stage1_ID = model.stage1_ID,
                stage2_ID = model.stage2_ID,
                stage3_ID = model.stage3_ID,
                stage4_ID = s4.stage4_ID
            };

            foreach (var word in words)
            {
                metatag tag = new metatag();
                {
                    tag.tag = word;
                    db.metatag.Add(tag);
                    tagList.Add(tag);
                    db.SaveChanges();
                }
            }

            foreach (var obj in tagList)
            {
                helptexttag ht = new helptexttag();
                {
                    ht.helptext_ID = help.helptext_ID;
                    ht.metatag_ID = obj.metatag_ID;
                    db.helptexttag.Add(ht);
                }
            }

            db.stage4.Add(s4);
            db.info.Add(info);
            db.helptext.Add(help);
            db.SaveChanges();
            tagList.Clear();

            return RedirectToAction("FullList");
        }

        private void PopulateStage1DropDownList(object stage1 = null)
        {
            var stage = from s in db.stage1
                        orderby s.stage1_name
                        select s;
            ViewBag.stage1_ID = new SelectList(stage, "stage1_ID", "stage1_name", stage1);
        }

        private void PopulateStage2DropDownList(object stage2 = null)
        {
            var stage = from s in db.stage2
                        orderby s.stage2_name
                        select s;
            ViewBag.stage2_ID = new SelectList(stage, "stage2_ID", "stage2_name", stage2);
        }

        private void PopulateStage3DropDownList(object stage3 = null)
        {
            var stage = from s in db.stage3
                        orderby s.stage3_name
                        select s;
            ViewBag.stage3_ID = new SelectList(stage, "stage3_ID", "stage3_name", stage3);
        }

        private void PopulateStage4DropDownList(object stage4 = null)
        {
            var stage = from s in db.stage4
                        orderby s.stage4_name
                        select s;
            ViewBag.stage4_ID = new SelectList(stage, "stage4_ID", "stage4_name", stage4);
        }

        [HttpGet]
        public ActionResult DeleteInfo(int id)
        {
            var obj = conn.Query<InfoViewModel>("SELECT info.info_ID, stage1.stage1_name, stage2.stage2_name, stage3.stage3_name, stage4.stage4_name, helptext.helptext_ID, helptext.helptext_header, helptext.helptext_short, helptext.helptext_long FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID WHERE info.info_ID = @infoID;", new { infoID = id });

            if (obj != null)
            {
                InfoViewModel model = new InfoViewModel();
                model.info_ID = obj.FirstOrDefault().info_ID;
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                model.stage3_name = obj.FirstOrDefault().stage3_name;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                model.helptext_header = obj.FirstOrDefault().helptext_header;
                model.helptext_short = obj.FirstOrDefault().helptext_short;
                model.helptext_long = obj.FirstOrDefault().helptext_long;
                model.tag = GetTags(model.info_ID);
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteInfo(info model, int id)
        {
            var obj = conn.Execute("DELETE FROM info WHERE info_ID = @infoID", new { infoID = id });

            return RedirectToAction("FullList");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<InfoViewModel>("SELECT info.info_ID, stage1.stage1_name, stage2.stage2_name, stage3.stage3_name, stage4.stage4_name, helptext.helptext_header, helptext.helptext_short, helptext.helptext_long, metatag.tag FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID WHERE info.info_ID = @info;", new { info = id });

            if (obj != null)
            {
                InfoViewModel model = new InfoViewModel();
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                model.stage3_name = obj.FirstOrDefault().stage3_name;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
                model.helptext_header = obj.FirstOrDefault().helptext_header;
                model.helptext_short = obj.FirstOrDefault().helptext_short;
                model.helptext_long = obj.FirstOrDefault().helptext_long;
                model.tag = obj.FirstOrDefault().tag;
                return View(model);
            }
            return View();
        }

        /*
        [HttpPost]
        public ActionResult Edit(InfoViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Info info = new Info()
            {
                stage
            }
            //var infoUpdate = conn.Execute("UPDATE info set [stage1_ID] = @s1, [stage2_ID] = @s2, [stage3_ID] = @s3, [stage4_ID] = @s4 WHERE info_ID = @info;", new { info = id, s1 = model.stage1_ID, s2 = model.stage2_ID, s3 = model.stage3_ID, s4 = model.stage4_ID });
            var helptextUpdate = conn.Execute("Update helptext set [helptext_header] = @headertxt, [helptext_short] = @shorttxt, [helptext_long] = @longtxt WHERE helptext_ID = @helpID;", new { helpID = model.helptext_ID, headertxt = model.helptext_header, shorttxt = model.helptext_short, longtxt = model.helptext_long });
            //var infoUpdate2 = conn.Execute("Update InfoViewModel set [stage1_name] = @name1, [stage2_name] = @name2, [stage3_name] = @name3, [stage4_name] = @name4, [helptext_header] = @headertxt, [helptext_short] = @shorttxt, [helptext_long] = @longtxt, [tag] = @metatag FROM InfoViewModel WHERE info_ID = @info", new { info = id, name1 = model.stage1_name, name2 = model.stage2_name, name3 = model.stage3_name, name4 = model.stage4_name, headertxt = model.helptext_header, shorttxt = model.helptext_short, longtxt = model.helptext_long, metatag = model.tag });
            return RedirectToAction("FullList");
        }*/
        [HttpGet]
        public string GetTags(int id)
        {
            List<string> tags = new List<string>();
            var obj = conn.Query<metatag>("SELECT metatag.tag FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID where info_ID = @infoID;", new { infoID = id });
            
            if (obj != null)
            {
                foreach(var tag in obj)
                {

                    tags.Add(tag.tag.ToString());
                }
            }
            string text = string.Join(", ", tags);
            return text;
        }
        


}
}
