using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace Web.Controllers
{
    public class IVRController : TwilioController
    {
        // GET: IVR
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public TwiMLResult Welcome()
        {
            var response = new TwilioResponse();
            response.BeginGather(new { action = Url.Action("Show", "Menu"), numDigits = "1" })
                .Say("Thank you for calling DEMO Pharmacy." + " If you would like to talk to a pharmacist, press 1. " +
                     " To change the way we contact you, press 2.")
                     .Pause(2)
                     .Say("Thank you for calling DEMO Pharmacy." + " If you would like to talk to a pharmacist, press 1. " +
                     " To change the way we contact you, press 2.")
                     .Pause(2)
                     .Say("Thank you for calling DEMO Pharmacy." + " If you would like to talk to a pharmacist, press 1. " +
                     " To change the way we contact you, press 2.")
                     .Pause(2)
                     .Say("Goodbye")
                .EndGather();
            return TwiML(response);
        }
    }
}