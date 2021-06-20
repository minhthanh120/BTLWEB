using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models.Entities;

namespace OnlineBookStore.Models.DAO
{
    public class USERDAO
    {
        DBModel db;
        public USERDAO()
        {
            db = new DBModel();
        }
        public USER SEARCHID(int id)
        {
            return db.USER.Find(id);
        }
        public int AddAdmin(string name, string acc, string pwd, string address, string phone, string email, string pic)
        {
            USER us = new USER();
            us.USERNAME = name;
            us.ACCOUNT = acc;
            us.PASSWORD = pwd;
            us.ADDRESS = address;
            us.PHONE = phone;
            us.EMAIL = email;
            us.AVATAR = pic;
            us.ADMRIGHT = true;
            db.USER.Add(us);
            db.SaveChanges();
            return us.USERID;
        }
        public void DeleteAdmin(int id)
        {
            USER us = db.USER.Find(id);
            if (us != null)
            {
                db.Database.ExecuteSqlCommand(  " Delete From dbo.[USER]" +
                                                " Where ADMRIGHT = 1 AND USERID = @id",
                                                new SqlParameter("@id", id));
                db.SaveChanges();
            }
        }
        public void UpdatePass(int id, string pass)
        {
            USER us = db.USER.Find(id);
            if (us != null)
            {
                db.Database.ExecuteSqlCommand(  " Update dbo.[USER]" +
                                                " Set PASSWORD = @pass" +
                                                " Where ADMRIGHT = 1 AND USERID = @id",
                                                new SqlParameter("@id", id),
                                                new SqlParameter("@pass",pass));
                db.SaveChanges();
            } 
                
        }
    }
}