using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsAppTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int AppID { set; get; }
        public string ApplicationTypeTitle { set; get; }
        public float AppFees { set; get; }


       
        private clsAppTypes(int AppID, string ApplicationTypeTitle, float ApplicationFees)

        {
            this.AppID = AppID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.AppFees = ApplicationFees;

            Mode = enMode.Update;

        }

        
        private bool _UpdateAppTypeByID()
        {
            //call DataAccess Layer 

            return clsAppTypesData.UpdateAppTypesFeesandTiitleByID(this.AppID, this.ApplicationTypeTitle,this.AppFees);

        }

        public static clsAppTypes Find(int ID)
        {

            string ApplicationTypeTitle = "";


            float AppFees = 0.0f;

            if (clsAppTypesData.GeAppTypesByID(ID, ref ApplicationTypeTitle, ref AppFees))

                return new clsAppTypes(ID, ApplicationTypeTitle, AppFees);
            else
                return null;

        }

       


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Update:
                    return _UpdateAppTypeByID();
            }

            return false;
        }

        public static DataTable GetAllAppTypes()
        {
            return clsAppTypesData.GetAllAppTypes();
        }

     
    }
}
