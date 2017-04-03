using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AspNet.Identity.Dapper;
using ppok_refill.Models;
using System.Threading;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin, Pharmacist")]
    public class ImportController : Controller
    {

        private ApplicationUserManager _userManager;
        private RoleManager<IdentityRole, int> _userRoleManager;
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

        public RoleManager<IdentityRole, int> RoleManager
        {
            get
            {
                ApplicationDbContext context = ApplicationDbContext.Create();
                return _userRoleManager ?? new RoleManager<IdentityRole, int>(new RoleStore<IdentityRole>(context));
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

        // GET: Import
        public ActionResult Index()
        {
            ImportDBManager importDBManager = new ImportDBManager();
            List<Import> list = importDBManager.getImports();
            var importViewModel = new ImportViewModel { Imports = list };

            if (TempData["CustomError"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
            }

            if (TempData["previous_url"] != null && (string)TempData["previous_url"] == "~/home/Upload")
            {
                ViewBag.UploadSuccess = "File uploaded successfully"; 
            }
            return View(importViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Upload( HttpPostedFileBase upload)
        {
            string type = Request.Form["type"].ToString();
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var allowedExtensions = new[] { ".xlsx", ".csv" };
                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        StreamReader csvreader = new StreamReader(stream);

                        csvreader.ReadLine(); // skip the headers : fisrt line

                        while (!csvreader.EndOfStream)
                        {
                            var line = csvreader.ReadLine();
                            var values = line.Split(',');

                            string PersonCode = values[0];  
                            string PatientFirstName = values[1];    
                            string PatientLastName = values[2];     
                            string DOB = values[3];                    
                            string PatientZipCode = values[4];  
                            string Phone = values[5];   
                            string Email = values[6];
                            string DateFilled = values[7];         
                            string PrescriptionNumber = values[8];
                            string DaysSupply = values[9];
                            string NumberOfRefills = values[10];
                            string NDCUPCHRI = values[11];
                            string GPIGenericName = values[12];

                            //Email = PatientFirstName.ToLower() + "." + PatientLastName.ToLower() + "@ppok.com"; // email format: firstname.lastname
                            string username = PatientFirstName + "." + PatientLastName; // username format: firstname.lastname
                            if (DOB == "NULL") { DOB = "19760323"; }    // just for now 
                            if (DateFilled == "NULL") { DateFilled = "19760323"; }    // just for now

                            //Thread.Sleep(10); // for testing

                            if (UserManager.FindByEmail(Email) == null && username != ".") // skip duplicated users - at least for now
                            {
                                DateTime parsedDate;
                                string[] formats = { "yyyyMMdd", "HHmmss" };
                                if (DateTime.TryParseExact(DOB, formats, null, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal, out parsedDate))
                                {
                                    // we have successfully parsed DOB to the date time
                                    // in this case, parsedDate will have the corresponding value
                                }
                                else
                                {
                                    // unable to parse the DOB to date time
                                    parsedDate = new DateTime();
                                }

                                /* step 01: create a user and save them to the database */
                                var appMember = new AppMember { UserName = username, PhoneNumber = Phone, Email = Email, DateBirth = parsedDate, Address = PatientZipCode, Active = true };
                                var result = UserManager.Create(appMember);
                                if (result.Succeeded)
                                {
                                    UserManager.AddToRole(appMember.Id, "Patient");
                                }

                                /* step 02: create a medication record */
                                string med_id = NDCUPCHRI;
                                string med_name = GPIGenericName;
                                string med_description = "no-description";
                                MedicationDBManager medDB = new MedicationDBManager();
                                if (!medDB.exists(med_id)) // make sure the medication is in the db
                                {
                                    Medication medication = new Medication();
                                    medication.Id = med_id;
                                    medication.Med_Name = med_name;
                                    medication.Med_Description = med_description;
                                    medDB.create(medication);
                                }

                                /* step 03: create a perscription record, link to the previously created user and save it to the db */
                                int user_id = appMember.Id;
                                int refills_left = Int32.Parse(NumberOfRefills);    // TODO: need to deal with exceptions here
                                int days_suply = Int32.Parse(DaysSupply);   // TODO: need to deal with exceptions here
                                DateTime parsedDateFilled;
                                if (DateTime.TryParseExact(DateFilled, formats, null, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal, out parsedDateFilled))
                                {
                                    // we have successfully parsed DOB to the date time
                                    // in this case, parsedDate will have the corresponding value
                                }
                                else
                                {
                                    // unable to parse the DateFilled to date time - just leave it null
                                    parsedDateFilled = new DateTime();
                                }
                                Prescription prescription = new Prescription();
                                prescription.User_Id = user_id;
                                prescription.Medication_Id = med_id;
                                prescription.Refills_Left = refills_left;
                                prescription.Days_Supply = days_suply;
                                prescription.Last_Date_Filled = parsedDateFilled;
                                PrescriptionDBManager prescDB = new PrescriptionDBManager();
                                int prescriptionID = prescDB.create(prescription);

                                /* step 04: create a schedule record and save it to the db */
                                DateTime future_refill_date = new DateTime();
                                if (DateTime.Compare(parsedDateFilled, DateTime.Now) < 0)
                                {
                                    future_refill_date = parsedDateFilled.AddDays(Int32.Parse(DaysSupply));
                                }
                                else
                                {
                                    future_refill_date = parsedDateFilled;
                                }
                                Schedule schedule = new Schedule();
                                schedule.Prescription_Id = prescriptionID;
                                schedule.Future_Refill_Date = future_refill_date;
                                schedule.Approved = false;

                                ScheduleDBManager scheduleDBManager = new ScheduleDBManager();
                                scheduleDBManager.create(schedule);
                            }
                        }

                        // save the import record
                        //ImportDBManager importDBManager = new ImportDBManager();
                        //Import import = new Import { UserName  = User.Identity.Name , FileName = upload.FileName, Type = type };
                        //importDBManager.addImport(import);

                        TempData["previous_url"] = "~/home/Upload";
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
                    TempData["CustomError"] = "You need to pload a file!";
                }
            }
            return RedirectToAction("Index");
        }

    }
}