using ppok_refill.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PharmacistViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Please provide your birth date.")]
        [Display(Name = "Birth Date")]
        public DateTime DateBirth { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class PharmacistsViewModel
    {
        public IEnumerable<PharmacistViewModel> Pharmacists { get; set; }
    }

    public class PatientViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Please provide your birth date.")]
        [Display(Name = "Birth Date")]
        public DateTime DateBirth { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(minimum:1, maximum:3, ErrorMessage ="Wrong communication type choice.")]
        [Display(Name = "Communication Preference")]
        public int CommunicationType { get; set; }
    }

    public class PatientsViewModel
    {
        public IEnumerable<PatientViewModel> Patients { get; set; }
    }

    public class RefillLineViewModel
    {
        public int pId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Patient")]
        public string PatientName { get; set; }

        [Required]
        [Display(Name = "Medecine")]
        public string MedecineName { get; set; }
    }

    public class RefillLinesViewModel
    {
        public List<RefillLineViewModel> Refills { get; set; }
    }

    public class SystemSummaryViewModel
    {
        public int number_patients { get; set; }
        public RefillLinesViewModel Due_Refills { get; set; }
        public RefillLinesViewModel Pending_Refills { get; set; }
    }

    public class RecallLineViewModel
    {
        [Required]
        public bool Selected { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Patient")]
        public string PatientName { get; set; }

        [Required]
        [Display(Name = "Medecine")]
        public string MedecineName { get; set; }
    }

    public class RecallLinesViewModel
    {
        public List<RecallLineViewModel> Recalls { get; set; }
    }
    
    /* this is for the import page */
    public class ImportViewModel
    {
        public List<Import> Imports { get; set; }
    }
}