using iShop_ht.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using iShop_ht.Models;
using iShop_ht.ViewModels;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace iShop_ht.Controllers
{
    public class HomeController : Controller
    {

        BookContext db = new BookContext();
        Ishop_Entities StoreDB = new Ishop_Entities();
        public ActionResult Index(int? ClassId)
        {

            return View();
        }

        public ActionResult BestBook(int? ClassId)

        {

            if (Request.IsAjaxRequest())
            {
                int a = 1;
            }
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books.Where(n => n.ClassId == ClassId || ClassId == null);
            // возвращаем представление
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult _TreeList2()
        {
            //IEnumerable<I_class> i_classes = StoreDB.I_classes;
            ////записываем phones в динамическое свойство ViewBag
            //ViewBag.I_classes = i_classes.OrderBy(n=>n.Upcode);
            var clss = from cls in db.Classes
                           //join cmm in StoreDB.I_commodities on cls.Code equals cmm.I_class_code
                       where db.Books.Any(p => p.ClassId == cls.Id || cls.Upcode == 0)
                       select cls;

            return PartialView(clss);
        }

        [ChildActionOnly]
        public ActionResult _TreeList()
        {
            var clss = from cls in StoreDB.I_classes
                           //join cmm in StoreDB.I_commodities on cls.Code equals cmm.I_class_code
                       where StoreDB.I_commodities.Any(p => p.I_class_code == cls.Code || cls.Isgroup == true)
                       select cls;

            return PartialView(clss);

        }

        public ActionResult IndexClassPart(int? ClassId, int? page)
        {

            if (Request.IsAjaxRequest())
            {
                int a = 1;
            }
            if (ClassId != null) { TempData["currentClass"] = ClassId; }


            int currentClass = 0;
            if (TempData["currentClass"] != null)
            {
                currentClass = int.Parse(TempData["currentClass"].ToString());
                TempData.Keep();
            }

            if (TempData["currentClass"] != null)
            {
                currentClass = int.Parse(TempData["currentClass"].ToString());
                TempData.Keep();
            }


            int pageSize = 3;
            int pageNumber = page??1;



            System.Data.SqlClient.SqlParameter param_class = new System.Data.SqlClient.SqlParameter("Class", currentClass);

            var I_commodities = StoreDB.Database.SqlQuery<I_commodity>("GetI_commodities_class @Class ", param_class).ToList();

            //return View(I_commodities.OrderBy(s => s.Price).ToPagedList(pageNumber, pageSize));

            //return PartialView(I_commodities);
            return PartialView(I_commodities.OrderBy(s => s.Price).ToPagedList(pageNumber, pageSize));

        }

        [HttpGet]
        public ActionResult Commodity(int id = 0)
        {

            var commodity2 = from cm in StoreDB.I_commodities
                             join img in StoreDB.I_images on cm.Code equals img.Upcode
                             where cm.Code == id && img.Ext == "txt"
                             select new Commodity_property { Code = cm.Code, Name = cm.Name, Price = cm.Price, Property = img.Img };



            return View(commodity2);

        }
        [HttpGet]
        public ActionResult Contacts(int id = 0)
        {
           
            return View();

        }

        [HttpGet]
        public ActionResult About(int id = 0)
        {

            return View();

        }
        [HttpGet]
        public ActionResult Delivery(int id = 0)
        {

            return View();

        }

        public void ReturnImage(int id)

        {

            var Images = StoreDB.I_images.Where(x => x.Upcode == id && x.Ext == "JPG").FirstOrDefault();
            var img = Images.Img150x150;
        }

        [ChildActionOnly]
        public ActionResult breadcrumbs_tree()
        {
            int currentClass = 0;
            if (TempData["currentClass"] != null)
            {
                currentClass = int.Parse(TempData["currentClass"].ToString());
                TempData.Keep();
            }
            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("currentClass", currentClass);
            var I_classes = StoreDB.Database.SqlQuery<I_class>("GetBreadcrumbs_tree	@currentClass", param);

            return PartialView(I_classes);
        }

    }
}