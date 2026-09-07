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

namespace DVLD.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {

        private int _AppID = -1;
        private clsApplication _AppInfo;


        public int AppID
        {
            get { return _AppID; }
        }

        public clsApplication SelectedAppInfo
        {
            get { return _AppInfo; }
        }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }


        public void LoadAppInfo(int AppID)
        {
            _AppID = AppID;

            _AppInfo = clsApplication.FindApplicationByID(_AppID);

            if (_AppInfo == null)
            {
                ResetAppInfo();
                MessageBox.Show("No Application with AppID = " + AppID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadDataOfAppInfo();
        }


        private void _LoadDataOfAppInfo()
        {
            lblApplicationID.Text = _AppInfo.ApplicationID.ToString();
            lblStatus.Text = _AppInfo.StatusText;
            lblFees.Text = _AppInfo.PaidFees.ToString();
            lblType.Text = _AppInfo.AppTypeInfo.ApplicationTypeTitle;
            lblApplicant.Text = _AppInfo.PersonInfo.FullName;
            lblDate.Text = _AppInfo.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = _AppInfo.LastStatusDate.ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        }

        public void ResetAppInfo()
        {
            _AppID = -1;
            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblFees.Text = "[$$$]";
            lblType.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";          
        }



        private void ctrlApplicationBasicInfo_Load(object sender, EventArgs e)
        {

        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_AppInfo.ApplicantPersonID);

            frm.ShowDialog();
        }
    }
}
