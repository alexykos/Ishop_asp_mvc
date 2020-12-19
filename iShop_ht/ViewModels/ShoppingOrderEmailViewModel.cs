using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iShop_ht.Models;

namespace iShop_ht.ViewModels
{
    public class ShoppingOrderEmailViewModel
    {
        public List<Order> OrderItem { get; set; }
        public List<OrderDetail> OrderDetailItem { get; set; }

    }
}