using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Medication
    {
        public string Id { get; set; }
        public string Med_Name { get; set; }
        public string Med_Description { get; set; }
    }
}