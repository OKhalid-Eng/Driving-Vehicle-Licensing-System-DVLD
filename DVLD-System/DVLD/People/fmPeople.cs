using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD
{
    public partial class fmPeople : Form
    {
        public fmPeople()
        {
            InitializeComponent();
        }
        private static DataTable _dtAllPeople;


        private DataTable _dtPeople;

        private void fmPeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleLList();
        }

      

        private void _RefreshPeopleLList()
        {
            _dtAllPeople = ClsPerson.GetAlPersons();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                      "FirstName", "SecondName", "ThirdName", "LastName",
                                                      "GendorCaption", "DateOfBirth", "CountryName",
                                                      "Phone", "Email", "ImagePath");

            dgvPeople.DataSource = _dtPeople;

            cbFilter.SelectedIndex = 0;
            lblCountRec.Text = dgvPeople.Rows.Count.ToString();
            if (dgvPeople.Rows.Count > 0)
            {

                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 120;


                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;


                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 120;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 140;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;


                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;


                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 170;

                dgvPeople.Columns[11].HeaderText = "ImagePath";
                dgvPeople.Columns[11].Width = 190;

            }
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
          if(MessageBox.Show($"Are you sure you want to delete Person [{dgvPeople.CurrentRow.Cells[0].Value}]","Confirm Delete", MessageBoxButtons.OKCancel,MessageBoxIcon.Information) == DialogResult.OK)
            {
                if(ClsPerson.DeletePersonByID((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.");
                    _RefreshPeopleLList();
                }
                else
                    MessageBox.Show("Person is not deleted.");

            }
        }

        private void tsmAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmAdd = new frmAddEditPerson(-1);
            frmAdd.ShowDialog();
            _RefreshPeopleLList();

        }
        

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmEdit = new frmAddEditPerson((int)dgvPeople.CurrentRow.Cells[0].Value);
            frmEdit.ShowDialog();
            _RefreshPeopleLList();


        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmAdd = new frmAddEditPerson(-1);
            frmAdd.ShowDialog();
            _RefreshPeopleLList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.SelectedIndex)
            {
                case 1:
                    FilterColumn = "PersonID";
                    break;

                case 2:
                    FilterColumn = "NationalNo";
                    break;

                case 3:
                    FilterColumn = "FirstName";
                    break;

                case 4:
                    FilterColumn = "SecondName";
                    break;

                case 5:
                    FilterColumn = "ThirdName";
                    break;

                case 6:
                    FilterColumn = "LastName";
                    break;

                case 7:
                    FilterColumn = "CountryName";
                    break;

                case 8:
                    FilterColumn = "GendorCaption";
                    break;

                case 9:
                    FilterColumn = "Phone";
                    break;

                case 10:
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblCountRec.Text = dgvPeople.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());

            lblCountRec.Text = dgvPeople.Rows.Count.ToString();

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tsmSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void tsmPhone_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilter.Visible = (cbFilter.SelectedIndex != 0 && cbFilter.SelectedIndex != 8);

            cbGendor.Visible = (cbFilter.SelectedIndex == 8);

            if (txtFilter.Visible)
            {
                txtFilter.Text = "";
                txtFilter.Focus();
            }

            if (cbGendor.Visible)
                cbGendor.SelectedIndex = 0;



            if (cbFilter.SelectedIndex != 5)
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblCountRec.Text = dgvPeople.Rows.Count.ToString();
            }
        }

        private void cbGendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbGendor.SelectedIndex)
            {
                case 0: // All
                    _dtPeople.DefaultView.RowFilter = "";
                    break;

                case 1: // Male
                    _dtPeople.DefaultView.RowFilter = "[GendorCaption] = 'Male'";
                    break;

                case 2: // Female
                    _dtPeople.DefaultView.RowFilter = "[GendorCaption] = 'Female'";
                    break;
            }

            
            lblCountRec.Text = dgvPeople.Rows.Count.ToString();

        }
    }
}
