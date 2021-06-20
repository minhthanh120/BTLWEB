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
    public class PUBLISHDAO
    {
        DBModel db;
        public PUBLISHDAO()
        {
            db = new DBModel();
        }
        public IEnumerable<PUBLISH> PUBLISHLIST(int pageNum, int pageSize)
        {
            var list = db.Database.SqlQuery<PUBLISH>(" Select * From PUBLISH").ToPagedList<PUBLISH>(pageNum, pageSize);
            return list;
        }
        public PUBLISH SEARCHID(int id)
        {
            return db.PUBLISH.Find(id);
        }
        public IEnumerable<PUBLISH> SEARCHNAME(string input, int pageNum, int pageSize)
        {
            var list = db.Database.SqlQuery<PUBLISH>(
                " Select * From PUBLISH" +
                " Where PUBLISHNAME like '%' + '" + input + "' + '%'").ToPagedList<PUBLISH>(pageNum, pageSize);
            return list;
        }
        public void INSERT(string name)
        {
            PUBLISH publish = new PUBLISH();
            publish.PUBLISHNAME = name;
            db.PUBLISH.Add(publish);
            db.SaveChanges();
        }
        public void UPDATE(PUBLISH temp)
        {
            if (temp.PUBLISHID != 0)
            {
                PUBLISH publish = db.PUBLISH.Find(temp.PUBLISHID);
                if (publish != null)
                {
                    publish.PUBLISHNAME = temp.PUBLISHNAME;
                    db.SaveChanges();
                }
            }
        }
        public void DELETE(int id)
        {
            if (id != 0)
            {
                PUBLISH publish = db.PUBLISH.Find(id);
                if (publish != null)
                {
                    db.Database.ExecuteSqlCommand(  " Update BOOK" +
                                                    " Set PUBLISHID = 0" +
                                                    " Where PUBLISHID = @id" +
                                                    " Delete From PUBLISH Where PUBLISHID = @id",
                                                    new SqlParameter("@id", id));
                    db.SaveChanges();
                }
            }
        }
    }
}