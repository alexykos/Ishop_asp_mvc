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

            From = "alextestsendmail@mail.ru";//model.From;

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
            ViewData["orderTotal"] = total;
            ViewData["orderiD"] = orderId;
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