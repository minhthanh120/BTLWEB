using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models.DAO;
using OnlineBookStore.Models.Entities;

namespace OnlineBookStore.Controllers
{
    public class AuthorController : Controller
    {
        public bool isLoggedIn()
        {
            if (Session["Admin"] == null) return false;
            else return true;
        }
        // GET: Author
        public ActionResult Index(string search, int pageNum = 1, int pageSize = 10)
        {
            if(isLoggedIn())
            {
                AUTHORDAO dao = new AUTHORDAO();
                if (search != "" && search != null) return View(dao.SEARCHNAME(search, pageNum, pageSize));
                else return View(dao.AUTHORLIST(pageNum, pageSize));
            }
            else return RedirectToAction("Login", "User");
        }
        public ActionResult Create()
        {
            if(isLoggedIn())
                return View();
            else return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public ActionResult Create(AUTHOR author)
        {
            if(isLoggedIn())
            {
                AUTHORDAO dao = new AUTHORDAO();
                dao.INSERT(author.AUTHORNAME);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Login", "User");
        }
        public ActionResult Edit(int id)
        {
            if (isLoggedIn())
            {
                AUTHORDAO dao = new AUTHORDAO();
                return View(dao.SEARCHID(id));
            }
            else return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public ActionResult Edit(int id, AUTHOR au)
        {
            if (isLoggedIn())
            {
                AUTHORDAO dao = new AUTHORDAO();
                dao.UPDATE(au);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Login", "User");
        }
        public ActionResult Details(int id)
        {
            if (isLoggedIn())
            {
                AUTHORDAO dao = new AUTHORDAO();
                return View(dao.SEARCHID(id));
            }
            else return RedirectToAction("Login", "User");
        }
        public ActionResult Delete(int id)
        {        
            if (isLoggedIn())
            {
                AUTHORDAO dao = new AUTHORDAO();
                return View(dao.SEARCHID(id));
            }
            else return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public ActionResult Delete(int id, AUTHOR au)
        {
            if (isLoggedIn()) 
            {
                AUTHORDAO dao = new AUTHORDAO();
                dao.DELETE(id);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Login", "User");
        }
    }
}