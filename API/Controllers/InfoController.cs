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

namespace NorgesEnergi.Controllers
{
    public class InfoController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();
        
        /**
        * Get all values from model InfoViewModel ordered by Stage1 name.
        * Values are inserted to list
        * 
        * @return obj - returns the variable which stores the values in a list
        */
        public List<InfoViewModel> GetFullList()
        {
            var obj = conn.Query<InfoViewModel>("SELECT info.info_ID, stage1.stage1_name, stage2.stage2_name, stage3.stage3_name, stage4.stage4_name, helptext.helptext_ID, helptext.helptext_header, helptext.helptext_short, helptext.helptext_long FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID;" /*INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID;"*/).OrderByDescending(u => u.Stage1_name).ToList();

            return obj;
        }

        /**
        * Uses GetAll method to find all rows of everything in the database using the InfoViewModel
        * if the GetFullList methods find any records, each record is build and stored in list
        * 
        * @return View(result) - returns the list of all records in a view
        */
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
                    model.Info_ID = row.Info_ID;
                    model.Stage1_name = row.Stage1_name;
                    model.Stage2_name = row.Stage2_name;
                    model.Stage3_name = row.Stage3_name;
                    model.Stage4_name = row.Stage4_name;
                    model.Helptext_header = row.Helptext_header;
                    model.Helptext_short = row.Helptext_short;
                    model.Helptext_long = row.Helptext_long;
                    model.Tag = GetTags(row.Info_ID);
                    result.Add(model);
                }
            }
            return View(result);
        }

        /**
        * Uses GetAll method to find all rows of everything in the database using the InfoViewModel
        * if the FullList_Admin methods find any records, each record is build and stored in list
        * 
        * @return View(result) - returns the list of all records in a view
        */
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
                    model.Info_ID = row.Info_ID;
                    model.Stage1_name = row.Stage1_name;
                    model.Stage2_name = row.Stage2_name;
                    model.Stage3_name = row.Stage3_name;
                    model.Stage4_name = row.Stage4_name;
                    model.Helptext_ID = row.Helptext_ID;
                    model.Helptext_header = row.Helptext_header;
                    model.Helptext_short = row.Helptext_short;
                    model.Helptext_long = row.Helptext_long;
                    model.Tag = GetTags(row.Info_ID);

                    result.Add(model);
                }
            }
            return View(result);
        }

        /**
        * Dependency method for create method
        * @return view - returns the view for creating new classname data
        */
        [HttpGet]
        public ActionResult CreateHelptext()
        {
            PopulateStage1DropDownList();
            PopulateStage2DropDownList();
            PopulateStage3DropDownList();
            PopulateStage4DropDownList();
            return View();
        }

        /**
        * Creates a new row in the database for all tables using the InfoViewModel
        * Execution in database using Dapper
        * 
        * @param InfoViewModel model - the model that is being created. Values are filled in using a view 
        * related to this method. 
        * @return redirectToAction(“FullList”); - returns the user to given action
        */
        [HttpPost]
        public ActionResult CreateHelptext(InfoViewModel model)
        {
            char[] delimiterChars = { ',', '.', ':', };

            string text = model.Tag;

            //If no text is put in the tags textbox, error occurs
            //Needs code to handle error
            string[] words = text.Split(delimiterChars);

            List<metatag> tagList = new List<metatag>();

            helptext help = new helptext
            {
                helptext_ID = model.Helptext_ID,
                helptext_header = model.Helptext_header,
                helptext_short = model.Helptext_short,
                helptext_long = model.Helptext_long
            };

            stage4 s4 = new stage4()
            {
                stage4_ID = model.Stage4_ID,
                stage4_name = model.Stage4_name,
                helptext_ID = model.Helptext_ID
            };

            info info = new info
            {
                stage1_ID = model.Stage1_ID,
                stage2_ID = model.Stage2_ID,
                stage3_ID = model.Stage3_ID,
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

        /**
         * Populating the dropdown list with all objects from stage1. 
         */
        private void PopulateStage1DropDownList(object stage1 = null)
        {
            var stage = from s in db.stage1
                        orderby s.stage1_name
                        select s;
            ViewBag.stage1_ID = new SelectList(stage, "Stage1_ID", "Stage1_name", stage1);
        }

        /**
         * Populating the dropdown list with all objects from stage2. 
         */
        private void PopulateStage2DropDownList(object stage2 = null)
        {
            var stage = from s in db.stage2
                        orderby s.stage2_name
                        select s;
            ViewBag.stage2_ID = new SelectList(stage, "Stage2_ID", "Stage2_name", stage2);
        }

        /**
         * Populating the dropdown list with all objects from stage3. 
         */
        private void PopulateStage3DropDownList(object stage3 = null)
        {
            var stage = from s in db.stage3
                        orderby s.stage3_name
                        select s;
            ViewBag.stage3_ID = new SelectList(stage, "Stage3_ID", "Stage3_name", stage3);
        }

        /**
         * Populating the dropdown list with all objects from stage4. 
         */
        private void PopulateStage4DropDownList(object stage4 = null)
        {
            var stage = from s in db.stage4
                        orderby s.stage4_name
                        select s;
            ViewBag.stage4_ID = new SelectList(stage, "Stage4_ID", "Stage4_name", stage4);
        }

        /**
        * Builds a DeleteInfo model based on ID value 
        * Execution in database using Dapper
        * Builds a classname model to show values to user in view
        * 
        * @param int id - model with the given ID value, if exists, is build
        * @return view - returns the view with the values of the model with the given ID value  
        */
        [HttpGet]
        public ActionResult DeleteInfo(int id)
        {
            var obj = conn.Query<InfoViewModel>("SELECT info.info_ID, stage1.stage1_name, stage2.stage2_name, stage3.stage3_name, stage4.stage4_name, helptext.helptext_ID, helptext.helptext_header, helptext.helptext_short, helptext.helptext_long FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID WHERE info.info_ID = @infoID;", new { infoID = id });

            if (obj != null)
            {
                InfoViewModel model = new InfoViewModel();
                model.Info_ID = obj.FirstOrDefault().Info_ID;
                model.Stage1_name = obj.FirstOrDefault().Stage1_name;
                model.Stage2_name = obj.FirstOrDefault().Stage2_name;
                model.Stage3_name = obj.FirstOrDefault().Stage3_name;
                model.Stage4_name = obj.FirstOrDefault().Stage4_name;
                model.Helptext_ID = obj.FirstOrDefault().Helptext_ID;
                model.Helptext_header = obj.FirstOrDefault().Helptext_header;
                model.Helptext_short = obj.FirstOrDefault().Helptext_short;
                model.Helptext_long = obj.FirstOrDefault().Helptext_long;
                model.Tag = GetTags(model.Info_ID);
                return View(model);
            }
            return View();
        }

        /**
        * Deletes a row in the database in classname table based on ID
        * Execution in database using Dapper
        * 
        * @param Info model - the model that is being deleted
        * @param int id - model with the given ID value, if exists, is being deleted 
        * @return redirectToAction(“FullList”) - returns the user to given action
        */
        [HttpPost]
        public ActionResult DeleteInfo(info model, int id)
        {
            var obj = conn.Execute("DELETE FROM info WHERE info_ID = @infoID", new { infoID = id });

            return RedirectToAction("FullList");
        }

        /**
        * Builds a Edit model based on ID value 
        * Execution in database using Dapper
        * Builds a classname model to show values to user in view 
        * 
        * @param int id - model with the given ID value, if exists, is build 
        * @return View - returns the view with the values of the model with the given ID value
        */
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<InfoViewModel>("SELECT info.info_ID, stage1.stage1_name, stage2.stage2_name, stage3.stage3_name, stage4.stage4_name, helptext.helptext_header, helptext.helptext_short, helptext.helptext_long FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID WHERE helptext.helptext_ID = @help; ", new { help = id });

            if (obj != null)
            {
                InfoViewModel model = new InfoViewModel();
                model.Stage1_name = obj.FirstOrDefault().Stage1_name;
                model.Stage2_name = obj.FirstOrDefault().Stage2_name;
                model.Stage3_name = obj.FirstOrDefault().Stage3_name;
                model.Stage4_name = obj.FirstOrDefault().Stage4_name;
                model.Helptext_header = obj.FirstOrDefault().Helptext_header;
                model.Helptext_short = obj.FirstOrDefault().Helptext_short;
                model.Helptext_long = obj.FirstOrDefault().Helptext_long;
                model.Tag = GetTags(obj.FirstOrDefault().Info_ID);
                return View(model);
            }
            return View();
        }

        /**
        * Edits a row in the database in helptext table based on ID
        * Execution in database using Dapper
        * 
        * @param InfoViewModel model - the model that is being updated
        * @param int id - model with the given ID value, if exists, is being updated
        * @return redirectToAction(“FullList”) - returns the user to given action
        */
        [HttpPost]
        public ActionResult Edit(InfoViewModel model, int id)
        
        {
            var obj = conn.Execute("UPDATE helptext SET [helptext_header] = @header, [helptext_short] = @text_short, [helptext_long] = @text_long WHERE helptext_ID = @help_ID", new { help_ID = id, header = model.Helptext_header, text_short = model.Helptext_short, text_long = model.Helptext_long });
            return RedirectToAction("FullList");
        }

        /*
        * Selects metatags from database based on info_ID value
        * Execution in database using Dapper
        * Using inner joins in SQL-query to find correct values
        * If method finds multiple values, each metatag is put in a list and later the list is made as a string
        * 
        * @param int id - the info_ID value for a helptext_ID value
        * @return text - the list of all found metatags join as a string
        */
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
