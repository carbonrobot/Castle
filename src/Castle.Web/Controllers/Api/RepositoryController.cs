using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Castle.Services;

namespace Castle.Web.Controllers.Api
{
    [RoutePrefix("api/v1/repository")]
    public class RepositoryController : BaseController
    {
        private readonly DomainService DomainService;

        public RepositoryController(DomainService domainService)
        {
            this.DomainService = domainService;
        }

         [HttpGet]
        [Route("{key}/history/recent")]
        public IEnumerable<Domain.SourceLogEntry> RecentRepositoryHistory(string key)
        {
            var response = this.DomainService.GetRepositoryHistory(key, 7);
            EnsureReponse(response);
            
            return response.Result;
        }
    }
}