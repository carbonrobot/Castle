using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Domain;
using Castle.Services;

namespace Castle.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjectService ProjectService;

        public HomeController(ProjectService projService)
        {
            this.ProjectService = projService;
        }

        public ActionResult Index()
        {
            //var options = new SourceProviderOptions()
            //{
            //    UserName = "CXBrown",
            //    Password = "Discoverywins14$$"
            //};
            //var provider = new SourceProvider(options);
            //var history = provider.GetRecentHistory();

            var projectResponse = this.ProjectService.GetProjects();

            var model = new Models.Home.IndexViewModel()
            {
                Projects = projectResponse.Result,
                RecentHistory = new List<SourceLogEntry>()
            };
            return View(model);
        }
    }
}