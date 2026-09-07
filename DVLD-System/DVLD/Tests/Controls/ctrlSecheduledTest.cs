using DVLD.Properties;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests.Controls
{
    public partial class ctrlSecheduledTest : UserControl
    {

        private clsLocalDrivingApplication _LDLApp;
        private int _LDLAppID = -1;
        private int _TestAppointmentID = -1;
        private int _TestID = -1;
        private clsTestAppointment _TestAppointment;
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;

        public clsTestTypes.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }

            set
            {
                _TestTypeID = value;
                switch (_TestTypeID)
                {
                    case clsTestTypes.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.eye_scan;
                            break;
                        }

                    case clsTestTypes.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.research__1_;
                            break;
                        }
                    case clsTestTypes.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.test__2_;
                            break;
                        }
                }
            }
        }


        public int TestAppointmentID
        {
            get { return _TestAppointmentID; }
        }

        public int TestID
        {
            get { return _TestID; }
        }
        public ctrlSecheduledTest()
        {
            InitializeComponent();
        }

       public void LoadInfo(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;

            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

            _TestID = _TestAppointment.TestID;

            _LDLAppID = _TestAppointment.LocalDrivingLicenseApplicationID;
            _LDLApp = clsLocalDrivingApplication.FindByLDLiD(_LDLAppID);

            if (_LDLApp == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LDLAppID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLDLAppID.Text = _LDLApp.LDLAppID.ToString();
            lblClassName.Text = _LDLApp.LicenseClassInfo.ClassName;
            lblName.Text = _LDLApp.PersonFullName;


            //this will show the trials for this test before 
            lblTrial.Text = _LDLApp.TotalTrialsPerTest(_TestTypeID).ToString();



            lblDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = (_TestAppointment.TestID == -1) ? "Not Taken Yet" : _TestAppointment.TestID.ToString();

        }

        private void gbTestType_Enter(object sender, EventArgs e)
        {

        }
    }
}
