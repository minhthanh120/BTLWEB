using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models.DAO;
using OnlineBookStore.Models.Entities;

namespace OnlineBookStore.Controllers
{
    public class TypeController : Controller
    {
        public bool isLoggedIn()
        {
            if (Session["Admin"] == null) return false;
            else return true;
        }
        // GET: Type
        public ActionResult Index(string search, int pageNum = 1, int pageSize = 5)
        {
            if(isLoggedIn())
            {
                TYPEDAO dao = new TYPEDAO();
                if (search != "" && search != null) return View(dao.SEARCHNAME(search, pageNum, pageSize));
                else return View(dao.TYPELIST(pageNum, pageSize));
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
        public ActionResult Create(BOOKTYPE type)
        {
            if(isLoggedIn())
            {
                TYPEDAO dao = new TYPEDAO();
                dao.INSERT(type.TYPENAME);
                return RedirectToAction("Index");
            }   
            else return RedirectToAction("Login", "User");
        }
        public ActionResult Edit(int id)
        {
            if (isLoggedIn())
            {
                TYPEDAO dao = new TYPEDAO();
                return View(dao.SEARCHID(id));
            }
            else return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public ActionResult Edit(int id, BOOKTYPE type)
        {
            if (isLoggedIn())
            {
                TYPEDAO dao = new TYPEDAO();
                dao.UPDATE(type);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Login", "User");
        }
        public ActionResult Details(int id)
        {
            if (isLoggedIn())
            {
                TYPEDAO dao = new TYPEDAO();
                return View(dao.SEARCHID(id));
            }
            else return RedirectToAction("Login", "User");
        }
        public ActionResult Delete(int id)
        {
            if (isLoggedIn())
            {
                TYPEDAO dao = new TYPEDAO();
                return View(dao.SEARCHID(id));
            }
            else return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public ActionResult Delete(int id, BOOKTYPE type)
        {
            if (isLoggedIn())
            {
                TYPEDAO dao = new TYPEDAO();
                dao.DELETE(id);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Login", "User");
        }
    }
}