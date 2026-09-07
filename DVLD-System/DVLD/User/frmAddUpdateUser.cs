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
using System.Text.RegularExpressions;

namespace DVLD.User
{
    public partial class frmAddUpdateUser : Form
    {

        private enum enMode { Add, Update };
        private enMode mode;

        private clsUser _User;

        private int _PersonID = -1;
        private int _UserID = -1;

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;

            if (UserID == -1)
                mode = enMode.Add;
            else
                mode = enMode.Update;
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void _LoadData()
        {
            if (mode == enMode.Add)
            {
                lblMode.Text = "Add New User";

                _User = new clsUser();


                tpLoginInfo.Enabled = false;

                return;
            }


            _User = clsUser.FindByUserID(_UserID);

            if (_User == null)
            {
                MessageBox.Show($"This form will be Close No User with ID:{_UserID}", "Don't Find ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

            lblMode.Text = "Update User " + _UserID.ToString();
            ctrlPersonCardWithFilter1.FilterEnable = false;

            txUserName.Text = _User.UserName;
            txPassword.Text = _User.Password;
            txCondirmPassword.Text = _User.Password;


            if (_User.IsActive == true)
                chkIsActive.Checked = true;
            else
                chkIsActive.Checked = false;

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

            _User.UserName = txUserName.Text.Trim();
            _User.Password = txPassword.Text.Trim();
            _User.PersonID = _PersonID;

            if (chkIsActive.Checked)
                _User.IsActive = true;
            else
                _User.IsActive = false;

            if (_User.Save())
            {
                lblMode.Text = "Update User " + _User.UserID.ToString();
                mode = enMode.Update;

                btnNext.Visible = false;
                lblUserID.Text = _User.UserID.ToString();

                MessageBox.Show(
                  "User data has been saved successfully.",
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (mode == enMode.Update)
            {
                tpLoginInfo.Enabled = true;
                tabControl1.SelectedTab = tpLoginInfo;
                return;
            }


            if (_PersonID!=-1)
            {
                if (clsUser.isUserExistForPersonID(_PersonID))
                {
                    MessageBox.Show(
                        "This person already has a user account. Please select another person.",
                        "User Already Exists",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                else
                {
                    tpLoginInfo.Enabled = true;
                    tabControl1.SelectedTab = tpLoginInfo;
                }
            }

            else
            {
                MessageBox.Show(
                    "Please select a person first.",
                    "No Person Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            
        }



        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    

        private void txUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txUserName.Text))
            {
                e.Cancel = true;
                txUserName.Focus();

                errorProvider1.SetError(txUserName, "This Filed Is Required.");
                return; 
            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txUserName, "");
            }

            if (mode == enMode.Add) 
            {
                if (clsUser.isUserExist(txUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
                
            }
            else
            {
                if (_User.UserName != txUserName.Text.Trim())
                {
                    if (clsUser.isUserExist(txUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txUserName, null);
                    }
                    
                }
            }
        }

        private void txPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txPassword.Text))
            {
                e.Cancel = true;

                errorProvider1.SetError(txPassword, "This Filed Is Required.");
            }


            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void txCondirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txCondirmPassword.Text))
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();

                errorProvider1.SetError(txCondirmPassword, "This Filed Is Required.");
            }

            if (txPassword.Text.Trim() != txCondirmPassword.Text.Trim())
            {
                e.Cancel = true;
                txCondirmPassword.Focus();

                errorProvider1.SetError(txCondirmPassword, "Password Confirmation Does not natch Password!");
                return;
            }

            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txCondirmPassword, "");
            }
        }

        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();

        }
    }
}
