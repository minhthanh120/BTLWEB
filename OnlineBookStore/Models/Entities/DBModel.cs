using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace OnlineBookStore.Models.Entities
{
    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<AUTHOR> AUTHOR { get; set; }
        public virtual DbSet<BILL> BILL { get; set; }
        public virtual DbSet<BOOK> BOOK { get; set; }
        public virtual DbSet<BOOKTYPE> BOOKTYPE { get; set; }
        public virtual DbSet<COMMENT> COMMENT { get; set; }
        public virtual DbSet<DETAILBILL> DETAILBILL { get; set; }
        public virtual DbSet<PUBLISH> PUBLISH { get; set; }
        public virtual DbSet<USER> USER { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AUTHOR>()
                .HasMany(e => e.BOOK)
                .WithRequired(e => e.AUTHOR)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BILL>()
                .HasMany(e => e.DETAILBILL)
                .WithRequired(e => e.BILL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BOOK>()
                .HasMany(e => e.DETAILBILL)
                .WithRequired(e => e.BOOK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BOOKTYPE>()
                .HasMany(e => e.BOOK)
                .WithRequired(e => e.BOOKTYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PUBLISH>()
                .HasMany(e => e.BOOK)
                .WithRequired(e => e.PUBLISH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.BILL)
                .WithRequired(e => e.USER)
                .WillCascadeOnDelete(false);
        }
    }
}
