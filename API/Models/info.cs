//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class info
    {
        public int info_ID { get; set; }
        public int stage1_ID { get; set; }
        public Nullable<int> stage2_ID { get; set; }
        public Nullable<int> stage3_ID { get; set; }
        public Nullable<int> stage4_ID { get; set; }
    
        public virtual stage1 stage1 { get; set; }
        public virtual stage2 stage2 { get; set; }
        public virtual stage3 stage3 { get; set; }
        public virtual stage4 stage4 { get; set; }
    }
}
