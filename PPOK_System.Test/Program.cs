using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNet.Identity.Dapper;

namespace PPOK_System.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!!!!");
            MedicationDBManager test = new MedicationDBManager();
            Console.WriteLine(test.exists("MYI"));
        }
    }
}
