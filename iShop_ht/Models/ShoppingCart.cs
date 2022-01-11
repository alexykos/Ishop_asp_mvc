using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using System.Data.Entity;

namespace iShop_ht.Models
{
    public class ShoppingCart
    {

        Ishop_Entities StoreDB = new Ishop_Entities();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        //
        public int AddToCart(I_commodity i_commodity)
        {
            // Get the matching cart and album instances
            var cartItem = StoreDB.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.I_commodityCode == i_commodity.Code);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                //DateTime d = DateTime.Now;
                //new DateTime(2008, 3, 9, 16, 5, 7, 123);
                //DateTime.Now;
                //string dateString = d.ToString("yyyyMMddHHmmss").;

                cartItem = new Cart
                {
                    I_commodityCode = i_commodity.Code,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                StoreDB.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            StoreDB.SaveChanges();

            return cartItem.Count;
        }

        /////////////////////////
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = StoreDB.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {

                StoreDB.Carts.Remove(cartItem);
                // Save changes
                StoreDB.SaveChanges();
            }
            return itemCount;
        }

        public int MinusFromCart(int id)
        {
            // Get the cart
            var cartItem = StoreDB.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    StoreDB.Carts.Remove(cartItem);
                }
                // Save changes
                StoreDB.SaveChanges();
            }
            return itemCount;
        }

        /////////////////////
        public void EmptyCart()
        {
            var cartItems = StoreDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                StoreDB.Carts.Remove(cartItem);
            }
            // Save changes
            StoreDB.SaveChanges();
        }

        ////////////////////////////////
        public List<Cart> GetCartItems()
        {
            return StoreDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in StoreDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        ///////////////////////////////////////
        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in StoreDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.I_commodity.Price).Sum();

            return total ?? decimal.Zero;
        }
        //////////////////////////////////////////
        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    I_commodity_Code = item.I_commodityCode,
                    OrderId = order.OrderId,
                    Price = (order.is_company == 0 ? item.I_commodity.Price : item.I_commodity.Price2),
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.I_commodity.Price);

                StoreDB.OrderDetails.Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            StoreDB.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
        }
        ////////////////////////////////////////////
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = StoreDB.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            StoreDB.SaveChanges();
        }

        ///////////////////////////////////////
    }
    }