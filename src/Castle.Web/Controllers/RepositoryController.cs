using System.Web.Mvc;
using Castle.Services;
using Castle.Web.Models.Repository;

namespace Castle.Web.Controllers
{
    [RoutePrefix("source")]
    public class RepositoryController : DomainController
    {
        public RepositoryController(DomainService service)
            : base(service)
        {
        }

        [HttpGet, Route("{repositoryKey}")]
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
        public ActionResult New()
        {
            return View("Update");
        }
    }
}