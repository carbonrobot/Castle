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
    public class SourceController : BaseController
    {
        private readonly SourceService SourceService;

        public SourceController(SourceService sourceService)
        {
            this.SourceService = sourceService;
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

    }
}