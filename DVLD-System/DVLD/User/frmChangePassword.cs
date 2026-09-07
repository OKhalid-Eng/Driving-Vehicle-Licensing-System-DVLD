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

namespace DVLD.User
{
    public partial class frmChangePassword : Form
    {
        private int _UserID = -1;
        private clsUser _User;


        public frmChangePassword(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
        }


        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                MessageBox.Show("No User with UserID = " + _User.UserID.ToString(),
                    "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            ctrlUserCardcs1.LoadUserInfo(_UserID);


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show(
                   "Please correct the errors before saving.",
                   "Validation Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);

                return;
            }

            string NewPassword = txtConfirmPassword.Text.Trim();

            if (_User.ChangePassword(_UserID, NewPassword))
            {
                MessageBox.Show(
                  "Password Change successfully.",
                  "Success",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);

                _ResetDefultValue();
            }

            else
            {
                MessageBox.Show(
                    "An error occurred while Changing Password.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void _ResetDefultValue()
        {
            txtConfirmPassword.Text = "";
            txtCusserntPassword.Text = "";
            txtPassword.Text = "";
        }

        private void txtCusserntPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCusserntPassword.Text))
            {
                e.Cancel = true;
                txtCusserntPassword.Focus();

                errorProvider1.SetError(txtCusserntPassword, "This Filed Is Required.");
            }

            if (_User.Password.Trim() != txtCusserntPassword.Text.Trim()) 
            {
                e.Cancel = true;
                txtCusserntPassword.Focus();

                errorProvider1.SetError(txtCusserntPassword, "Current Password Does not natch User Password!");
                return;
            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCusserntPassword, "");
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                e.Cancel = true;
                txtPassword.Focus();

                errorProvider1.SetError(txtPassword, "This Filed Is Required.");
            }

           
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();

                errorProvider1.SetError(txtConfirmPassword, "This Filed Is Required.");
            }

            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();

                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation Does not natch Password!");
                return;
            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }
    }
}
