using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ClientApp.Data
{
    public class Info
    {
        public int info_ID { get; set; }
        [DisplayName("Stage 1")]
        public string stage1_name { get; set; }
        [DisplayName("Stage 2")]
        public string stage2_name { get; set; }
        [DisplayName("Stage 3")]
        public string stage3_name { get; set; }
        [DisplayName("Stage 4")]
        public string stage4_name { get; set; }


        public int stage1_ID { get; set; }
        public int stage2_ID { get; set; }
        public int stage3_ID { get; set; }
        public int stage4_ID { get; set; }



    }
}
