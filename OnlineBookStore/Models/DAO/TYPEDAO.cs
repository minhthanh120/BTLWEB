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
    public class TYPEDAO
    {
        DBModel db;
        public TYPEDAO()
        {
            db = new DBModel();
        }
        public IEnumerable<BOOKTYPE> TYPELIST(int pageNum, int pageSize)
        {
            var list = db.Database.SqlQuery<BOOKTYPE>(" Select * From BOOKTYPE").ToPagedList<BOOKTYPE>(pageNum, pageSize);
            return list;
        }
        public BOOKTYPE SEARCHID(int id)
        {
            return db.BOOKTYPE.Find(id);
        }
        public IEnumerable<BOOKTYPE> SEARCHNAME(string input, int pageNum, int pageSize)
        {
            var list = db.Database.SqlQuery<BOOKTYPE>(
                " Select * From BOOKTYPE" +
                " Where TYPENAME like '%' + '" + input + "' + '%'").ToPagedList<BOOKTYPE>(pageNum, pageSize);
            return list;
        }
        public void INSERT(string name)
        {
            BOOKTYPE type = new BOOKTYPE();
            type.TYPENAME = name;
            db.BOOKTYPE.Add(type);
            db.SaveChanges();
        }
        public void UPDATE(BOOKTYPE temp)
        {
            if (temp.TYPEID != 0)
            {
                BOOKTYPE type = db.BOOKTYPE.Find(temp.TYPEID);
                if (type != null)
                {
                    type.TYPENAME = temp.TYPENAME;
                    db.SaveChanges();
                }
            }
        }
        public void DELETE(int id)
        {
            if (id != 0)
            {
                BOOKTYPE type = db.BOOKTYPE.Find(id);
                if (type != null)
                {
                    db.Database.ExecuteSqlCommand(  " Update BOOK" +
                                                    " Set TYPEID = 0" +
                                                    " Where TYPEID = @id" +
                                                    " Delete From BOOKTYPE Where TYPEID = @id",
                                                    new SqlParameter("@id", id));
                    db.SaveChanges();
                }
            }
        }
    }
}