using System.Web.Mvc;
using Castle.Domain;
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

        [HttpGet]
        public ActionResult Create()
        {
            var model = new RepositoryViewModel()
            {
                Repository = new Repository()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RepositoryViewModel model)
        {
            var response = this.DomainService.CreateRepository(model.Repository.Name, model.Repository.Path);
            if (response.HasError)
            {
                // TODO: handle errors
                return View(model);
            }

            return RedirectToAction("Index", new { repositoryKey = response.Result.Key });
        }

        [HttpPost, Route("repository/{repositoryKey}/delete")]
        public ActionResult Delete(string repositoryKey)
        {
            var response = this.DomainService.DeleteRepository(repositoryKey);
            if (response.HasError)
            {
                // TODO: handle errors
            }

            return RedirectToAction("Index", "Source");
        }

        [HttpGet, Route("source/{repositoryKey}")]
        public ActionResult Index(string repositoryKey)
        {
            var response = this.DomainService.GetRepository(repositoryKey);

            // TODO: handle errors

            var model = new RepositoryViewModel()
            {
                Repository = response.Result
            };
            return View(model);
        }

        [HttpGet, Route("repository/{repositoryKey}/settings")]
        public ActionResult Settings(string repositoryKey)
        {
            var response = this.DomainService.GetRepository(repositoryKey);

            // TODO: handle errors

            var model = new RepositoryViewModel()
            {
                Repository = response.Result
            };
            return View(model);
        }

        [HttpPost, Route("repository/{repositoryKey}/settings")]
        public ActionResult Settings(RepositoryViewModel model)
        {
            var response = this.DomainService.UpdateRepository(model.Repository);
            if (response.HasError)
            {
                // TODO: handle errors
                return View(model);
            }

            return RedirectToAction("Settings", new { repositoryKey = response.Result.Key });
        }

    }
}