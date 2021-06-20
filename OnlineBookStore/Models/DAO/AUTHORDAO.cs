using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models.Entities;
using PagedList;

namespace OnlineBookStore.Models.DAO
{
    public class AUTHORDAO
    {
        DBModel db;
        public AUTHORDAO()
        {
            db = new DBModel();
        }
        public IEnumerable<AUTHOR> AUTHORLIST(int pageNum, int pageSize)
        {
            var list = db.Database.SqlQuery<AUTHOR>(" Select * From AUTHOR").ToPagedList<AUTHOR>(pageNum, pageSize);
            return list;
        }
        public AUTHOR SEARCHID(int id)
        {
            return db.AUTHOR.Find(id);
        }
        public IEnumerable<AUTHOR> SEARCHNAME(string input, int pageNum, int pageSize)
        {
            var list = db.Database.SqlQuery<AUTHOR>(
                " Select * From AUTHOR" +
                " Where AUTHORNAME like '%' + '" + input + "' + '%'").ToPagedList<AUTHOR>(pageNum, pageSize);
            return list;
        }
        public void INSERT(string name)
        {
            AUTHOR author = new AUTHOR();
            author.AUTHORNAME = name;
            db.AUTHOR.Add(author);
            db.SaveChanges();
        }
        public void UPDATE(AUTHOR temp)
        {
            if (temp.AUTHORID != 0)
            {
                AUTHOR author = db.AUTHOR.Find(temp.AUTHORID);
                if (author != null) 
                {
                    author.AUTHORNAME = temp.AUTHORNAME;
                    db.SaveChanges();
                }
            }
        }
        public void DELETE(int id)
        {
            if (id != 0)
            {
                AUTHOR author = db.AUTHOR.Find(id);
                if (author != null)
                {
                    db.Database.ExecuteSqlCommand(  " Update BOOK" +
                                                    " Set AUTHORID = 0" +
                                                    " Where AUTHORID = @id" +
                                                    " Delete From AUTHOR Where AUTHORID = @id",
                                                    new SqlParameter("@id", id));
                    db.SaveChanges();
                }
            }
        }
    }
}