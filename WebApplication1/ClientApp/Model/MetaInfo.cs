using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ClientApp.Data
{
    public class MetaInfo
    {
        public int metainfo_ID { get; set; }
        public int category_ID { get; set; }
        public string category_name { get; set; }
        public int metatag_ID { get; set; }
        public string metatag_tag { get; set; }
    }
}
