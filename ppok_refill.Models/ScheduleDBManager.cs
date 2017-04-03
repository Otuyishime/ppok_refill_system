using Dapper;
using ppok_refill.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models;

namespace AspNet.Identity.Dapper
{
    public class ScheduleDBManager
    {
        private DbManager dbManager;
        // string connectionString = ConfigurationManager.ConnectionStrings[0].Name;
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ppok_refill_system;Integrated Security=True";

        public ScheduleDBManager()
        {
        }

        public ScheduleDBManager(DbManager database)
        {
            dbManager = database;
        }

        public List<Schedule> getSchedules()
        {
            return null;
        }

        // This returns the ID of the newly created prescription
        public void create(Schedule schedule)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Schedule VALUES (@Prescription_Id, @Future_Refill_Date, @Approved);";
                connection.Query<Schedule>(sql, schedule);
            }
        }

        // Get all due refills
        public List<Schedule> getDueRefills()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Schedule>("Select * from Schedule where Future_Refill_Date = @date", 
                    new { date = "2008-08-04" }).ToList();
            }
        }

        // Get all due refills
        public List<refillHelper> getDueRefillsInfo()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<refillHelper>(@"SELECT S_P.sch_Id as Schedule_Id, Pres_Id as Prescription_Id,
                                                        S_P.User_Id as Patient_Id, UserName as PatientName, med_name as MedicationName 
                                                        FROM Member, Medication, 
                                                        (SELECT Id as Pres_Id, User_Id, S.sch_Id, Medication_Id FROM Prescription,
                                                        (SELECT Prescription_Id, Id as sch_Id FROM Schedule WHERE Future_Refill_Date = @date) AS S
                                                        WHERE Prescription.Id = S.Prescription_Id) AS S_P
                                                        WHERE Member.Id = S_P.User_Id AND Medication.ID = S_P.Medication_Id",
                    new { date = "2008/08/04" }).ToList();
            }
        }
    }
}
