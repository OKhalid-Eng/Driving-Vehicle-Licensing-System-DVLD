using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplication
    {
        public enum enApplicationStatus
        {
            New = 1,
            Cancelled = 2,
            Completed = 3
        }

        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode = enMode.AddNew;

        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public int ApplicationID { get; set; }

        public int ApplicantPersonID { get; set; }
        public ClsPerson PersonInfo { get; set; }
        public DateTime ApplicationDate { get; set; }

        public byte ApplicationTypeID { get; set; }

        public clsAppTypes AppTypeInfo;

        public enApplicationStatus AppStatus { get; set; }

        public string StatusText
        {
            get
            {
                switch (AppStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "None";
                }
            }
        }


        public DateTime LastStatusDate { get; set; }

        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public clsUser User;



        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = 1;
            this.AppStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;

        }

        public clsApplication(int ApplicationID, int ApplicantPersonID,
    DateTime ApplicationDate, byte ApplicationTypeID,
    enApplicationStatus AppStatus, DateTime LastStatusDate,
    decimal PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersonInfo = ClsPerson.FindPersonByID(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.AppTypeInfo = clsAppTypes.Find(ApplicationTypeID);
            this.AppStatus = AppStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.User = clsUser.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;

        }

        private bool _AddApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(
                this.ApplicantPersonID,
                this.ApplicationDate,
                this.ApplicationTypeID,
                (byte)this.AppStatus,
                this.LastStatusDate,
                this.PaidFees,
                this.CreatedByUserID);

            return ApplicationID != -1;
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(
                this.ApplicationID,
                this.ApplicantPersonID,
                this.ApplicationDate,
                this.ApplicationTypeID,
                (byte)this.AppStatus,
                this.LastStatusDate,
                this.PaidFees,
                this.CreatedByUserID);
        }

        public static clsApplication FindApplicationByID(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now;
            byte ApplicationTypeID = 1;
            byte ApplicationStatus = 0;
            DateTime LastStatusDate = DateTime.Now;
            decimal PaidFees = 0;
            int CreatedByUserID = -1;

            if (clsApplicationData.GetApplicationByID(
                ApplicationID,
                ref ApplicantPersonID,
                ref ApplicationDate,
                ref ApplicationTypeID,
                ref ApplicationStatus,
                ref LastStatusDate,
                ref PaidFees,
                ref CreatedByUserID))
            {
                return new clsApplication(
                    ApplicationID,
                    ApplicantPersonID,
                    ApplicationDate,
                    ApplicationTypeID,
                    (enApplicationStatus)ApplicationStatus,
                    LastStatusDate,
                    PaidFees,
                    CreatedByUserID);
            }

            return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }

                    return false;

                case enMode.Update:

                    return _UpdateApplication();
            }

            return false;
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationData.GetAllApplications();
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplicationByID(this.ApplicationID);
        }

        public static bool IsApplicationExist(int ID)
        {
            return clsApplicationData.IsApplicationExist(ID);
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(this.ApplicationID, 2);
        }


        public bool SetComplete()

        {
            return clsApplicationData.UpdateStatus(ApplicationID, 3);
        }


        public static bool DoesPersonHaveActiveApplication(int PersonID, byte ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }


        public bool DoesPersonHaveActiveApplication(byte ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (byte)ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (byte)ApplicationTypeID, LicenseClassID);
        }


        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }



        public static bool UpdateStatus(int ApplicationID, byte NewStatus)
        {
            return clsApplicationData.UpdateStatus(ApplicationID, NewStatus);
        }
    }
}
