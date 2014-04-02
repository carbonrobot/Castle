using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Services;

namespace Castle.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var options = new SourceProviderOptions()
            {
                UserName = "CXBrown",
                Password = "Discoverywins14$$"
            };
            var provider = new SourceProvider(options);
            var history = provider.GetRecentHistory();

            var model = new Models.Home.IndexViewModel()
            {
                RecentHistory = history.OrderByDescending(x => x.Time)
            };
            return View(model);
        }
    }
}