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
using DVLD.Classes;

namespace DVLD.Applications.Application_Types
{
    public partial class frmUpdateApplicationType : Form
    {

        private int _AppID;
        private clsAppTypes _AppType;
        public frmUpdateApplicationType(int AppID)
        {
            InitializeComponent();

            _AppID = AppID;
            _AppType = clsAppTypes.Find(_AppID);
            if (_AppType == null) 
            {
                MessageBox.Show($"This form will be Close No Application Type with ID:{_AppID}", "Don't Find ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void _LoadData()
        {
            lblAppID.Text = _AppType.AppID.ToString();
            txtApptitle.Text = _AppType.ApplicationTypeTitle;
            txtFees.Text = _AppType.AppFees.ToString();
        }

        

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _AppType.ApplicationTypeTitle = txtApptitle.Text.Trim();
            _AppType.AppFees = Convert.ToSingle(txtFees.Text);


            if (_AppType.Save())
            {
                MessageBox.Show(
                "saved successfully.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show(
                    "An error occurred while saving the App Type data.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        
        }

        private void txtApptitle_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtApptitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtApptitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtApptitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);

            }
            if (!clsValidatoin.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
            
            
        }
    }
}
