﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Web.Models;
using AspNet.Identity.Dapper;
using Web.Controllers;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Configuration;

namespace Web
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            //return Task.FromResult(0);
            return sendMessage(message);
        }

        Task sendMessage(IdentityMessage message)
        {
            #region formatter
            string text = message.Body;
            string html = message.Body;
            #endregion

            var credentialsInfo = new NetworkCredential(
                 ConfigurationManager.AppSettings["mailAccount"],
                 ConfigurationManager.AppSettings["mailPassword"]
                 );

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(credentialsInfo.UserName);
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));

            NetworkCredential credentials = new NetworkCredential(credentialsInfo.UserName, credentialsInfo.Password);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;

            var task = Task.Run(() => {
                smtpClient.Send(msg);
            });

            return task;
        }

    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application AppMember manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<AppMember,int>
    {
        public ApplicationUserManager(IUserStore<AppMember,int> store)
            : base(store)
        {
        }

        //public async Task<IdentityResult> ResetPasswordAsync(AppMember user,
        //    string token, string newPassword)
        //{
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException("user");
        //    }

        //    // Make sure the token is valid and the stamp matches.
        //    if (!await UserTokenProvider.ValidateAsync("ResetPassword", token,
        //        this, user))
        //    {
        //        return IdentityResult.Failed("Invalid token.");
        //    }

        //    // Make sure the new password is valid.
        //    var result = await PasswordValidator.ValidateAsync(newPassword)
        //        .ConfigureAwait(false);
        //    if (!result.Succeeded)
        //    {
        //        return result;
        //    }

        //    // Update the password hash and invalidate the current security stamp.
        //    user.PasswordHash = PasswordHasher.HashPassword(newPassword);
        //    user.SecurityStamp = Guid.NewGuid().ToString();

        //    // Save the user and return the outcome.
        //    return await UpdateAsync(user).ConfigureAwait(false);
        //}

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            //var manager = new ApplicationUserManager(
                //new UserStore<AppMember>(
                    //context.Get<ApplicationDbContext>()));

            var manager = new ApplicationUserManager(
                new UserStore<AppMember>(
                    context.Get<ApplicationDbContext>() as DbManager));

            // Configure validation logic for usernames
            //manager.UserValidator = new UserValidator<AppMember,int>(manager)
            //{
            //    AllowOnlyAlphanumericUserNames = false,
            //    RequireUniqueEmail = true
            //};

            // Configure validation logic for passwords
            manager.UserValidator = new customUserValidator<AppMember>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure AppMember lockout defaults
            manager.UserLockoutEnabledByDefault = false;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the AppMember
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<AppMember,int>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<AppMember,int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<AppMember,int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<AppMember, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppMember AppMember)
        {
            return AppMember.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
