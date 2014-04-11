using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Castle.Domain;
using Castle.Services;

namespace Castle.Web.Controllers.Api
{
    [RoutePrefix("api/v1/source")]
    public class SourceController : ApiController
    {
        private readonly DomainService DomainService;
        private readonly SourceService SourceService;

        public SourceController(DomainService domainService, SourceService sourceService)
        {
            this.DomainService = domainService;
            this.SourceService = sourceService;
        }

        [HttpGet]
        [Route("repository/{key}/history/recent")]
        public IEnumerable<Domain.SourceLogEntry> RecentRepositoryHistory(string key)
        {
            var response = this.DomainService.GetRepositoryHistory(key, 7);
            EnsureReponse(response);
            
            return response.Result;
        }

        [HttpGet]
        [Route("project/{key}/history/recent")]
        public IEnumerable<Domain.SourceLogEntry> RecentProjectHistory(string key)
        {
            var response = this.DomainService.GetProjectHistory(key, 7);
            EnsureReponse(response);

            return response.Result;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<SourceFileInfo> Index(string path)
        {
            var response = this.SourceService.GetFileInfo(path);
            EnsureReponse(response);

            return response.Result;
        }

        [HttpGet]
        [Route("raw")]
        public HttpResponseMessage Raw(string path)
        {
            var response = this.SourceService.GetFileContent(path);
            EnsureReponse(response);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(response.Result, Encoding.UTF8, "text/plain")
            };
        }

        /// <summary>
        /// Ensures the reponse does not contain an error or null result
        /// </summary>
        /// <param name="response">The response.</param>
        protected void EnsureReponse<T>(ServiceResponse<T> response)
        {
            if (response.HasError)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            if (response.Result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}