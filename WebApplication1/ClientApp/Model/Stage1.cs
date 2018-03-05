using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ClientApp.Model
{
    public class Stage1
    {
        public int stage1_ID { get; set; }
        public string stage1_name { get; set; }
        public int helptext_ID { get; set; }
        public IEnumerable<SelectListItem> Stage1List { get; set; }
    }
}
