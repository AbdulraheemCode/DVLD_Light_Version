using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class ClsCountryData
    {
        public static DataTable GetAllCountries()
        {
            var dtAllCountries = new DataTable();

            var connection = new SqlConnection(ClsDataSetting.ConnectionString);

            var query = "SELECT * FROM Countries";

            var command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtAllCountries.Load(reader);

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                connection.Close();
            }

            return dtAllCountries;
        }

        public static bool Find(int countryId, ref string countryName)
        {
            var isFound = false;

            var connection = new SqlConnection(ClsDataSetting.ConnectionString);

            var query = "SELECT * FROM Countries WHERE CountryID = @countryId";

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@countryId", countryId);

            try
            {
                connection.Open();
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    countryName = reader["CountryName"].ToString();
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool Find(string countryName, ref int countryId)
        {
            var isFound = false;

            var connection = new SqlConnection(ClsDataSetting.ConnectionString);

            var query = "SELECT * FROM Countries WHERE CountryName = @countryName";

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue(countryName, countryName);

            try
            {
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    countryId = Convert.ToInt32(reader["CountryID"]);
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }


            return isFound;
        }
    }
}