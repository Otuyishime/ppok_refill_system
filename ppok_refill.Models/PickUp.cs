using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppok_refill.Models
{
    public class PickUp
    {
        public int PickupId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string MedicineName { get; set; }
        public string GuidRand { get; set; }
        public bool IsPickUpReady { get; set; }
    }
}
