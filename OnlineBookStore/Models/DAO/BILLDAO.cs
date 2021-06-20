using OnlineBookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using OnlineBookStore.Models.DTO;
namespace OnlineBookStore.Models.DAO
{
    public class BILLDAO
    {
        DBModel db;
        public BILLDAO()
        {
            db = new DBModel();
        }

        public IEnumerable<BILL> BILLLIST(int Pagenum, int Pagesize)
        {
            var bill = (from b in db.BILL orderby b.BILLDATE descending select b).ToPagedList<BILL>(Pagenum, Pagesize);
            return bill;
        }

        public IEnumerable<BILL> BILLLISTCHECKED(int Pagenum, int Pagesize)
        {
            var bill = (from b in db.BILL where b.CHECKED == true orderby b.BILLDATE descending select b).ToPagedList<BILL>(Pagenum, Pagesize);
            return bill;
        }

        public IEnumerable<BILL> BILLLISTUNCHECK(int Pagenum, int Pagesize)
        {
            var bill = (from b in db.BILL where b.CHECKED==false orderby b.BILLDATE descending select b).ToPagedList<BILL>(Pagenum, Pagesize);
            return bill;
        }

        public List<BILLDTO> FIND(int id)
        {
            var dto = db.Database.SqlQuery<BILLDTO>("SELECT b.BILLID, bk.BOOKNAME, d.AMOUNT, d.PRICE, b.TOTALBILL, b.USERID,b.USERNAME, b.ADDRESS, b.BILLDATE,b.CHECKED,b.EMAIL,b.PHONE FROM DETAILBILL d JOIN BILL b on d.BILLID=b.BILLID join BOOK bk on d.BILLID=bk.BOOKID WHERE d.BILLID= '"+id+"' ORDER BY d.BILLID").ToList();
            return dto;
        }


        public ICollection<COMMENT> LOADDETAIL(int id)
        {
            var dt = db.Database.SqlQuery<COMMENT>("Select d.bookid, d,billid, d.amount, d.price, b.bookname from DETAILBILL d join book b on d.bookid=b.bookid where billid='" + id + "'").ToList();
            return dt;
        }

    }
}