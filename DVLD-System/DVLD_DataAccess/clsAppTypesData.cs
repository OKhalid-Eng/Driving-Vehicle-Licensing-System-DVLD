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
    public class clsAppTypesData
    {
        public static bool GeAppTypesByID(int AppID, ref string Tittle,ref float AppFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @AppID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppID", AppID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    Tittle = (string)reader["ApplicationTypeTitle"];

                    AppFees = Convert.ToSingle(reader["ApplicationFees"]);
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


        public static bool UpdateAppTypesFeesandTiitleByID(int AppId, string ApplicationTypeTitle, float AppFees)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update ApplicationTypes 
                            set ApplicationTypeTitle = @ApplicationTypeTitle, 
                                ApplicationFees = @AppFees 
                                where ApplicationTypeID = @AppId";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppId", AppId);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@AppFees", AppFees);

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



        public static DataTable GetAllAppTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes";

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
