using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class ClsUserData
    {
        public static DataTable GetAllUsers()
        {
            var dt = new DataTable();

            var connection = new SqlConnection(ClsDataSetting.ConnectionString);

            var query = @"SELECT *
                            FROM Users
                            ";

            var command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);

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

            return dt;
        }


        public static int AddNewUser(int personId, string userName, string password, bool isActive)
        {
            var userId = -1;

            var connection = new SqlConnection(ClsDataSetting.ConnectionString);

            var query = @"INSERT INTO Users
                                (PersonID, UserName, Password, IsActive)
                                VALUES (@PersonID,
                                        @UserName,
                                        @Password,
                                        @IsActive);
                            SELECT SCOPE_IDENTITY();";

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", userId);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@IsActive", isActive);

            try
            {
                connection.Open();
                var result = command.ExecuteScalar();

                if (result != null && int.TryParse(result as string, out var insertedUserId)) userId = insertedUserId;
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


            return userId;
        }


        public static bool UpdateUser(int userId, int personId,
            string userName, string password, bool isActive)
        {
            var rowAffected = 0;

            var connection = new SqlConnection(ClsDataSetting.ConnectionString);

            var query = @"UPDATE Users
                                SET Users.PersonID = @PersonID,
                                    Users.UserName = @UserName,
                                    Users.Password = @Password,
                                    Users.IsActive = @IsActive
                                WHERE Users.UserID = @UserId";

            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", personId);
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@IsActive", isActive);
            command.Parameters.AddWithValue("@UserId", userId);

            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
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

            return rowAffected > 0;
        }
    }
}