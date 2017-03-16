using AspNet.Identity.Dapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
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
                .Select( p => new PharmacistViewModel { Name = p.UserName, DateBirth = p.DateBirth, Address = p.Address, Active = p.Active, Email = p.Email, PhoneNumber = p.PhoneNumber, Id = p.Id});
            
            var pharmacistsModel = new PharmacistsViewModel();
            pharmacistsModel.Pharmacists = pharmacists;

            return View(pharmacistsModel);
        }

        public ActionResult Templates()
        {

            return View();
        }

        public ActionResult EditPharmacist(int id)
        {

            return View();
        }

        [HttpPost]
        public ActionResult EditPharmacist(PharmacistViewModel model)
        {

            return View();
        }
    }
}