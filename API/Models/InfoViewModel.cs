using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class InfoViewModel
    {
        public int info_ID { get; set; }
        public int stage1_ID { get; set; }
        [DisplayName ("Nation")]
        public string stage1_name { get; set; }
        public int stage2_ID { get; set; }
        [DisplayName("System")]
        public string stage2_name { get; set; }
        public int stage3_ID { get; set; }
        [DisplayName("Place i system")]
        public string stage3_name { get; set; }
        public int stage4_ID { get; set; }
        [DisplayName("Label")]
        public string stage4_name { get; set; }
        public int helptext_ID { get; set; }
        [DisplayName("Headertext")]
        public string helptext_header { get; set; }
        [DisplayName("Summary")]
        public string helptext_short { get; set; }
        [DisplayName("Full text")]
        public string helptext_long { get; set; }
        public int metatag_ID { get; set; }
        [DisplayName("Tags")]
        public string tag { get; set; }

    }
}