namespace OnlineBookStore.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COMMENT")]
    public partial class COMMENT
    {
        public int COMMENTID { get; set; }

        public int BOOKID { get; set; }

        public int? USERID { get; set; }

        public DateTime COMMENTDATE { get; set; }

        [Column("COMMENT", TypeName = "ntext")]
        public string COMMENT1 { get; set; }

        public int? RATING { get; set; }

        public virtual BOOK BOOK { get; set; }

        public virtual USER USER { get; set; }
    }
}
