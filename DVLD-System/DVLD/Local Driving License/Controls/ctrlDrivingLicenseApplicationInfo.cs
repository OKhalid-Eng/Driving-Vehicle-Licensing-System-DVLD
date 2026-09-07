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
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private clsLocalDrivingApplication _LDLApp;
        private int _LDLAppID;

        private int _AppID;

        public int LDLAppID
        {
            get { return _LDLAppID; }
        }
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadLDLAppInfoByLDLAppID(int LDLID)
        {
            _LDLAppID = LDLID;

            _LDLApp = clsLocalDrivingApplication.FindByLDLiD(_LDLAppID);

            if (_LDLApp == null)
            {
                ResetLDLAppInfo();
                MessageBox.Show("No Local Driving License Application with L.D.AppID = " + _LDLAppID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadDataOfLDLAppInfo();

        }

        public void LoadLDLAppInfoByAppID(int AppID)
        {
            _AppID = AppID;

            _LDLApp = clsLocalDrivingApplication.FindAppID(_LDLAppID);

            if (_LDLApp == null)
            {
                ResetLDLAppInfo();
                MessageBox.Show("No Application with AppID = " + _AppID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadDataOfLDLAppInfo();

        }



        private void _LoadDataOfLDLAppInfo()
        {
            lblDL.Text = _LDLApp.LDLAppID.ToString();
            lblClassName.Text = _LDLApp.LicenseClassInfo.ClassName;
            ctrlApplicationBasicInfo1.LoadAppInfo(_LDLApp.ApplicationID);
        }

        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }


        public void ResetLDLAppInfo()
        {
            _LDLAppID = -1;
            lblDL.Text = "[????]";
            lblClassName.Text = "[????]";
            lblPassedTest.Text = "0";
            ctrlApplicationBasicInfo1.ResetAppInfo();
        }
    }
}
