using System.Web.Mvc;
using Castle.Services;
using Castle.Web.Models.Repository;

namespace Castle.Web.Controllers
{
    public class RepositoryController : DomainController
    {
        public RepositoryController(DomainService service)
            : base(service)
        {
        }

        [HttpGet, Route("source/{repositoryKey}")]
        public ActionResult Index(string repositoryKey)
        {
            var response = this.ProjectService.GetRepository(repositoryKey);

            // TODO: handle errors

            var model = new RepositoryViewModel()
            {
                Repository = response.Result
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new RepositoryViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RepositoryViewModel model)
        {
            return View(model);
        }
    }
}