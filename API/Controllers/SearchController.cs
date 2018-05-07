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

        [HttpGet]
        public ActionResult Index1(string tags)
        {

            char[] delimiterChars = new char[] { ',', '.', ':', };
            
            string test = ("SELECT info.info_ID, helptext.helptext_header FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID;");
            
            List<string> criteria = new List<string>
            {
                test
            };
            
            string text = tags;

            string[] words;

            words = "nor, test, faktura".Split(delimiterChars);

            foreach (var word in words)
            {
                criteria.Add("metatag.tag like = '%" + word + "%' and");
            }

            string searchtext = criteria.ToString();
            //return View(db.helptext.Where(h => h.helptext_header.Contains(tags) || tags == null).ToList());

            //var obj = conn.Query<InfoViewModel>("SELECT info.info_ID, helptext.helptext_header FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID where metatag.tag like @tag;", new { tag = tags }).ToList();
            var obj = conn.Query<InfoViewModel>(test).ToList();
            
            List<InfoViewModel> result = new List<InfoViewModel>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    InfoViewModel model2 = new InfoViewModel
                    {
                        stage1_name = GetStage1(row.info_ID),
                        stage2_name = GetStage2(row.info_ID),
                        stage3_name = GetStage3(row.info_ID),
                        stage4_name = GetStage4(row.info_ID),
                        helptext_ID = GetHelptextID(row.helptext_header),
                        helptext_header = row.helptext_header,
                        helptext_short = GetShortText(row.info_ID),
                        helptext_long = GetLongText(row.info_ID),
                        tag = GetTags(row.info_ID),
                    };
                    result.Add(model2);
                }
            }
            return View(result);
        }
        /**
        [HttpGet]
        public string GetSearch(string tags)
        {
            string test = ("SELECT info.info_ID, helptext.helptext_header FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID;");
            List<string> criteria = new List<string>();
            criteria.Add(test);

            char[] delimiterChars = { ',', '.', ':', };

            string text = tags;

            string[] words = text.Split(delimiterChars);

            int value = words.Length;

            foreach (var tag in text)
            {
                criteria.Add("where metatag.tag like = '%" + tag + "%' and");
            }

            return criteria.ToString();
        }
    */

        [HttpGet]
        public string GetStage1(int id)
        {
            var obj = conn.Query<stage1>("select stage1_name, helptext_header from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID inner join stage1 on info.stage1_ID = stage1.stage1_ID where info.info_ID = @header;", new { header = id }).FirstOrDefault().stage1_name;

            return obj;
        }

        [HttpGet]
        public string GetStage2(int id)
        {
            var obj = conn.Query<stage2>("select stage2_name, helptext_header from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID inner join stage2 on info.stage2_ID = stage2.stage2_ID where info.info_ID like @header;", new { header = id }).FirstOrDefault().stage2_name;

            return obj;
        }

        [HttpGet]
        public string GetStage3(int id)
        {
            var obj = conn.Query<stage3>("select stage3_name, helptext_header from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID inner join stage3 on info.stage3_ID = stage3.stage3_ID where info.info_ID like @header;", new { header = id }).FirstOrDefault().stage3_name;

            return obj;
        }

        [HttpGet]
        public string GetStage4(int id)
        {
            var obj = conn.Query<stage4>("select stage4_name, helptext_header from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID where info.info_ID like @header;", new { header = id }).FirstOrDefault().stage4_name;

            return obj;
        }
        [HttpGet]
        public string GetLongText(int id)
        {
            var obj = conn.Query<helptext>("SELECT helptext.helptext_long FROM helptext INNER JOIN stage4 ON helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID where info.info_ID = @header", new { header = id }).FirstOrDefault().helptext_long;

            return obj;
        }

        [HttpGet]
        public string GetShortText(int id)
        {
            var obj = conn.Query<helptext>("SELECT helptext.helptext_short FROM helptext INNER JOIN stage4 ON helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID where info.info_ID = @header", new { header = id }).FirstOrDefault().helptext_short;

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
        public int GetHelptextID(string name)
        {
            var obj = conn.Query<helptext>("SELECT helptext_ID from helptext where helptext_header = @header", new { header = name }).FirstOrDefault().helptext_ID;

            return obj;
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