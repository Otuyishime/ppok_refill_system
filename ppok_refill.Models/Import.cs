using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppok_refill.Models
{
    public class Import
    {
        public int ID { get; set; }
        public DateTime Date_Uploaded { get; set; }
        public  string UserName { get; set; }
        public string Type { get; set; }
        public string FileName { get; set; }
    }
}
