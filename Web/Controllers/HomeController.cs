using AspNet.Identity.Dapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin, Pharmacist")]
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;

        private RoleManager<IdentityRole, int> _userRoleManager;

        private ApplicationSignInManager _signInManager;

        private ApplicationDbContext _context;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        public ActionResult Index()
        {
            var system_overview_model = new SystemSummaryViewModel();
            ScheduleDBManager scheduleManager = new ScheduleDBManager();

            var usertable = new UserTable<AppMember>(Context);
            var num_patients = usertable.GetUserByRole("Patient").Count();


            system_overview_model.Due_Refills = new RefillLinesViewModel();
            system_overview_model.Due_Refills.Refills = new List<RefillLineViewModel>();

            system_overview_model.Pending_Refills = new RefillLinesViewModel();
            system_overview_model.Pending_Refills.Refills = new List<RefillLineViewModel>();

            var retrievedDueRefills = scheduleManager.getDueRefills();
            foreach (var refill in retrievedDueRefills)
            {
                var refillLine = new RefillLineViewModel { PatientName = refill.Prescription_Id.ToString(),
                    MedecineName = refill.Future_Refill_Date.ToString(), pId = refill.Id.Value };
                system_overview_model.Due_Refills.Refills.Add(refillLine);
            }

            system_overview_model.number_patients = num_patients;
            return View(system_overview_model);
        }

        public ActionResult Patients()
        {
            var usertable = new UserTable<AppMember>(Context);
            var patients = usertable.GetUserByRole("Patient")
                .Select(p => new PatientViewModel { Name = p.UserName, DateBirth = p.DateBirth, Address = p.Address, Email = p.Email, PhoneNumber = p.PhoneNumber, Id = p.Id });

            var patientsModel = new PatientsViewModel();
            patientsModel.Patients = patients;

            return View(patientsModel);
        }

        public async Task<ActionResult> EditPatient(int id)
        {
            // initialize the view model
            var patient_model = new PatientViewModel();

            // find the user by email
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return new EmptyResult();
            }

            patient_model.Name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.UserName.ToLower());
            patient_model.Email = user.Email.ToLower();
            patient_model.DateBirth = user.DateBirth;
            patient_model.Address = user.Address;
            patient_model.PhoneNumber = user.PhoneNumber;
            patient_model.Id = user.Id;

            return View(patient_model);
        }

        [HttpPost]
        public async Task<ActionResult> EditPatient(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var AppMember = new AppMember
                {
                    UserName = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    DateBirth = model.DateBirth,
                    Address = model.Address,
                    Id = model.Id
                };
                var userStore = new UserStore<AppMember>(Context);
                await userStore.UpdateAsync(AppMember);

                return RedirectToAction("Patients", "Home");

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Recall()
        {

            return View();
        }
    }
}