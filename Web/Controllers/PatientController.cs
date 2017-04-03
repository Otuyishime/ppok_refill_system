using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("Patient")]
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        [Route("Index/{userId:int}/{code}")]
        public ActionResult Index(int userId, string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        public ActionResult Index(string model)
        {
            if (ModelState.IsValid)
            {
                RedirectToAction("HomePage");
            }
            return View();
        }

        public ActionResult HomePage()
        {
            return View(new RefillResponseViewModel());
        }

        [HttpPost]
        public ActionResult SubmitResponse(RefillResponseViewModel model)
        {
            if (ModelState.IsValid)
            {
                RedirectToAction("Index");
            }
            return View();
        }
    }
}