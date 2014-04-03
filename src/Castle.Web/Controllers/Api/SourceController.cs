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
        private readonly SourceService SourceService;

        public SourceController(SourceService service)
        {
            this.SourceService = service;
        }

        [HttpGet]
        [Route("history/recent")]
        public IEnumerable<Domain.SourceLogEntry> RecentHistory()
        {
            var response = this.SourceService.GetRecentHistory(7);
            if (response.HasError)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            if (response.Result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return response.Result;
        }
    }
}