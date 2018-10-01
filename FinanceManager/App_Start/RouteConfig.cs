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
                url: "{controller}/{action}/{account}/{category}/{period}",
                defaults: new { controller = "Transactions", action = "Index", account = UrlParameter.Optional, category = UrlParameter.Optional, period = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Reports",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Reports", action = "AccountBalanceReport", id = UrlParameter.Optional }
            );
        }
    }
}
