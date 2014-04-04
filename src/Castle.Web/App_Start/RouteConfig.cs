using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Castle.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Action",
                url: "{controller}/{action}",
                defaults: new { controller = "Source", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{id}/{action}",
                defaults: new { controller = "Source", action = "Index" },
                constraints: new { id = @"\d+" }
            );

        }
    }
}
