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
    public class PrescriptionDBManager
    {
        private DbManager dbManager;
        //string connectionString = ConfigurationManager.ConnectionStrings[0].Name;
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ppok;Integrated Security=True";

        public PrescriptionDBManager ()
        {
        }

        public PrescriptionDBManager(DbManager database)
        {
            dbManager = database;
        }

        public List<Prescription> getPrescriptions()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Prescription;";
                return connection.Query<Prescription>(sql).AsList();
            }
        }

        // This returns the ID of the newly created prescription
        public int create(Prescription prescription)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Prescription VALUES (@User_Id, @Medication_Id, @Refills_Left, @Days_Supply, @Last_Date_Filled); SELECT CAST(SCOPE_IDENTITY() as int);";
                int id = connection.Query<int>(sql, prescription).Single();
                return id;
            }
        }
    }
}
