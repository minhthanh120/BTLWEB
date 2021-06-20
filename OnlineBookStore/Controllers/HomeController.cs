using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models.Entities;
using OnlineBookStore.Models.DAO;
using System.Data.Entity;
namespace OnlineBookStore.Controllers
{
    public class HomeController : Controller
    {
        private DBModel db = new DBModel();

        public ActionResult Index()
        {
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
            return View();
        }


        public PartialViewResult Publish()
        {
            var pb = db.PUBLISH;
            return PartialView(pb);
        }
        public PartialViewResult Author()
        {
            var author = db.AUTHOR;
            return PartialView(author);
        }
        public PartialViewResult Slide()
        {
            return PartialView();
        }
        public PartialViewResult NewBook()
        {
            var bOOK = db.BOOK.Include(b => b.AUTHOR).Include(b => b.BOOKTYPE).Include(b => b.PUBLISH);
            return PartialView(bOOK.ToList());
        }
        public PartialViewResult Booktype()
        {
            var list = db.BOOKTYPE;
            return PartialView(list);
        }
        public PartialViewResult NewBook1()
        {
            var bOOK = db.BOOK.Include(b => b.AUTHOR).Include(b => b.BOOKTYPE).Include(b => b.PUBLISH);
            return PartialView(bOOK.ToList());
        }
        public PartialViewResult NewBook2()
        {
            var bOOK = db.BOOK.Include(b => b.AUTHOR).Include(b => b.BOOKTYPE).Include(b => b.PUBLISH);
            return PartialView(bOOK.ToList());
        }
        public PartialViewResult NewBook3()
        {
            var bOOK = db.BOOK.Include(b => b.AUTHOR).Include(b => b.BOOKTYPE).Include(b => b.PUBLISH);
            return PartialView(bOOK.ToList());
        }
    }
}
