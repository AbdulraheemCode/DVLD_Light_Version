using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BussinessLogic
{
    public class ClsCountry
    {
        
        
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        
        
        public ClsCountry()
        {
            this.CountryId = -1;
            this.CountryName = string.Empty;
        }

        public ClsCountry(int countryID, string countryName)
        {
            this.CountryId = countryID;
            this.CountryName = countryName;
        }


        public static ClsCountry FindById(int countryId)
        {

            string countryName = "";

            bool isFound = ClsCountryData.Find(countryId, ref countryName);

            if (isFound)
                return new ClsCountry(countryId, countryName);
            else
                return null;

        }
        
        public static ClsCountry FindByName(string countryName)
        {

            int countryId = -1;

            bool isFound = ClsCountryData.Find(countryName, ref countryId);

            if (isFound)
                return new ClsCountry(countryId, countryName);
            else
                return null;

        }
        

        public DataTable GetAllCountries()
        {
            return ClsCountryData.GetAllCountries();
        }
        
        
    }
}