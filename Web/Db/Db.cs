using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Web.Models;

namespace ppok_refill_system.UI.Db
{
    public class Db
    {
        // Needs database
        public static string Connection = "REPLACE THIS";

        //
        // USER
        //
        public static int CreateUser(User user)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "REPLACE THIS";

                return conn.Query<int>(sql, user).Single();
            }
        }

        public static void UpdateUser(User user)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "REPLACE THIS";

                conn.Query(sql, user);
            }
        }

        public static User GetUser(int? userId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "REPLACE THIS";

                User user = conn.QueryFirstOrDefault<User>(sql,
                    new
                    {
                        UserId = userId
                    });

                /*Get user */

                return user;
            }
        }

        public static List<User> GetAllUsers()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<User> userList = conn.Query<User>("REPLACE THIS").AsList();

                return userList;
            }
        }

        public static List<User> GetAllUsersByType(String type)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<User> userList = conn.Query<User>("REPLACE THIS").AsList();

                return userList;
            }
        }

        public static void DeleteUser(User user)
        {
            DeleteUser(user.Id);
        }

        public static void DeleteUser(int? userId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("REPLACE THIS",
                    new
                    {
                        UsedId = userId
                    });
            }
        }

        //
        // PRESCRIPTIONS
        //
        public static int CreatePrescription(Prescription prescription)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "REPLACE THIS";

                return conn.Query<int>(sql, prescription).Single();
            }
        }

        public static void UpdatePrescription(Prescription prescription)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "REPLACE THIS";

                conn.Query(sql, prescription);
            }
        }

        public static Prescription GetPrescription(int? prescriptionId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "REPLACE THIS";

                Prescription prescription = conn.QueryFirstOrDefault<Prescription>(sql,
                    new
                    {
                        Id = prescriptionId
                    });

                /*Get prescription */

                return prescription;
            }
        }

        public static List<Prescription> GetAllPrescriptions()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Prescription> prescriptionList = conn.Query<Prescription>("REPLACE THIS").AsList();

                return prescriptionList;
            }
        }

        public static List<Prescription> GetAllPrescriptionsForUser(User user)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Prescription> prescriptionList = conn.Query<Prescription>("REPLACE THIS").AsList();

                return prescriptionList;
            }
        }

        public static List<Prescription> GetAllPrescriptionsForDate(DateTime date)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Prescription> prescriptionList = conn.Query<Prescription>("REPLACE THIS").AsList();

                return prescriptionList;
            }
        }

        public static void DeletePrescription(Prescription prescription)
        {
            DeletePrescription(prescription.Id);
        }

        public static void DeletePrescription(int? prescriptionId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("REPLACE THIS",
                    new
                    {
                        Id = prescriptionId
                    });
            }
        }

        //
        // MEDICATION
        //

        public static int CreateMedication(Medication medication)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "REPLACE THIS";

                return conn.Query<int>(sql, medication).Single();
            }
        }

        public static void UpdateMedication(Medication medication)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "REPLACE THIS";

                conn.Query(sql, medication);
            }
        }

        public static Medication GetMedication(int? medicationId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "REPLACE THIS";

                Medication medication = conn.QueryFirstOrDefault<Medication>(sql,
                    new
                    {
                        Id = medicationId
                    });

                /*Get medication */

                return medication;
            }
        }

        public static List<Medication> GetAllMedication()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Medication> medicationList = conn.Query<Medication>("REPLACE THIS").AsList();

                return medicationList;
            }
        }

        public static Medication GetMedicationByPrescription(Prescription prescription)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                Medication medication = conn.Query<Medication>("REPLACE THIS");

                return medication;
            }
        }

        public static void DeleteMedication(Medication medication)
        {
            DeleteMedication(medication.Id);
        }

        public static void DeleteMedication(int? medicationId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("REPLACE THIS",
                    new
                    {
                        Id = medicationId
                    });
            }
        }

        //
        // Template
        //

        public static Template GetTemplate(int? templateId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "REPLACE THIS";

                Template template = conn.QueryFirstOrDefault<Template>(sql,
                    new
                    {
                        Id = templateId
                    });

                /*Get template*/

                return template;
            }
        }

        public static void UpdateTemplate(Template template)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "REPLACE THIS";

                conn.Query(sql, template);
            }
        }

        //Refill, confirmation, recall, birthday
        public static List<Template> GetAllTemplatesByTempType(String type)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Template> templateList = conn.Query<Template>("REPLACE THIS").AsList();

                return templateList;
            }
        }

        // phone, text, email
        public static List<Template> GetAllTemplatesByComType(String type)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Template> templateList = conn.Query<Template>("REPLACE THIS").AsList();

                return templateList;
            }
        }

        //
        // SCHEDULE
        //

        // return all the messages to be sent on or after a given date
        public static List<Schedule> GetScheduleAfterDate(DateTime date)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Schedule> schedule = conn.Query<Schedule>("REPLACE THIS").AsList();

                return schedule;
            }
        }

        // return all the messages to be sent before a given date
        public static List<Schedule> GetScheduleBeforeDate(DateTime date)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Schedule> schedule = conn.Query<Schedule>("REPLACE THIS").AsList();

                return schedule;
            }
        }

        // return the schedule between 2 dates
        public static List<Schedule> GetScheduleBtweenDates(DateTime date1, DateTime date2)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Schedule> schedule = conn.Query<Schedule>("REPLACE THIS").AsList();

                return schedule;
            }
        }

        // return the schedule for one day
        public static List<Schedule> GetScheduleForDate(DateTime date)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                List<Schedule> schedule = conn.Query<Schedule>("REPLACE THIS").AsList();

                return schedule;
            }
        }
    }
}