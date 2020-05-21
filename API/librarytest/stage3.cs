using System;
using System.Collections.Generic;
using System.Text;
using librarytest.Model;

namespace librarytest.Model
{
    class stage3
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public stage3()
        {
            this.info = new HashSet<info>();
        }

        public int stage3_ID { get; set; }
        public string stage3_name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<info> info { get; set; }
    }
}
