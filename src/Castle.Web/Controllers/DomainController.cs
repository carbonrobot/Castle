using System.Web.Mvc;
using Castle.Services;

namespace Castle.Web.Controllers
{
    public class DomainController : Controller
    {
        public DomainController(DomainService service)
        {
            this.DomainService = service;
        }

        protected readonly DomainService DomainService;
    }
}