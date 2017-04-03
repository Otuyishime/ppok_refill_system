using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        // GET: NotFound
        public ActionResult NotFound()
        {
            return View();
        }

        // GET: BadRequest
        public ActionResult BadRequest()
        {
            return View();
        }
    }
}