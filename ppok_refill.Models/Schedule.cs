using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Schedule
    {
        public int? Id { get; set; }
        // public string Prescription_Id { get; set; }
        // public Prescription prescription { get; set; }
        public int Prescription_Id { get; set; }
        public DateTime Future_Refill_Date { get; set; }
        public bool Approved { get; set; }
    }
}