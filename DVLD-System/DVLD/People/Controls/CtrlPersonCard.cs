using DVLD.Properties;
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
    public partial class ctrlPersoninfo : UserControl
    {

        private int _PersonID = -1;
        private ClsPerson _Person;

       public int PersonID
        {
            get { return _PersonID; }
        }

        public ClsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }


        public ctrlPersoninfo()
        {
            InitializeComponent();
        }
       
        public void LoadPersonInfo(int PersonID)
        {
            _Person = ClsPerson.FindPersonByID(PersonID);

            if (_Person==null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadDataOfPersonInfo();
        }

        public void LoadPersonInfo(string NationalID)
        {
            _Person = ClsPerson.FindPersonByID(NationalID);

            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with NationalID = " + NationalID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadDataOfPersonInfo();
        }

        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbGendor.Image = Resources.male_student_silhouette;
            else
                pbGendor.Image = Resources.woman_avatar1;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _LoadDataOfPersonInfo()
        {
            lblEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblFullName.Text = _Person.FullName;
            lblPhone.Text = _Person.Phone;
            lblNationalNo.Text = _Person.NationalNo.ToString();
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("yyyy-MM-dd");

            string CountryName = clsCountry.Find(_Person.NationalityCountryID).CountryName;

            lblCountry.Text = CountryName;

            if (_Person.Gendor == 0)
                lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";

            if (_Person.Email != "")
                lblEmail.Text = _Person.Email;
            else
                lblEmail.Text = "";

            _LoadPersonImage();

        }

        private void Personinfo_Load(object sender, EventArgs e)
        {
           // _LoadData();
        }

        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_PersonID);
            frm.ShowDialog();

            LoadPersonInfo(_PersonID);
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGendor.Image = Resources.male_student_silhouette;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.profile_picture1;

        }

    }
}
