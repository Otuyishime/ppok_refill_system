using AspNet.Identity.Dapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;

        private RoleManager<IdentityRole, int> _userRoleManager;

        private ApplicationSignInManager _signInManager;

        private ApplicationDbContext _context;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public RoleManager<IdentityRole, int> RoleManager
        {
            get
            {
                return _userRoleManager ?? new RoleManager<IdentityRole, int>(new RoleStore<IdentityRole>(Context));
            }
            private set
            {
                _userRoleManager = value;
            }
        }

        public ApplicationDbContext Context
        {
            get
            {
                return _context ?? ApplicationDbContext.Create();
            }
            set
            {
                _context = value;
            }
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pharmacists()
        {
            var usertable = new UserTable<AppMember>(Context);
            var pharmacists = usertable.GetUserByRole("Pharmacist")
                .Select( p => new PharmacistViewModel { Name = toUpperCase(p.UserName), DateBirth = p.DateBirth,
                                                        Address = p.Address, Active = p.Active, Email = p.Email.ToLower(),
                                                        PhoneNumber = p.PhoneNumber, Id = p.Id});
            
            var pharmacistsModel = new PharmacistsViewModel();
            pharmacistsModel.Pharmacists = pharmacists;

            return View(pharmacistsModel);
        }

        public async Task<ActionResult> EditPharmacist(int id)
        {
            // initialize the view model
            var pharmacist_model = new PharmacistViewModel();

            // find the user by email
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return new EmptyResult();
            }

            pharmacist_model.Name = toUpperCase(user.UserName);
            pharmacist_model.Email = user.Email.ToLower();
            pharmacist_model.DateBirth = user.DateBirth;
            pharmacist_model.Address = user.Address;
            pharmacist_model.Active = user.Active;
            pharmacist_model.PhoneNumber = user.PhoneNumber;
            pharmacist_model.Id = user.Id;

            return View(pharmacist_model);
        }

        [HttpPost]
        public async Task<ActionResult> EditPharmacist(PharmacistViewModel model)
        {
            if (ModelState.IsValid)
            {
                var AppMember = new AppMember { UserName = model.Name, PhoneNumber = model.PhoneNumber,
                    Email = model.Email, DateBirth = model.DateBirth, Address = model.Address,
                    Active = model.Active, Id = model.Id};
                var userStore = new UserStore<AppMember>(Context);
                await userStore.UpdateAsync(AppMember);

                return RedirectToAction("Pharmacists", "Admin");
                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Templates()
        {

            return View();
        }



        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private string toUpperCase(string s)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }
        #endregion
    }
}