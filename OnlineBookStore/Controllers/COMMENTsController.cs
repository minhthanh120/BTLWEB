using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models.Entities;

namespace OnlineBookStore.Controllers
{
    public class COMMENTsController : Controller
    {
        private DBModel db = new DBModel();

        // GET: COMMENTs
        public ActionResult Index()
        {
            var cOMMENT = db.COMMENT.Include(c => c.BOOK).Include(c => c.USER);
            return View(cOMMENT.ToList());
        }

        // GET: COMMENTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMENT cOMMENT = db.COMMENT.Find(id);
            if (cOMMENT == null)
            {
                return HttpNotFound();
            }
            return View(cOMMENT);
        }

        // GET: COMMENTs/Create
        public ActionResult Create()
        {
            ViewBag.BOOKID = new SelectList(db.BOOK, "BOOKID", "BOOKNAME");
            ViewBag.USERID = new SelectList(db.USER, "USERID", "ACCOUNT");
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
            return View();
        }

        // POST: COMMENTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(COMMENT cOMMENT,int ?id)
        {
            if (ModelState.IsValid)
            {
                db.COMMENT.Add(cOMMENT);
                db.SaveChanges();
                return RedirectToAction("Details","BOOKS",new  {id=cOMMENT.BOOKID });
            }
            ViewBag.BOOKID = new SelectList(db.BOOK, "BOOKID", "BOOKNAME", cOMMENT.BOOKID);
            ViewBag.USERID = new SelectList(db.USER, "USERID", "ACCOUNT", cOMMENT.USERID);
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
            
            return View(cOMMENT);
        }

        // GET: COMMENTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMENT cOMMENT = db.COMMENT.Find(id);
            if (cOMMENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.BOOKID = new SelectList(db.BOOK, "BOOKID", "BOOKNAME", cOMMENT.BOOKID);
            ViewBag.USERID = new SelectList(db.USER, "USERID", "ACCOUNT", cOMMENT.USERID);
            return View(cOMMENT);
        }

        // POST: COMMENTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "COMMENTID,BOOKID,USERID,COMMENTDATE,COMMENT1,RATING")] COMMENT cOMMENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMMENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BOOKID = new SelectList(db.BOOK, "BOOKID", "BOOKNAME", cOMMENT.BOOKID);
            ViewBag.USERID = new SelectList(db.USER, "USERID", "ACCOUNT", cOMMENT.USERID);
            return View(cOMMENT);
        }

        // GET: COMMENTs/Delete/5
        public ActionResult Delete(int? id)
        {
            COMMENT cOMMENT = db.COMMENT.Find(id);
            db.COMMENT.Remove(cOMMENT);
            db.SaveChanges();
            return RedirectToAction("Details", "BOOKS", new { id = cOMMENT.BOOKID });
        }

        // POST: COMMENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            COMMENT cOMMENT = db.COMMENT.Find(id);
            db.COMMENT.Remove(cOMMENT);
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
