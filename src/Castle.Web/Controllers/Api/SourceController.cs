using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Castle.Services;

namespace Castle.Web.Controllers.Api
{
    [RoutePrefix("api/v1/source")]
    public class SourceController : ApiController
    {
        private readonly DomainService DomainService;

        public SourceController(DomainService service)
        {
            this.DomainService = service;
        }

        [HttpGet]
        [Route("repository/{key}/history/recent")]
        public IEnumerable<Domain.SourceLogEntry> RecentRepositoryHistory(string key)
        {
            var response = this.DomainService.GetRepositoryHistory(key, 7);
            if (response.HasError)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            if (response.Result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            
            return response.Result;
        }

        [HttpGet]
        [Route("project/{key}/history/recent")]
        public IEnumerable<Domain.SourceLogEntry> RecentProjectHistory(string key)
        {
            var response = this.DomainService.GetProjectHistory(key, 7);
            if (response.HasError)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            if (response.Result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return response.Result;
        }
    }
}