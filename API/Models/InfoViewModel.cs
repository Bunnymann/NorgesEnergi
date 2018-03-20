using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class InfoViewModel
    {
        public int Stage1_ID { get; set; }
        public string Stage1_name { get; set; }
        public int Stage2_ID { get; set; }
        public string Stage2_name { get; set; }
        public int Stage3_ID { get; set; }
        public string Stage3_name { get; set; }
        public int Stage4_ID { get; set; }
        public string Stage4_name { get; set; }
        public int Helptext_ID { get; set; }
        public string Helptext_header { get; set; }
        public string Helptext_short { get; set; }
        public string Helptext_long { get; set; }
        public int Metatag_ID { get; set; }
        public string Tag { get; set; }


    }
}