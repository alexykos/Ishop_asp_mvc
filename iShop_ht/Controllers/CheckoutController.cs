using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iShop_ht.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace iShop_ht.Controllers
{
    public class CheckoutController : Controller
    {
        Ishop_Entities StoreDB = new Ishop_Entities();

        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            ViewData["Deliveries"] = from dlvr in StoreDB.I_deliveries
                                     where dlvr.Disabled != true
                                     orderby dlvr.Sort
                                    select new SelectListItem { Text = dlvr.Name, Value = dlvr.Code.ToString()};
            return View();
        }
        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {

            var order = new Order();
            TryUpdateModel(order);

            //try
            //{


            order.Username = User.Identity.Name;
            order.OrderDate = DateTime.Now;

            //Save Order
            StoreDB.Orders.Add(order);
            StoreDB.SaveChanges();
            //Process the order
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.CreateOrder(order);

            //SendEmailAsync("alexykos@mail.ru");

            // string lMail = "mail";
            //string lSMTP, lLogin, lPassword, lComment;
            //int lPort;
            //    lSMTP = "smtp.mail.ru";
            //    lLogin = "alextestsendmail@mail.ru";
            //    lPassword = "U654321u";
            //    lComment = "mail";
            //    lPort = 587;

            //// отправитель - устанавливаем адрес и отображаемое в письме имя
            //MailAddress from = new MailAddress(lLogin, lComment);
            //// кому отправляем
            //MailAddress to = new MailAddress("alexykos@mail.ru");
            //// создаем объект сообщения
            //MailMessage m = new MailMessage(from, to);
            //// тема письма
            //m.Subject = "Тест";
            //// текст письма
            //m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            //// письмо представляет код html
            //m.IsBodyHtml = true;
            //// адрес smtp-сервера и порт, с которого будем отправлять письмо
            //SmtpClient smtp = new SmtpClient(lSMTP, lPort);
            //// логин и пароль
            //smtp.Credentials = new NetworkCredential(lLogin, lPassword);
            //smtp.EnableSsl = true;
            //smtp.Send(m);

            TempData["OrderId"] = order.OrderId;

            EmailModel model = new EmailModel();
            model.To = values["Email"];
            //new EmailController().SendEmail(model).Deliver();

            var verificationEmail = new EmailController().SendEmail(model, order.OrderId);
            Task.Run(() => verificationEmail.Deliver());

            //new EmailController().SendEmail(model).DeliverAsync();
            /*
            var verificationEmail = new EmailController().SendEmail(model);
            Task.Run(() => verificationEmail.Deliver());
            */
            //new EmailController().SendEmailAsync("alexykos@mail.ru");
            return RedirectToAction("Complete", new { id = order.OrderId });

            //}
            //catch (Exception e)
            //{

            //    Console.WriteLine($"Ошибка: {e.Message}");
            //    //Invalid - redisplay with errors
            //    return View(order);
            //}
        }
        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = StoreDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                //var result = 

                //    from ord in StoreDB.Orders
                //             join det in StoreDB.OrderDetails on ord.OrderId equals det.OrderId
                //    where ord.OrderId == id
                //    select new { Date = ord.OrderDate, I_commodity_Code = det.I_commodity_Code, Quantity = det.Quantity, Price = det.Price};
                ////ViewData["complete"] = result.ToList();
                //ViewBag.Model = result.ToList();

                decimal? total = (from ord in StoreDB.OrderDetails
                                  where ord.OrderId == id
                                  select (int?)ord.Quantity *
                                  ord.Price).Sum();
                /*decimal? DeliveryCost = (from ord in StoreDB.Orders
                            where ord.OrderId == id
                            join delivery in StoreDB.I_deliveries on ord.Delivery equals delivery.Code into deliveryEmpty
                            from delivery in deliveryEmpty.DefaultIfEmpty()
                            select (decimal?)(delivery == null ? 0 : delivery.Price)).Sum();
                */
                var deliveryVar = (from ord in StoreDB.Orders
                                         where ord.OrderId == id
                                         join delivery in StoreDB.I_deliveries on ord.Delivery equals delivery.Code into deliveryEmpty
                                         from delivery in deliveryEmpty.DefaultIfEmpty()
                                         select new {Name = delivery ==null ? "": delivery.Name
                                                    , Price = delivery == null ? 0 : delivery.Price }
                                  );

                string deliveryName = "";
                decimal deliveryPrice = 0;
                foreach (var dlvr in deliveryVar)
                {
                    deliveryName = dlvr.Name;
                    deliveryPrice = dlvr.Price;
                }



                ViewData["orderTotal"] = String.Format("{0:### ### ###}", total + deliveryPrice);
                ViewData["orderId"] = id;
                ViewData["deliveryName"] = deliveryName;
                ViewData["deliveryPrice"] = String.Format("{0:### ### ###}", deliveryPrice);
                IEnumerable<OrderDetail> OrderDetails = StoreDB.OrderDetails.Where(x => x.OrderId == id);
                return View(OrderDetails);
            }
            else
            {
                return View("Error");
            }
        }


    }
}