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

        public bool Active { get; set; }

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
}