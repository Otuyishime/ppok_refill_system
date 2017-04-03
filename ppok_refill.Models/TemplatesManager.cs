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
        public static string connString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ppok_refill_system;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public DbManager db = new DbManager(connString);
        
        public List<Template> listTemplates()
        {
            string sql = @"SELECT A.Id, A.Temp_Message ,A.Temp_Type_Id, A.Temp_Type_Id, A.Temp_Type_Name, a.Temp_Type_Description, Comm_Type_Id,Comm_Type_Id, comm_type_name, comm_type_description
            FROM
            (SELECT Template.Id, Template.Temp_Message, Template.Comm_Type_Id, Template.Temp_Type_Id, TemplateType.Temp_Type_Name, TemplateType.Temp_Type_Description 
            FROM Template 
            JOIN TemplateType ON Template.Temp_Type_Id = TemplateType.Id) A 
            JOIN CommunicationType ON CommunicationType.ID=A.Comm_Type_Id";

            var queryResult = db.Connection.Query<Template, TemplateType, CommunicationType, Template>(sql,

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

        public Template findTemplateGivenId(int id)
        {
            var result = db.Connection.Query<Template>("SELECT * FROM Template WHERE Id = @templateId", new { templateId = id }).First();

            return result;
        }

        //Updating a template given an id
        public void updateTemplate(int Id, string Temp_Message)
        {
            db.Connection.Query<Template>("UPDATE Template SET " +

                "Temp_Message = @tMessage " + " WHERE " + "Id = @Id", new { tMessage=Temp_Message, Id = Id});
            
        }
        
    }
}
