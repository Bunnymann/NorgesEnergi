using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class InfoViewModel
    {
        public int stage1_ID { get; set; }
        public string stage1_name { get; set; }
        public int stage2_ID { get; set; }
        public string stage2_name { get; set; }
        public int stage3_ID { get; set; }
        public string stage3_name { get; set; }
        public int stage4_ID { get; set; }
        public string stage4_name { get; set; }
        public int helptext_ID { get; set; }
        public string helptext_header { get; set; }
        public string helptext_short { get; set; }
        public string helptext_long { get; set; }
        public int metatag_ID { get; set; }
        public string tag { get; set; }

    }
}