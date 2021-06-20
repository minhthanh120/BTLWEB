namespace OnlineBookStore.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BOOKTYPE")]
    public partial class BOOKTYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOKTYPE()
        {
            BOOK = new HashSet<BOOK>();
        }

        [Key]
        public int TYPEID { get; set; }

        [Required]
        [StringLength(50)]
        public string TYPENAME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOK> BOOK { get; set; }
    }
}
