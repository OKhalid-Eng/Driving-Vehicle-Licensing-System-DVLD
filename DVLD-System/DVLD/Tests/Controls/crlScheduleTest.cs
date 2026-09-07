using DVLD.Classes;
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
using static DVLD_Business.clsTestTypes;

namespace DVLD.Tests.Controls
{
    public partial class crlScheduleTest : UserControl
    {
        public crlScheduleTest()
        {
            InitializeComponent();
        }

        public enum enMode { Add = 1, Update = 2 };
        private enMode _mode = enMode.Add;
        public enum enCreationMode { FirstTimeSchedule = 1, RetakeTestSchedule = 2 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;
        private clsLocalDrivingApplication _LDLApp;
        private int _LDLAppID = -1;
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;





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


        public void LoadInfo(int LDLAppID, int TestAppointmentID = -1)
        {
            if (TestAppointmentID == -1)
                _mode = enMode.Add;
            else
                _mode = enMode.Update;

            _LDLAppID = LDLAppID;

            _TestAppointmentID = TestAppointmentID;

            _LDLApp = clsLocalDrivingApplication.FindByLDLiD(_LDLAppID);


            if (_LDLApp == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LDLAppID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            if (_LDLApp.DoesAttendTestType(_TestTypeID))
                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;


            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                gbRetakeTestInfo.Enabled = true;
                lblRetakeAppFees.Text = clsAppTypes.Find((int)clsApplication.enApplicationType.RetakeTest).AppFees.ToString();
                lblRetakeTestAppID.Text = clsAppTypes.Find((int)clsApplication.enApplicationType.RetakeTest).AppID.ToString();
                lblTitle.Text = "Schedule Retake Test";
            }
            else
            {

                gbRetakeTestInfo.Enabled = false;
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
                lblTitle.Text = "Schedule Test";
            }


            lblLDLAppID.Text = _LDLApp.LDLAppID.ToString();
            lblName.Text = _LDLApp.PersonFullName;
            lblClassName.Text = _LDLApp.LicenseClassInfo.ClassName;
            lblTrial.Text = _LDLApp.TotalTrialsPerTest(_TestTypeID).ToString();


            if (_mode == enMode.Add)
            {
                lblFees.Text = clsTestTypes.Find((int)_TestTypeID).TestFees.ToString();

                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";
                _TestAppointment = new clsTestAppointment();
            }

            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }

            lblTotalFees.Text = (Convert.ToDecimal(lblFees.Text) + Convert.ToDecimal(lblRetakeAppFees.Text)).ToString();

            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;


            if (!_HandlePrviousTestConstraint())
                return;

        }


        private bool _HandlePrviousTestConstraint()
        {
            switch (_TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    lblUserMessage.Visible = false;
                    return true;

                case clsTestTypes.enTestType.WrittenTest:
                    if (!_LDLApp.DoesPassTestType(clsTestTypes.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }

                    return true;
                case clsTestTypes.enTestType.StreetTest:
                    if (!_LDLApp.DoesPassTestType(clsTestTypes.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }

                    return true;
            }
            return true;
        }


        private bool _HandleAppointmentLockedConstraint()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                return false;
            }
            return false;
        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_mode == enMode.Add && clsLocalDrivingApplication.IsThereAnActiveScheduledTest(_LDLApp.LDLAppID, _TestTypeID))
            {
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                return false;
            }

            return true;
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            //we compare the current date with the appointment date to set the min date.
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;

            dtpTestDate.Value = _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();

            }
            return true;
        }


        private bool _HandleRetakeApplication()
        {
            if (_mode == enMode.Add && _CreationMode == enCreationMode.RetakeTestSchedule)
            {

                clsApplication App = new clsApplication();

                App.ApplicantPersonID = _LDLApp.ApplicantPersonID;
                App.ApplicationDate = DateTime.Now;
                App.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                App.AppStatus = clsApplication.enApplicationStatus.Completed;
                App.LastStatusDate = DateTime.Now;
                App.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                App.PaidFees = Convert.ToDecimal(clsAppTypes.Find((int)clsApplication.enApplicationType.RetakeTest).AppFees);
                if (!App.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                lblRetakeTestAppID.Text = App.ApplicationID.ToString();
                _TestAppointment.RetakeTestApplicationID = App.ApplicationID;
            }
            return true;

        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LDLAppID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);

            if (_TestAppointment.Save())
            {
                _mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
  
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void crlScheduleTest_Load(object sender, EventArgs e)
        {

        }
    }
}
