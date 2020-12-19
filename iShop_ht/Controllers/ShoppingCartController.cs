using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iShop_ht.Models;
using iShop_ht.ViewModels;

namespace iShop_ht.Controllers
{
    public class ShoppingCartController : Controller
    {
        Ishop_Entities StoreDB = new Ishop_Entities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        //public ActionResult AddToCart(int id)
        //{
        //    // Retrieve the album from the database
        //    var addedAlbum = StoreDB.Phones
        //        .Single(phone => phone.Id == id);

        //    // Add it to the shopping cart
        //    var cart = ShoppingCart.GetCart(this.HttpContext);

        //    cart.AddToCart(addedAlbum);

        //    // Go back to the main store page for more shopping
        //    return RedirectToAction("Index");
        //    //ViewData["CartCount"] = cart.GetCount();
        //    //return PartialView("CartSummary");

        //}
        ////
        //[HttpPost]
        //public RedirectToRouteResult AddToCart(int id, string returnUrl)
        //{
        //    // Retrieve the album from the database
        //    var addedAlbum = StoreDB.Phones
        //        .Single(phone => phone.Id == id);

        //    // Add it to the shopping cart
        //    var cart = ShoppingCart.GetCart(this.HttpContext);

        //    cart.AddToCart(addedAlbum);

        //    // Go back to the main store page for more shopping
        //    return RedirectToAction("Index", new { returnUrl });


        //}
        //
        [HttpPost]
        //public EmptyResult AddToCart(int id)
        //{
        //    // Retrieve the album from the database
        //    var addedAlbum = StoreDB.Phones
        //        .Single(phone => phone.Id == id);

        //    // Add it to the shopping cart
        //    var cart = ShoppingCart.GetCart(this.HttpContext);

        //    cart.AddToCart(addedAlbum);

        //    // Go back to the main store page for more shopping
        //    //return RedirectToAction("Index", new { returnUrl });
        //    return new EmptyResult();

        //}

        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var addedAlbum = StoreDB.I_commodities
                .Single(phone => phone.Code == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            string cartId = cart.GetCartId(this.HttpContext);

            int itemCount = cart.AddToCart(addedAlbum);

            int RecordId = StoreDB.Carts
                .Single(item => item.I_commodityCode == id && item.CartId == cartId).RecordId;

            var results = new ShoppingCartRemoveViewModel
            {

                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = RecordId
            };
            return Json(results);

        }

        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult MinusFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string albumName = StoreDB.Carts
                .Single(item => item.RecordId == id).I_commodity.Name;


            // Remove from cart
            int itemCount = cart.MinusFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) +
                    " has been minus from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string albumName = StoreDB.Carts
                .Single(item => item.RecordId == id).I_commodity.Name;


            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}