using AspNet.Identity.Dapper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using System;
using Web.Models;

[assembly: OwinStartupAttribute(typeof(Web.Startup))]
namespace Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createUserRolesAndDefaultUser();
        }

        public void createUserRolesAndDefaultUser()
        {
            ApplicationDbContext context = ApplicationDbContext.Create();

            var roleManager = new RoleManager<IdentityRole, int>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<AppMember, int>(new UserStore<AppMember>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = "Admin";
                var chkRole = roleManager.Create(role);
                if (chkRole.Succeeded)
                {
                    //Here we create a Admin super user who will maintain the website                  

                    var user = new AppMember();
                    user.UserName = "admin";
                    user.Email = "oliviertyishime@gmail.com";
                    user.DateBirth = new DateTime(1994, 10, 31);

                    string user_password = "Test_1";

                    var chkUser = userManager.Create(user, user_password);

                    //Add default User to Role Admin   
                    if (chkUser.Succeeded)
                    {
                        var result1 = userManager.AddToRole(user.Id, "Admin");

                    }
                }
            }

            // creating Pharmacist role    
            if (!roleManager.RoleExists("Pharmacist"))
            {
                var role = new IdentityRole();
                role.Name = "Pharmacist";
                roleManager.Create(role);

            }

            // creating Patient role    
            if (!roleManager.RoleExists("Patient"))
            {
                var role = new IdentityRole();
                role.Name = "Patient";
                roleManager.Create(role);

            }
        }
    }
}
