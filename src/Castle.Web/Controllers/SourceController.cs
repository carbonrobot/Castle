using System.Web.Mvc;
using Castle.Services;
using Castle.Web.Models.Project;
using Castle.Web.Models.Source;

namespace Castle.Web.Controllers
{
    public class SourceController : DomainController
    {
        public SourceController(DomainService service)
            : base(service)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var response = this.ProjectService.GetRepositories();

            // TODO: handle errors

            var model = new SourceViewModel()
            {
                RepositoryList = response.Result
            };
            return View(model);
        }
    }
}