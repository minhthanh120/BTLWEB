using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models.DAO;
using OnlineBookStore.Models.Entities;
using OnlineBookStore.Models.DTO;

namespace OnlineBookStore.Controllers
{
    public class CheckBillController : Controller
    {
        DBModel db = new DBModel()
        {

        };

        public bool isLoggedIn()
        {
            return true;
            //if (Session["Admin"] == null) return false;
            //else return true;
        }

        // GET: CheckBill
        List<SelectListItem> a= new List<SelectListItem>
        {
            new SelectListItem { Text = "Mới nhất", Value = "1" },
                new SelectListItem { Text = "Đã duyệt", Value = "2" },
                new SelectListItem { Text = "Chưa duyệt", Value = "3" } };


        public ActionResult Index(string sort)
        {
            if (isLoggedIn())
            {
                BILLDAO dao = new BILLDAO();
                ViewBag.sort = a;
                if (sort == "2")
                    return View(dao.BILLLISTCHECKED(1, 10));
                else if (sort == "3")
                    return View(dao.BILLLISTUNCHECK(1, 10));
                else
                    return View(dao.BILLLIST(1, 10));
            }
            else return RedirectToAction("Login", "User");
        }

        // GET: CheckBill/Details/5
        public ActionResult Details(int id)
        {
            if (isLoggedIn())
            {
                return View();
            }
            else return RedirectToAction("Login", "User");

        }

        // GET: CheckBill/Create
        public ActionResult Create()
        {
            if (isLoggedIn())
            {
                return View();
            }
            else return RedirectToAction("Login", "User");

        }

        public ActionResult DetailBill(int id)
        {
            if (isLoggedIn())
            {
                BILL b = new BILL();
                b.DETAILDTO = db.Database.SqlQuery<DETAILDTO>("select b.bookname, d.billid,d.bookid,d.amount,d.price from detailbill d join book b on b.bookid=d.bookid where billid='" + id + "'").ToList();
                return PartialView(b);
            }
            else return RedirectToAction("Login", "User");

            
        }

        // POST: CheckBill/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckBill/Edit/5
        public ActionResult Edit(int id)
        {
            if (isLoggedIn())
            {
                BILL b = db.BILL.Find(id);
                return View(b);
            }
            else return RedirectToAction("Login", "User");

        }

        // POST: CheckBill/Edit/5
        [HttpPost]
        public ActionResult Edit( string jup)
        {
            try
            {
                // TODO: Add delete logic here
                if (jup != "")
                {
                    int key = Int32.Parse(jup);
                    BILL a = db.BILL.Find(key);
                    if (a != null)
                    {
                        a.CHECKED = true;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckBill/Delete/5
        public ActionResult Delete(int id)
        {
            if (isLoggedIn())
            {
                return View();
            }
            else return RedirectToAction("Login", "User");
            
        }

        // POST: CheckBill/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
