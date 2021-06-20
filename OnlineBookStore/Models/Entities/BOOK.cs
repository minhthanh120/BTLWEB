namespace OnlineBookStore.Models.Entities
{
    using OnlineBookStore.Models.DTO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;
    using System.Web.Mvc;

    [Table("BOOK")]
    public partial class BOOK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOK()
        {
            COMMENT = new HashSet<COMMENT>();
            DETAILBILL = new HashSet<DETAILBILL>();
            PIC1 = "~/Image/add.png";
            PIC2 = "~/Image/add.png";
            PIC3 = "~/Image/add.png";
            PIC4 = "~/Image/add.png";
        }

        public int BOOKID { get; set; }
        [DisplayName("Tên sách: ")]
        [Required]
        [StringLength(200)]
        public string BOOKNAME { get; set; }
        [DisplayName("Mô tả: ")]
        [Column(TypeName = "ntext")]
        public string DESCRIPTIONS { get; set; }
        [DisplayName("Thể loại: ")]
        public int TYPEID { get; set; }
        [DisplayName("Nhà xuất bản: ")]
        public int PUBLISHID { get; set; }
        [DisplayName("Số lượng: ")]
        public int AMOUNT { get; set; }
        [DisplayName("Tác giả: ")]
        public int AUTHORID { get; set; }
        [DisplayName("Ảnh: ")]
        public string PIC1 { get; set; }
        [DisplayName("Ảnh: ")]
        [StringLength(100)]
        public string PIC2 { get; set; }
        [DisplayName("Ảnh: ")]
        [StringLength(100)]
        public string PIC3 { get; set; }
        [DisplayName("Ảnh: ")]
        [StringLength(100)]
        public string PIC4 { get; set; }
        [DisplayName("Giá: ")]
        public int PRICE { get; set; }
        
        [NotMapped]
        public HttpPostedFileBase Image1 { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image2 { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image3 { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image4 { get; set; }

        public virtual AUTHOR AUTHOR { get; set; }

        public virtual PUBLISH PUBLISH { get; set; }

        public virtual BOOKTYPE BOOKTYPE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENT { get; set; }
        public ICollection<COMMENTDTO> COMMENTDTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETAILBILL> DETAILBILL { get; set; }
    }
}
