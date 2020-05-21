using System;
using System.Collections.Generic;
using System.Text;
using librarytest.Model;

namespace librarytest.Model
{
    class helptexttag
    {
        public int helptexttag_ID { get; set; }
        public int helptext_ID { get; set; }
        public Nullable<int> metatag_ID { get; set; }

        public virtual helptext helptext { get; set; }
        public virtual metatag metatag { get; set; }
    }
}
