using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class User
    {
        public User()
        {
            Id = null;
            FirstName = null;
            LastName = null;
            Dob = null;
            Zip = null;
            Phone = null;
            Email = null;
            Type = null;
        }
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
    }
}