using OnlineBookStore.Models;
using OnlineBookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace OnlineBookStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        DBModel db = new DBModel();
        // GET: ShoppingCart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddtoCart(int id)
        {
            var pro = db.BOOK.SingleOrDefault(s => s.BOOKID == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult AddtoCart1(int id)
        {
            var pro = db.BOOK.SingleOrDefault(s => s.BOOKID == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("Details", "BOOKS", new { id = id });
        }
        public ActionResult ShowToCart()
        {
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
            if (Session["Cart"] == null)
                return RedirectToAction("ShowToCart", "ShoppingCart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Shopping(id_pro, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int _t_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                _t_item = cart.Total_Quantity();
            }
            ViewBag.infoCart = _t_item;
            return PartialView("BagCart");
        }
        public ActionResult Shopping_Sucess()
        {
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
            return View();
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
        public ActionResult Edit(USER uSER, FormCollection form)
        {
            ViewBag.AUTHORID = new SelectList(db.AUTHOR, "AUTHORID", "AUTHORNAME");
            ViewBag.TYPEID = new SelectList(db.BOOKTYPE, "TYPEID", "TYPENAME");
            ViewBag.PUBLISHID = new SelectList(db.PUBLISH, "PUBLISHID", "PUBLISHNAME");
            if (ModelState.IsValid)
            {
                Cart cart = Session["Cart"] as Cart;
                BILL bILL = new BILL();
                bILL.USERID = int.Parse(form["USERID"]);
                bILL.BILLDATE = DateTime.Now;
                bILL.USERNAME = form["USERNAME"];
                bILL.ADDRESS = form["ADDRESS"];
                bILL.PHONE = form["PHONE"];
                bILL.TOTALBILL = cart.Total_Money();
                bILL.EMAIL = form["EMAIL"];
                db.BILL.Add(bILL);
                db.SaveChanges();
                foreach (var item in cart.Items)
                {
                    DETAILBILL dETAILBILL = new DETAILBILL();
                    dETAILBILL.BILLID = bILL.BILLID;
                    dETAILBILL.AMOUNT = item._shopping_quantity;
                    dETAILBILL.PRICE = item._shopping_product.PRICE;
                    dETAILBILL.BOOKID = item._shopping_product.BOOKID;
                    db.Database.ExecuteSqlCommand("insert into detailbill(BILLID, AMOUNT, PRICE, BOOKID) values('" + dETAILBILL.BILLID + "',N'" + dETAILBILL.AMOUNT + "',N'" + dETAILBILL.PRICE + "',N'" + dETAILBILL.BOOKID + "')");

                }
                db.Entry(uSER).State = EntityState.Modified;
                cart.ClearCart();
                return RedirectToAction("Shopping_Sucess", "ShoppingCart");
            }
            else
            {
                return Content("Có lỗi! Vui lòng kiểm tra lại thông tin");
            }
        }
    }
}