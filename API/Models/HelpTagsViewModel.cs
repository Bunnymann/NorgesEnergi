using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class HelpTagsViewModel
    {
        public int helptext_ID { get; set; }
        public string helptext_header { get; set; }
        public string helptext_short { get; set; }
        public string helptext_long { get; set; }
        public int tag_ID { get; set; }
        public string tag { get; set; }
    }
}