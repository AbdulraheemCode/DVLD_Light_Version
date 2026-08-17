using System;
using System.Data;
using System.Diagnostics;
using DVLD_BussinessLogic;

namespace DVLD_Console_App
{
    internal class Program
    {
        public static void Main(string[] args)
        {


            bool isCountryFound = ClsCountry.IsCountryExists("United States");
            Console.WriteLine(isCountryFound);


        }
    }
}