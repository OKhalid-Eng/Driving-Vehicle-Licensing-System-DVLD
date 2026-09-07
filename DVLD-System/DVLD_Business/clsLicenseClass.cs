using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLicenseClass
    {

        private enum enMode
        {
            Add,
            Update
        }

        private enMode Mode = enMode.Add;

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }


        public clsLicenseClass()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;
            Mode = enMode.Add;
        }


        
        private clsLicenseClass(
            int LicenseClassID,
            string ClassName,
            string ClassDescription,
            byte MinimumAllowedAge,
            byte DefaultValidityLength,decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;

            Mode = enMode.Update;
        }


        private bool _AddLicenseClass()
        {
            this.LicenseClassID = clsLicenseClassesData.AddNewLicenseClass(
                this.ClassName,
                this.ClassDescription,
                this.MinimumAllowedAge,
                this.DefaultValidityLength,
                this.ClassFees);

            return LicenseClassID != -1;
        }


        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassesData.UpdateLicenseClass(
                this.LicenseClassID,
                this.ClassName,
                this.ClassDescription,
                this.MinimumAllowedAge,
                this.DefaultValidityLength,
                this.ClassFees);
        }


        public static clsLicenseClass FindLicenseClassByID(int LicenseClassID)
        {
            string ClassName = "";
            string ClassDescription = "";

            byte MinimumAllowedAge = 0;
            byte DefaultValidityLength = 0;

            decimal ClassFees = 0;

            if (clsLicenseClassesData.GetLicenseClassByID(
                LicenseClassID,
                ref ClassName,
                ref ClassDescription,
                ref MinimumAllowedAge,
                ref DefaultValidityLength,
                ref ClassFees))
            {
                return new clsLicenseClass(
                    LicenseClassID,
                    ClassName,
                    ClassDescription,
                    MinimumAllowedAge,
                    DefaultValidityLength,
                    ClassFees);
            }

            return null;
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddLicenseClass())
                    {
                        Mode = enMode.Update;
                        return true;
                    }

                    return false;


                case enMode.Update:
                    return _UpdateLicenseClass();
            }

            return false;
        }


        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesData.GetAllLicenseClasses();
        }


        public static bool DeleteLicenseClass(int LicenseClassID)
        {
            return clsLicenseClassesData.DeleteLicenseClass(LicenseClassID);
        }


        public static bool IsLicenseClassExist(int LicenseClassID)
        {
            return clsLicenseClassesData.IsLicenseClassExist(LicenseClassID);
        }
    }
}
