using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Castle.Web.Controllers
{
    public class LibraryController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
	}
}