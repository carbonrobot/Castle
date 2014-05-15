using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Castle.Services;

namespace Castle.Web.Controllers.Api
{
    public class BaseController : ApiController
    {
        /// <summary>
        /// Ensures the reponse does not contain an error or null result
        /// </summary>
        /// <param name="response">The response.</param>
        protected void EnsureResponse<T>(ServiceResponse<T> response)
        {
            if (response.HasError)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            if (response.Result == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Ensures the reponse does not contain an error or null result
        /// </summary>
        /// <param name="response">The response.</param>
        protected void EnsureResponse(ServiceResponse response)
        {
            if (response.HasError)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
        }
    }
}