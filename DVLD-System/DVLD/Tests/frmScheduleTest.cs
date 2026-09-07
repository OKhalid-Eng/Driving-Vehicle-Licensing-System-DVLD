using DVLD.Tests.Controls;
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

namespace DVLD.Tests
{
    public partial class frmScheduleTest : Form
    {

        private int _LDLAppID = -1;
        private clsTestTypes.enTestType _TestType = clsTestTypes.enTestType.VisionTest;
        private int _TestAppointmentID = -1;


        public frmScheduleTest(int LDLAppID, clsTestTypes.enTestType TestType, int TestAppointmentID = -1)
        {
            InitializeComponent();

            _LDLAppID = LDLAppID;
            _TestType = TestType;
            _TestAppointmentID = TestAppointmentID;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            crtScheduleTest1.TestTypeID = _TestType;
            crtScheduleTest1.LoadInfo(_LDLAppID, _TestAppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
