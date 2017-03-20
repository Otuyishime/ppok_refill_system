using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Prescription
    {
        public int? Id { get; set; }
        public int User_Id { get; set; }
        // public string Medication_Id { get; set; } //NDC UPC HRI
        public Medication medication { get; set; }
        public string Medication_Id { get; set; }
        public int Refills_Left { get; set; }
        public int Days_Supply { get; set; }
        public DateTime? Last_Date_Filled { get; set; }
    }
}