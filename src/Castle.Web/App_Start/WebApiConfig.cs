namespace Castle.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //    name: "DefaultRoutes",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: null,
            //    constraints: new { id = @"\d+" }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "ActionRoutes",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional }
            //);
        }
    }
}
