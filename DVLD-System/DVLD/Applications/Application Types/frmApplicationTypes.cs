using DVLD.User;
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

namespace DVLD.Applications.Application_Types
{
    public partial class frmApplicationTypes : Form
    {
        public frmApplicationTypes()
        {
            InitializeComponent();
        }

        private DataTable _dtTable;


        private void _RefreshAppType()
        {
            _dtTable = clsAppTypes.GetAllAppTypes();
            dgvAppTypes.DataSource = _dtTable;
            lblCountRec.Text = dgvAppTypes.Rows.Count.ToString();

            if (dgvAppTypes.Rows.Count > 0)
            {

                dgvAppTypes.Columns[0].HeaderText = "App ID";
                dgvAppTypes.Columns[0].Width = 110;

                dgvAppTypes.Columns[1].HeaderText = "Title";
                dgvAppTypes.Columns[1].Width = 230;


                dgvAppTypes.Columns[2].HeaderText = "Fees";
                dgvAppTypes.Columns[2].Width = 110;
            }

        }

        private void frmApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshAppType();
            dgvAppTypes.ContextMenuStrip = cmsAppTypesManagement;


        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType frm = new frmUpdateApplicationType((int)dgvAppTypes.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
            _RefreshAppType();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
