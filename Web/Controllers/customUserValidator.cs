using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using AspNet.Identity.Dapper;
using Web.Models;

namespace Web.Controllers
{
    public class customUserValidator<TUser> : UserValidator<TUser, int>
    where TUser : AppMember
    {
        private readonly UserManager<TUser, int> _userManager;

        public customUserValidator(UserManager<TUser,int> manager):base (manager)
        {
            _userManager = manager;
        }

        public override async Task<IdentityResult> ValidateAsync(TUser user)
        {
            var errors = new List<string>();

            if (_userManager != null)
            {
                //check username availability. and add a custom error message to the returned errors list.
                var existingAccount = await _userManager.FindByEmailAsync(user.Email);
                if (existingAccount != null && existingAccount.Id != user.Id)
                {
                    errors.Add("User with email '" + user.Email.ToString() + "' already exist. Try login instead.");
                }
            }
            //set the returned result (pass/fail) which can be read via the Identity Result returned from the CreateUserAsync
            return errors.Any()
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;
        }
    }
}