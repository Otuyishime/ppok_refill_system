using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Patient : User
    {
        public Patient()
        {
            comPref = "phone";
        }
        public string comPref { get; set; }
    }
}