namespace OnlineBookStore.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("USER")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            BILL = new HashSet<BILL>();
            COMMENT = new HashSet<COMMENT>();
            AVATAR = "~/Image/nulluser.png";
        }

        public int USERID { get; set; }

        [Required]
        [StringLength(50)]
        public string ACCOUNT { get; set; }

        [Required]
        [StringLength(50)]
        public string PASSWORD { get; set; }

        [Required]
        [StringLength(50)]
        public string USERNAME { get; set; }

        [StringLength(200)]
        public string ADDRESS { get; set; }

        public bool ADMRIGHT { get; set; }

        [StringLength(11)]
        public string PHONE { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(50)]
        public string AVATAR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BILL> BILL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENT { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}
