using DVLD.Classes;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAddEditPerson : Form
    {
        private enum enMode { Add,Update};
        private enMode mode;

        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

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
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            if (mode == enMode.Add)
            {
                lblMode.Text = "Add New Contact.";

                _Person = new ClsPerson();
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


            string ImagePath = _Person.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pictureBox1.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            lblRemoveImage.Visible = (_Person.ImagePath != "");
            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);


        }
        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != pictureBox1.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (pictureBox1.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pictureBox1.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pictureBox1.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
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


            if (!_HandlePersonImage())
                return;

            int CountryId = clsCountry.Find(cbCountry.Text).ID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalityCountryID = CountryId;
            _Person.NationalNo = txtNationalID.Text.Trim();

            _Person.DateOfBirth = dtpDateOfBirth.Value;
          
            _Person.Gendor = rdMale.Checked ? (byte)0 : (byte)1;

            _Person.Phone = txtPhone.Text;
            if(txtEmail.Text!="")
                _Person.Email = txtEmail.Text.Trim();
            else
                _Person.Email ="";


            _Person.Address = txtAddress.Text;

            if (pictureBox1.ImageLocation != null)
                _Person.ImagePath = pictureBox1.ImageLocation;
            else
                _Person.ImagePath = "";



            if (_Person.Save())
            {
                mode = enMode.Update;

                lblMode.Text = "Edit Person ID = " + _Person.PersonID;
                lblPersonID.Text = _Person.PersonID.ToString();



                MessageBox.Show(
                    "Person data has been saved successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DataBack?.Invoke(this, _Person.PersonID);

            }
            else
            {
                MessageBox.Show(
                    "An error occurred while saving the person data.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


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
                e.Cancel = false;
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
            e.Cancel = false;
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

                lblRemoveImage.Visible = true;
                // ...
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
