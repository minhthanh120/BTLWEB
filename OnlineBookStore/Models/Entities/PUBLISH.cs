namespace OnlineBookStore.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PUBLISH")]
    public partial class PUBLISH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PUBLISH()
        {
            BOOK = new HashSet<BOOK>();
        }

        public int PUBLISHID { get; set; }

        [Required]
        [StringLength(50)]
        public string PUBLISHNAME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOK> BOOK { get; set; }
    }
}
