using OnlineBookStore.Models.DTO;
using OnlineBookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
//using Microsoft.EntityFrameworkCore.;

namespace OnlineBookStore.Models.DAO
{
    public class BOOKDAO
    {
        DBModel db;
        public BOOKDAO()
        {
            db = new DBModel();
        }
        //public IQueryable<BOOK> listBOOK()
        //{
        //    var lst = (from s in db.BOOK orderby s.BOOKNAME descending select s);
        //    return lst;
        //}
        public IEnumerable<BOOKDTO> lstBOOK(int Pagenum, int PageSize){
            var lst = db.Database.SqlQuery<BOOKDTO>("SELECT B.BOOKID,B.TYPEID, B.AUTHORID, B.PUBLISHID, B.BOOKNAME,A.AUTHORNAME,P.PUBLISHNAME,BT.TYPENAME, B.DESCRIPTIONS, B.AMOUNT,B.PIC1,B.PIC2,B.PIC3,B.PIC4,B.PRICE"+
                " from BOOK as B JOIN PUBLISH as P on P.PUBLISHID=B.PUBLISHID JOIN AUTHOR as A on A.AUTHORID=B.AUTHORID JOIN BOOKTYPE as BT ON BT.TYPEID=B.TYPEID ORDER BY B.BOOKNAME DESC").ToPagedList<BOOKDTO>(Pagenum, PageSize);
            return lst;
                }
        public BOOK Find(int id)
        {
            return db.BOOK.Find(id);
        }

        public void Add(BOOK book)
        {
            db.BOOK.Add(book);
            db.SaveChanges();
        }

        string sorting(string s)
        {
            string sort="";
            if (s == "3")
                sort = "";
            else if (s == "4")
            {
                sort = " and B.AMOUNT<'10'";
            }
            else if (s == "1")
                sort = " ORDER BY B.PRICE DESC";
            else if (s == "2")
                sort = " ORDER BY B.PRICE ASC";
            return sort;
        }

        public IEnumerable<BOOKDTO> SEATCHBYNAME(string input, int Pagenum, int PageSize, string sort)
        {
            var lst = db.Database.SqlQuery<BOOKDTO>("SELECT B.BOOKID,B.BOOKNAME,A.AUTHORNAME,P.PUBLISHNAME,BT.TYPENAME, B.DESCRIPTIONS, B.AMOUNT,B.PIC1,B.PIC2,B.PIC3,B.PIC4,B.PRICE" +
                " from BOOK as B JOIN PUBLISH as P on P.PUBLISHID=B.PUBLISHID JOIN AUTHOR as A on A.AUTHORID=B.AUTHORID JOIN BOOKTYPE as BT ON BT.TYPEID=B.TYPEID WHERE B.BOOKNAME LIKE '%"+input+"%'" +sorting(sort)).ToPagedList<BOOKDTO>(Pagenum, PageSize);
            return lst;
        }

        public IEnumerable<BOOKDTO> SEATCHBYAUTHOR(string input, string authorid, int Pagenum, int PageSize, string sort)
        {
            string q = "SELECT B.BOOKID,B.BOOKNAME,A.AUTHORNAME,P.PUBLISHNAME,BT.TYPENAME, B.DESCRIPTIONS, B.AMOUNT,B.PIC1,B.PIC2,B.PIC3,B.PIC4,B.PRICE" +
                " from BOOK as B JOIN PUBLISH as P on P.PUBLISHID=B.PUBLISHID JOIN AUTHOR as A on A.AUTHORID=B.AUTHORID JOIN BOOKTYPE as BT ON BT.TYPEID=B.TYPEID WHERE B.BOOKNAME LIKE '%" + input + "%' AND A.AUTHORID='"+authorid+"'" ;
            var lst = db.Database.SqlQuery<BOOKDTO>(q+sorting(sort)).ToPagedList<BOOKDTO>(Pagenum, PageSize);
            return lst ;
        }

        public IEnumerable<BOOKDTO> SEATCHBYTYPE(string input, string typeid, int Pagenum, int PageSize, string sort)
        {
            string q = "SELECT B.BOOKID,B.BOOKNAME,A.AUTHORNAME,P.PUBLISHNAME,BT.TYPENAME, B.DESCRIPTIONS, B.AMOUNT,B.PIC1,B.PIC2,B.PIC3,B.PIC4,B.PRICE" +
                " from BOOK as B JOIN PUBLISH as P on P.PUBLISHID=B.PUBLISHID JOIN AUTHOR as A on A.AUTHORID=B.AUTHORID JOIN BOOKTYPE as BT ON BT.TYPEID=B.TYPEID WHERE B.BOOKNAME LIKE '%" + input + "%' AND B.TYPEID='" + typeid + "'";
            var lst = db.Database.SqlQuery<BOOKDTO>(q+sorting(sort)).ToPagedList<BOOKDTO>(Pagenum, PageSize);
            return lst;
        }

        public IEnumerable<BOOKDTO> SEATCHBYPRICE(string input,string min, string max, int Pagenum, int PageSize, string sort)
        {
            string q = "SELECT B.BOOKID,B.BOOKNAME,A.AUTHORNAME,P.PUBLISHNAME,BT.TYPENAME, B.DESCRIPTIONS, B.AMOUNT,B.PIC1,B.PIC2,B.PIC3,B.PIC4,B.PRICE from BOOK as B JOIN PUBLISH as P on P.PUBLISHID=B.PUBLISHID JOIN AUTHOR as A on A.AUTHORID = B.AUTHORID JOIN BOOKTYPE as BT ON BT.TYPEID = B.TYPEID WHERE B.BOOKNAME LIKE '%l%' AND B.PRICE BETWEEN '" + min+"' and '"+max+"'";
            var lst = db.Database.SqlQuery<BOOKDTO>(q+sorting(sort)).ToPagedList<BOOKDTO>(Pagenum, PageSize);
            return lst;
        }

        public void DELETE(int id)
        {
            BOOK b = db.BOOK.Find(id);
            if (b != null)
            {
                db.COMMENT.RemoveRange(db.COMMENT.Where(x => x.BOOKID == b.BOOKID));
                db.SaveChanges();
                db.BOOK.Remove(b);
                db.SaveChanges();
            }

        }


    }
    public static class StringExtensions
    {
        public static string SubStringTo(this string thatString, int limit)
        {

            if (thatString.Length > limit)
            {
                return thatString.Substring(0, limit)+"...";
            }
            return thatString;

        }
    }
}