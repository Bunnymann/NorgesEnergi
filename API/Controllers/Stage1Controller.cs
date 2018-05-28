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
    public class Stage1Controller : Controller
    {
        
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();
       
       /**
       * Uses GetAll method to find all rows of stage1 in database
       * if the GetAll methods find any records, each record is build and stored in list
       * 
       * @return View(result) - returns the list of all records in a view
       */

        public ActionResult List()
        {
            var obj = GetAll();
            List<stage1> result = new List<stage1>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    stage1 model = new stage1();
                    model.stage1_ID = row.stage1_ID;
                    model.stage1_name = row.stage1_name;
                    result.Add(model);
                }
            }
            return View(result);
        }

        /**
        * Get all values from table stage1 ordered by stage1_ID
        * Values are inserted to list
        * 
        * @return obj - returns the variable which stores the values in a list
        */
        public List<stage1> GetAll()
        {
            var obj = conn.Query<stage1>("Select * FROM stage1").OrderByDescending(u => u.stage1_ID).Take(10).ToList();
            return obj;
        }

        /**
        * Dependency method for create method
        * @return view - returns the view for creating new stage1 data
        */

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /**
        * Creates a new row in the database in stage1 table
        * Execution in database using Dapper
        * 
        * @param stage1 model - the model that is being created. Values are filled in using a view 
        * related to this method. 
        * @return redirectToAction(“list”); - returns the user to given action
        */

        [HttpPost]
        public ActionResult Create(stage1 model)
        {
            return RedirectToAction("list");
        }

        /**
        * Reads values from database in stage1 table based on ID
        * Execution in database using Dapper
        *
        * @param int id - builds the model based on id value
        * @return view - return the view to show user the models values
        */
        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<stage1>("SELECT * from stage1 WHERE stage1_ID =  @stage1_ID", new { stage1_ID = id });

            if (obj != null)
            {
                stage1 model = new stage1();
                model.stage1_ID = obj.FirstOrDefault().stage1_ID;
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                return View(model);
            }
            return View();
        }

        /**
       * Builds a stage1 model based on ID value 
       * Execution in database using Dapper
       * Builds a stage1 model to show values to user in view 
       * 
       * @param int id - model with the given ID value, if exists, is build 
       * @return View - returns the view with the values of the model with the given ID value
       */
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<stage1>("SELECT * from stage1 WHERE stage1_ID = @stage1_ID", new { stage1_ID = id });

            if (obj != null)
            {
                stage1 model = new stage1();
                model.stage1_ID = obj.FirstOrDefault().stage1_ID;
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                return View(model);
            }
            return View();
        }

        /**
        * Edits a row in the database in stage1 table based on ID
        * Execution in database using Dapper
        * 
        * @param classname model - the model that is being updated
        * @param int id - model with the given ID value, if exists, is being updated
        * @return redirectToAction(“list - returns the user to given action
        */
        [HttpPost]
        public ActionResult Edit(stage1 model, int id)
        {
            var obj = conn.Execute("UPDATE stage1 set [stage1_name] = @stage1Name WHERE stage1_ID = @stage1_ID", new { stage1_ID = id, stage1Name = model.stage1_name });

            return RedirectToAction("list");
        }

        /**
        * Builds a stage1 model based on ID value 
        * Execution in database using Dapper
        * Builds a stage4 model to show values to user in view
        * 
        * @param int id - model with the given ID value, if exists, is build
        * @return view - returns the view with the values of the model with the given ID value  
        */
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<stage1>("SELECT * from stage1 WHERE stage1_ID = @stage1_ID", new { stage1_ID = id });

            if (obj != null)
            {
                stage1 model = new stage1();
                model.stage1_ID = obj.FirstOrDefault().stage1_ID;
                model.stage1_name = obj.FirstOrDefault().stage1_name;
                return View(model);
            }
            return View();
        }


        /**
        * Deletes a row in the database in stage1 table based on ID
        * Execution in database using Dapper
        * 
        * @param stage1 model - the model that is being deleted
        * @param int id - model with the given ID value, if exists, is being deleted 
        * @return redirectToAction(list) - returns the user to given action
        */
        [HttpPost]
        public ActionResult Delete(stage1 model, int id)
        {
            var obj = conn.Execute("DELETE from stage1 WHERE stage1_ID = @stage1_ID", new { stage1_ID = id });

            return RedirectToAction("list");
        }

    }
}