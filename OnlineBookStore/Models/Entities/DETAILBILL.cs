namespace OnlineBookStore.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DETAILBILL")]
    public partial class DETAILBILL
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BILLID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BOOKID { get; set; }

        public int AMOUNT { get; set; }

        public int PRICE { get; set; }

        public virtual BILL BILL { get; set; }

        public virtual BOOK BOOK { get; set; }
    }
}
