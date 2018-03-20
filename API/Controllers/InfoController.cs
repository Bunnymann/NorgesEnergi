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
    }
}