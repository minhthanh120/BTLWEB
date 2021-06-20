using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models.DAO;
using System.IO;
using OnlineBookStore.Models.DTO;
using OnlineBookStore.Models.Entities;

namespace OnlineBookStore.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        DBModel db = new DBModel();
        public ActionResult Index(string input, string option, string submit, int PageNum = 1, int PageSize = 10)
        {
            BOOKDAO dao = new BOOKDAO();
            return View(dao.lstBOOK(PageNum, PageSize));
        }
    }
}