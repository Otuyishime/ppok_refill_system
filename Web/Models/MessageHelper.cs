using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using AspNet.Identity.Dapper;
using System.Web.Mvc;

namespace Web.Models
{
    public class MessageHelper
    {
        DbManager dbManager;
        TemplatesManager template_manager;

        public MessageHelper()
        {
            template_manager = new TemplatesManager();
        }

        public MessageHelper(DbManager database)
        {
            dbManager = database;
        }

        public async Task SendBirthdayMessageAsync(ApplicationUserManager UserManager, AppMember user)
        {
            /*
             * Get the message template for communication preference 
             */
            var result = template_manager.findTemplateByTypeAndCommPref((int)MessageTypeId.Birthday, user.CommunicationType);
            //var message_template = result.Temp_Message ?? "No Template";
            if (user.CommunicationType == (int)CommunicationPreferenceId.Email)
            {
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                await UserManager.SendEmailAsync(user.Id,
                    subject: "PPOK Refill System: Birthday Notification",
                    body: "HAPPY BIRTHDAY TO YOU TODAY!!!" +
                    "<br /><br />" +
                    "To unsubscribe, click here " + "<a href=\"" + "" + "\">unsubscribe</a>");
            }
            
        }

        public async Task<bool> SendRefillMessageAsync(ApplicationUserManager UserManager, AppMember user, string callbackUrl, string unSubscribeCallBack, string code)
        {
            /*
             * Get the message template for communication preference 
             */
            var result = template_manager.findTemplateByTypeAndCommPref((int)MessageTypeId.Refill, user.CommunicationType);
            //var message_template = result.Temp_Message ?? "No Template";
            if (user.CommunicationType == (int)CommunicationPreferenceId.Email)
            {
                await UserManager.SendEmailAsync(user.Id,
                    subject: "PPOK Refill System: Refill Notification",
                    body: "Please confirm your refill by clicking <a href=\"" + callbackUrl + "\">here</a><br />Or" +
                    " click on the copy the following link on the browser: " + HttpUtility.HtmlEncode(callbackUrl) + 
                    "<br /> Use the following code to access the confirmation page: <h3>" + HttpUtility.HtmlEncode(code)+ "</h4>" +
                    "To unsubscribe, click here " + "<a href=\"" + HttpUtility.HtmlEncode(unSubscribeCallBack) + "\">unsubscribe</a>");
            }
            return true;
        }

        public async Task SendRecallMessageAsync(ApplicationUserManager UserManager, AppMember user)
        {
            /*
             * Get the message template for communication preference 
             */
            var result = template_manager.findTemplateByTypeAndCommPref((int)MessageTypeId.Recall, user.CommunicationType);
            var message_template = result.Temp_Message ?? "No Template";
            if (user.CommunicationType == (int)CommunicationPreferenceId.Email)
            {
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                UrlHelper Url = new UrlHelper();
                var callbackUrl = Url.Action("Index", "Patient", routeValues: new { userId = user.Id, code = code }, protocol: "https");

                await UserManager.SendEmailAsync(user.Id,
                    subject: "PPOK Refill System: Recall Notification",
                    body: "This notification is to let you know this (X) medecine has been recalled" +
                    "<br /><br />" +
                    "To unsubscribe, click here " + "<a href=\"" + callbackUrl + "\">unsubscribe</a>");
            }
        }

        public async Task SendPickUpMessageAsync(ApplicationUserManager UserManager, AppMember user)
        {
            /*
             * Get the message template for communication preference 
             */
            var result = template_manager.findTemplateByTypeAndCommPref((int)MessageTypeId.PickUp, user.CommunicationType);
            var message_template = result.Temp_Message ?? "No Template";
            if (user.CommunicationType == (int)CommunicationPreferenceId.Email)
            {
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                await UserManager.SendEmailAsync(user.Id,
                    subject: "PPOK Refill System: Pick Up Notification",
                    body: "This notification is to let you know your (X) medecine is ready for pick up." +
                    "<br /><br />" +
                    "To unsubscribe, click here " + "<a href=\"" + " " + "\">unsubscribe</a>");
            }
        }

        #region Helpers

        public enum MessageTypeId
        {
            Refill = 1,
            Recall,
            Birthday,
            PickUp
        }

        public enum CommunicationPreferenceId
        {
            TextMessage = 1,
            PhoneCall,
            Email
        }

        #endregion
    }
}