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
    public class clsLicenseClassesData
    {
        public static bool GetLicenseClassByID(
    int LicenseClassID,
    ref string ClassName,
    ref string ClassDescription,
    ref byte MinimumAllowedAge,
    ref byte DefaultValidityLength,
    ref decimal ClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT *
                     FROM LicenseClasses
                     WHERE LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToDecimal(reader["ClassFees"]);
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

        public static int AddNewLicenseClass(
    string ClassName,
    string ClassDescription,
    byte MinimumAllowedAge,
    byte DefaultValidityLength,
    decimal ClassFees)
        {
            int LicenseClassID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO LicenseClasses
                    (ClassName, ClassDescription, MinimumAllowedAge,
                     DefaultValidityLength, ClassFees)
                     VALUES
                    (@ClassName, @ClassDescription, @MinimumAllowedAge,
                     @DefaultValidityLength, @ClassFees);

                     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    LicenseClassID = ID;
                }
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

            return LicenseClassID;
        }


        public static bool UpdateLicenseClass(
    int LicenseClassID,
    string ClassName,
    string ClassDescription,
    byte MinimumAllowedAge,
    byte DefaultValidityLength,
    decimal ClassFees)
        {
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE LicenseClasses
                     SET ClassName = @ClassName,
                         ClassDescription = @ClassDescription,
                         MinimumAllowedAge = @MinimumAllowedAge,
                         DefaultValidityLength = @DefaultValidityLength,
                         ClassFees = @ClassFees
                     WHERE LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
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

            return RowsAffected > 0;
        }

        public static bool DeleteLicenseClass(int LicenseClassID)
        {
            int RowsAffected = 0;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"DELETE FROM LicenseClasses
                     WHERE LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
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

            return RowsAffected > 0;
        }

        public static bool IsLicenseClassExist(int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1
                     FROM LicenseClasses
                     WHERE LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object Result = command.BeginExecuteNonQuery();

                isFound = (Result != null);
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

            return isFound;
        }

        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT *
                     FROM LicenseClasses";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);

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
