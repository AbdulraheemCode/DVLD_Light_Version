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
            DataTable dtAllUser = ClsUser.GetAllUsers();

            foreach (DataRow row in dtAllUser.Rows)
            {
                Console.WriteLine(row[0] + " " + row[1] + " " + row[2] + " " + row[3]);
            }
        }
    }
}