using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class ClsPerson
    {
        public enum enMode { Add, Update };

        public enMode Mode = enMode.Add;

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        public DateTime DateOfBirth { get; set; }
        public byte Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public clsCountry CountryInfo;
        public string ImagePath { get; set; }

        public ClsPerson()
        {
            Mode = enMode.Add;
            PersonID = -1;
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            Gendor = 0;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = -1;
            ImagePath = "";
        }


        private ClsPerson(int PersonID, string NationalNo, string FirstName, string SecondName,
          string ThirdName, string LastName, DateTime DateOfBirth, byte Gendor, string Address,
          string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            Mode = enMode.Update;
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.CountryInfo = clsCountry.Find(NationalityCountryID);
            this.ImagePath = ImagePath;
        }



        private bool _AddPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);

            return PersonID != -1;
        }

        private bool _UpdatePerson()
        {
            // Call DataAccess.
            return clsPersonData.UpdatePersonByID(this.PersonID, this.NationalNo, this.FirstName, this.SecondName,
                 this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }


        public static ClsPerson FindPersonByID(int PersonID)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "",
                Phone = "", Email = "", ImagePath = "", NationalNo="";

            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;

            int NationalityCountryID = -1;

            if (clsPersonData.GetPersonById(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
               ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath)) 
            {
                return new ClsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }

            return null;

        }

        public static ClsPerson FindPersonByID(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "",
                Phone = "", Email = "", ImagePath = "";

            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;

            int NationalityCountryID = -1, PersonID = -1;

            if (clsPersonData.GetPersonByNationalNumber(NationalNo,  ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
               ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new ClsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }

            return null;

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }

                    return false;

                case enMode.Update:
                    return _UpdatePerson();
            }

            return false;
        }


        public static DataTable GetAlPersons()
        {
            return clsPersonData.GetAllPersons();
        }

        public static bool DeletePersonByID(int ID)
        {
            return clsPersonData.DeletePersonByID(ID);
        }

        public static bool IsPersonExist(int ID)
        {
            return clsPersonData.IsPersomExist(ID);
        }

        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersomExist(NationalNo);
        }

        public static bool IsNationalNoUsedByAnotherPerson(int PersonID, string NationalNo)
        {
            return clsPersonData.IsNationalNoUsedByAnotherPerson(PersonID, NationalNo);
        }

    }


} 

