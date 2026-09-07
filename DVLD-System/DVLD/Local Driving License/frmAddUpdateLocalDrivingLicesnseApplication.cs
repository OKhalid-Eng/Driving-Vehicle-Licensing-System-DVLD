using DVLD.Classes;
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
using static DVLD_Business.clsApplication;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        private clsApplication _Application;

        private enum enMode { Add=1,Update=2};
        private enMode _Mode;
        private clsLocalDrivingApplication _LocalDrivingApplication;
        private int _LDLAppID;

        private int _PersonID;


        public frmAddUpdateLocalDrivingLicenseApplication(int LDLID)
        {
            InitializeComponent();

            _LDLAppID = LDLID;

            if (LDLID == -1)
                _Mode = enMode.Add;
            else
                _Mode = enMode.Update;
           
        }

        private void _FillLicenseClassWithComboBox()
        {
            DataTable dtLicenseClass = clsLicenseClass.GetAllLicenseClasses();

            cbLicenseClass.DataSource = dtLicenseClass;
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";
        }



        private void _LoadDate()
        {
            _FillLicenseClassWithComboBox();
            cbLicenseClass.SelectedIndex = 0;

            lblUserCreated.Text = clsGlobal.CurrentUser.UserName;
           
            if (_Mode==enMode.Add)
            {
                lblMode.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";

                _LocalDrivingApplication = new clsLocalDrivingApplication();

                tbAppInfo.Enabled = false;

                lblAppFees.Text = clsAppTypes.Find((int)clsApplication.enApplicationType.NewDrivingLicense).AppFees.ToString();

                lblAppDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                return;
            }


            _LocalDrivingApplication = clsLocalDrivingApplication.FindByLDLiD(_LDLAppID);

            if (_LocalDrivingApplication == null)
            {
                MessageBox.Show($"This form will be Close No L.D.L.Application with ID:{_LDLAppID}", "Don't Find ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            _PersonID = _LocalDrivingApplication.ApplicantPersonID;

            ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);

            lblMode.Text = "Update Local Driving License Application";
            this.Text = "Update Local Driving License Application";
            ctrlPersonCardWithFilter1.FilterEnable = false;


            lblAppFees.Text = _LocalDrivingApplication.PaidFees.ToString();


            lblAppDate.Text = _LocalDrivingApplication.ApplicationDate.ToString("yyyy-MM-dd");

            cbLicenseClass.SelectedValue = _LocalDrivingApplication.LicenseClassID;

            lblLDLAppID.Text = _LocalDrivingApplication.LDLAppID.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            _LocalDrivingApplication.LicenseClassID = Convert.ToByte(cbLicenseClass.SelectedValue);

            _LocalDrivingApplication.ApplicantPersonID = _PersonID;


            _LocalDrivingApplication.ApplicationDate = DateTime.Now;

            _LocalDrivingApplication.ApplicationTypeID = 1;

            _LocalDrivingApplication.AppStatus = clsApplication.enApplicationStatus.New;

            _LocalDrivingApplication.LastStatusDate = DateTime.Now;

            _LocalDrivingApplication.PaidFees = Convert.ToDecimal(lblAppFees.Text);

            _LocalDrivingApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        

            if (GetActiveApplicationIDForLicenseClass(_PersonID, enApplicationType.NewDrivingLicense, _LocalDrivingApplication.LicenseClassID) != -1) 
            {
                MessageBox.Show("This person has already applied for this license class. You cannot select the same class again.",
                                "Application Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                cbLicenseClass.Focus();
                return;
            }


            if (_LocalDrivingApplication.Save())
            {
                lblMode.Text = "Update Local Driving License Application";

                lblLDLAppID.Text = _LocalDrivingApplication.LDLAppID.ToString();
                _Mode = enMode.Update;

                MessageBox.Show(
                  " data Saved successfully.",
                  "Success",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show(
                    "An error occurred while saving the User data.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int ID)
        {
            _PersonID = ID;

        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _LoadDate();
        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNextt_Click(object sender, EventArgs e)
        {
            if (_PersonID == -1)
            {
                MessageBox.Show(
                  "Please select a person first.",
                  "No Person Selected",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning);
            }

            else
            {
                tbAppInfo.Enabled = true;
                tabControl1.SelectedTab = tbAppInfo;

            }
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}
