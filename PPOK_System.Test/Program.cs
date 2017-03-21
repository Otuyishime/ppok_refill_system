using System;
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
