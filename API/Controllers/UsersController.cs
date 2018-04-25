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

namespace API.Controllers
{
    public class UsersController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();

        // GET: Users
        [HttpGet]
        public List<users> GetAll()
        {
            var obj = conn.Query<users>("Select * from Users").OrderByDescending(u => u.loginname).ToList();

            return obj;
        }

        public ActionResult Index()
        {
            var obj = GetAll();
            List<users> result = new List<users>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    users model = new users()
                    {
                        loginname = row.loginname,
                        loginpassword = row.loginpassword,
                        stage1_ID = row.stage1_ID
                    };
                    result.Add(model);
                }
            }
            return View(result);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            PopulateStageDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "loginname, loginpassword, stage1_ID")] users user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /*dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateStageDropDownList(user.stage1_ID);
            return View(user);
        }

        private void PopulateStageDropDownList(object selectStage = null)
        {
            var stage = from s in db.stage1
                           orderby s.stage1_name
                           select s;
            ViewBag.stage1_ID = new SelectList(stage, "stage1_ID", "stage1_name", selectStage);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(users model)
        {
            var obj = InsertUser(model);
            return RedirectToAction("Index");
        }
        public bool InsertUser(users model)
        {
            int rowsAffected = conn.Execute("INSERT INTO users([loginname, loginpassword, stage1_ID) VALUES (@name, @pwd, @stage) ", new { name = model.loginname, pwd = model.loginpassword, stage = model.stage1_ID });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
