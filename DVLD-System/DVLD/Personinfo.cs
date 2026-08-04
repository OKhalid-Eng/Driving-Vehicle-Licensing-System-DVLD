using DVLD_Business;
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

namespace DVLD
{
    public partial class ctrlPersoninfo : UserControl
    {

        private int _PersonID;
        private ClsPerson _Person;

       
        public ctrlPersoninfo()
        {
            InitializeComponent();
        }
       
        public void LoadData(int ID)
        {
            _PersonID = ID;
            _LoadData();
        }

        private void _LoadData()
        {

            _Person = ClsPerson.FindPersonByID(_PersonID);

            lblPersonID.Text = _Person.PersonID.ToString();
            lblFirtName.Text = _Person.FirstName;
            lblSecondName.Text = _Person.SecondName;
            lblThirdName.Text = _Person.ThirdName;
            lblLastName.Text = _Person.LastName;
            lblPhone.Text = _Person.Phone;
            lblNationalNo.Text = _Person.NationalNo.ToString();
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();

            if (_Person.Gendor == 0)
                lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";

            if (_Person.Email != "")
                lblEmail.Text = _Person.Email;
            else
                lblEmail.Text = "";


            if (_Person.ImagePath != "")
                pictureBox1.Load(_Person.ImagePath);

            string CountryName = clsCountry.Find(_Person.NationalityCountryID).CountryName;
        }

        private void Personinfo_Load(object sender, EventArgs e)
        {
           // _LoadData();
        }

        
    }
}
