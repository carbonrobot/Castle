using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StackExchange.Profiling;
using Castle.Services.Logging;
using System.Web.Http;

namespace Castle.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_AuthenticateRequest()
        {
            this.Log().Info(() => "Application_AuthenticateRequest");
        }

        protected void Application_Start()
        {
            Log.InitializeWith<Castle.Web.Logging.NLogLog>();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            JsonConfig.Register();
        }

        protected void Application_BeginRequest()
        {
            this.Log().Info(() => "Begin Request");
            MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
            this.Log().Info(() => "End Request");
        }
    }
}
