using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class ClsPersonData
    {
        public static DataTable GetAllPeople()
        {
            var dtAllPeople = new DataTable();

            var connection = new SqlConnection(ClsDataSetting.ConnectionString);

            var Query = "SELECT * FROM People";

            var command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtAllPeople.Load(reader);

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

            return dtAllPeople;
        }


        public static bool FindPersonByID(int PersonID, ref string nationalNo, ref string firstName,
            ref string secondName, ref string thirdName, ref string lastName, ref DateTime dateOfBirth,
            ref byte gendor, ref string address, ref string phone, ref string email,
            ref int nationalityCountryId, ref string imagePath)
        {
            var isFound = false;

            var connection = new SqlConnection(ClsDataSetting.ConnectionString);

            var query = @"SELECT * FROM People WHERE PersonID = @PersonID";

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    firstName = reader["FirstName"].ToString();
                    secondName = reader["SecondName"].ToString();

                    if (reader["ThirdName"] == DBNull.Value)
                        thirdName = "";
                    else
                        thirdName = reader["ThirdName"].ToString();


                    lastName = reader["LastName"].ToString();
                    dateOfBirth = Convert.ToDateTime(reader["DateOfBirth"].ToString());
                    gendor = (byte)reader["Gendor"];
                    address = reader["Address"].ToString();
                    phone = reader["Phone"].ToString();
                    email = reader["Email"].ToString();
                    nationalityCountryId = Convert.ToInt32(reader["NationalityCountryID"]);

                    if (reader["ImagePath"] == DBNull.Value)
                        imagePath = "";
                    else
                        imagePath = reader["ImagePath"].ToString();
                }


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


            return isFound;
        }


        public static int AddNewPerson(string nationalNo, string firstName,
            string secondName, string thirdName, string lastName,
            DateTime dateOfBirth, byte gendor, string address, string phone,
            string email, int nationalityCountryId, string imagePath)
        {
            var personId = -1;

            var connection = new SqlConnection(ClsDataSetting.ConnectionString);

            var query = @"
                            INSERT INTO People
                                (
                                 nationalno, 
                                 firstname, secondname, 
                                 thirdname, lastname, 
                                 dateofbirth, gendor, 
                                 address, 
                                 phone, 
                                 email, 
                                 nationalitycountryid, 
                                 imagepath) 
                            VALUES
                                (
                                 
                                 @nationalno,
                                 @firstName,
                                 @secondName,
                                 @thirdName,
                                 @lastName,
                                 @dateOfBirth,
                                 @gendor,
                                 @address,
                                 @phone,
                                 @email,
                                 @nationalityCountrtyId,
                                 @imagePath
                                    
                                );
                            SELECT SCOPE_IDENTITY();";

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@nationalno", nationalNo);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@secondName", secondName);


            if (!string.IsNullOrEmpty(thirdName))
                command.Parameters.AddWithValue("@thirdName", thirdName);
            else
                command.Parameters.AddWithValue("@thirdName", DBNull.Value);


            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@gendor", gendor);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@phone", phone);

            //if (email != null && email != "")
            //    command.Parameters.AddWithValue("@email", email);
            // else
            //    command.Parameters.AddWithValue("@email", System.DBNull.Value);

            command.Parameters.AddWithValue
            (
                "@email",
                string.IsNullOrEmpty(email) ? DBNull.Value : (object)email
            );


            command.Parameters.AddWithValue("@nationalityCountrtyId", nationalityCountryId);

            if (!string.IsNullOrEmpty(imagePath))
                command.Parameters.AddWithValue("@imagePath",
                    imagePath);
            else
                command.Parameters.AddWithValue("@imagePath", DBNull.Value);

            try
            {
                connection.Open();
                var result = command.ExecuteScalar();

                if (result != null && int.TryParse(Convert.ToString(result), out var insertPersonId))
                    personId = insertPersonId;
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


            return personId;
        }


        public static bool UpdatePerson(int personId, string nationalNo, string firstName,
            string secondName, string thirdName, string lastName, DateTime dateOfBirth,
            byte gendor, string address, string phone, string email, int nationalityCountryId,
            string imagePath)
        {
            byte rowAffected = 0;

            var connection = new SqlConnection(ClsDataSetting.ConnectionString);

            var query = @"UPDATE People
                                SET People.NationalNo           = @nationalNo,
                                    People.FirstName            = @firstName,
                                    People.SecondName           = @secondName,
                                    People.ThirdName            = @thirdName,
                                    People.LastName             = @lastName,
                                    People.DateOfBirth          = @dateOfBirth,
                                    People.Gendor               = @gendor,
                                    People.Address              = @address,
                                    People.Phone                = @phone,
                                    People.Email                = @email,
                                    People.NationalityCountryID = @nationalityCountryId,
                                    People.ImagePath            = @imagePath
                                WHERE People.PersonID = @personId;";

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@personId", personId);
            command.Parameters.AddWithValue("@nationalNo", nationalNo);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@secondName", secondName);


            command.Parameters.AddWithValue
            (
                "@thirdName", string.IsNullOrEmpty(thirdName) ? DBNull.Value : (object)thirdName
            );


            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@gendor", gendor);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@phone", phone);


            command.Parameters.AddWithValue
            (
                "@email", string.IsNullOrEmpty(email) ? DBNull.Value : (object)email
            );


            command.Parameters.AddWithValue("@nationalityCountryId", nationalityCountryId);

            command.Parameters.AddWithValue
            (
                "@imagePath", string.IsNullOrEmpty(imagePath) ? DBNull.Value : (object)imagePath
            );


            try
            {
                connection.Open();
                rowAffected = (byte)command.ExecuteNonQuery();
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


            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return rowAffected > 0;
        }

        public static bool DeletePerson(int personId)
        {
            var rowAffected = 0;

            var connection = new SqlConnection(ClsDataSetting.ConnectionString);
            var query = @"DELETE FROM People WHERE People.PersonID = @personId;";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personId", personId);

            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
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

            return rowAffected > 0;
        }

        public static bool IsPersonExist(int personId)
        {
            var isFound = false;

            var connection = new SqlConnection(ClsDataSetting.ConnectionString);

            var query = @"SELECT 1
                                From People
                                WHERE People.PersonID = @personID;";

            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personID", personId);


            try
            {
                connection.Open();
                isFound = command.ExecuteScalar() != null;
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

            return isFound;
        }
    }
}