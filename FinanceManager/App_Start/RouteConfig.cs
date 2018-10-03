using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FinanceManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
           // routes.MapRoute(
           //    name: "Transactions",
           //    url: "{controller}/{id}",
           //    defaults: new { controller = "Transactions", action = "Index", id = UrlParameter.Optional }
           //);
            routes.MapRoute(
                name: "Transactions",
                url: "{controller}/{action}/{groupType}",
                defaults: new { controller = "Transactions", action = "Index", groupType = UrlParameter.Optional}
            );
            routes.MapRoute(
                name: "Budget",
                url: "{controller}/{action}",
                defaults: new { controller = "Budget", action = "Index" }
            );
            routes.MapRoute(
                name: "Reports",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Reports", action = "AccountBalanceReport", id = UrlParameter.Optional }
            );
        }
    }
}
