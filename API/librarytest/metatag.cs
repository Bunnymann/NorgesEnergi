using System;
using System.Collections.Generic;
using System.Text;
using librarytest.Model;

namespace librarytest.Model
{
    class metatag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public metatag()
        {
            this.helptexttag = new HashSet<helptexttag>();
        }

        public int metatag_ID { get; set; }
        public string tag { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<helptexttag> helptexttag { get; set; }
    }
}
