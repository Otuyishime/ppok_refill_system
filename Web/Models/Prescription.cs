using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public int MedId { get; set; } //NDC UPC HRI
        public int NumRefills { get; set; }
        public int DaysSupply { get; set; }
        public int PatientId { get; set; }
        public DateTime? DateFilled { get; set; }
    }
}