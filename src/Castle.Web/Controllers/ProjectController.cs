using System.Web.Mvc;
using Castle.Services;
using Castle.Web.Models.Project;

namespace Castle.Web.Controllers
{
    [RoutePrefix("source")]
    public class ProjectController : DomainController
    {
        public ProjectController(DomainService service) : base(service) { }

        [HttpGet, Route("{repositoryKey}/{projectKey}")]
        public ActionResult Index(string repositoryKey, string projectKey)
        {
            var response = this.ProjectService.GetProject(projectKey);

            // TODO: handle errors

            var model = new ProjectViewModel()
            {
                Project = response.Result
            };
            return View(model);
        }
    }
}