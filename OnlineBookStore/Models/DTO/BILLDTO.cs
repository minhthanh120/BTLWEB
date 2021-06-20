using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Web.Mvc;

namespace OnlineBookStore.Models.DTO
{
    public class BILLDTO
    {
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
        [DisplayName("Trạng thái")]
        public bool CHECKED { get; set; }
        [DisplayName("Tên sách")]
        [Required]
        [StringLength(200)]
        public string BOOKNAME { get; set; }
        [DisplayName("Số lượng")]
        public int AMOUNT { get; set; }
        [DisplayName("Đơn giá")]
        public int PRICE { get; set; }

    }
}