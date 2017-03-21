using Dapper;
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
    }
}
