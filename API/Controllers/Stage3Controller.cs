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

namespace NorgesEnergi.Controllers
{
    /**
    *The main Stage3 controller
    *Contains all methods regarding this database table
    */

    public class stage3Controller : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();

        /**
        * Uses GetAll method to find all rows of Stage3 in database
        * if the GetAll methods find any records, each record is build and stored in list
        * 
        * @return View(result) - returns the list of all records in a view
        */
        public ActionResult List()
        {
            var obj = GetAll();
            List<stage3> result = new List<stage3>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    stage3 model = new stage3();
                    model.stage3_ID = row.stage3_ID;
                    model.stage3_name = row.stage3_name;
                    result.Add(model);
                }
            }
            return View(result);
        }

        /**
        * Get all values from table Stage3 ordered by tablecolumn
        * Values are inserted to list
        * 
        * @return variable name - returns the variable which stores the values in a list
        */
        public List<stage3> GetAll()
        {
            var obj = conn.Query<stage3>("SELECT * FROM stage3").OrderByDescending(u => u.stage3_ID).Take(10).ToList();
            return obj;
        }

        /**
        * Dependency method for create method
        * @return view - returns the view for creating new Stage3 data
        */
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /**
        * Creates a new row in the database in Stage3 table
        * 
        * @param Stage3 model - the model that is being created. Values are filled in using a view 
        * related to this method. 
        * @return redirectToAction(“list”); - returns the user to given action
        */
        [HttpPost]
        public ActionResult Create(stage3 model)
        {
            return RedirectToAction("list");
        }

        /**
        * Reads values from database in Stage3 table based on ID
        * Execution in database using Dapper
        *
        * @param int id - builds the model based on id value
        * @return view - return the view to show user the models values
        */
        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<stage3>("SELECT * from Stage3 WHERE Stage3_ID = @stage3ID", new { stage3ID = id });

            if(obj != null)
            {
                stage3 model = new stage3();
                    model.stage3_ID = obj.FirstOrDefault().stage3_ID;
                    model.stage3_name = obj.FirstOrDefault().stage3_name;
                    return View(model);                
            }
            return View();
        }

        /**
        * Builds a Stage3 model based on ID value 
        * Execution in database using Dapper
        * Builds a Stage3 model to show values to user in view 
        * 
        * @param int id - model with the given ID value, if exists, is build 
        * @return View - returns the view with the values of the model with the given ID value
        */
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<stage3>("SELECT * from Stage3 WHERE Stage3_ID = @stage3ID", new { stage3ID = id });

            if (obj != null)
            {
                stage3 model = new stage3();
                model.stage3_ID = obj.FirstOrDefault().stage3_ID;
                model.stage3_name = obj.FirstOrDefault().stage3_name;
                return View(model);
            }
            return View();
        }

        /**
        * Edits a row in the database in Stage3 table based on ID
        * Execution in database using Dapper
        * 
        * @param Stage3 model - the model that is being updated
        * @param int id - model with the given ID value, if exists, is being updated
        * @return redirectToAction(“list”) - returns the user to given action
        */
        [HttpPost]
        public ActionResult Edit(stage3 model, int id)
        {
            var obj = conn.Execute("UPDATE stage3 set [Stage3_name] = @stage3_Name WHERE stage3_ID = @stage3ID", new { stage3ID = id, stage3_Name = model.stage3_name });

            return RedirectToAction("list");
        }

        /**
        * Builds a Stage3 model based on ID value 
        * Execution in database using Dapper
        * Builds a Stage3 model to show values to user in view
        * 
        * @param int id - model with the given ID value, if exists, is build
        * @return view - returns the view with the values of the model with the given ID value  
        */
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<stage3>("SELECT * from Stage3 WHERE stage3_ID = @stage3ID", new {stage3ID = id });

            if (obj != null)
            {
                stage3 model = new stage3();
                model.stage3_ID = obj.FirstOrDefault().stage3_ID;
                model.stage3_name = obj.FirstOrDefault().stage3_name;
                return View(model);
            }
            return View();
        }

        /**
        * Deletes a row in the database in Stage3 table based on ID
        * Execution in database using Dapper
        * 
        * @param Stage3 model - the model that is being deleted
        * @param int id - model with the given ID value, if exists, is being deleted 
        * @return redirectToAction(“list”) - returns the user to given action
        */
        [HttpPost]
        public ActionResult Delete(stage3 model, int id)
        {
            var obj = conn.Execute("DELETE from Stage3 WHERE stage3_ID = @stage3ID", new { stage3ID = id });

            return RedirectToAction("list");
        }

    }
}