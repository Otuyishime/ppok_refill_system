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
        // DbManager dbManager = new DbManager();
        // string connectionString = ConfigurationManager.ConnectionStrings[0].Name;
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ppok_refill_system;Integrated Security=True";

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
                connection.Close();
            }
        }
    }
}
