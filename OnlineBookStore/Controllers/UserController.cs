using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineBookStore.Models.Entities;
using System.Data.SqlClient;
using OnlineBookStore.Models.DAO;
using System.Data.Entity;
using System.IO;

namespace OnlineBookStore.Controllers
{
    public class UserController : Controller
    {
        DBModel db = new DBModel();
        public static string constr = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=BOOKSTORE;Integrated Security=True";
        public bool isLoggedIn()
        {
            if (Session["Admin"] == null) return false;
            else return true;
        }
        public string toHidden(string input)
        {
            string res = "";
            for (int i = 0; i < input.Length; i++)
                res += "*";
            return res;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginAction(USER us)
        {
            SqlConnection con = new SqlConnection(constr);
            string query = "Select * From dbo.[USER]" +
                            " Where ACCOUNT = '" + us.ACCOUNT + 
                            "' and PASSWORD = '" + us.PASSWORD + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                // Admin
                if (reader["ADMRIGHT"].Equals(true))
                {
                    int adminID = Int32.Parse(reader["USERID"].ToString());
                    USERDAO dao = new USERDAO();
                    USER u =new USER();
                    u = dao.SEARCHID(adminID);
                    Session["UN"] = u.USERNAME;
                    Session["ID"] = u.USERID;
                    Session["AVT"] = u.AVATAR;
                    Session["Admin"] = dao.SEARCHID(adminID);
                    return RedirectToAction("AdminInfo", "User", new { id = adminID });
                }
                // Người dùng
                else
                {
                    int Clientid = Int32.Parse(reader["USERID"].ToString());
                    USERDAO dao = new USERDAO();
                    Session["Client"] = dao.SEARCHID(Clientid);
                    Session["User"] = us.ACCOUNT;
                    Session["Userid"] = Clientid;
                    return RedirectToAction("Index", "Home", new { id=Clientid});
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult AdminInfo(int id)
        {
            if (isLoggedIn())
            {
                USERDAO dao = new USERDAO();
                ViewBag.HiddenPass = toHidden(dao.SEARCHID(id).PASSWORD);
                return View(dao.SEARCHID(id));
            }
            else return RedirectToAction("Login", "User");
        }

        public ActionResult AdminEdit(int id)
        {
            if (isLoggedIn())
            {
                USERDAO dao = new USERDAO();
                return View(dao.SEARCHID(id));
            }
            else return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public ActionResult AdminEdit(int id, USER us)
        {
            if(isLoggedIn())
            {
                if (us.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(us.ImageUpload.FileName);
                    string extension = Path.GetExtension(us.ImageUpload.FileName);
                    fileName = fileName + extension;
                    us.AVATAR = "~/Image/" + fileName;
                    us.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Image/"), fileName));
                }
                db.Entry(us).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminInfo", "User", new { id = us.USERID });
            }
            else return RedirectToAction("Login", "User");
        }

        public ActionResult AdminCreate()
        {
            if(isLoggedIn())
            {
                USER us = new USER();
                return View(us);
            } 
            else return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public ActionResult AdminCreate(USER us)
        {
            if(isLoggedIn())
            {
                if (us.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(us.ImageUpload.FileName);
                    string extension = Path.GetExtension(us.ImageUpload.FileName);
                    fileName = fileName + extension;
                    us.AVATAR = "~/Image/" + fileName;
                    us.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Image/"), fileName));
                }
                USERDAO dao = new USERDAO();
                //return RedirectToAction("Index", "Manager");
                return RedirectToAction("AdminInfo", "User", new 
                { id = dao.AddAdmin(us.USERNAME, us.ACCOUNT, us.PASSWORD, us.ADDRESS, us.PHONE, us.EMAIL, us.AVATAR) });
            }
            else return RedirectToAction("Login", "User");
        }

        public ActionResult AdminDelete(int id)
        {
            if(isLoggedIn())
            {
                USERDAO dao = new USERDAO();
                return View(dao.SEARCHID(id));
            }
            else return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public ActionResult AdmDelAction(int id)
        {
            if(isLoggedIn())
            {
                USERDAO dao = new USERDAO();
                dao.DeleteAdmin(id);
                return RedirectToAction("Login", "User");
            }
            else return RedirectToAction("Login", "User");
        }

        public ActionResult AdmChgPass(int id)
        {
            USERDAO dao = new USERDAO();
            return View(dao.SEARCHID(id));
        }

        [HttpPost]
        public ActionResult AdmChgPass(int id, string CurPass, string NewPass, string Retype)
        {
            USERDAO dao = new USERDAO();
            if(CurPass == dao.SEARCHID(id).PASSWORD)
            {
                if(NewPass == Retype)
                {
                    dao.UpdatePass(id, NewPass);
                    return RedirectToAction("AdminInfo", "User", new { id = id });
                }
                else
                {
                    ViewBag.WrongPass = "Bạn gõ lại mật khẩu sai";
                    return View(dao.SEARCHID(id));
                }
            }
            else
            {
                ViewBag.WrongPass = "Bạn đã điền sai mật khẩu hiện tại";
                return View(dao.SEARCHID(id));
            }    
        }

        public ActionResult LogOut()
        {
            if (Session["Admin"] != null)
            {
                Session.Abandon();
                return RedirectToAction("Login", "User");
            }
            else if (Session["Client"] != null)
            {
                Session.Abandon();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Details(int? id)
        {
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
            USER uSER = db.USER.Find(id);
            return View(uSER);
        }
        public ActionResult Edit(int? id)
        {
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
            USER uSER = db.USER.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);

        }

        // POST: USERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( USER uSER)
        {
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
            if (ModelState.IsValid)
            {
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","User",new {id=Session["Userid"] });

            }

            return View(uSER);
        }
        public ActionResult Create()
        {
            return View();
        }

        // POST: USERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.USER.Add(uSER);
                db.SaveChanges();
                return RedirectToAction("Login","User");
            }

            return View(uSER);
        }
    }
}
