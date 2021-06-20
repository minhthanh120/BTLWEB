using OnlineBookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using OnlineBookStore.Models.DTO;

namespace OnlineBookStore.Models.DAO
{
    public class DETAILBILLDAO
    {
        DBModel db;
        public DETAILBILLDAO()
        {
            db = new DBModel();
        }

        public IEnumerable<DETAILDTO> LIST(string id)
        {
            var detail = db.Database.SqlQuery<DETAILDTO>("SELECT d.BILLID, d.BOOKID, b.BOOKNAME, d.AMOUNT, d.PRICE FROM DETAILBILL d JOIN BOOK b ON d.BOOKID=b.BOOKID WHERE d.BILLID='"+id+"'").ToList();
            return detail;
        }

    }
}