using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using AspNet.Identity.Dapper;
using System.Web.Mvc;
using Web.Controllers;

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

        public async Task SendBirthdayMessageAsync(ApplicationUserManager UserManager, AppMember user, string unSubscribeCallBack)
        {
            string msgBody = "HAPPY BIRTHDAY TO YOU TODAY";
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
                    "To unsubscribe, click here " + "<a href=\"" + HttpUtility.HtmlEncode(unSubscribeCallBack) + "\">unsubscribe</a>");
            }

            if (user.CommunicationType == (int)CommunicationPreferenceId.TextMessage)
            {
                var birthdayText = new MessageController();
                birthdayText.SendSms("+1" + user.PhoneNumber, msgBody);
            }

            if (user.CommunicationType == (int)CommunicationPreferenceId.PhoneCall)
            {
                var birthdayVoiceCall = new MessageController();
                birthdayVoiceCall.SendVoiceCall("+1" + user.PhoneNumber);
            }
        }

        public async Task<bool> SendRefillMessageAsync(ApplicationUserManager UserManager, AppMember user, string callbackUrl, string unSubscribeCallBack, string code, string medicineName)
        {
            String msgBody = "Please confirm your refill by clicking!";
            /*
             * Get the message template for communication preference 
             */
            var result = template_manager.findTemplateByTypeAndCommPref((int)MessageTypeId.Refill, user.CommunicationType);
            if (user.CommunicationType == (int)CommunicationPreferenceId.Email)
            {
                await UserManager.SendEmailAsync(user.Id,
                    subject: "PPOK Refill System: Refill Notification",
                    body: "Please confirm your refill by clicking <a href=\"" + callbackUrl + "\">here</a><br />Or" +
                    " click on the copy the following link on the browser: " + HttpUtility.HtmlEncode(callbackUrl) +
                    "<br /> Use the following code to access the confirmation page: <h3>" + HttpUtility.HtmlEncode(code) + "</h4>" +
                    "To unsubscribe, click here " + "<a href=\"" + HttpUtility.HtmlEncode(unSubscribeCallBack) + "\">unsubscribe</a>");
            }

            // Need to implement text message and phone call
            if (user.CommunicationType == (int)CommunicationPreferenceId.TextMessage)
            {
                var refillText = new MessageController();
                refillText.SendSms("+1" + user.PhoneNumber, msgBody);
            }

            if (user.CommunicationType == (int)CommunicationPreferenceId.PhoneCall)
            {
                var refillVoiceCall = new MessageController();
                refillVoiceCall.SendVoiceCall("+1" + user.PhoneNumber);
            }

            return true;
        }

        public async Task SendRecallMessageAsync(ApplicationUserManager UserManager, AppMember user, string unSubscribeCallBack, string medicineName)
        {
            String msgBody = "This notification is to let you know this " + medicineName + " medecine has been recalled";
            /*
             * Get the message template for communication preference 
             */
            var result = template_manager.findTemplateByTypeAndCommPref((int)MessageTypeId.Recall, user.CommunicationType);
            if (user.CommunicationType == (int)CommunicationPreferenceId.Email)
            {
                await UserManager.SendEmailAsync(user.Id,
                    subject: "PPOK Refill System: Recall Notification",
                    body: "This notification is to let you know this medecine: " + medicineName + " has been recalled" +
                    "<br /><br />" +
                    "To unsubscribe, click here " + "<a href=\"" + unSubscribeCallBack + "\">unsubscribe</a>");
            }

            if (user.CommunicationType == (int)CommunicationPreferenceId.TextMessage)
            {
                var recallText = new MessageController();
                recallText.SendSms("+1" + user.PhoneNumber, msgBody);
            }

            if (user.CommunicationType == (int)CommunicationPreferenceId.PhoneCall)
            {
                var recallVoiceCall = new MessageController();
                recallVoiceCall.SendVoiceCall("+1" + user.PhoneNumber);
            }
        }

        public async Task<bool> SendPickUpMessageAsync(ApplicationUserManager UserManager, AppMember user, string unSubscribeCallBack, string medicineName)
        {
            String msgBody = "This notification is to let you know your " + medicineName + " medecine is ready for pick up.";
            /*
             * Get the message template for communication preference 
             */
            var result = template_manager.findTemplateByTypeAndCommPref((int)MessageTypeId.PickUp, user.CommunicationType);
            //var message_template = result.Temp_Message ?? "No Template";
            if (user.CommunicationType == (int)CommunicationPreferenceId.Email)
            {
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                await UserManager.SendEmailAsync(user.Id,
                    subject: "PPOK Refill System: Pick Up Notification",
                    body: "This notification is to let you know your medecine: " + medicineName + " is ready for pick up." +
                    "<br /><br />" +
                    "To unsubscribe, click here " + "<a href=\"" + HttpUtility.HtmlEncode(unSubscribeCallBack) + "\">unsubscribe</a>");
            }

            if (user.CommunicationType == (int)CommunicationPreferenceId.TextMessage)
            {
                var pickupText = new MessageController();
                pickupText.SendSms("+1" + user.PhoneNumber, msgBody);
            }

            if (user.CommunicationType == (int)CommunicationPreferenceId.PhoneCall)
            {
                var pickupVoiceCall = new MessageController();
                pickupVoiceCall.SendVoiceCall("+1" + user.PhoneNumber);
            }

            return true;
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