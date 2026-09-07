using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        public enTestType TestType { set; get; }


        public int TestTypeID { set; get; }
        public string TestTypeTitle { set; get; }
        public string TestTypeDescription { set; get; }
        public float TestFees { set; get; }



        private clsTestTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestFees)

        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestFees = TestFees;

            Mode = enMode.Update;

        }


        private bool _UpdateTestType()
        {
            //call DataAccess Layer 

            return clsTestTypesData.UpdateTestTypesFeesAndTiitleAndDescByID(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestFees);

        }

        public static clsTestTypes Find(int ID)
        {

            string TestTypeTitle = "";
            string TestTypeDescription = "";


            float TestFees = 0.0f;

            if (clsTestTypesData.GeTestTypesByID(ID, ref TestTypeTitle, ref TestTypeDescription, ref TestFees))
                return new clsTestTypes(ID, TestTypeTitle, TestTypeDescription, TestFees);
            else
                return null;

        }




        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Update:
                    return _UpdateTestType();
            }

            return false;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }


    }

}
