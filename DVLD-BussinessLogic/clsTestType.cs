using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BussinessLogic
{
    public class ClsTestType
    {
        public enum EnMode
        {
            AddNew = 1,
            Update = 2
        }

        public enum EnTestType
        {
            VisionTest = 1,
            WrittenTest = 2,
            StreetTest = 3
        }

        private EnMode _mode;

        public EnTestType TestTypeId { get; set; } = EnTestType.VisionTest;
        public string TestTypeTitle { get; set; } = "";
        public string TestTypeDescription { get; set; } = "";
        public decimal TestTypeFees { get; set; } = 0;


        public ClsTestType() => _mode = EnMode.AddNew;

        private ClsTestType(int testTypeId, string testTypeTitle,
            string testTypeDescription, decimal testTypeFees)
        {
            this.TestTypeId = (EnTestType)testTypeId;
            this.TestTypeTitle = testTypeTitle;
            this.TestTypeDescription = testTypeDescription;
            this.TestTypeFees = testTypeFees;

            _mode = EnMode.Update;
        }

        public static ClsTestType FindById(int testTypeId)
        {
            string testTypeTitle = "";
            string testTypeDescription = "";
            decimal testTypeFees = 0;

            bool isFound = ClsTestTypesData.FindById(testTypeId,
                ref testTypeTitle, ref testTypeDescription, ref testTypeFees);

            if (!isFound) return null;

            return new ClsTestType
            (
                testTypeId,
                testTypeTitle,
                testTypeDescription,
                testTypeFees
            );
        }

        public static ClsTestType FindByTitle(string testTypeTitle)
        {
            int testTypeId = -1;
            string testTypeDescription = "";
            decimal testTypeFees = 0;

            bool isFound = ClsTestTypesData.FindByTitle(testTypeTitle,
                ref testTypeId, ref testTypeDescription, ref testTypeFees);

            if (!isFound)
                return null;

            return new ClsTestType
            (
                testTypeId,
                testTypeTitle,
                testTypeDescription,
                testTypeFees
            );
        }

        public bool Save()
        {
            switch (_mode)
            {
                case EnMode.AddNew:
                    if (_AddNewTestType())
                    {
                        _mode = EnMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case EnMode.Update:
                    return _UpdateTestType();
            }

            return false;
        }

        private bool _UpdateTestType()
        {
            return ClsTestTypesData.UpdateTestType
            (
                (int)TestTypeId, TestTypeTitle, TestTypeDescription, TestTypeFees
            );
        }

        private bool _AddNewTestType()
        {
            this.TestTypeId = (EnTestType)ClsTestTypesData.AddNewTestType
            (
                this.TestTypeTitle,
                this.TestTypeDescription,
                this.TestTypeFees
            );

            return ((int)this.TestTypeId != -1);
        }

        public static bool DeleteTestType(int testTypeId)
        {
            return ClsTestTypesData.DeleteTestType(testTypeId);
        }

        public static DataTable GetAllTestTypes()
        {
            return ClsTestTypesData.GetAllTestTypes();
        }

        public void PrintClassMode()
        {
            Console.WriteLine("Class Mode: {0}", _mode.ToString());
        }
    }
}