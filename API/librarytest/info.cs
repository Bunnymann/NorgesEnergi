using System;
using System.Collections.Generic;
using System.Text;
using librarytest.Model;

namespace librarytest.Model
{
    class info
    {
        public int info_ID { get; set; }
        public int stage1_ID { get; set; }
        public int stage2_ID { get; set; }
        public int stage3_ID { get; set; }
        public int stage4_ID { get; set; }

        public virtual stage1 stage1 { get; set; }
        public virtual stage2 stage2 { get; set; }
        public virtual stage3 stage3 { get; set; }
        public virtual stage4 stage4 { get; set; }
    }
}
