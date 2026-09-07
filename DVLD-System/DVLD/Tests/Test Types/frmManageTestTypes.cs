using DVLD.Applications.Application_Types;
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

namespace DVLD.Tests.Test_Types
{
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }


        private DataTable _dtTable;

        private void _RefreshTestType()
        {
            _dtTable = clsTestTypes.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtTable;
            lblCountRec.Text = dgvTestTypes.Rows.Count.ToString();

            if (dgvTestTypes.Rows.Count > 0)
            {

                dgvTestTypes.Columns[0].HeaderText = "App ID";
                dgvTestTypes.Columns[0].Width = 110;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 180;


                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 450;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 120;
            }

        }


        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestType();
            dgvTestTypes.ContextMenuStrip = cmsTestTypesManagement;
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            frmEditManageTestTypes frm = new frmEditManageTestTypes((int)dgvTestTypes.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
            _RefreshTestType();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
