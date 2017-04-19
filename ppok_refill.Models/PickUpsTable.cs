using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppok_refill.Models
{
    public class PickUpsTable
    {
        // string connectionString = ConfigurationManager.ConnectionStrings[0].Name;
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ppok_refill_system;Integrated Security=True";

        public List<PickUp> getDuePickUps()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM DuePickUps WHERE IsPickUpReady = 1;";
                return connection.Query<PickUp>(sql).AsList();
            }
        }

        public void create(PickUp pickup)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"INSERT INTO DuePickUps ( PatientId, PatientName, MedecineName, GuidRand, IsPickUpReady)
                                VALUES(@PatientId, @PatientName, @MedecineName, @GuidRand, @IsPickUpReady)";
                connection.Query<PickUp>(sql, new { PatientId = pickup.PatientId, PatientName = pickup.PatientName,
                    MedecineName = pickup.MedecineName, GuidRand = pickup.GuidRand,
                    IsPickUpReady = pickup.IsPickUpReady});
            }
        }

        public PickUp findPickUpByPatientName(string patientName)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM DuePickUps WHERE PatientName = @patientName;";
                return connection.Query<PickUp>(sql, new { patientName = patientName}).FirstOrDefault();
            }
        }

        public PickUp findPickUpById(int pickupId)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM DuePickUps WHERE PickupId = @pickupId;";
                return connection.Query<PickUp>(sql, new { pickupId = pickupId }).FirstOrDefault();
            }
        }

        public int confirmRefill(int pickupId)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE DuePickUps SET IsPickUpReady = 1 WHERE PickupId = @pickupId;";
                return connection.Execute(sql, new { pickupId = pickupId });
            }
        }

        public void delete(int pickupId)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM DuePickUps WHERE PickupId = @pickupId;";
                connection.Query<PickUp>(sql, new { PickupId = pickupId });
            }
        }
    }
}
