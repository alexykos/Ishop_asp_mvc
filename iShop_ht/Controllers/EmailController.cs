using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionMailer.Net.Mvc;
using iShop_ht.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace iShop_ht.Controllers
{
    public class EmailController : MailerBase
    {
        Ishop_Entities StoreDB = new Ishop_Entities();
        // GET: Email
        public EmailResult SendEmail(EmailModel model, int orderId)
        {
            To.Add(model.To);

            From = "info@ht-comp.ru";//model.From; //"alextestsendmail@mail.ru";//

            Subject = "Спасибо за заказ.";//model.Subject;


            //int OrderId = 0;
            //if (TempData["OrderId"] != null)
            //{
            //    OrderId = int.Parse(TempData["OrderId"].ToString());

            //}

            decimal? total = (from ord in StoreDB.OrderDetails
                              where ord.OrderId == orderId
                              select (int?)ord.Quantity *
                              ord.Price).Sum();

            var deliveryVar = (from ord in StoreDB.Orders
                               where ord.OrderId == orderId
                               join delivery in StoreDB.I_deliveries on ord.Delivery equals delivery.Code into deliveryEmpty
                               from delivery in deliveryEmpty.DefaultIfEmpty()
                               select new
                               {
                                   Name = delivery == null ? "" : delivery.Name
                                          ,
                                   Price = delivery == null ? 0 : delivery.Price
                               }
                                  );

            string deliveryName = "";
            decimal deliveryPrice = 0;
            foreach (var dlvr in deliveryVar)
            {
                deliveryName = dlvr.Name;
                deliveryPrice = dlvr.Price;
            }

            ViewData["orderTotal"] = String.Format("{0:### ### ###}", total + deliveryPrice) ;
            ViewData["orderiD"] = orderId;
            ViewData["deliveryName"] = deliveryName;
            ViewData["deliveryPrice"] = String.Format("{0:### ### ###}", deliveryPrice);
            IEnumerable<OrderDetail> OrderDetails = StoreDB.OrderDetails.Where(x => x.OrderId == orderId);

            return Email("SendEmail", OrderDetails);
        }

        public async Task SendEmailAsync(string to_client)
        {
            MailAddress from = new MailAddress("alextestsendmail@mail.ru", "Tom");
            MailAddress to = new MailAddress(to_client);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Тест1";
            m.Body = "Письмо-тест 2 работы smtp-клиента";
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("alextestsendmail@mail.ru", "U654321u");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);

        }

    }
}