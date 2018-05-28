using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using API.Models;
using Dapper;

/**
*The main Helptexttag controller
*Contains all methods regarding this database table
*/
namespace NorgesEnergi.Controllers
{
    public class HelptexttagController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();

        /**
        * Uses GetAll method to find all rows of Helptexttag in database
        * if the GetAll methods find any records, each record is build and stored in list
        * 
        * @return View(“result”) - returns the list of all records in a view
        */
        public ActionResult List()
        {
            var obj = GetAll();
            List<HelpTagsViewModel> result = new List<HelpTagsViewModel>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    HelpTagsViewModel model = new HelpTagsViewModel();
                    model.helptext_ID = row.helptext_ID;
                    model.helptext_header = row.helptext_header;
                    model.helptext_short = row.helptext_short;
                    model.tag = row.tag;
                    result.Add(model);
                }
            }
            return View(result);
        }

        /**
        * Get all values from table Helptexttag ordered by helptext header
        * Values are inserted to list
        * 
        * @return variable name - returns the variable which stores the values in a list
        */
        public List<HelpTagsViewModel> GetAll()
        {
            var obj = conn.Query<HelpTagsViewModel>("SELECT helptext.helptext_header, helptext.helptext_short, metatag.tag FROM helptext INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID; ").OrderByDescending(h => h.helptext_header).ToList();
            return obj;
        }

        /**
        * Dependency method for create method
        * @return view - returns the view for creating new Helptexttag data
        */
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /**
        * Creates a new row in the database in Helptexttag table
        * Execution in database using Dapper
        * 
        * @param Helptexttag model - the model that is being created. Values are filled in using a view 
        * related to this method. 
        * @return redirectToAction(“list”); - returns the user to given action
        */
        [HttpPost]
        public ActionResult Create(helptexttag model)
        {
            return RedirectToAction("list");
        }

        /**
        * Dependency method for create method
        * @return view - returns the view for creating new Helptexttag data
        */
        public ActionResult CreateMetahelp()
        {
            PopulateHelptextDropDownList();
            PopulateTagDropDownList();
            return View();
        }

        /**
        * Creates a new row in the database in Helptexttag table
        *	
        * 
        * @param InfoViewModel model - the model that is being created. Values are filled in using a view 
        * related to this method. 
        * @return redirectToAction(“List”); - returns the user to given action
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMetahelp(InfoViewModel model)
        {
            char[] delimiterChars = { ',', '.', ':', };


            string text = model.Tag;

            string[] words = text.Split(delimiterChars);

            List<metatag> tagList = new List<metatag>();

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
                    ht.helptext_ID = model.Helptext_ID;
                    ht.metatag_ID = obj.metatag_ID;
                    db.helptexttag.Add(ht);
                    db.SaveChanges();
                }
            }

            tagList.Clear();

            PopulateHelptextDropDownList(model.Helptext_ID);
            return RedirectToAction("List");
        }
        /**
         * Method for populating a dropdownlist of helptext_headers to be used in other methods and views
         */
        private void PopulateHelptextDropDownList(object helptext = null)
        {
            var help = from h in db.helptext
                        orderby h.helptext_header
                        select h;
            ViewBag.helptext_ID = new SelectList(help, "helptext_ID", "helptext_header", helptext);
        }

        /**
        * Method for populating a dropdownlist of metatags to be used in other methods and views
        */
        private void PopulateTagDropDownList(object tag = null)
        {
            var metatag = from m in db.metatag
                        orderby m.tag
                        select m;
            ViewBag.metatag_ID = new SelectList(metatag, "metatag_ID", "tag", tag);
        }
    }
}