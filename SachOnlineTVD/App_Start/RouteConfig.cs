using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SachOnlineTVD
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Trang chủ",
                url: "",
                defaults: new { controller = "SachOnline", action = "Index", id = UrlParameter.Optional }
                , namespaces: new[] { "SachOnline.Controllers" }
            );


            routes.MapRoute(
              name: "Sach theo chu de",
              url: "sach-theo-chu-de-{iMaCD}",
              defaults: new { controller = "SachOnline", action = "BookByTopic", iMaCD = UrlParameter.Optional }
              , namespaces: new[] { "SachOnline.Controllers" }
          );

            routes.MapRoute(
              name: "Sach theo nha xuat ban",
              url: "sach-theo-nha-xuat-ban-{iMaNXB}",
              defaults: new { controller = "SachOnline", action = "BookByNXB", iMaNXB = UrlParameter.Optional }
              , namespaces: new[] { "SachOnline.Controllers" }
          );

            routes.MapRoute(
              name: "Trang tin",
              url: "{metatitle}",
              defaults: new { controller = "SachOnline", action = "TrangTin", metatitle = UrlParameter.Optional }
              , namespaces: new[] { "SachOnline.Controllers" }
          );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , namespaces: new[] { "SachOnline.Controllers" }
            );



        }


    }
}
