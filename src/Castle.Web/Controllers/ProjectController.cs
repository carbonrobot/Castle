using System.Web.Mvc;
using Castle.Domain;
using Castle.Services;
using Castle.Web.Models.Project;

namespace Castle.Web.Controllers
{
    public class ProjectController : DomainController
    {
        public ProjectController(DomainService service) : base(service) { }

        [HttpGet, Route("source/{repositoryKey}/{projectKey}")]
        public ActionResult Index(string repositoryKey, string projectKey)
        {
            var response = this.DomainService.GetProject(projectKey);

            // TODO: handle errors

            var model = new ProjectViewModel()
            {
                Project = response.Result
            };
            return View(model);
        }

        [HttpGet, Route("repository/{repositoryKey}/create")]
        public ActionResult Create()
        {
            var model = new ProjectViewModel()
            {
                Project = new Project()
            };
            return View(model);
        }

        [HttpPost, Route("repository/{repositoryKey}/create")]
        public ActionResult Create(string repositoryKey, ProjectViewModel model)
        {
            var response = this.DomainService.CreateProject(repositoryKey, model.Project.Name, model.Project.Path);
            if (response.HasError)
            {
                // TODO: handle errors
                return View(model);
            }

            var project = response.Result;
            return RedirectToAction("Index", new { repositoryKey = project.Repository.Key, projectKey = project.Key });
        }

    }
}