using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class ClsTestTypesData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dtAllTestTypes = new DataTable();

            const string query = @"
                                    SELECT TestTypeID,
                                           TestTypeTitle,
                                           TestTypeDescription,
                                           TestTypeFees
                                    FROM TestTypes";

            using (SqlConnection connection = new SqlConnection(ClsDataSetting.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                connection.Open();
                dtAllTestTypes.Load(reader);
            }

            return dtAllTestTypes;
        }

        public static bool FindById(int testTypeId, ref string testTypeTitle,
            ref string testTypeDescription, ref decimal testTypeFees)
        {
            string query = @"SELECT TestTypeID,
                                   TestTypeTitle,
                                   TestTypeDescription,
                                   TestTypeFees
                            FROM TestTypes
                            WHERE TestTypeID = @testTypeId";


            using (SqlConnection connection = new SqlConnection(ClsDataSetting.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@testTypeId", SqlDbType.Int).Value = testTypeId;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return false;

                    testTypeTitle = reader["TestTypeTitle"].ToString();
                    testTypeDescription = reader["TestTypeDescription"].ToString();
                    testTypeFees = Convert.ToDecimal(reader["TestTypeFees"]);
                }
            }

            return true;
        }

        public static bool FindByTitle(string testTypeTitle, ref int testTypeId,
            ref string testTypeDescription, ref decimal testTypeFees)
        {
            string query = @"SELECT TestTypeID, 
                                   TestTypeTitle, 
                                   TestTypeDescription, 
                                   TestTypeFees
                            FROM TestTypes
                            WHERE TestTypeTitle = @testTypeTitle";

            using (SqlConnection connection = new SqlConnection(ClsDataSetting.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@testTypeTitle", SqlDbType.NVarChar, 100).Value = testTypeTitle;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        return false;

                    testTypeId = Convert.ToInt32(reader["TestTypeID"]);
                    testTypeDescription = reader["TestTypeDescription"].ToString();
                    testTypeFees = Convert.ToDecimal(reader["TestTypeFees"]);
                }

                return true;
            }
        }

        public static int AddNewTestType(string testTypeTitle,
            string testTypeDescription, decimal testTypeFees)
        {
            string query = @"INSERT INTO TestTypes
                                (TestTypeTitle,
                                 TestTypeDescription,
                                 TestTypeFees)
                                VALUES (@testTypeTitle,
                                        @testTypeDescription,
                                        @testTypeFees); 
                                SELECT SCOPE_IDENTITY();";

            int testTypeId = -1;

            using (SqlConnection connection = new SqlConnection(ClsDataSetting.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@testTypeTitle", SqlDbType.NVarChar, 100).Value = testTypeTitle;
                command.Parameters.Add("@testTypeDescription", SqlDbType.NVarChar, 500).Value = testTypeDescription;
                command.Parameters.Add("@testTypeFees", SqlDbType.Decimal).Value = testTypeFees;

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    testTypeId = insertedId;
                }
            }

            return testTypeId;
        }

        public static bool UpdateTestType(int testTypeId, string testTypeTitle,
            string testTypeDescription, decimal testTypeFees)
        {
            string query = @"UPDATE TestTypes
                            SET TestTypeTitle       = @testTypeTitle,
                                TestTypeDescription = @testTypeDescription,
                                TestTypeFees        = @testTypeFees
                            WHERE TestTypeID = @testTypeID";

            using (SqlConnection connection = new SqlConnection(ClsDataSetting.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@testTypeTitle", SqlDbType.NVarChar, 100).Value = testTypeTitle;
                command.Parameters.Add("@testTypeDescription", SqlDbType.NVarChar, 500).Value = testTypeDescription;
                command.Parameters.Add("@testTypeFees", SqlDbType.Decimal).Value = testTypeFees;
                command.Parameters.Add("@testTypeID", SqlDbType.Int).Value = testTypeId;

                connection.Open();
                return (command.ExecuteNonQuery() > 0);
            }
        }

        public static bool DeleteTestType(int testTypeId)
        {
            string query = @"DELETE FROM TestTypes WHERE TestTypeID = @testTypeID";

            using (SqlConnection connection = new SqlConnection(ClsDataSetting.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@testTypeID", SqlDbType.Int).Value = testTypeId;
                connection.Open();
                return (command.ExecuteNonQuery() > 0);
            }
        }
    }
}