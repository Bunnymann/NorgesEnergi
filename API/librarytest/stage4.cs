using System;
using System.Collections.Generic;
using System.Text;
using librarytest.Model;

namespace librarytest.Model
{
    class stage4
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public stage4()
        {
            this.info = new HashSet<info>();
        }

        public int stage4_ID { get; set; }
        public string stage4_name { get; set; }
        public Nullable<int> helptext_ID { get; set; }

        public virtual helptext helptext { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<info> info { get; set; }

    }
}
