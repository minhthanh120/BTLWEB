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
    public class DETAILDTO
    {
        [DisplayName("Mã đơn hàng")]
        [Key]
        public int BILLID { get; set; }
        [DisplayName("Mã sách")]
        public int BOOKID { get; set; }
        [DisplayName("Tên sách")]
        [Required]
        [StringLength(200)]
        public string BOOKNAME { get; set; }
        [DisplayName("Số lượng")]
        public int AMOUNT { get; set; }
        [DisplayName("Giá")]
        public int PRICE { get; set; }
    }
}