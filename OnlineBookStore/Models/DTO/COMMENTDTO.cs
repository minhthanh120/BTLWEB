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
    public class COMMENTDTO
    {
        [DisplayName("Mã comment")]
        [Key]
        public int COMMENTID { get; set; }
        [DisplayName("Mã sách")]
        public int? BOOKID { get; set; }
        [DisplayName("Mã comment")]
        public int? USERID { get; set; }
        [DisplayName("Tên người dùng")]
        public string USERNAME { get; set; }
        [DisplayName("Ngày comment")]
        public DateTime COMMENTDATE { get; set; }
        [DisplayName("Nội dung")]
        public string COMMENT { get; set; }
        [DisplayName("Đánh giá")]
        public int? RATING { get; set; }
    }
}