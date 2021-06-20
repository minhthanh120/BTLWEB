namespace OnlineBookStore.Models.Entities
{
    using OnlineBookStore.Models.DTO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BILL")]
    public partial class BILL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BILL()
        {
            DETAILBILL = new HashSet<DETAILBILL>();
        }
        
        public int BILLID { get; set; }
        [DisplayName("Ngày lập hóa đơn")]
        public DateTime BILLDATE { get; set; }
        [DisplayName("Tổng tiền")]
        public int? TOTALBILL { get; set; }
        [DisplayName("Mã người dùng")]
        public int USERID { get; set; }
        [DisplayName("Tên khách hàng")]
        [Required]
        [StringLength(50)]
        public string USERNAME { get; set; }
        [DisplayName("Địa chỉ")]
        [Required]
        [StringLength(200)]
        public string ADDRESS { get; set; }
        [DisplayName("Số điện thoại")]
        [Required]
        [StringLength(11)]
        public string PHONE { get; set; }
        [DisplayName("E-Mail")]
        [StringLength(50)]
        public string EMAIL { get; set; }
        [DisplayName("Trạng thái (Duyêt/Chưa duyệt)")]
        public bool CHECKED { get; set; }

        public virtual USER USER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<DETAILBILL> DETAILBILL { get; set; }
        public List<DETAILDTO> DETAILDTO { get; set; }
        public List<DETAILBILL> LISTDETAILBILL { get; set; }
}
}
