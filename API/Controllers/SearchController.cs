using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Configuration;
using API.Models;
using System.Data.SqlClient;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace API.Controllers
{
    public class SearchController : Controller
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        Norges_EnergiEntities db = new Norges_EnergiEntities();

        
        [HttpPost]
        public string GetSearch(string tags)
        {
            List<string> taglist = new List<string>();
            List<string> criteria = new List<string>();

            string sqlstring = ("SELECT info.info_ID, count(metatag.tag) as metatags FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID ");
            criteria.Add(sqlstring);

            /*
             * Search parameters must as for now be written in the code.
             * The code works and result are correct according to database data
             * The split function get error System.NullReferenceException
             */
            string search = "nor, am, privat, us";

            string[] words = search.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var tag in words)
            {
                taglist.Add(tag);
            }

            int occ = taglist.Count();
            int count = 0;


            if (occ == 1)
            {
                foreach (var tag in taglist)
                {
                    criteria.Add("where metatag.tag like '%" + tag + "%';");
                }
            }
            else
            {
                while (count < occ)
                {
                    foreach (var tag in taglist)
                    {
                        if (count == 0)
                        {
                            criteria.Add("where metatag.tag like '%" + tag + "%' or ");
                        }
                        else if (count > 0 && count < (occ - 1))
                        {
                            criteria.Add("metatag.tag like '%" + tag + "%' or ");
                        }
                        else if (count == (occ - 1))
                        {
                            criteria.Add("metatag.tag like '%" + tag + "%' group by info.info_ID order by metatags desc;");
                        }

                        count++;
                    }
                }
            }
            string text = string.Join("", criteria);
            return text;
        }

        [HttpGet]
        public ActionResult Index(string tags)
        {
            var obj = conn.Query<InfoViewModel>(GetSearch(tags)).Take(4).ToList();
            
            List<InfoViewModel> result = new List<InfoViewModel>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    InfoViewModel model = new InfoViewModel
                    {
                        Stage1_name = GetStage1(row.Info_ID),
                        Stage2_name = GetStage2(row.Info_ID),
                        Stage3_name = GetStage3(row.Info_ID),
                        Stage4_name = GetStage4(row.Info_ID),
                        Helptext_ID = GetHelptextID(row.Info_ID),
                        Helptext_header = GetHelptextHeader(GetHelptextID(row.Info_ID)),
                        Helptext_short = GetShortText(GetHelptextID(row.Info_ID)),
                        Helptext_long = GetLongText(GetHelptextID(row.Info_ID)),
                        Tag = GetTags(row.Info_ID),
                    };
                    result.Add(model);
                }
            }
            return View(result);
        }

        [HttpGet]
        public string GetStage1(int id)
        {
            var obj = conn.Query<stage1>("select stage1_name from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID inner join stage1 on info.stage1_ID = stage1.stage1_ID where info.info_ID = @infoID;", new { infoID = id }).FirstOrDefault().stage1_name;

            return obj;
        }

        [HttpGet]
        public string GetStage2(int id)
        {
            var obj = conn.Query<stage2>("select stage2_name from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID inner join stage2 on info.stage2_ID = stage2.stage2_ID where info.info_ID = @infoID;", new { infoID = id }).FirstOrDefault().stage2_name;

            return obj;
        }

        [HttpGet]
        public string GetStage3(int id)
        {
            var obj = conn.Query<stage3>("select stage3_name from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID inner join stage3 on info.stage3_ID = stage3.stage3_ID where info.info_ID = @infoID;", new { infoID = id }).FirstOrDefault().stage3_name;

            return obj;
        }

        [HttpGet]
        public string GetStage4(int id)
        {
            var obj = conn.Query<stage4>("select stage4_name from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID where info.info_ID = @infoID;", new { infoID = id }).FirstOrDefault().stage4_name;

            return obj;
        }
        [HttpGet]
        public string GetLongText(int id)
        {
            var obj = conn.Query<helptext>("SELECT helptext.helptext_long FROM helptext where helptext_ID = @helpID;", new { helpID = id }).FirstOrDefault().helptext_long;

            return obj;
        }

        [HttpGet]
        public string GetShortText(int id)
        {
            var obj = conn.Query<helptext>("SELECT helptext.helptext_short FROM helptext WHERE helptext_ID = @helpID;", new { helpID = id }).FirstOrDefault().helptext_short;

            return obj;
        }

        [HttpGet]
        public string GetHelptextHeader(int id)
        {
            var obj = conn.Query<helptext>("SELECT helptext_header from helptext where helptext_ID = @helpID", new { helpid = id }).FirstOrDefault().helptext_header;

            return obj;
        }

        [HttpGet]
        public int GetHelptextID(int id)
        {
            int obj = conn.Query<helptext>("SELECT helptext.helptext_ID from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID where info.info_ID = @infoID", new { infoID = id }).FirstOrDefault().helptext_ID;

            return obj;
        }

        public string GetTags(int id)
        {
            List<string> tags = new List<string>();
            var obj = conn.Query<metatag>("SELECT metatag.tag FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID where info_ID = @infoID;", new { infoID = id });

            if (obj != null)
            {
                foreach (var tag in obj)
                {

                    tags.Add(tag.tag.ToString());
                }
            }
            string text = string.Join(", ", tags);
            return text;
        }

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
    }
}