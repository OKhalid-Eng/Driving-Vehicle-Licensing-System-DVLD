using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD
{
    public partial class frmAddEditPerson : Form
    {
        private enum enMode { Add,Update};
        private enMode mode;

        private int _PersonID;
        private ClsPerson _Person;


        public frmAddEditPerson(int ID)
        {
            InitializeComponent();

            _PersonID = ID;

            if (_PersonID == -1)
                mode = enMode.Add;
            else
                mode = enMode.Update;


        }

        private void _FillCountriesWithComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();
            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }





        private void _LoadData()
        {
            _FillCountriesWithComboBox();
            cbCountry.SelectedIndex = 1;

            if (mode == enMode.Add)
            {
                lblMode.Text = "Add New Contact.";

                ClsPerson pr = new ClsPerson();
                return;
            }

            _Person = ClsPerson.FindPersonByID(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show($"This form will be Close No contact with ID:{_PersonID}", "Don't Find ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }


            lblMode.Text = "Edit Contact ID = " + _PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtPhone.Text = _Person.Phone;
            txtNationalID.Text = _Person.NationalNo.ToString();
            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gendor == 0)
                rdMale.Checked = true;
            else
                rdFemale.Checked = true;

            if (_Person.Email != "")
            {
                txtEmail.Text = _Person.Email;
            }


            if (_Person.ImagePath!="")
            {
                pictureBox1.Load(_Person.ImagePath);
            }

            lblRemoveImage.Visible = (_Person.ImagePath != "");
            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);


        }
        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            int CountryId = clsCountry.Find(cbCountry.Text).ID;

            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.NationalityCountryID = CountryId;
            _Person.NationalNo = txtNationalID.Text;

            _Person.DateOfBirth = dtpDateOfBirth.Value;
          
            _Person.Gendor = rdMale.Checked ? (byte)0 : (byte)1;

            _Person.Phone = txtPhone.Text;
            if(txtEmail.Text!="")
                _Person.Email = txtEmail.Text;
            else
                _Person.Email ="";


            _Person.Address = txtAddress.Text;

            if (pictureBox1.ImageLocation != null)
                _Person.ImagePath = pictureBox1.ImageLocation;
            else
                _Person.ImagePath = "";



            if (_Person.Save())
            {
                MessageBox.Show(
                    "Person data has been saved successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "An error occurred while saving the person data.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            mode = enMode.Update;

            lblMode.Text = "Edit Person ID = " + _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();

        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                e.Cancel = true;
                txtFirstName.Focus();

                errorProvider1.SetError(txtFirstName, "This Filed Is Required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFirstName, "");
            }
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSecondName.Text))
            {
                e.Cancel = true;
                txtFirstName.Focus();

                errorProvider1.SetError(txtSecondName, "This Filed Is Required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtSecondName, "");
            }
        }

        private void txtThirdName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtThirdName.Text))
            {
                e.Cancel = true;
                txtFirstName.Focus();

                errorProvider1.SetError(txtThirdName, "This Filed Is Required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtThirdName, "");
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                e.Cancel = true;
                txtFirstName.Focus();

                errorProvider1.SetError(txtLastName, "This Filed Is Required.");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtLastName, "");
            }
        }

        private void txtNationalID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNationalID.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalID, "National ID cannot be empty.");
                return;
            }

            if (ClsPerson.IsNationalNoUsedByAnotherPerson(_Person.PersonID, txtNationalID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalID, "This National ID is already in use.");
            }
            else
            {
                errorProvider1.SetError(txtNationalID, "");
            }
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPhone, "Phone number cannot be empty.");
                return;
            }

            if (!txtPhone.Text.All(char.IsDigit))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPhone, "Phone number must contain digits only.");
                return;
            }

            errorProvider1.SetError(txtPhone, "");
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
           
            try
            {
                var email = new System.Net.Mail.MailAddress(txtEmail.Text);

                if (email.Address != txtEmail.Text)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtEmail, "Invalid email format.");
                    return;
                }
            }
            catch
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid email format.");
                return;
            }

            errorProvider1.SetError(txtEmail, "");
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPhone, "Address  cannot be empty.");
                return;
            }


            errorProvider1.SetError(txtPhone, "");
        }

        private void lblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = null;
            lblRemoveImage.Visible = false;
        }

        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                pictureBox1.Load(selectedFilePath);
                // ...
            }
        }
    }
}
