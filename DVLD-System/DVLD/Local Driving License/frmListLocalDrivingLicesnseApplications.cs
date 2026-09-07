using DVLD.Tests;
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

namespace DVLD.Local_Driving_License
{
    public partial class frmListLocalDrivingLicesnseApplications : Form
    {
        public frmListLocalDrivingLicesnseApplications()
        {
            InitializeComponent();
        }




        private DataTable _dtLDLAPP;


        private void frmListLocalDrivingLicesnseApplications_Load(object sender, EventArgs e)
        {
            _RefreshLDLAPPLList();
        }

        private void _RefreshLDLAPPLList()
        {
            _dtLDLAPP = clsLocalDrivingApplication.GetAllLocalDrivingLicenseApplications();


            dgvLDLApp.DataSource = _dtLDLAPP;

            cbFilter.SelectedIndex = 0;
            lblCountRec.Text = dgvLDLApp.Rows.Count.ToString();
            if (dgvLDLApp.Rows.Count > 0)
            {

                dgvLDLApp.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLDLApp.Columns[0].Width = 110;

                dgvLDLApp.Columns[1].HeaderText = "Driving Class";
                dgvLDLApp.Columns[1].Width = 200;


                dgvLDLApp.Columns[2].HeaderText = "Natiional NO.";
                dgvLDLApp.Columns[2].Width = 120;

                dgvLDLApp.Columns[3].HeaderText = "Full Name";
                dgvLDLApp.Columns[3].Width = 200;


                dgvLDLApp.Columns[4].HeaderText = "Application Date";
                dgvLDLApp.Columns[4].Width = 140;

                dgvLDLApp.Columns[5].HeaderText = "Passed Tests";
                dgvLDLApp.Columns[5].Width = 120;

                dgvLDLApp.Columns[6].HeaderText = "Status";
                dgvLDLApp.Columns[6].Width = 120;

            }
        }


        private void btnAddLDLApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication(-1);
            frm.ShowDialog();
            _RefreshLDLAPPLList();

        }

        private void tsmCancelApp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsLocalDrivingApplication LDLApp =
                clsLocalDrivingApplication.FindByLDLiD(
                    Convert.ToInt32(dgvLDLApp.CurrentRow.Cells[0].Value)
                );
            if (MessageBox.Show($"Are you sure you want to Cancel this Application", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {

                if (LDLApp.Cancel())
                {
                    MessageBox.Show("Application Canceled Successfully.");
                    _RefreshLDLAPPLList();
                }

                else
                {
                    MessageBox.Show("Application is not Canceled.");
                }
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.SelectedIndex)
            {
                case 1:
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case 2:
                    FilterColumn = "NationalNo";
                    break;

                case 3:
                    FilterColumn = "FullName";
                    break;

                case 4:
                    FilterColumn = "Status";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtLDLAPP.DefaultView.RowFilter = "";
                lblCountRec.Text = dgvLDLApp.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                //in this case we deal with integer not string.
                _dtLDLAPP.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            else
                _dtLDLAPP.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());

            lblCountRec.Text = dgvLDLApp.Rows.Count.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedIndex == 1)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = cbFilter.SelectedIndex != 0;

            if (txtFilter.Visible)
            {
                txtFilter.Text = "";
                txtFilter.Focus();
            }
        }

        private void tsmShowAppDetails_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvLDLApp.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void tsmEditApp_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication((int)dgvLDLApp.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshLDLAPPLList();
        }

        private void tsmDeleteApp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LDLAppID= (int)dgvLDLApp.CurrentRow.Cells[0].Value;

            clsLocalDrivingApplication LDLApp = clsLocalDrivingApplication.FindByLDLiD(LDLAppID);

            if (LDLApp != null)
            {
                if (LDLApp.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    _RefreshLDLAPPLList();
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _ScheduleTest(clsTestTypes.enTestType TestType)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLDLApp.CurrentRow.Cells[0].Value;

            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            _RefreshLDLAPPLList();
        }



        private void tsmScheduleVisionTest_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.VisionTest);
        }

        private void tsmScheduleWrittenTest_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.WrittenTest);

        }

        private void tsmScheduleStreetTest_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.StreetTest);

        }

        private void cmsLDLApplication_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLDLApp.CurrentRow.Cells[0].Value;
            clsLocalDrivingApplication LocalDrivingLicenseApplication =
                    clsLocalDrivingApplication.FindByLDLiD
                                                    (LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)dgvLDLApp.CurrentRow.Cells[5].Value;


            //Enabled only if person passed all tests and Does not have license. 


            //Enable/Disable Cancel Menue Item
            //We only canel the applications with status=new.
            tsmCancelApp.Enabled = (LocalDrivingLicenseApplication.AppStatus == clsApplication.enApplicationStatus.New);

            //Enable/Disable Delete Menue Item
            //We only allow delete incase the application status is new not complete or Cancelled.
            tsmDeleteApp.Enabled =
                (LocalDrivingLicenseApplication.AppStatus == clsApplication.enApplicationStatus.New);



            //Enable Disable Schedule menue and it's sub menue
            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.VisionTest); ;
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypes.enTestType.StreetTest);

            tsmsechduleTests.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.AppStatus == clsApplication.enApplicationStatus.New);

            if (tsmsechduleTests.Enabled)
            {
                //To Allow Schdule vision test, Person must not passed the same test before.
                tsmScheduleVisionTest.Enabled = !PassedVisionTest;

                //To Allow Schdule written test, Person must pass the vision test and must not passed the same test before.
                tsmScheduleWrittenTest.Enabled = PassedVisionTest && !PassedWrittenTest;

                //To Allow Schdule steet test, Person must pass the vision * written tests, and must not passed the same test before.
                tsmScheduleStreetTest.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

            }

        }
    }
}
