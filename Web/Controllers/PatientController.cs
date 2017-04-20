using AspNet.Identity.Dapper;
using Microsoft.AspNet.Identity.Owin;
using ppok_refill.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("Patient")]
    public class PatientController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context;

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

        // GET: Patient
        public ActionResult Index(string patientName)
        {
            var model = new RefillViewModel() { PatientName = patientName};
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> SubmitResponse(RefillResponseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pickupManager = new PickUpsDBManager();
                var retrievedPickup = pickupManager.findPickUpById(model.pickupId);

                if (model.SelectedConfirm && retrievedPickup != null)
                {
                    pickupManager.confirmRefill(retrievedPickup.PickupId);
                    var appmember = await UserManager.FindByNameAsync(retrievedPickup.PatientName);
                    
                    if (appmember != null)
                    {
                        appmember.CommunicationType = model.CommunicationType;
                        await UserManager.UpdateAsync(appmember);
                    }
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult getForm(RefillViewModel model)
        {
            if (ModelState.IsValid)
            {
                // find the guid in the table and return the form
                var pickupManager = new PickUpsDBManager();
                var retrievedPickup = pickupManager.findPickUpByPatientName(model.PatientName);

                if (model.Code == retrievedPickup.GuidRand)
                {
                    var refillresponsemodel = new RefillResponseViewModel();
                    refillresponsemodel.CommunicationType = (int)CommunicationPreferenceId.Email;
                    refillresponsemodel.pickupId = retrievedPickup.PickupId;
                    return PartialView("_RefillResponsePartialView", refillresponsemodel);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> UnSubscribe(string patientName)
        {
            if (ModelState.IsValid)
            {
                var userStore = new UserStore<AppMember>(Context);
                var appmember = await userStore.FindByNameAsync(patientName);

                if (appmember != null)
                {
                    appmember.CommunicationType = (int)CommunicationPreferenceId.TextMessage;
                    
                    await userStore.UpdateAsync(appmember);
                }
                return View();
            }
            return new EmptyResult();
        }

        #region Helpers
        public enum CommunicationPreferenceId
        {
            TextMessage = 1,
            PhoneCall,
            Email
        }
        #endregion
    }
}