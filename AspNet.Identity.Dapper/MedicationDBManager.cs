using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Web.Models;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;

namespace AspNet.Identity.Dapper
{
    public class MedicationDBManager
    {
        // DbManager dbManager = new DbManager();

        // string connectionString = ConfigurationManager.ConnectionStrings[0].Name;
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ppok_refill_system;Integrated Security=True";
        public MedicationDBManager()
        {
        }

        public List<Medication> getMedications()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Medication";
                return connection.Query<Medication>(sql).AsList();
            }
        }

        public void create (Medication medication)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Medication VALUES(@Id, @Med_Name, @Med_Description)";
                connection.Query<Medication>(sql, medication);
                connection.Close();
            }
        }

        public void delete(string Id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Medication WHERE Id = @Id;";
                connection.Query<Medication>(sql, Id);
                connection.Close();
            }
        }

        public void update (Medication medication)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE  Medication SET med_name = @med_name, med_description = @med_description WHERE ID = @Id;";
                connection.Query<Medication>(sql, medication);
                connection.Close();
            }
        }

        public Medication get(string Id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Medication WHERE Id = @Id;";
                try
                {
                    return connection.Query<Medication>(sql, new { Id = Id }).First();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool exists(string Id)
        {
            if (this.get(Id) == null)
                return false;
            return true;
        }
    }
}
