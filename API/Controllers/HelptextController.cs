using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using API.Models;
using System.Web.Mvc;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;


/**
*The Helptext controller
*Contains all methods regarding this database table
*/

namespace NorgesEnergi.Controllers
{
    public class HelptextController : Controller
    {
            // Setting up the connectionstring and call it : "conn" 
            // We will use the string at all plases we want to connect to the Database.

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();

        /**
        * Uses GetAll method to find all rows of helptext in database
        * if the GetAll methods find any records, each record is build and stored in list
        * 
        * @return View(“result”) - returns the list of all records in a view
        */

        public ActionResult List()
        {
            var obj = GetAll();
            List<helptext> result = new List<helptext>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    helptext model = new helptext();
                    model.helptext_ID = row.helptext_ID;
                    model.helptext_header = row.helptext_header;
                    model.helptext_short = row.helptext_short;
                    model.helptext_long = row.helptext_long;
                    result.Add(model);
                }
            }
            return View(result);
        }

        /**
        * Get all values from table helptext ordered by helptext header    
        * Values are inserted to list
        * 
        * @return variable name - returns the variable which stores the values in a list
        */

        public List<helptext> GetAll()
        {
            var obj = conn.Query<helptext>("Select * FROM helptext").OrderByDescending(u => u.helptext_header).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

       
        /**
        * Creates a new row in the database in classname table
        * Execution in database using Dapper
        * 
        * @param Helptext model - the model that is being created. Values are filled in using a view 
        * related to this method. 
        * @return redirectToAction(“List”); - returns the user to given action
        */

        [HttpPost]
        public ActionResult Create(helptext model)
        {
            var obj = InsertHelptext(model);
            return RedirectToAction("List");
        }

        
        public bool InsertHelptext(helptext model)
        {
            int rowsAffected = conn.Execute("INSERT INTO helptext([helptext_header], [helptext_short], [helptext_long]) VALUES (@header, @text_short, @text_long)", new { header = model.helptext_header, text_short = model.helptext_short, text_long = model.helptext_long });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        /**
        * Reads values from database in classname table based on ID
        *
        * @param int id - builds the model based on id value
        * @return view - return the view to show user the models values
        */

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<helptext>("SELECT * FROM helptext WHERE helptext_ID =  @text_ID", new { text_ID = id });

            if (obj != null)
            {
                helptext model = new helptext();
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                model.helptext_header = obj.FirstOrDefault().helptext_header;
                model.helptext_short = obj.FirstOrDefault().helptext_short;
                model.helptext_long = obj.FirstOrDefault().helptext_long;
                return View(model);
            }
            return View();
        }

        /**
        * Builds a classname model based on ID value 
        * Execution in database using Dapper
        * Builds a classname model to show values to user in view 
        * 
        * @param int ID - model with the given ID value, if exists, is build 
        * @return View - returns the view with the values of the model with the given ID value
        */

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<InfoViewModel>("SELECT * FROM helptext WHERE helptext_ID = @textID", new { textID = id });

            if (obj != null)
            {
                InfoViewModel model = new InfoViewModel();
                model.Helptext_ID = obj.FirstOrDefault().Helptext_ID;
                model.Helptext_header = obj.FirstOrDefault().Helptext_header;
                model.Helptext_short = obj.FirstOrDefault().Helptext_short;
                model.Helptext_long = obj.FirstOrDefault().Helptext_long;
                return View(model);
            }
            return View();
        }

        /**
        * Edits a row in the database in classname table based on ID
        * Execution in database using Dapper
        * 
        * @param InfoViewModel model - the model that is being updated
        * @param int ID - model with the given ID value, if exists, is being updated
        * @return redirectToAction(“list”) - returns the user to given action
        */

        [HttpPost]
        public ActionResult Edit(InfoViewModel model, int id)
        {
            var obj = conn.Execute("UPDATE helptext SET [helptext_header] = @header, [helptext_short] = @text_short, [helptext_long] = @text_long WHERE helptext_ID = @helpID", new { helpID = id, header = model.Helptext_header, text_short = model.Helptext_short, text_long = model.Helptext_long });

            return RedirectToAction("list");
        }


        /**
        * Builds a classname model based on ID value 
        * Execution in database using Dapper
        * Builds a classname model to show values to user in view
        * 
        * @param int id - model with the given ID value, if exists, is build
        * @return view - returns the view with the values of the model with the given ID value 
        */

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<helptext>("SELECT * FROM helptext WHERE helptext_ID = @textID", new { textID = id });

            if (obj != null)
            {
                helptext model = new helptext();
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                model.helptext_header = obj.FirstOrDefault().helptext_header;
                model.helptext_short = obj.FirstOrDefault().helptext_short;
                model.helptext_long = obj.FirstOrDefault().helptext_long;
                return View(model);
            }
            return View();
        }

        /**
        * Deletes a row in the database in classname table based on ID
        * Execution in database using Dapper
        * 
        * @param helptext model - the model that is being deleted
        * @param int id - model with the given ID value, if exists, is being deleted 
        * @return redirectToAction("list"); - returns the user to given action
        */


        [HttpPost]
        public ActionResult Delete(helptext model, int id)
        {
            var obj = conn.Execute("DELETE FROM helptext WHERE helptext_ID = @textID", new { textID = id });

            return RedirectToAction("list");
        }

        public ActionResult CreateHelptext()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHelptext([Bind(Include = "helptext_ID, helptext_header, helptext_short, helptext_long")]helptext help)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.helptext.Add(help);
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(help);
        }

        public ActionResult AddTags()
        {
            Norges_EnergiEntities db = new Norges_EnergiEntities();
            return View();
        }

        [HttpPost]
        public ActionResult AddTags(HelpTagsViewModel model)
        {
            try
            {
                Norges_EnergiEntities db = new Norges_EnergiEntities();

                helptext help = new helptext();
                help.helptext_ID = model.helptext_ID;
                help.helptext_header = model.helptext_header;
                help.helptext_short = model.helptext_short;
                help.helptext_long = model.helptext_long;
                
                metatag tag = new metatag();
                tag.metatag_ID = model.tag_ID;
                tag.tag = model.tag;
                
                helptexttag htag = new helptexttag();
                htag.helptext_ID = help.helptext_ID;
                htag.metatag_ID = tag.metatag_ID;

                db.helptext.Add(help);
                db.metatag.Add(tag);
                db.helptexttag.Add(htag);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            return View(model);
        }

    }
}