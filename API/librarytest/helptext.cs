using System;
using System.Collections.Generic;
using System.Text;
using librarytest.Model;

namespace librarytest.Model
{
    class helptext
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public helptext()
        {
            this.helptexttag = new HashSet<helptexttag>();
            this.stage4 = new HashSet<stage4>();
        }

        public int helptext_ID { get; set; }
        public string helptext_header { get; set; }
        public string helptext_short { get; set; }
        public string helptext_long { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<helptexttag> helptexttag { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<stage4> stage4 { get; set; }
    }
}
