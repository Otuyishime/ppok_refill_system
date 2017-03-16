using AspNet.Identity.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Patient : IdentityMember
    {
        public Patient()
        {
            comPref = "phone";
        }
        public string comPref { get; set; }
    }
}