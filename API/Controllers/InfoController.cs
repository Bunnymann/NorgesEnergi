using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Web.Mvc;
using API.Models;

namespace WebApplication1.Controllers
{
    public class InfoController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());

        public ActionResult StagesDDL()
        {
            Norges_EnergiEntities myStages1 = new Norges_EnergiEntities();
            var getStages1 = myStages1.stage1.ToList();
            SelectList list1 = new SelectList(getStages1, "stage1_ID", "stage1_name");
            ViewBag.stage1List = list1;

            Norges_EnergiEntities myStages2 = new Norges_EnergiEntities();
            var getStages2 = myStages2.stage2.ToList();
            SelectList list2 = new SelectList(getStages2, "stage2_ID", "stage2_name");
            ViewBag.stage2List = list2;

            Norges_EnergiEntities myStages3 = new Norges_EnergiEntities();
            var getStages3 = myStages3.stage3.ToList();
            SelectList list3 = new SelectList(getStages3, "stage3_ID", "stage3_name");
            ViewBag.stage3List = list3;

            Norges_EnergiEntities myStages4 = new Norges_EnergiEntities();
            var getStages4 = myStages4.stage4.ToList();
            SelectList list4 = new SelectList(getStages4, "stage4_ID", "stage4_name");
            ViewBag.stage4List = list4;

            return View();
        }

        public ActionResult DDLSearchStage3()
        {
            Norges_EnergiEntities stages = new Norges_EnergiEntities();
            return View(stages.stage3.ToList());
        }

        public ActionResult DDLSearchStage4()
        {
            Norges_EnergiEntities stages = new Norges_EnergiEntities();
            return View(stages.stage4.ToList());
        }

        public ActionResult Stage3DDLSearch(string search)
        {
            Norges_EnergiEntities stages = new Norges_EnergiEntities();
            var stageList = stages.stage3.Where(x => x.stage3_name.Contains(search)).ToList();
            return Json(stageList,JsonRequestBehavior.AllowGet);
        }

        public ActionResult viewAll()
        {
            Norges_EnergiEntities info = new Norges_EnergiEntities();

            return View();
        }

        //Get list from function "GetALL" under
        public ActionResult List()
        {
            var obj = GetAll();
            List<info> result = new List<info>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    info model = new info();
                    model.info_ID = row.info_ID;
                    model.stage1_ID = row.stage1_ID;
                    model.stage2_ID = row.stage2_ID;
                    model.stage3_ID = row.stage3_ID;
                    model.stage4_ID = row.stage4_ID;
                    result.Add(model);
                }
            }
            return View(result);
        }

        //Get all values from table helptext ordered by helptext header name
        public List<info> GetAll()
        {
            var obj = conn.Query<info>("Select * FROM info").OrderByDescending(u => u.info_ID).Take(10).ToList();
            return obj;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(info model)
        {
            var obj = InsertHelptext(model);
            return RedirectToAction("list");
        }
        public bool InsertHelptext(info model)
        {
            int rowsAffected = conn.Execute("INSERT INTO info([stage1_ID], [stage2_ID], [stage3_ID], [stage4_ID]) VALUES (@stage1, @stage2, @stage3, @stage4)", new { stage1 = model.stage1_ID, stage2 = model.stage2_ID, stage3 = model.stage3_ID, stage4 = model.stage4_ID });
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        /**
        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = conn.Query<info>("SELECT * FROM helptext WHERE helptext_ID =  @text_ID", new { text_ID = id });

            if (obj != null)
            {
                info model = new info();
                model.info_ID = obj.FirstOrDefault().info_ID;
                model.stage1_ID = obj.FirstOrDefault().stage1_ID;
                model.stage2_ID = obj.FirstOrDefault().stage2_ID;
                model.stage3_ID = obj.FirstOrDefault().stage3_ID;
                model.stage4_ID = obj.FirstOrDefault().stage4_ID;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = conn.Query<info>("SELECT * FROM helptext WHERE helptext_ID = @textID", new { textID = id });

            if (obj != null)
            {
                info model = new info();
                model.info_ID = obj.FirstOrDefault().info_ID;
                model.stage1_ID = obj.FirstOrDefault().stage1_ID;
                model.stage2_ID = obj.FirstOrDefault().stage2_ID;
                model.stage3_ID = obj.FirstOrDefault().stage3_ID;
                model.stage4_ID = obj.FirstOrDefault().stage4_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(info model, int id)
        {
            var obj = conn.Execute("UPDATE helptext SET [helptext_header] = @header, [helptext_short] = @text_short, [helptext_long] = @text_long WHERE helptext_ID = @helpID", new { helpID = id, header = model.helptext_header, text_short = model.helptext_short, text_long = model.helptext_long });

            return RedirectToAction("list");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = conn.Query<info>("SELECT * FROM helptext WHERE helptext_ID = @textID", new { textID = id });

            if (obj != null)
            {
                info model = new info();
                model.info_ID = obj.FirstOrDefault().info_ID;
                model.stage1_ID = obj.FirstOrDefault().stage1_ID;
                model.stage2_ID = obj.FirstOrDefault().stage2_ID;
                model.stage3_ID = obj.FirstOrDefault().stage3_ID;
                model.stage4_ID = obj.FirstOrDefault().stage4_ID;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(info model, int id)
        {
            var obj = conn.Execute("DELETE FROM helptext WHERE helptext_ID = @textID", new { textID = id });

            return RedirectToAction("list");
        }
    
     */

    }
}
