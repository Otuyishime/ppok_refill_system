using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ppok_refill.Models;
using Dapper;

namespace AspNet.Identity.Dapper
{
    public class TemplatesManager
    {
        DbManager dbManager;
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ppok_refill_system;Integrated Security=True";

        public TemplatesManager()
        {
        }

        public TemplatesManager(DbManager database)
        {
            dbManager = database;
        }

        public Template findTemplateById(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<Template>("SELECT * FROM Template WHERE Id = @templateId", new { templateId = id }).First();
                return result;
            }
            
        }

        public Template findTemplateByTypeAndCommPref(int templateType, int communicationType)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<Template>(@"SELECT * FROM Template WHERE Template.Comm_Type_Id=@communicationType AND Template.Temp_Type_Id=@templateType", 
                                        new { communicationType = communicationType, templateType = templateType }).FirstOrDefault();
                return result;
            }

        }
    }
}
