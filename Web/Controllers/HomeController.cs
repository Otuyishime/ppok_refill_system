using AspNet.Identity.Dapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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

        private RecallLinesViewModel RecallLinesModel;
        

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

            var retrievedDueRefills = scheduleManager.getDueRefillsInfo();
            foreach (var refill in retrievedDueRefills)
            {
                var refillLine = new RefillLineViewModel { PatientName = toUpperCase(refill.PatientName.ToString()),
                    MedecineName = refill.MedicationName.ToString(), pId = refill.Patient_Id, IsSelected = true };
                system_overview_model.Due_Refills.Refills.Add(refillLine);
            }

            system_overview_model.number_patients = num_patients;
            return View(system_overview_model);
        }

        public ActionResult Patients()
        {
            var usertable = new UserTable<AppMember>(Context);
            var patients = usertable.GetUserByRole("Patient")
                .Select(p => new PatientViewModel { Name = toUpperCase(p.UserName), DateBirth = p.DateBirth,
                    Address = p.Address, Email = p.Email.ToLower(), PhoneNumber = p.PhoneNumber, Id = p.Id,
                    CommunicationType = p.CommunicationType });

            var patientsModel = new PatientsViewModel();
            patientsModel.Patients = patients;

            return View(patientsModel);
        }

        public async Task<ActionResult> EditPatient(int id, string returnUrl)
        {
            // initialize the view model
            var patient_model = new PatientViewModel();

            // find the user by email
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return new EmptyResult();
            }

            patient_model.Name = toUpperCase(user.UserName);
            patient_model.Email = user.Email.ToLower();
            patient_model.DateBirth = user.DateBirth;
            patient_model.Address = user.Address;
            patient_model.PhoneNumber = user.PhoneNumber;
            patient_model.CommunicationType = user.CommunicationType;
            patient_model.Id = user.Id;

            // set the return Url
            ViewBag.returnUrl = returnUrl;
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
                    CommunicationType = model.CommunicationType,
                    Id = model.Id
                };
                var userStore = new UserStore<AppMember>(Context);
                await userStore.UpdateAsync(AppMember);

                return RedirectToAction("Patients", "Home");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> sendMessages(List<RefillLineViewModel> model)
        {
            if (ModelState.IsValid)
            {
                var retrivedData = model;
                var msgHelper = new MessageHelper();

                // Processing here ...
                if(UserManager.EmailService != null)
                {
                    foreach (var data in retrivedData)
                    {
                        if (data.IsSelected)
                        {
                            // Need to add medecine as well
                            var app_member = await UserManager.FindByIdAsync(data.pId);
                            await msgHelper.SendBirthdayMessageAsync(UserManager, app_member);
                        }
                    }
                }
                
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ActionName(name: "Recall")]
        public PartialViewResult ImportRecalls(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var allowedExtensions = new[] { ".xlsx", ".csv" };
                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        StreamReader csvreader = new StreamReader(stream);

                        csvreader.ReadLine(); // skip the headers : first line

                        RecallLinesModel = new RecallLinesViewModel();
                        RecallLinesModel.Recalls = new List<RecallLineViewModel>();

                        while (!csvreader.EndOfStream)
                        {
                            var line = csvreader.ReadLine();
                            var values = line.Split(',');

                            string PatientName = values[0];
                            string MedecineName = values[1];

                            // create recall lines
                            var RecallLineModel = new RecallLineViewModel { Selected = false, PatientName = PatientName, MedecineName = MedecineName };
                            RecallLinesModel.Recalls.Add(RecallLineModel);
                            
                        }
                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        TempData["CustomError"] = "This is a .xlsx file!";
                    }
                    else
                    {
                        TempData["CustomError"] = "The file has to be a .csv file!";
                    }
                }
                else
                {
                    TempData["CustomError"] = "You need to upload a file!";
                }

            }
            
            return PartialView("RecallsPartialView", RecallLinesModel);
        }

        [HttpGet]
        public ActionResult Recall()
        {

            RecallLinesModel = new RecallLinesViewModel();
            RecallLinesModel.Recalls = new List<RecallLineViewModel>();

            var recallLinesModel = (RecallLinesViewModel)ViewData["recalls"];
            if (recallLinesModel != null )
            {
                RecallLinesModel = recallLinesModel;
            }
            return View(RecallLinesModel);
        }

        [HttpGet]
        public ActionResult getRecall()
        {
            RecallLinesModel = new RecallLinesViewModel();
            RecallLinesModel.Recalls = new List<RecallLineViewModel> { new RecallLineViewModel { PatientName = "Random", MedecineName = "Random", Selected = false },
            new RecallLineViewModel { PatientName = "Random", MedecineName = "Random", Selected = true }, new RecallLineViewModel { PatientName = "Random", MedecineName = "Random", Selected = false },
            new RecallLineViewModel { PatientName = "Random", MedecineName = "Random", Selected = true }, new RecallLineViewModel { PatientName = "Random", MedecineName = "Random", Selected = false },
            new RecallLineViewModel { PatientName = "Random", MedecineName = "Random", Selected = false }, new RecallLineViewModel { PatientName = "Random", MedecineName = "Random", Selected = false }};

            return PartialView("RecallsPartialView", RecallLinesModel);
        }

        #region Helpers
        private string toUpperCase(string s)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }

        public enum CommunicationPreferenceId
        {
            PhoneCall,
            TextMessage,
            Email
        }

        public enum MessageTypeId
        {
            Refill,
            PickUp,
            Recall,
            Birthday
        }

        #endregion
    }
}