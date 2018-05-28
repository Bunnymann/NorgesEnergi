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

/**
*The main Stage2 controller
*Contains all methods regarding this database table
*/
namespace WebApplication .Controllers
{
    public class Stage2Controller : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();

        /**
        * Uses GetAll method to find all rows of Stage2 in database
        * if the GetAll methods find any records, each record is build and stored in list
        * 
        * @return View(“name”) - returns the list of all records in a view
        */
        public ActionResult List()
        {
            var obj = GetAll();
            List<stage2> result = new List<stage2>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    stage2 model = new stage2();
                    model.stage2_ID = row.stage2_ID;
                    model.stage2_name = row.stage2_name;
                    result.Add(model);
                }
            }
            return View(result);
        }

        /**
        * Get all values from table Stage2 ordered by tablecolumn
        * Values are inserted to list
        * 
        * @return variable name - returns the variable which stores the values in a list
        */
        public List<stage2> GetAll()
        {
            var obj = conn.Query<stage2>("SELECT * FROM Stage2").OrderByDescending(u => u.stage2_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /**
        * Dependency method for create method
        * @return view - returns the view for creating new Stage2 data
        */
        [HttpPost]
        public ActionResult Create(stage2 model)
        {
            return RedirectToAction("list");
        }

        /**
        * Reads values from database in Stage2 table based on ID
        * Execution in database using Dapper
        *
        * @param int id - builds the model based on id value
        * @return view - return the view to show user the models values
        */
        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<stage2>("SELECT * FROM Stage2 WHERE stage2_ID =  @Stage2_ID", new { stage2_ID = id });

            if (obj != null)
            {
                stage2 model = new stage2();
                model.stage2_ID = obj.FirstOrDefault().stage2_ID;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                return View(model);
            }
            return View();
        }

        /**
        * Builds a Stage2 model based on ID value 
        * Execution in database using Dapper
        * Builds a Stage2 model to show values to user in view 
        * 
        * @param int id - model with the given ID value, if exists, is build 
        * @return View - returns the view with the values of the model with the given ID value
        */
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<stage2>("SELECT * from Stage2 WHERE stage2_ID = @stage2_ID", new { stage2_ID = id });

            if (obj != null)
            {
                stage2 model = new stage2();
                model.stage2_ID = obj.FirstOrDefault().stage2_ID;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                return View(model);
            }
            return View();
        }

        /**
        * Edits a row in the database in Stage2 table based on ID
        * Execution in database using Dapper
        * 
        * @param Stage2 model - the model that is being updated
        * @param int id - model with the given ID value, if exists, is being updated
        * @return redirectToAction(“action”) - returns the user to given action
        */
        [HttpPost]
        public ActionResult Edit(stage2 model, int id)
        {
            var obj = conn.Execute("UPDATE Stage2 set [stage2_name] = @stage2_name WHERE stage2_ID = @stage2_ID", new { stage2_ID = id, stage2_name = model.stage2_name });

            return RedirectToAction("list");
        }

        /**
        * Builds a Stage2 model based on ID value 
        * Execution in database using Dapper
        * Builds a Stage2 model to show values to user in view
        * 
        * @param int id - model with the given ID value, if exists, is build
        * @return view - returns the view with the values of the model with the given ID value  
        */
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<stage2>("SELECT * from Stage2 WHERE stage2_ID = @stage2_ID", new { stage2_ID = id });

            if (obj != null)
            {
                stage2 model = new stage2();
                model.stage2_ID = obj.FirstOrDefault().stage2_ID;
                model.stage2_name = obj.FirstOrDefault().stage2_name;
                return View(model);
            }
            return View();
        }

        /**
        * Deletes a row in the database in Stage2 table based on ID
        * Execution in database using Dapper
        * 
        * @param Stage2 model - the model that is being deleted
        * @param int id - model with the given ID value, if exists, is being deleted 
        * @return redirectToAction(“action”) - returns the user to given action
        */
        [HttpPost]
        public ActionResult Delete(stage2 model, int id)
        {
            var obj = conn.Execute("DELETE from Stage2 WHERE stage2_ID = @stage2_ID", new { stage2_ID = id });

            return RedirectToAction("list");
        }

    }
}