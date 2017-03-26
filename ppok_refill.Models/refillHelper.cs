using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppok_refill.Models
{
    public class refillHelper
    {
        public int Schedule_Id { get; set; }
        public int Prescription_Id { get; set; }
        public int Patient_Id { get; set; }
        public string PatientName { get; set; }
        public string MedicationName { get; set; }
    }
}
