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
        [Route("{repositoryKey}/history/recent")]
        public IEnumerable<Domain.SourceLogEntry> RecentHistory(string repositoryKey)
        {
            var response = this.DomainService.GetRepositoryHistory(repositoryKey, 7);
            if (response.HasError)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            if (response.Result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            
            return response.Result;
        }
    }
}