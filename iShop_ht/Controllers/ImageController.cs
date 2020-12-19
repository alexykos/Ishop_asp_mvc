using iShop_ht.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iShop_ht.Controllers
{
    public class ImageController : Controller
    {
        Ishop_Entities StoreDB = new Ishop_Entities();
        // GET: Image
        public FileContentResult GetImage(int id, string img)
        {
            var Images = StoreDB.I_images.Where(x => x.Upcode == id && x.Ext == "JPG").FirstOrDefault();
            

            if (Images != null)
            {
                var imageByte = Images.Img150x150;
                switch (img)
                {
                    case "300":
                        imageByte = Images.Img300x300;
                        break;
                }
                if (imageByte != null)
                {
                    return File(imageByte, "image/jpeg");
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}