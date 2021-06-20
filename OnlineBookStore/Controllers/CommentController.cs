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
    public class CommentController : Controller
    {
        DBModel db = new DBModel()
        {
        };
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comment/Create
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

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public bool isLoggedIn()
        {
            if (Session["Admin"] == null) return false;
            else return true;
        }
        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            if (isLoggedIn())
            {
                COMMENTDTO dto = new COMMENTDTO();
                dto = db.Database.SqlQuery<COMMENTDTO>("SELECT c.BOOKID,c.COMMENT,c.COMMENTDATE,c.COMMENTID,c.RATING,c.USERID,u.USERNAME from COMMENT c JOIN [USER] u on c.USERID=u.USERID where c.bookid='" + id + "' order by c.rating asc ").FirstOrDefault();
                return View(dto);
            }
            else return RedirectToAction("Login", "User");

            
        }

        // POST: Comment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                COMMENT c = db.COMMENT.Find(id);
                int? go = c.BOOKID;
                db.COMMENT.Remove(c);
                db.SaveChanges();
                return RedirectToAction("Details","Book", new { id =go});
            }
            catch
            {
                return View();
            }
        }
    }
}
