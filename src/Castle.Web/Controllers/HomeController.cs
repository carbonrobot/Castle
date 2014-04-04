using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Domain;
using Castle.Services;
using Castle.Web.Models.Home;

namespace Castle.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DomainService ProjectService;

        public HomeController(DomainService projService)
        {
            this.ProjectService = projService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            var response = this.ProjectService.GetRepositories();

            // TODO: handle errors

            var model = new Models.Home.IndexViewModel()
            {
                RepositoryList = response.Result
            };
            return View(model);
        }

        [HttpGet]
        [Route("{repositoryKey}")]
        public ActionResult Index(string repositoryKey)
        {
            var response = this.ProjectService.GetRepository(repositoryKey);

            // TODO: handle errors

            var model = new RepositoryViewModel()
            {
                Repository = response.Result
            };
            return View("Repository", model);
        }
    }
}