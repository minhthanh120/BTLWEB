using System;
using System.Collections.Generic;
using System.Net;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models.DAO;
using System.IO;
using OnlineBookStore.Models.DTO;
using OnlineBookStore.Models.Entities;
using PagedList;
namespace OnlineBookStore.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        DBModel db = new DBModel()
        {
        };

        public bool isLoggedIn()
        {
            if (Session["Admin"] == null) return false;
            else return true;
        }

        List<SelectListItem> a = new List<SelectListItem>
            {
                new SelectListItem { Text = "Tên Sách", Value = "1" },
                new SelectListItem { Text = "Thể Loại", Value = "2" },
                new SelectListItem { Text = "Tác giả", Value = "3" },
                new SelectListItem { Text = "Giá", Value = "4" } };
        List<SelectListItem> b = new List<SelectListItem>
            {
                new SelectListItem { Text = "0 - 100 000", Value = "1" },
                new SelectListItem { Text = "100 000 - 200 000", Value = "2" },
                new SelectListItem { Text = "200 000 - 500 000", Value = "3" },
                new SelectListItem { Text = "> 500 000", Value = "4" } };

        List<SelectListItem> c = new List<SelectListItem>
            {
                new SelectListItem { Text = "Đánh giá cao", Value = "3" },
                new SelectListItem { Text = "Đánh giá thấp", Value = "4" },
                new SelectListItem { Text = "Mới nhất", Value = "1" },
                new SelectListItem { Text = "Cũ nhất", Value = "2" }
                 };

        List<SelectListItem> d = new List<SelectListItem>
            {
                new SelectListItem { Text = "Tất cả", Value = "3" },
                new SelectListItem { Text = "Sắp hết hàng", Value = "4" },
                new SelectListItem { Text = "Giá cao đến thấp", Value = "1" },
                new SelectListItem { Text = "Giá thấp đến cao", Value = "2" }
                 };
        public ActionResult Index(string input, string sort, string Select,string selectprice, string authorid, string typeid,string submit, int PageNum = 1, int PageSize = 10)
        {
            if (isLoggedIn())
            {
                ViewBag.Select = a;
                ViewBag.selectprice = b;
                ViewBag.authorid = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
                ViewBag.typeid = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
                ViewBag.sort = d;
                BOOKDAO dao = new BOOKDAO();
                //if (input != "")
                //{
                if(input!=null&&input!="")
                {
                    if (Select == "1")
                        return View(dao.SEATCHBYNAME(input, PageNum, PageSize, sort));
                    else if (Select == "3")
                        return View(dao.SEATCHBYAUTHOR(input, authorid, PageNum, PageSize, sort));
                    else if (Select == "2")
                        return View(dao.SEATCHBYTYPE(input, typeid, PageNum, PageSize, sort));
                    else if (Select == "4")
                    {
                        if (selectprice == "1")
                            return View(dao.SEATCHBYPRICE(input, "0", "100000", PageNum, PageSize, sort));
                        else if (selectprice == "2")
                            return View(dao.SEATCHBYPRICE(input, "100000", "200000", PageNum, PageSize, sort));
                        else if (selectprice == "3")
                            return View(dao.SEATCHBYPRICE(input, "200000", "500000", PageNum, PageSize, sort));
                        else if (selectprice == "4")
                            return View(dao.SEATCHBYPRICE(input, "500000", "100000000", PageNum, PageSize, sort));
                    }
                }    

                return View(dao.lstBOOK(PageNum, PageSize));
            }
            else return RedirectToAction("Login", "User");
        }



        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            if (isLoggedIn())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BOOK b = db.BOOK.Find(id);
                if (b == null)
                {
                    return HttpNotFound();
                }
                return View(b);
            }
            else return RedirectToAction("Login", "User");
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            if (isLoggedIn())
            {
                BOOK b = new BOOK();
                ViewBag.publishid = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
                ViewBag.authorid = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
                ViewBag.typeid = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");

                return View(b);
            }
            else return RedirectToAction("Login", "User");
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(BOOK b,FormCollection collection)
        {
            if (isLoggedIn())
            {
                if (b.Image1 != null)
                {

                    string fileName = Path.GetFileNameWithoutExtension(b.Image1.FileName);
                    string extension = Path.GetExtension(b.Image1.FileName);
                    fileName = fileName + extension;
                    b.PIC1 = "~/Image/" + fileName;
                    b.Image1.SaveAs(Path.Combine(Server.MapPath("~/Image/"), fileName));


                }
                if (b.Image2 != null)
                {

                    string fileName = Path.GetFileNameWithoutExtension(b.Image2.FileName);
                    string extension = Path.GetExtension(b.Image2.FileName);
                    fileName = fileName + extension;
                    b.PIC2 = "~/Image/" + fileName;
                    b.Image2.SaveAs(Path.Combine(Server.MapPath("~/Image/"), fileName));


                }
                if (b.Image3 != null)
                {

                    string fileName = Path.GetFileNameWithoutExtension(b.Image3.FileName);
                    string extension = Path.GetExtension(b.Image3.FileName);
                    fileName = fileName + extension;
                    b.PIC3 = "~/Image/" + fileName;
                    b.Image3.SaveAs(Path.Combine(Server.MapPath("~/Image/"), fileName));


                }
                if (b.Image4 != null)
                {

                    string fileName = Path.GetFileNameWithoutExtension(b.Image4.FileName);
                    string extension = Path.GetExtension(b.Image4.FileName);
                    fileName = fileName + extension;
                    b.PIC4 = "~/Image/" + fileName;
                    b.Image4.SaveAs(Path.Combine(Server.MapPath("~/Image/"), fileName));


                }
                if (b.PIC4 == "~/Image/add.png")
                {
                    b.PIC4 = null;
                }
                if (b.PIC3 == "~/Image/add.png")
                {
                    b.PIC3 = null;
                }
                if (b.PIC2 == "~/Image/add.png")
                {
                    b.PIC2 = null;
                }
                if (b.PIC1 == "~/Image/add.png")
                {
                    b.PIC1 = null;
                }
                db.BOOK.Add(b);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Login", "User");
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            if (isLoggedIn())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BOOK b = db.BOOK.Find(id);
                if (b == null)
                {
                    return HttpNotFound();
                }
                ViewBag.publishid = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME", b.PUBLISHID);
                ViewBag.authorid = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME", b.AUTHORID);
                ViewBag.typeid = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME", b.TYPEID);
                return View(b);
            }
            else return RedirectToAction("Login", "User");
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(BOOK b)
        {
            ViewBag.publishid = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME", b.PUBLISHID);
            ViewBag.authorid = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME", b.AUTHORID);
            ViewBag.typeid = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME", b.TYPEID);
                // TODO: Add update logic here
                if(ModelState.IsValid)
                {
                    if (b.Image1 != null)
                    {

                        string fileName = Path.GetFileNameWithoutExtension(b.Image1.FileName);
                        string extension = Path.GetExtension(b.Image1.FileName);
                        fileName = fileName + extension;
                        b.PIC1 = "~/Image/" + fileName;
                        b.Image1.SaveAs(Path.Combine(Server.MapPath("~/Image/"), fileName));
                    }
                    if (b.Image2 != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(b.Image2.FileName);
                        string extension = Path.GetExtension(b.Image2.FileName);
                        fileName = fileName + extension;
                        b.PIC2 = "~/Image/" + fileName;
                        b.Image2.SaveAs(Path.Combine(Server.MapPath("~/Image/"), fileName));
                    }
                    if (b.Image3 != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(b.Image3.FileName);
                        string extension = Path.GetExtension(b.Image3.FileName);
                        fileName = fileName + extension;
                        b.PIC3 = "~/Image/" + fileName;
                        b.Image3.SaveAs(Path.Combine(Server.MapPath("~/Image/"), fileName));
                    }
                    if (b.Image4 != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(b.Image4.FileName);
                        string extension = Path.GetExtension(b.Image4.FileName);
                        fileName = fileName + extension;
                        b.PIC4 = "~/Image/" + fileName;
                        b.Image4.SaveAs(Path.Combine(Server.MapPath("~/Image/"), fileName));
                    }
                if (b.PIC4 == "~/Image/add.png")
                {
                    b.PIC4 = null;
                }
                if (b.PIC3 == "~/Image/add.png")
                {
                    b.PIC3 = null;
                }
                if (b.PIC2 == "~/Image/add.png")
                {
                    b.PIC2 = null;
                }
                if (b.PIC1 == "~/Image/add.png")
                {
                    b.PIC1 = null;
                }
                db.Entry(b).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(b);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                BOOKDAO dao = new BOOKDAO();
                return View(dao.Find(id));
            }
            else return RedirectToAction("Index");
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (isLoggedIn())
            {
                if (id > 0)
                {
                    BOOKDAO dao = new BOOKDAO();
                    dao.DELETE(id);
                    return RedirectToAction("Index");
                }
                else return RedirectToAction("Index");
            }
            else return RedirectToAction("Login", "User");
        }


        public ActionResult Comment(int id)
        {
            int PageSize = 10;
           // int PageNum = (page ?? 1);
            BOOK b = new BOOK();
            ViewBag.sort = c;
            b.COMMENTDTO = (db.Database.SqlQuery<COMMENTDTO>("SELECT c.BOOKID,c.COMMENT,c.COMMENTDATE,c.COMMENTID,c.RATING,c.USERID,u.USERNAME from COMMENT c JOIN [USER] u on c.USERID=u.USERID where c.bookid='"+id+ "' order by c.rating desc ")).ToList();
            return PartialView(b);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult DeleteComment(int id)
        {
            if (isLoggedIn())
            {
                COMMENTDTO dto = new COMMENTDTO();
                dto = db.Database.SqlQuery<COMMENTDTO>("SELECT c.BOOKID,c.COMMENT,c.COMMENTDATE,c.COMMENTID,c.RATING,c.USERID,u.USERNAME from COMMENT c JOIN [USER] u on c.USERID=u.USERID where c.commentid='" + id + "'").FirstOrDefault();
                return View(dto);
            }
            else return RedirectToAction("Login", "User");
        }

        // POST: Comment/Delete/5
        [HttpPost]
        public ActionResult DeleteComment(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                COMMENT c = db.COMMENT.Find(id);
                int? go = c.BOOKID;
                db.COMMENT.Remove(c);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = go });
            }
            catch
            {
                return View();
            }
        }

       


    }
}
