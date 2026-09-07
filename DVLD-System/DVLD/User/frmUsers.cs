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
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD.User
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }


        private DataTable _dtUsers;

        

        private void _RefreshUserLList()
        {

            _dtUsers = clsUser.GetAllUsers();
            dvgUsers.DataSource = _dtUsers;
            cbFilter.SelectedIndex = 0;
            lblCountRec.Text = dvgUsers.Rows.Count.ToString();
            if (dvgUsers.Rows.Count > 0)
            {

                dvgUsers.Columns[0].HeaderText = "User ID";
                dvgUsers.Columns[0].Width = 110;

                dvgUsers.Columns[1].HeaderText = "Person id";
                dvgUsers.Columns[1].Width = 120;


                dvgUsers.Columns[2].HeaderText = "Full Name";
                dvgUsers.Columns[2].Width = 210;


                dvgUsers.Columns[3].HeaderText = "User Name";
                dvgUsers.Columns[3].Width = 100;

                dvgUsers.Columns[4].HeaderText = "Is Active";
                dvgUsers.Columns[4].Width = 80;

            }
        }

        private void frnUsers_Load(object sender, EventArgs e)
        {

            _RefreshUserLList();
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to delete User [{dvgUsers.CurrentRow.Cells[0].Value}]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (clsUser.DeleteUser((int)dvgUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully.", " Deleted Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshUserLList();
                }
                else
                    MessageBox.Show("User is not deleted.");

            }
        }

        private void tsmAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser(-1);

            frm.ShowDialog();
            _RefreshUserLList();
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dvgUsers.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
            _RefreshUserLList();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser(-1);

            frm.ShowDialog();
            _RefreshUserLList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.SelectedIndex)
            {
                case 1:
                    FilterColumn = "UserID";
                    break;

                case 2:
                    FilterColumn = "PersonID";
                    break;

                case 3:               
                    FilterColumn = "UserName";
                    break;

                case 4:
                    FilterColumn = "FullName";
                    break;

                case 5:
                    FilterColumn = "IsActive";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblCountRec.Text = dvgUsers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID" || FilterColumn == "UserID")
                //in this case we deal with integer not string.
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());

            lblCountRec.Text = dvgUsers.Rows.Count.ToString();

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {

            //we allow number incase person id is selected.
            if (cbFilter.SelectedIndex == 1 || cbFilter.SelectedIndex == 2)
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
            txtFilter.Visible = (cbFilter.SelectedIndex != 0 && cbFilter.SelectedIndex != 5);

            cbIsActive.Visible = (cbFilter.SelectedIndex == 5);

            if (txtFilter.Visible)
            {
                txtFilter.Text = "";
                txtFilter.Focus();
            }
            if (cbIsActive.Visible)
            {
                cbIsActive.SelectedIndex = 0;
                txtFilter.Focus();
            }



            if (cbFilter.SelectedIndex != 5)
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblCountRec.Text = dvgUsers.Rows.Count.ToString();
            }
        }

        
        private void cbIsActive_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (cbIsActive.SelectedIndex)
            {
                case 0: // All
                    _dtUsers.DefaultView.RowFilter = "";
                    break;

                case 1: // Yes
                    _dtUsers.DefaultView.RowFilter = "[IsActive] = true";
                    break;

                case 2: // No
                    _dtUsers.DefaultView.RowFilter = "[IsActive] = false";
                    break;
            }


            lblCountRec.Text = dvgUsers.Rows.Count.ToString();

        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            frmUserDetails frm = new frmUserDetails((int)dvgUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void tsmChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dvgUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
