using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DVLD_Business
{
    public class clsLocalDrivingApplication : clsApplication
    {
        private enum enMode { Add=1, Update=2};

        private enMode Mode = enMode.Add;

        public int LDLAppID { get; set; }
        public byte LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;

        public string PersonFullName
        {
            get
            {
                return ClsPerson.FindPersonByID(ApplicantPersonID).FullName;
            }
        }



        public clsLocalDrivingApplication()
        {
            this.LDLAppID = -1;
            this.LicenseClassID = 0;
            Mode = enMode.Add;

        }

        public clsLocalDrivingApplication(int LDLAppID, byte LicenseClassID, int ApplicationID, int ApplicantPersonID,
    DateTime ApplicationDate, byte ApplicationTypeID,
    enApplicationStatus AppStatus, DateTime LastStatusDate,
    decimal PaidFees, int CreatedByUserID)
        {
            this.LDLAppID = LDLAppID;
            this.LicenseClassID = LicenseClassID;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.AppStatus = AppStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassInfo = clsLicenseClass.FindLicenseClassByID(LicenseClassID);
            Mode = enMode.Update;
        }

        private bool _AddLocalDrivingApplication()
        {
            this.LDLAppID = clsLocalDrivingApplicationData.AddNewLocalDrivingLicenseApplication(
                this.ApplicationID, this.LicenseClassID);

            return this.LDLAppID != -1;
        }


        private bool _UpdateLocalDrivingApplication()
        {
            return clsLocalDrivingApplicationData.UpdateLocalDrivingLicenseApplication(
                this.LDLAppID,this.ApplicationID,this.LicenseClassID);
        }


        public static clsLocalDrivingApplication FindByLDLiD(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1;
            byte LicenseClassID = 0;

            if (clsLocalDrivingApplicationData.GetLocalDrivingLicenseByLDLID(
                LocalDrivingLicenseApplicationID,
                ref ApplicationID,
                ref LicenseClassID))
            {
                clsApplication App = clsApplication.FindApplicationByID(ApplicationID);


                return new clsLocalDrivingApplication(LocalDrivingLicenseApplicationID, LicenseClassID,
                    App.ApplicationID, App.ApplicantPersonID, App.ApplicationDate, App.ApplicationTypeID,
                   (enApplicationStatus)App.AppStatus, App.LastStatusDate, App.PaidFees, App.CreatedByUserID);
            }

            return null;
        }

        public static clsLocalDrivingApplication FindAppID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            byte LicenseClassID = 0;

            if (clsLocalDrivingApplicationData.GetLocalDrivingLicenseApplicationByApplicationID(
                ApplicationID,
                ref LocalDrivingLicenseApplicationID,
                ref LicenseClassID))
            {
                clsApplication App = clsApplication.FindApplicationByID(ApplicationID);


                return new clsLocalDrivingApplication(LocalDrivingLicenseApplicationID, LicenseClassID,
                    App.ApplicationID, App.ApplicantPersonID, App.ApplicationDate, App.ApplicationTypeID,
                   (enApplicationStatus)App.AppStatus, App.LastStatusDate, App.PaidFees, App.CreatedByUserID);
            }

            return null;
        }

        public bool Save()
        {
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.Add:
                    if (_AddLocalDrivingApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }

                    return false;


                case enMode.Update:
                    return _UpdateLocalDrivingApplication();
            }

            return false;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        

        public bool Delete()
        {
            bool IsLDLDeleted;
            bool IsAppDeleted;

            IsLDLDeleted = clsLocalDrivingApplicationData.DeleteLocalDrivingLicense(this.LDLAppID);

            if (!IsLDLDeleted)
                return false;

            IsAppDeleted = base.Delete();

            return IsAppDeleted;
        }


        public static bool IsLocalDrivingLicenseApplicationExist(int LDLAppID)
        {
            return clsLocalDrivingApplicationData.IsLocalDrivingLicenseApplicationExist(LDLAppID);
        }



       public bool DoesAttendTestType(clsTestTypes.enTestType TestType)
        {
            return clsLocalDrivingApplicationData.DoesAttendTestType(this.LDLAppID, (int)TestType);
        }

        public byte TotalTrialsPerTest(clsTestTypes.enTestType TestType)
        {
            return clsLocalDrivingApplicationData.TotalTrialsPerTest(this.LDLAppID, (int)TestType);
        }

        public static bool IsThereAnActiveScheduledTest(int LDLAppID, clsTestTypes.enTestType TestType)
        {
            return clsLocalDrivingApplicationData.IsThereAnActiveScheduledTest(LDLAppID, (int)TestType);
        }

        public bool DoesPassTestType(clsTestTypes.enTestType TestType)
        {
            return clsLocalDrivingApplicationData.DoesPassTestType(this.LDLAppID, (int)TestType);
        }

        public bool IsThereAnActiveScheduledTest(clsTestTypes.enTestType TestTypeID)

        {

            return clsLocalDrivingApplicationData.IsThereAnActiveScheduledTest(this.LDLAppID, (int)TestTypeID);
        }

        public clsTest GetLastTestPerTestType(clsTestTypes.enTestType TestType)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestType);
        }

       public bool PassedAllTests()
        {
           return clsTest.PassedAllTests(this.LDLAppID);
        }

        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }


        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;
            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if(Driver==null)
            {
                Driver = new clsDriver();

                Driver.CreatedByUserID = CreatedByUserID;
                Driver.PersonID = this.ApplicantPersonID;


                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees =Convert.ToSingle(this.LicenseClassInfo.ClassFees);
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                this.SetComplete();
                return License.LicenseID;
            }

            else
                return -1;

        }

    }


}
