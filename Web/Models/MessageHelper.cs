﻿using System;
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

        public async Task SendBirthdayMessageAsync(ApplicationUserManager UserManager, AppMember user)
        {
            String msgBody = "HAPPY BIRTHDAY TO YOU TODAY!!!";
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
                    body: msgBody);
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

        public async Task SendRefillMessageAsync(ApplicationUserManager UserManager, AppMember user)
        {
            /*
             * Get the message template for communication preference 
             */
            var result = template_manager.findTemplateByTypeAndCommPref((int)MessageTypeId.Refill, user.CommunicationType);
            var message_template = result.Temp_Message ?? "No Template";
            if (user.CommunicationType == (int)CommunicationPreferenceId.Email)
            {
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                UrlHelper Url = new UrlHelper();
                var callbackUrl = Url.Action("Index", "Patient", routeValues: new { userId = user.Id, code = code }, protocol: "http");

                await UserManager.SendEmailAsync(user.Id,
                    subject: "PPOK Refill System: Refill Notification",
                    body: "Please confirm your refill by clicking <a href=\"" + callbackUrl + "\">here</a><br />Or" +
                    " click on the copy the following link on the browser: " + HttpUtility.HtmlEncode(callbackUrl));
            }

            // Need to implement text message and phone call
        }

        public async Task SendRecallMessageAsync(ApplicationUserManager UserManager, AppMember user)
        {
            String msgBody = "This notification is to let you know this (X) medecine has been recalled";
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
                    body: msgBody);
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

        public async Task SendPickUpMessageAsync(ApplicationUserManager UserManager, AppMember user)
        {
            String msgBody = "This notification is to let you know your (X) medecine is ready for pick up.";
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
                    body: msgBody);
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