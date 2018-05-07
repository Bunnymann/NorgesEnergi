using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using API.Models;
using Newtonsoft.Json;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;

namespace API.APIControllers
{
    [RoutePrefix("api/helptext")]
    public class APISearchController : ApiController
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());
        private Norges_EnergiEntities db = new Norges_EnergiEntities();
        [HttpGet]
        [Route("search/{tags}")]
        public HttpResponseMessage Search2(string tags)
        {
            try
            {
                var httpResponseMessage = new HttpResponseMessage();
                httpResponseMessage.Content = new StringContent
                    (JsonConvert.SerializeObject(db.helptext.Where
                    (h => h.helptext_header.Contains(tags)).ToList()));
                httpResponseMessage.Content.Headers.ContentType = new
                    System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                return httpResponseMessage;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<InfoViewModel> Searchs(string tags, InfoViewModel model)
        {
            var obj = conn.Query<HelpTagsViewModel>("SELECT helptext.helptext_header, count(metatag.tag) as tags FROM info INNER JOIN stage1 ON info.stage1_ID = stage1.stage1_ID INNER JOIN stage2 ON info.stage2_ID = stage2.stage2_ID INNER JOIN stage3 ON info.stage3_ID = stage3.stage3_ID INNER JOIN stage4 ON info.stage4_ID = stage4.stage4_ID INNER JOIN helptext ON stage4.helptext_ID = helptext.helptext_ID INNER JOIN helptexttag ON helptext.helptext_ID = helptexttag.helptext_ID INNER JOIN metatag ON helptexttag.metatag_ID = metatag.metatag_ID where stage1.stage1_ID = @natID and stage2.stage2_ID = sysID and " +
                "(" + GetSearch(tags) + ") group by helptext.helptext_header order by tags desc;", new { natID = model.stage1_ID, sysID = model.stage2_ID }).Take(4).ToList();
            List<InfoViewModel> result = new List<InfoViewModel>();
            if (obj != null)
            {
                foreach (var row in obj)
                {
                    InfoViewModel model2 = new InfoViewModel
                    {
                        stage1_name = GetStage1(row.helptext_header, model.stage1_ID),
                        stage2_name = GetStage2(row.helptext_header, model.stage2_ID),
                        stage3_name = GetStage3(row.helptext_header, model.stage3_ID),
                        stage4_name = GetStage4(row.helptext_header, model.stage4_ID),
                        helptext_header = row.helptext_header,
                        helptext_short = GetShortText(row.helptext_header),
                        helptext_long = GetLongText(row.helptext_header),
                    };
                    result.Add(model2);
                }
            }
            return result;
        }
        [HttpGet]
        public string GetSearch(string tags)
        {
            List<string> criteria = new List<string>();

            char[] delimiterChars = { ',', '.', ':', };

            string text = tags;

            string[] words = text.Split(delimiterChars);

            foreach (var tag in text)
            {
                criteria.Add("metatag.tag like = '%" + tag + "%'");
            }

            return criteria.ToString();
        }

        [HttpGet]
        public string GetStage1(string name, int id)
        {
            var obj = conn.Query<stage1>("select stage1.stage1_ID, stage1_name, helptext_header from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID inner join stage1 on info.stage1_ID = stage1.stage1_ID where helptext_header like '%@header%' and stage1.stage1_ID = @userid;", new { header = name, userid = id }).FirstOrDefault().stage1_name;

            return obj;
        }

        [HttpGet]
        public string GetStage2(string name, int id)
        {
            var obj = conn.Query<stage2>("select stage2.stage2_ID, stage2_name, helptext_header from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID inner join stage2 on info.stage2_ID = stage2.stage2_ID where helptext_header like '%@header%' and stage2.stage2_ID = @userid;", new { header = name, userid = id }).FirstOrDefault().stage2_name;

            return obj;
        }

        [HttpGet]
        public string GetStage3(string name, int id)
        {
            var obj = conn.Query<stage3>("select stage3.stage3_ID, stage3_name, helptext_header from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID inner join stage3 on info.stage3_ID = stage3.stage3_ID where helptext_header like '%@header%' and stage3.stage3_ID = @userid;", new { header = name, userid = id }).FirstOrDefault().stage3_name;

            return obj;
        }

        [HttpGet]
        public string GetStage4(string name, int id)
        {
            var obj = conn.Query<stage4>("select stage4.stage4_ID, stage4_name, helptext_header from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID where helptext_header like '%@header%' and stage1.stage1_ID = @userid;", new { header = name, userid = id }).FirstOrDefault().stage4_name;

            return obj;
        }

        [HttpGet]
        public string GetLongText(string name)
        {
            var obj = conn.Query<helptext>("SELECT helptext_long where helptext_header = '%@header'&", new { header = name }).FirstOrDefault().helptext_long;

            return obj;
        }

        [HttpGet]
        public string GetShortText(string name)
        {
            var obj = conn.Query<helptext>("SELECT helptext_short where helptext_header = '%@header'&", new { header = name }).FirstOrDefault().helptext_short;

            return obj;
        }
    }
}
