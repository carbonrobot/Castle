using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Castle.Domain;
using Castle.Services;

namespace Castle.Web.Controllers.Api
{
    [RoutePrefix("api/v1/project")]
    public class ProjectController : BaseController
    {
        private readonly DomainService DomainService;

        public ProjectController(DomainService domainService)
        {
            this.DomainService = domainService;
        }

        [HttpGet, Route("{key}")]
        public Project Get(string key)
        {
            var response = this.DomainService.GetProject(key);
            EnsureResponse(response);

            return response.Result;
        }

        [HttpGet]
        [Route("{key}/history/recent")]
        public IEnumerable<Domain.SourceLogEntry> RecentProjectHistory(string key)
        {
            var response = this.DomainService.GetProjectHistory(key, 7);
            EnsureResponse(response);

            return response.Result;
        }

        [HttpPost, Route("{key}/update")]
        public Project Update(Project project)
        {
            var response = this.DomainService.UpdateProject(project);
            EnsureResponse(response);

            return response.Result;
        }

        [HttpPost, Route("{key}/delete")]
        public void Delete(string key)
        {
            var response = this.DomainService.DeleteProject(key);
            EnsureResponse(response);
        }
    }
}
