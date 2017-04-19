using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PPOK_Refill_System_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PPOK_Refill_System_Service" in both code and config file together.
    public class PPOK_Refill_System_Service : IPPOK_Refill_System_Service
    {
        public string DoWork()
        {
            submitUserConfirmation(1, true, 1);
            return "Using the PPOK Service!";
        }

        public void submitUserConfirmation(int userId, bool confirmation, int communticationId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["wcfConnection"].Name;
            /*@"Data Source=.\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";*/
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO test_wcf VALUES (@userId, @confirmation, @communicationId);";
                connection.Query(sql, new { userId = userId,
                                            confirmation = confirmation,
                                            communticationId = communticationId});
            }
        }

        public void unSubscribe(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
