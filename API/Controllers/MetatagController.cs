using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Web.Mvc;
using API.Models;
using System.Data.Entity.Validation;

namespace NorgesEnergi.Controllers
{
    /**
    *The main metatags controller
    *Contains all methods regarding this database table
    */

    public class MetaTagController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();

        /**
        * Uses GetAll method to find all rows of metatags in database
        * if the GetAll methods find any records, each record is build and stored in list
        * 
        * @return View(result) - returns the list of all records in a view
        */
        public ActionResult List()
        {
            var obj = GetAll();
            List<metatag> result = new List<metatag>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    metatag model = new metatag();
                    model.metatag_ID = row.metatag_ID;
                    model.tag = row.tag;
                    result.Add(model);
                }
            }
            return View(result);
        }

        /**
        * Get all values from table metatag ordered by metatag_ID
        * Values are inserted to list
        * 
        * @return obj - returns the variable which stores the values in a list
        */
        public List<metatag> GetAll()
        {
            var obj = conn.Query<metatag>("SELECT * FROM Metatag").OrderByDescending(u => u.metatag_ID).ToList();
            return obj;
        }

        /**
        * Dependency method for create method
        * @return view - returns the view for creating new metatag data
        */
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /**
        * Creates a new row in the database in metatag table
        * Execution in database using Dapper
        * 
        * @param metatag model - the model that is being created. Values are filled in using a view 
        * related to this method. 
        * @return redirectToAction(“List”); - returns the user to given action
        */
        [HttpPost]
        public ActionResult Create(metatag model)
        {
            return RedirectToAction("List");
        }


        /**
        * Reads values from database in metatag table based on ID
        * Execution in database using Dapper
        *
        * @param int id - builds the model based on id value
        * @return view - return the view to show user the models values
        */
        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<metatag>("SELECT * FROM Metatag WHERE metatag_ID = @metatag_ID", new { Metatag_ID = id });

            if (obj != null)
            {
                metatag model = new metatag();
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                model.tag = obj.FirstOrDefault().tag;
                return View(model);
            }
            return View();
        }

        /**
        * Builds a metatag model based on ID value 
        * Execution in database using Dapper
        * Builds a metatag model to show values to user in view 
        * 
        * @param int id - model with the given ID value, if exists, is build 
        * @return View - returns the view with the values of the model with the given ID value
        */
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<metatag>("SELECT * FROM Metatag WHERE metatag_ID = @metatag_ID", new { metatag_ID = id });

            if (obj != null)
            {
                metatag model = new metatag();
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                model.tag = obj.FirstOrDefault().tag;
                return View(model);
            }
            return View();
        }

        /**
        * Edits a row in the database in metatag table based on ID
        * Execution in database using Dapper
        * 
        * @param metatag model - the model that is being updated
        * @param int id - model with the given ID value, if exists, is being updated
        * @return redirectToAction(“list”) - returns the user to given action
        */
        [HttpPost]
        public ActionResult Edit(metatag model, int id)
        {
            var obj = conn.Execute("UPDATE Metatag set [tag] = @mtag WHERE metatag_ID = @metatag_ID", new { metatag_ID = id, mtag = model.tag });

            return RedirectToAction("list");
        }

        /**
        * Builds a metatag model based on ID value 
        * Execution in database using Dapper
        * Builds a metatag model to show values to user in view
        * 
        * @param int id - model with the given ID value, if exists, is build
        * @return view - returns the view with the values of the model with the given ID value  
        */
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<metatag>("SELECT * FROM Metatag WHERE metatag_ID = @metatag_ID", new { metatag_ID = id });

            if (obj != null)
            {
                metatag model = new metatag();
                model.metatag_ID = obj.FirstOrDefault().metatag_ID;
                model.tag = obj.FirstOrDefault().tag;
                return View(model);
            }
            return View();
        }

        /**
        * Deletes a row in the database in metatag table based on ID
        * Execution in database using Dapper
        * 
        * @param classname model - the model that is being deleted
        * @param int id - model with the given ID value, if exists, is being deleted 
        * @return redirectToAction(“list”) - returns the user to given action
        */
        [HttpPost]
        public ActionResult Delete(metatag model, int id)
        {
            var obj = conn.Execute("DELETE FROM Metatag WHERE metatag_ID = @metatag_ID", new { metatag_ID = id });

            return RedirectToAction("list");
        }

    }
}