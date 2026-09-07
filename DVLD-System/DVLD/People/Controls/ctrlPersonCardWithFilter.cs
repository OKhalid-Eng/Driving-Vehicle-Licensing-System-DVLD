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

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
                handler(PersonID);
        }
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private bool _ShowAddPerson = true;

        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }

            set
            {
                _ShowAddPerson = value;
                btnAddPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnable = true;

        public bool FilterEnable
        {
            get
            {
                return _FilterEnable;
            }


            set
            {
                _FilterEnable = value;
                gbFilters.Enabled = _FilterEnable;
            }
        }

        private int _PersonID = -1;


        public int PersonID
        {
            get { return ctrlPersoninfo1.PersonID; }
        }


        public ClsPerson SelectedPersonInfo
        {
            get { return ctrlPersoninfo1.SelectedPersonInfo; }
        }

        public void FilterFocus()
        {
            txtFind.Focus();
        }

        public void LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 0;

            txtFind.Text = PersonID.ToString();
            FindNow();
        }

        public void LoadPersonInfo(string NationalNumber)
        {
            cbFilterBy.SelectedIndex = 1;

            txtFind.Text = NationalNumber;
            FindNow();
        }
        public void FindNow()
        {
            switch(cbFilterBy.SelectedIndex)
            {
                case 0:
                    ctrlPersoninfo1.LoadPersonInfo(int.Parse(txtFind.Text));
                    break;

                case 1:
                    ctrlPersoninfo1.LoadPersonInfo(txtFind.Text);
                    break;

                default:
                    break;

            }

            if (OnPersonSelected != null && FilterEnable) 
            {
                OnPersonSelected(ctrlPersoninfo1.PersonID);
            }


        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFind.Text = "";
            txtFind.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FindNow();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(-1);
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFind.Text = PersonID.ToString();
            LoadPersonInfo(PersonID);
        }

        private void txtFind_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFind.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFind, "This field is required!");
            }

            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFind, null);
            }
        }

        private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilterBy.SelectedIndex == 0)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
        }
    }
}
