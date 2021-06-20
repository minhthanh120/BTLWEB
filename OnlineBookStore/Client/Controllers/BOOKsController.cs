using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models.Entities;
using PagedList;
using OnlineBookStore.Models;

namespace OnlineBookStore.Client.Controllers
{
    public class BOOKsController : Controller
    {
        private DBModel db = new DBModel();

        // GET: BOOKs
        public ActionResult Index(string search, int page = 1, int pagesize = 16, string author = "", string type = "", string publish = "", int AUTHORID = 0, int TYPEID = 0, int PUBLISHID = 0, int? min = null, int? max = null, int? id = null)
        {
            var bOOK = db.BOOK.Include(b => b.AUTHOR).Include(b => b.BOOKTYPE).Include(b => b.PUBLISH);
            ViewBag.Search = search;
            ViewBag.Author = author;
            ViewBag.Type = type;
            ViewBag.Pulish = publish;
            ViewBag.Min = min;
            ViewBag.Max = max;
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
            if (!String.IsNullOrEmpty(search))
            {
                bOOK = bOOK.Where(s => s.BOOKNAME.Contains(search));
            }
            if (AUTHORID != 0)
            {
                bOOK = bOOK.Where(s => s.AUTHORID == AUTHORID);
            }
            if (TYPEID != 0)
            {
                bOOK = bOOK.Where(s => s.TYPEID == TYPEID);
            }
            if (PUBLISHID != 0)
            {
                bOOK = bOOK.Where(s => s.PUBLISHID == PUBLISHID);
            }
            if (min != 0 && min != null)
            {
                bOOK = bOOK.Where(s => s.PRICE >= min);
            }
            if (max != 0 && max != null)
            {
                bOOK = bOOK.Where(s => s.PRICE <= max);
            }
            if (!String.IsNullOrEmpty(author))
            {
                bOOK = bOOK.Where(a => a.AUTHOR.AUTHORNAME.Contains(author));
            }
            if (!String.IsNullOrEmpty(type))
            {
                bOOK = bOOK.Where(b => b.BOOKTYPE.TYPENAME.Contains(type));
            }
            if (!String.IsNullOrEmpty(publish))
            {
                bOOK = bOOK.Where(b => b.PUBLISH.PUBLISHNAME.Contains(publish));
            }
            return View(bOOK.ToList().ToPagedList(page, pagesize));
        }

        // GET: BOOKs/Details/5
        public ActionResult Details(int? id, COMMENT cOMMENT, FormCollection form)
        {
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
            ViewBag.USERID = new SelectList(db.USER, "USERID", "ACCOUNT", cOMMENT.USERID);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK bOOK = db.BOOK.Find(id);
            if (bOOK == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookid = id.Value;
            var comments = db.COMMENT.Where(d => d.BOOKID.Equals(id.Value)).ToList();
            ViewBag.Comments = comments;
            var ratings = db.COMMENT.Where(d => d.BOOKID.Equals(id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.RATING.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }
            return View(bOOK);
        }

        // GET: BOOKs/Create
        public ActionResult Create()
        {
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
            return View();
        }

        // POST: BOOKs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BOOKID,BOOKNAME,DESCRIPTIONS,TYPEID,PUBLISHID,AMOUNT,AUTHORID,PIC1,PIC2,PIC3,PIC4,PRICE")] BOOK bOOK)
        {
            if (ModelState.IsValid)
            {
                db.BOOK.Add(bOOK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME", bOOK.AUTHORID);
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME", bOOK.TYPEID);
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME", bOOK.PUBLISHID);
            return View(bOOK);
        }

        // GET: BOOKs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK bOOK = db.BOOK.Find(id);
            if (bOOK == null)
            {
                return HttpNotFound();
            }
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME", bOOK.AUTHORID);
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME", bOOK.TYPEID);
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME", bOOK.PUBLISHID);
            return View(bOOK);
        }

        // POST: BOOKs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BOOKID,BOOKNAME,DESCRIPTIONS,TYPEID,PUBLISHID,AMOUNT,AUTHORID,PIC1,PIC2,PIC3,PIC4,PRICE")] BOOK bOOK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bOOK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME", bOOK.AUTHORID);
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME", bOOK.TYPEID);
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME", bOOK.PUBLISHID);
            return View(bOOK);
        }

        // GET: BOOKs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK bOOK = db.BOOK.Find(id);
            if (bOOK == null)
            {
                return HttpNotFound();
            }
            return View(bOOK);
        }

        // POST: BOOKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BOOK bOOK = db.BOOK.Find(id);
            db.BOOK.Remove(bOOK);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
