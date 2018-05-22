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
        [Key]
        public int Info_ID { get; set; }
        //Foreign key
        public int Stage1_ID { get; set; }
        [DisplayName ("Nation")]
        public string Stage1_name { get; set; }
        //Foreign key
        public int Stage2_ID { get; set; }
        [DisplayName("System")]
        public string Stage2_name { get; set; }
        //Foreign key
        public int Stage3_ID { get; set; }
        [DisplayName("Place i system")]
        public string Stage3_name { get; set; }
        //Foreign key
        public int Stage4_ID { get; set; }
        [DisplayName("Label")]
        public string Stage4_name { get; set; }
        //Foreign key
        public int Helptext_ID { get; set; }
        [DisplayName("Headertext")]
        public string Helptext_header { get; set; }
        [DisplayName("Summary")]
        public string Helptext_short { get; set; }
        [DisplayName("Full text")]
        public string Helptext_long { get; set; }
        //Foreign key
        public int Metatag_ID { get; set; }
        [DisplayName("Tags")]
        public string Tag { get; set; }

    }
}