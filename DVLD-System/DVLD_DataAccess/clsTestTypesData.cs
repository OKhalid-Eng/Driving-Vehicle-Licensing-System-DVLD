using DVLD_DataAccessSettings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestTypesData
    {
        public static bool GeTestTypesByID(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref float TestFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];

                    TestFees = Convert.ToSingle(reader["TestTypeFees"]);
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(
                       "DVLD",
                       ex.ToString(),
                       EventLogEntryType.Error);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static bool UpdateTestTypesFeesAndTiitleAndDescByID(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestFees)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update TestTypes 
                            set TestTypeTitle = @TestTypeTitle, 
                                TestTypeDescription = @TestTypeDescription,
								TestTypeFees = @TestFees 
                                where TestTypeID = @TestTypeID";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestFees", TestFees);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(
                       "DVLD",
                       ex.ToString(),
                       EventLogEntryType.Error);
            }

            finally
            {
                connection.Close();
            }


            return rowsAffected > 0;

        }



        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes";

            SqlCommand command = new SqlCommand(query, connection);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(
                       "DVLD",
                       ex.ToString(),
                       EventLogEntryType.Error);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }


    }

}
