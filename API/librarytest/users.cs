using System;
using System.Collections.Generic;
using System.Text;
using librarytest.Model;

namespace librarytest.Model
{
    class users
    {
        public string loginname { get; set; }
        public string loginpassword { get; set; }
        public Nullable<int> stage1_ID { get; set; }

        public virtual stage1 stage1 { get; set; }
    }
}
