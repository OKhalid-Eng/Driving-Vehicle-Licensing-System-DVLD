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

namespace DVLD.Tests.Test_Types
{
    public partial class frmEditManageTestTypes : Form
    {

        private int _TestID;
        private clsTestTypes _TestType;
        public frmEditManageTestTypes(int TestID)
        {
            InitializeComponent();

            _TestID = TestID;
            _TestType = clsTestTypes.Find(_TestID);
            if (_TestType == null)
            {
                MessageBox.Show($"This form will be Close No Test Type with ID:{_TestID}", "Don't Find ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void _LoadData()
        {
            lblTestID.Text = _TestType.TestTypeID.ToString();
            txtTesttitle.Text = _TestType.TestTypeTitle;
            txtDescribtion.Text = _TestType.TestTypeDescription;
            txtFees.Text = _TestType.TestFees.ToString();
        }



        private void frmEditManageTestTypes_Load(object sender, EventArgs e)
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
            _TestType.TestTypeTitle = txtTesttitle.Text.Trim();
            _TestType.TestTypeDescription = txtDescribtion.Text.Trim();
            _TestType.TestFees = Convert.ToSingle(txtFees.Text);


            if (_TestType.Save())
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
                    "An error occurred while saving the Test Type data.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtTesttitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTesttitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTesttitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTesttitle, null);
            }
        }

        private void txtDescribtion_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescribtion.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescribtion, "Describtion cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtDescribtion, null);
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
