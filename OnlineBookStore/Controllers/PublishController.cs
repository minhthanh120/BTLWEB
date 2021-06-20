using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models.DAO;
using OnlineBookStore.Models.Entities;

namespace OnlineBookStore.Controllers
{
    public class PublishController : Controller
    {
        public bool isLoggedIn()
        {
            if (Session["Admin"] == null) return false;
            else return true;
        }
        // GET: Publish
        public ActionResult Index(string search, int pageNum = 1, int pageSize = 5)
        {
            if(isLoggedIn())
            {
                PUBLISHDAO dao = new PUBLISHDAO();
                if (search != "" && search != null) return View(dao.SEARCHNAME(search, pageNum, pageSize));
                else return View(dao.PUBLISHLIST(pageNum, pageSize));
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
        public ActionResult Create(PUBLISH publish)
        {
            if(isLoggedIn())
            {
                PUBLISHDAO dao = new PUBLISHDAO();
                dao.INSERT(publish.PUBLISHNAME);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Login", "User");
        }
        public ActionResult Edit(int id)
        {
            if (isLoggedIn())
            {
                PUBLISHDAO dao = new PUBLISHDAO();
                return View(dao.SEARCHID(id));
            }
            else return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public ActionResult Edit(int id, PUBLISH pub)
        {
            if (isLoggedIn())
            {
                PUBLISHDAO dao = new PUBLISHDAO();
                dao.UPDATE(pub);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Login", "User");
        }
        public ActionResult Details(int id)
        {
            if (isLoggedIn())
            {
                PUBLISHDAO dao = new PUBLISHDAO();
                return View(dao.SEARCHID(id));
            }
            else return RedirectToAction("Login", "User");
        }
        public ActionResult Delete(int id)
        {
            if (isLoggedIn())
            {
                PUBLISHDAO dao = new PUBLISHDAO();
                return View(dao.SEARCHID(id));
            }
            else return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public ActionResult Delete(int id, PUBLISH pub)
        {
            if (isLoggedIn())
            {
                PUBLISHDAO dao = new PUBLISHDAO();
                dao.DELETE(id);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Login", "User");
        }
    }
}