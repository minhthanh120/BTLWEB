using OnlineBookStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBookStore.Models
{
    public class CartItem
    {
        public BOOK _shopping_product { get; set; }
        public int _shopping_quantity { get; set; }

    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(BOOK _pro, int _quantity = 1)
        {
            var item = items.FirstOrDefault(s => s._shopping_product.BOOKID == _pro.BOOKID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity
                });
            }
            else
            {
                item._shopping_quantity += _quantity;
            }
        }
        public void Update_Quantity_Shopping(int id, int _quantity)
        {
            var item = items.Find(s => s._shopping_product.BOOKID == id);
            if (item != null)
            {
                item._shopping_quantity = _quantity;
            }
        }
        public int Total_Money()
        {
            var total = items.Sum(s => s._shopping_product.PRICE * s._shopping_quantity);
            return total;
        }
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._shopping_product.BOOKID == id);
        }
        public int Total_Quantity()
        {
            return items.Sum(s => s._shopping_quantity);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}