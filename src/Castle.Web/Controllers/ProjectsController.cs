using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpSvn;

namespace Castle.Web.Controllers
{
    public class ProjectsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
	}
}