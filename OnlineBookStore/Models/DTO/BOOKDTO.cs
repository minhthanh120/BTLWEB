using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Web.Mvc;
using OnlineBookStore.Models.DAO;

namespace OnlineBookStore.Models.DTO
{
    public class BOOKDTO
    {
        public int BOOKID { get; set; }
        [DisplayName("Tên sách")]
        [Required]
        [StringLength(200)]
        public string BOOKNAME { get; set; }
        [DisplayName("Mô tả")]
        [Column(TypeName = "ntext")]
        public string DESCRIPTIONS { get; set; }
        [DisplayName("Mã loại sách")]
        public int TYPEID { get; set; }
        [DisplayName("Tên loại sách")]
        public string TYPENAME { get; set; }
        [DisplayName("Mã nhà xuất bản")]
        public int PUBLISHID { get; set; }
        [DisplayName("Tên nhà xuất bản")]
        public string PUBLISHNAME { get; set; }
        [DisplayName("Số lượng")]
        public int AMOUNT { get; set; }
        [DisplayName("Mã tác giả")]
        public int AUTHORID { get; set; }
        [DisplayName("Tên tác giả")]
        public string AUTHORNAME { get; set; }
        [DisplayName("Ảnh")]
        [Required]
        [StringLength(100)]
        public string PIC1 { get; set; }
        [DisplayName("Ảnh")]
        [StringLength(100)]
        public string PIC2 { get; set; }
        [DisplayName("Ảnh")]
        [StringLength(100)]
        public string PIC3 { get; set; }
        [DisplayName("Ảnh")]
        [StringLength(100)]
        public string PIC4 { get; set; }
        [DisplayName("Giá")]
        public int PRICE { get; set; }
        public string DESCRIPSHORTED { get { return DESCRIPTIONS.ToString().SubStringTo(50); } }
    }
}