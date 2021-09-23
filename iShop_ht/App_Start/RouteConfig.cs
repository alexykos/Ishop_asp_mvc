using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace iShop_ht
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            
            routes.MapRoute(
                 "descr.aspx?code",
                 "code={id}",
                new { controller = "Home", action = "Commodity", id = UrlParameter.Optional }
                );

            //descr.aspx?code=249332
            routes.MapRoute(
                "descr.aspx?code2",
                "descr.aspx",
               new
               {
                   controller = "Home"
                   , action = "Commodity"
                   ,code = UrlParameter.Optional
                 });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );




        }
    }
}
