using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;

namespace Web.Controllers
{
    public class MessageController : Controller
    {
        // The account id and authoriztion token associated with our account
        private const string accountSid = "AC1407ac3f8db313782cc3901d2f3418ff";
        private const string authToken = "7dc8eff55ae6fac61e2f382afbaa8eac";

        // Our Twilio phone number
        private const string twilioPhoneNumber = "+18589432515";

        // Sends text message
        public ActionResult SendSms(String toPhoneNumber, String body)
        {
            var twilioMessage = new TwilioRestClient(accountSid, authToken);
            var message = twilioMessage.SendMessage(twilioPhoneNumber, toPhoneNumber, body);
  
            return null;
        }

        public ActionResult SendVoiceCall(String toPhoneNumber)
        {
            var twilioCall = new TwilioRestClient(accountSid, authToken);
            var options = new CallOptions();
            options.Url = "http://demo.twilio.com/docs/voice.xml";
            //options.Url = "https://localhost:44335/Birthday.xml";
            //options.Url = "~/Content/Twilio/Birthday.xml";
            options.To = toPhoneNumber;
            options.From = twilioPhoneNumber;

            var call = twilioCall.InitiateOutboundCall(options);

            return null;
        }
    }
}