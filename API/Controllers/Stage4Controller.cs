using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Web.Mvc;
using API.Models;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace NorgesEnergi.Controllers
{
    /**
    *The main classname controller
    *Contains all methods regarding this database table
    */

    public class Stage4Controller : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();

        /**
        * Uses GetAll method to find all rows of stage4 in database
        * if the GetAll methods find any records, each record is build and stored in list
        * 
        * @return View(result) - returns the list of all records in a view
        */
        public ActionResult List()
        {
            var obj = GetAll();
            List<stage4> result = new List<stage4>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    stage4 model = new stage4();
                    model.stage4_ID = row.stage4_ID;
                    model.stage4_name = row.stage4_name;
                    model.helptext_ID = row.helptext_ID;
                    result.Add(model);
                }
            }
            return View(result);
        }

        /**
         * Get all values from table stage4 ordered by stage4_ID
         * Values are inserted to list
         * 
         * @return obj - returns the variable which stores the values in a list
        */
        public List<stage4> GetAll()
        {
            var obj = conn.Query<stage4>("Select * FROM Stage4").OrderByDescending(u => u.stage4_ID).Take(10).ToList();
            return obj;
        }

        /**
        * Dependency method for create method
        * @return view - returns the view for creating new stage4 data
        */
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /**
        * Creates a new row in the database in stage4 table
        * Execution in database using Dapper
        * 
        * @param stage4 model - the model that is being created. Values are filled in using a view 
        * related to this method. 
        * @return redirectToAction(“list”); - returns the user to given action
        */
        [HttpPost]
        public ActionResult Create(stage4 model)
        {
            var obj = InsertStage4(model);
            return RedirectToAction("list");
        }

        /**
         * Method to give the stage4 an helptext_ID foreign key value
         * 
         * @param stage4 model - the model that is being used
         * @return if the values created is validated, insert is true. If values are not real, false is returned
         */
        public bool InsertStage4(stage4 model)
        {
            int rowsAffected = conn.Execute("INSERT INTO Stage4([stage4_name], [helptext_ID]) VALUES (@name, @helptextID)", new { name = model.stage4_name, helptextID = model.helptext_ID });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        /**
        * Reads values from database in stage4 table based on ID
        * Execution in database using Dapper
        *
        * @param int id - builds the model based on id value
        * @return view - return the view to show user the models values
        */
        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<stage4>("SELECT * from Stage4 WHERE stage4_ID =  @stage4_ID", new { stage4_ID = id });

            if (obj != null)
            {
                stage4 model = new stage4();
                model.stage4_ID = obj.FirstOrDefault().stage4_ID;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        /**
        * Builds a stage4 model based on ID value 
        * Execution in database using Dapper
        * Builds a stage4 model to show values to user in view 
        * 
        * @param int id - model with the given ID value, if exists, is build 
        * @return View - returns the view with the values of the model with the given ID value
        */
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<stage4>("SELECT * from Stage4 WHERE stage4_ID = @stage4_ID", new { stage4_ID = id });

            if (obj != null)
            {
                stage4 model = new stage4();
                model.stage4_ID = obj.FirstOrDefault().stage4_ID;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        /**
        * Edits a row in the database in stage4 table based on ID
        * Execution in database using Dapper
        * Can edit the name of the stage and the helptext_ID foregin key
        * 
        * @param classname model - the model that is being updated
        * @param int id - model with the given ID value, if exists, is being updated
        * @return redirectToAction(“list - returns the user to given action
        */
        [HttpPost]
        public ActionResult Edit(stage4 model, int id)
        {
            var obj = conn.Execute("UPDATE Stage4 set [stage4_name] = @name, [helptext_ID] = @helpID WHERE stage4_ID = @stage4_ID", new { stage4_ID = id, name = model.stage4_name, helpID = model.helptext_ID });

            return RedirectToAction("list");
        }

        /**
        * Builds a stage4 model based on ID value 
        * Execution in database using Dapper
        * Builds a stage4 model to show values to user in view
        * 
        * @param int id - model with the given ID value, if exists, is build
        * @return view - returns the view with the values of the model with the given ID value  
        */
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<stage4>("SELECT * from Stage4 WHERE stage4_ID = @stage4_ID", new { Stage4_ID = id });

            if (obj != null)
            {
                stage4 model = new stage4();
                model.stage4_ID = obj.FirstOrDefault().stage4_ID;
                model.stage4_name = obj.FirstOrDefault().stage4_name;
                model.helptext_ID = obj.FirstOrDefault().helptext_ID;
                return View(model);
            }
            return View();
        }

        /**
        * Deletes a row in the database in stage4 table based on ID
        * Execution in database using Dapper
        * 
        * @param stage4 model - the model that is being deleted
        * @param int id - model with the given ID value, if exists, is being deleted 
        * @return redirectToAction(list) - returns the user to given action
        */
        [HttpPost]
        public ActionResult Delete(stage4 model, int id)
        {
            var obj = conn.Execute("DELETE from Stage4 WHERE Stage4_ID = @Stage4_ID", new { Stage4_ID = id });

            return RedirectToAction("list");
        }

        /**
        * Dependency method for create method
        * @return view - returns the view for creating new classname data
        */
        [HttpGet]
        public ActionResult CreateStage4()
        {
            PopulateHelpDropDownList();
            return View();
        }


        /**
        * Creates a new row in the database in satge4 table
        * Uses the PopulateHelpDropDownList-method to select a stage4 from a dropdownlist
        * A row in the helptext table is also created and put as a foreign key in the stage4 row
        * 
        * @param stage4 stage - the model that is being created. Values are filled in using a view 
        * related to this method. 
        * @return redirectToAction(fulllist); - returns the user to given action
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStage4([Bind(Include = "stage4_ID, stage4_name, helptext_ID, helptext_header, helptext_short, helptext_long")] stage4 stage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.stage4.Add(stage);
                    db.SaveChanges();
                    return RedirectToAction("FullList");
                }
            }
            catch (RetryLimitExceededException /*dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateHelpDropDownList(stage.helptext_ID);
            return View(stage);
        }        

        /**
         * Method for populating a dropdownlist of stage4_name to be used in other methods and views
         */
        private void PopulateHelpDropDownList(object selecthelp = null)
        {
            var helptext = from h in db.helptext
                           orderby h.helptext_header
                           select h;
            ViewBag.helptext_ID = new SelectList(helptext, "helptext_ID", "helptext_header", selecthelp);
        }

    }
}