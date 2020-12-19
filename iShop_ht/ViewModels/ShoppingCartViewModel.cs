using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iShop_ht.Models;



namespace iShop_ht.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }

    }
}