using DVLD.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.User;
using DVLD.Login;
using DVLD.Applications.Application_Types;
using DVLD.Tests.Test_Types;
using DVLD.Local_Driving_License;

namespace DVLD
{
    public partial class frmMain : Form
    {
        frmLogin _Login;

        public frmMain(frmLogin Login)
        {
            InitializeComponent();
            _Login = Login;
        }
     

        private void Form1_Load(object sender, EventArgs e)
        {

            /*
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient)
                {
                    ctrl.BackColor = Color.Lavender;
                }
            }
             */
            msMainFrom.Renderer =
             new ToolStripProfessionalRenderer(new MyColorTable());

        }


        private void msPeople_Click(object sender, EventArgs e)
        {
            fmPeople fm = new fmPeople();

            fm.Show();
        }

       

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers frm = new frmUsers();
            frm.ShowDialog();
        }

        private void tsmCurrentUserInfo_Click(object sender, EventArgs e)
        {
            frmUserDetails frm = new frmUserDetails(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void tsmChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void tsmSignOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmManageAppType_Click(object sender, EventArgs e)
        {
            frmApplicationTypes frm = new frmApplicationTypes();
            frm.ShowDialog();
        }

        private void tsmManageTestType_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm = new frmManageTestTypes();
            frm.ShowDialog();
        }

        private void tsmLocalLicense_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication(-1);
            frm.ShowDialog();
        }

        private void tsmLocalDrivingLincenseApp_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }
    }
}
