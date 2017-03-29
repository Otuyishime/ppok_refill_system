using Dapper;
using ppok_refill.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.Identity.Dapper
{
    public class ImportDBManager
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ppok;Integrated Security=True";

        public void addImport(Import import)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Imports VALUES(@Date_Uploaded, @UserName, @Type, @FileName);";
                connection.Query<Import>(sql, import);
            }
        }

        public List<Import> getImports()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "select * from Imports";
                return connection.Query<Import>(sql).ToList();
            }
        }
    }
}
