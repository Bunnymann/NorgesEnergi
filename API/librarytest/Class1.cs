using System;
using System.Collections.Generic;
using System.Linq;

namespace librarytest
{
    public class search
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TelosNE"].ToString());

        public string getSearch(string tags)
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
        string search = tags;

        //Remove blank spaces does not work
        string[] words = search.Split(new[] { ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

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
                criteria.Add("where metatag.tag like '%" + tag + "%' group by info.info_ID order by metatags desc;");
                //string query = string.Format("where metatag.tag like '%{0}%';", tag);
                //criteria.Add(query);
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
                        //string query = string.Format("where metatag.tag like '%{0}%' or ", tag);
                        //criteria.Add(query);
                    }
                    else if (count > 0 && count < (occ - 1))
                    {
                        criteria.Add("metatag.tag like '%" + tag + "%' or ");
                        //string query = string.Format("metatag.tag like '%{0}%' or ", tag);
                        //criteria.Add(query);
                    }
                    else if (count == (occ - 1))
                    {
                        criteria.Add("metatag.tag like '%" + tag + "%' group by info.info_ID order by metatags desc;");
                        //string query = string.Format("metatag.tag like '%{0}%' group by info.info_ID order by metatags desc;", tag);
                        //criteria.Add(query);
                    }

                    count++;
                }
            }
        }
        string text = string.Join("", criteria);
        return text;
    }

        /**
         * Uses the GetSeach method to run a SQL-query for searching for helptexts
         * User input is the metatags being searched
         * If the GetSearch method returns any records, each recrod is build as a InfoViewModel
         * Each variable in InfoViewModel is built using the Info_ID value found in GetSearch method
         * Variable obj takes the 4 records with the most hits on user searchwords
         * 
         * @param string tags - the user input
         * @return View(result) - returns a list-view for the recrods build
         */
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

        /*
         * Selects stage1_name from database based on info_ID value
         * Execution in database using Dapper
         * Using inner joins in SQL-query to find correct values
         * 
         * @param int id - the info_ID value for a stage1_name value
         * @return obj - the stage1_name returned as string
         */
        [HttpGet]
        public string GetStage1(int id)
        {
            var obj = conn.Query<stage1>("select stage1_name from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID inner join stage1 on info.stage1_ID = stage1.stage1_ID where info.info_ID = @infoID;", new { infoID = id }).FirstOrDefault().stage1_name;

            return obj;
        }

        /*
        * Selects stage2_name from database based on info_ID value
        * Execution in database using Dapper
        * Using inner joins in SQL-query to find correct values
        * 
        * @param int id - the info_ID value for a stage2_name value
        * @return obj - the stage2_name returned as string
        */
        [HttpGet]
        public string GetStage2(int id)
        {
            var obj = conn.Query<stage2>("select stage2_name from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID inner join stage2 on info.stage2_ID = stage2.stage2_ID where info.info_ID = @infoID;", new { infoID = id }).FirstOrDefault().stage2_name;

            return obj;
        }

        /*
        * Selects stage3_name from database based on info_ID value
        * Execution in database using Dapper
        * Using inner joins in SQL-query to find correct values
        * 
        * @param int id - the info_ID value for a stage3_name value
        * @return obj - the stage3_name returned as string
        */
        [HttpGet]
        public string GetStage3(int id)
        {
            var obj = conn.Query<stage3>("select stage3_name from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID inner join stage3 on info.stage3_ID = stage3.stage3_ID where info.info_ID = @infoID;", new { infoID = id }).FirstOrDefault().stage3_name;

            return obj;
        }

        /*
        * Selects stage4_name from database based on info_ID value
        * Execution in database using Dapper
        * Using inner joins in SQL-query to find correct values
        * 
        * @param int id - the info_ID value for a stage4_name value
        * @return obj - the stage4_name returned as string
        */
        [HttpGet]
        public string GetStage4(int id)
        {
            var obj = conn.Query<stage4>("select stage4_name from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID where info.info_ID = @infoID;", new { infoID = id }).FirstOrDefault().stage4_name;

            return obj;
        }

        /*
        * Selects helptext_long from database based on helptext_ID value
        * Execution in database using Dapper
        * 
        * @param int id - the helptext_ID value for a helptext_long value
        * @return obj - the helptext_long returned as string
        */
        [HttpGet]
        public string GetLongText(int id)
        {
            var obj = conn.Query<helptext>("SELECT helptext.helptext_long FROM helptext where helptext_ID = @helpID;", new { helpID = id }).FirstOrDefault().helptext_long;

            return obj;
        }

        /*
        * Selects helptext_short from database based on helptext_ID value
        * Execution in database using Dapper
        * 
        * @param int id - the helptext_ID value for a helptext_short value
        * @return obj - the helptext_short returned as string
        */
        [HttpGet]
        public string GetShortText(int id)
        {
            var obj = conn.Query<helptext>("SELECT helptext.helptext_short FROM helptext WHERE helptext_ID = @helpID;", new { helpID = id }).FirstOrDefault().helptext_short;

            return obj;
        }

        /*
        * Selects helptext_header from database based on helptext_ID value
        * Execution in database using Dapper
        * 
        * @param int id - the helptext_ID value for a helptext_header value
        * @return obj - the helptext_header returned as string
        */
        [HttpGet]
        public string GetHelptextHeader(int id)
        {
            var obj = conn.Query<helptext>("SELECT helptext_header from helptext where helptext_ID = @helpID", new { helpid = id }).FirstOrDefault().helptext_header;

            return obj;
        }

        /*
        * Selects helptext_ID from database based on info_ID value
        * Execution in database using Dapper
        * 
        * @param int id - the info_ID value for a helptext_ID value
        * @return obj - the helptext_ID returned as int
        */
        [HttpGet]
        public int GetHelptextID(int id)
        {
            int obj = conn.Query<helptext>("SELECT helptext.helptext_ID from helptext inner join stage4 on helptext.helptext_ID = stage4.helptext_ID inner join info on stage4.stage4_ID = info.stage4_ID where info.info_ID = @infoID", new { infoID = id }).FirstOrDefault().helptext_ID;

            return obj;
        }

        /*
        * Selects metatags from database based on info_ID value
        * Execution in database using Dapper
        * Using inner joins in SQL-query to find correct values
        * If method finds multiple values, each metatag is put in a list and later the list is made as a string
        * 
        * @param int id - the info_ID value for a helptext_ID value
        * @return text - the list of all found metatags join as a string
        */
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
    }
}
