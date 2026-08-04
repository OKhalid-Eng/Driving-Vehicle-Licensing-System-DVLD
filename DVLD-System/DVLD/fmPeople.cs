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

        private void fmPeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleLList();
        }


        private void _RefreshPeopleLList()
        {
            dgvPeople.DataSource = ClsPerson.GetAlPersons();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleLList();
        }
    }
}
