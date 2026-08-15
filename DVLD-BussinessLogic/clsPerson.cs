using System;
using System.Data;
using DVLD_DataAccess;


namespace DVLD_BussinessLogic
{
    public class ClsPerson
    {
        private enum EnMode
        {
            AddNew = 1,
            Update = 2
        }

        private EnMode _mode;

        public int PersonId { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryId { get; set; }
        public ClsCountry CountryInfo { get; set; }
        public string ImagePath { get; set; }


        public ClsPerson()
        {
            this.PersonId = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gender = 0;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryId = -1;
            this.ImagePath = "";

            _mode = EnMode.AddNew;
        }

        private ClsPerson(int personId, string nationalNo, string firstName, string secondName,
            string thirdName, string lastName, DateTime dateOfBirth, byte gender, string address,
            string phone, string email, int nationalityCountryId, string imagePath)
        {
            this.PersonId = personId;
            this.NationalNo = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryId = nationalityCountryId;
            this.CountryInfo = ClsCountry.FindById(this.NationalityCountryId);
            this.ImagePath = imagePath;

            _mode = EnMode.Update;
        }

        public static ClsPerson FindPersonById(int personId)
        {
            string nationalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "";
            DateTime dateOfBirth = new DateTime();
            byte gender = 0;
            string address = "", phone = "", email = "", imagePath = "";
            int nationalityCountryId = -1;

            bool isFound = ClsPersonData.FindPersonByID
            (
                personId, ref nationalNo, ref firstName, ref secondName,
                ref thirdName, ref lastName, ref dateOfBirth, ref gender, ref address,
                ref phone, ref email, ref nationalityCountryId, ref imagePath
            );

            if (!isFound)
                return null;
            else
                return new ClsPerson
                (
                    personId, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth,
                    gender, address, phone, email, nationalityCountryId, imagePath
                );
        }


        public bool Save()
        {
            switch (_mode)
            {
                case EnMode.AddNew:
                    if (_AddNewPerson())
                    {
                        _mode = EnMode.Update;
                        return true;
                    }

                    break;

                case EnMode.Update:
                    return _UpdatePerson();

                default:
                    throw new ArgumentOutOfRangeException();
            }


            return false;
        }

        private bool _UpdatePerson()
        {
            return ClsPersonData.UpdatePerson
            (
                this.PersonId, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email,
                this.NationalityCountryId, this.ImagePath
            );
        }

        private bool _AddNewPerson()
        {
            this.PersonId = ClsPersonData.AddNewPerson
            (
                this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gender, this.Address,
                this.Phone, this.Email, this.NationalityCountryId, this.ImagePath
            );

            return (this.PersonId != -1);
        }

        public static bool DeletePerson(int personId)
        {
            return ClsPersonData.DeletePerson(personId);
        }

        public void PrintPersonInfo()
        {
            Console.WriteLine(this.FirstName + "'s Information: ");
            Console.WriteLine(FirstName);
            Console.WriteLine(SecondName);
            Console.WriteLine(ThirdName);
            Console.WriteLine(LastName);
            Console.WriteLine(DateOfBirth);
            Console.WriteLine(Gender);
            Console.WriteLine(Address);
            Console.WriteLine(Phone);
            Console.WriteLine(Email);
            Console.WriteLine(CountryInfo.CountryName);
            Console.WriteLine(ImagePath);
        }

        public static DataTable GetAllPeople()
        {
            return ClsPersonData.GetAllPeople();
        }

        public static bool IsPersonExist(int personId)
        {
            return ClsPersonData.IsPersonExist(personId);
        }
    }
}