using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPOK_System.Domain;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace AspNet.Identity.Dapper
{
    public class TemplatesManager
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ppok_refill_system;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Template> listTemplates()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = @"SELECT A.Id, A.Temp_Message , A.Temp_Type_Id, A.Temp_Type_Name, a.Temp_Type_Description, Comm_Type_Id, comm_type_name, comm_type_description
                FROM
                (SELECT Template.Id, Template.Temp_Message, Template.Comm_Type_Id, Template.Temp_Type_Id, TemplateType.Temp_Type_Name, TemplateType.Temp_Type_Description 
                FROM Template 
                JOIN TemplateType ON Template.Temp_Type_Id = TemplateType.Id) A 
                JOIN CommunicationType ON CommunicationType.ID=A.Comm_Type_Id";

                var queryResult = connection.Query<Template, TemplateType, CommunicationType,Template>(sql,

                        (t, tt, ct) =>
                        {
                            if (tt != null)
                                t.templateType = tt;
                            if (ct != null)
                                t.communicationType = ct;
                            return t;
                        },

                        splitOn: "Temp_Type_Id,Comm_Type_Id"

                    ).AsList();

                return queryResult;
            }

        }

        public Template findTemplateGivenId(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<Template>("SELECT * FROM Template WHERE Id = @templateId", new { templateId = id }).First();

                return result;
            }
        }

        //Updating a template given an id
        public void updateTemplate(Template template)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Query<Template>("UPDATE Template SET " +
                    
                    "Temp_Message = @Temp_Message " + " WHERE " + "Id = @Id", template);
            }
        }

    }
}
