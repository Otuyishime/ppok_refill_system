using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppok_refill.Models
{
    public class PickUpsDBManager
    {
        private PickUpsTable _pickUpTable { get; set; }

        public PickUpsDBManager()
        {
            _pickUpTable = new PickUpsTable();
        }

        public List<PickUp> getDuePickUps()
        {
            return _pickUpTable.getDuePickUps();
        }

        public void createPickUp(PickUp pickup)
        {
            _pickUpTable.create(pickup);
        }

        public PickUp findPickUpByPatientName(string patientName)
        {
            return _pickUpTable.findPickUpByPatientName(patientName);
        }

        public PickUp findPickUpById(int pickupId)
        {
           return _pickUpTable.findPickUpById(pickupId);
        }

        public int confirmRefill(int pickupId)
        {
            return _pickUpTable.confirmRefill(pickupId);
        }

        public void deletePickUp(int pickupId)
        {
            _pickUpTable.delete(pickupId);
        }
    }
}
