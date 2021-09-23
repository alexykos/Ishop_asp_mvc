﻿using iShop_ht.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index(int? ClassId, int? page)
        {

            page = 1;
            

            if (ClassId != null) { TempData["currentClass"] = ClassId; }


            int currentClass = 0;

            //if (TempData["currentClass"] != null)
            //{
            //    currentClass = int.Parse(TempData["currentClass"].ToString());
            //    TempData.Keep();
            //}
           
            //if (TempData["currentClass"] != null)
            //{
            //    currentClass = int.Parse(TempData["currentClass"].ToString());
            //    TempData.Keep();
            //}


            int pageSize = 10;
            int pageNumber = page ?? 1;



            System.Data.SqlClient.SqlParameter param_class = new System.Data.SqlClient.SqlParameter("Class", currentClass);
            System.Data.SqlClient.SqlParameter param_isTop = new System.Data.SqlClient.SqlParameter("isTop", 1);
            var I_commodities = StoreDB.Database.SqlQuery<I_commodity>("GetI_commodities_class @Class, @isTop ", param_class, param_isTop).ToList();

            //return View(I_commodities.OrderBy(s => s.Price).ToPagedList(pageNumber, pageSize));

            //return PartialView(I_commodities);
            return View(I_commodities.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult BestBook(int? ClassId)

        {

            //if (Request.IsAjaxRequest())
            //{
            //    int a = 1;
            //}
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books.Where(n => n.ClassId == ClassId || ClassId == null);
            // возвращаем представление
            return PartialView();
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
            //var clss = from cls in StoreDB.I_classes
            //               //join cmm in StoreDB.I_commodities on cls.Code equals cmm.I_class_code
            //           where StoreDB.I_commodities.Any(p => p.I_class_code == cls.Code || cls.Isgroup == true)
            //           select cls;
            var clss = StoreDB.Database.SqlQuery<I_class>("i_getClassForTree").ToList();

            return PartialView(clss);

        }

        public ActionResult IndexClassPart(int? ClassId, int? page, string SortOrder = "", string SearchGoods = "")
        {





            //if (Request.IsAjaxRequest())
            //{
            //    int a = 1;
            //}

            int isTop = 0;

            if (ClassId == null & page == null & SortOrder == "" & SearchGoods == "")
            {
                TempData.Remove("currentClass");
                TempData.Remove("currentSortOrder");
                TempData.Remove("searchGoodsFilt");
                TempData.Remove("lastSortOrder");
                isTop = 1;
            }

            string currentSortOrder = "";
            string lastSortOrder;
            string searchGoodsFilt = "";

            if (ClassId != null) { TempData["currentClass"] = ClassId;
                TempData["searchGoodsFilt"] = null;
                                    }
            if (SortOrder != "") { TempData["currentSortOrder"] = SortOrder; }
            if (SearchGoods != "") { TempData["searchGoodsFilt"] = SearchGoods; }

            int currentClass = 0;
            if (TempData["currentClass"] != null)
            {
                currentClass = int.Parse(TempData["currentClass"].ToString());
                TempData.Keep();
            }

           
                if (TempData["searchGoodsFilt"] != null)
                { 
                    searchGoodsFilt = TempData["searchGoodsFilt"].ToString();
                    TempData.Keep();
                }
           


            if (SortOrder != "")
            {
                if (TempData["lastSortOrder"] != null)
                {
                    lastSortOrder = TempData["lastSortOrder"].ToString();
                    
                }
                else
                {
                    lastSortOrder = "";
                }
                TempData["lastSortOrder"] = SortOrder;

                if (TempData["currentSortOrder"] != null)
                {
                    currentSortOrder = TempData["currentSortOrder"].ToString();
                    // если вызывается процедура с параметром сортировки, то сбрасываем страницы
                    page = 1;

                    if (SortOrder == "Price")
                    {
                        if (lastSortOrder == "Price")
                        {
                            currentSortOrder = "PriceDesc";
                            TempData["lastSortOrder"] = "";
                        }
                        else
                        { currentSortOrder = "Price"; }
                    }
                }

            }
            

            

            int pageSize = 10;
            int pageNumber = page??1;



            System.Data.SqlClient.SqlParameter param_class = new System.Data.SqlClient.SqlParameter("Class", currentClass);
            System.Data.SqlClient.SqlParameter param_search = new System.Data.SqlClient.SqlParameter("searchGoods", searchGoodsFilt);
            var I_commodities = StoreDB.Database.SqlQuery<I_commodity>("GetI_commodities_class @Class, @searchGoods ", param_class, param_search).ToList();


           

            switch (currentSortOrder)
            {
                case "Price":
                    return PartialView(I_commodities.OrderBy(s => s.Price).ToPagedList(pageNumber, pageSize));
                    
                case "PriceDesc":
                    return PartialView(I_commodities.OrderByDescending(s => s.Price).ToPagedList(pageNumber, pageSize));
                    
                default:
                    return PartialView(I_commodities.OrderBy(s => s.Price).ToPagedList(pageNumber, pageSize));
                    
            }
           

            //return PartialView(resultOrder.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult SearchGoods(string name)
        {
            //if (Request.IsAjaxRequest())
            //{
            //    int a = 1;
            //}
            int pageSize = 10
                , pageNumber = 1;
                

            System.Data.SqlClient.SqlParameter param_class = new System.Data.SqlClient.SqlParameter("searchName", name);
            System.Data.SqlClient.SqlParameter param_top = new System.Data.SqlClient.SqlParameter("top", pageSize);

            var I_commodities = StoreDB.Database.SqlQuery<I_commodity>("GetI_commodities_search @searchName, @top ", param_class, param_top).ToList();


            return PartialView("IndexClassPart",I_commodities.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult About()
        {
            //if (Request.IsAjaxRequest())
            //{
            //    int a = 1;
            //}
            var clss = StoreDB.Database.SqlQuery<I_class>("i_getClassForTree").ToList();

            return View(clss);
            
        }


        public ActionResult MainPanel()
        {
            //if (Request.IsAjaxRequest())
            //{
            //    int a = 1;
            //}
            return PartialView();
        }

        public ActionResult Pay()
        {

            return View();
        }

        public ActionResult DeliveryYourself()
        {

            return View();
        }

        public ActionResult Guarantee()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Commodity(int id = 0, int code = 0)
        {
            //code параметр старой ссылки descr.aspx?code=249332
            if (code != 0 & id == 0) id = code; 

            /*
            var commodity2 = from cm in StoreDB.I_commodities
                             join img in StoreDB.I_images on cm.Code equals img.Upcode into im
                             from imgEmpt in im.DefaultIfEmpty()
                             where cm.Code == id && imgEmpt.Ext == "txt"
                             select new Commodity_property { Code = cm.Code, Name = cm.Name, Price = cm.Price, Property = imgEmpt.Img  };
            */

            System.Data.SqlClient.SqlParameter param_class = new System.Data.SqlClient.SqlParameter("code", id);

            var commodity2 = StoreDB.Database.SqlQuery<Commodity_property>("i_GetI_commodity_param @code ", param_class).ToList();


            return PartialView(commodity2);

        }

    

        public ActionResult Contacts()
        {

            return View(); 
        }


        [HttpGet]
        public ActionResult Delivery()
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