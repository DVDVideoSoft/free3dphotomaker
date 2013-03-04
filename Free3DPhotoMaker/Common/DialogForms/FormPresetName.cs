using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DVDVideoSoft.DialogForms
{
    public partial class FormPresetName : Form
    {
        public FormPresetName()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void PresetName_Shown(object sender, EventArgs e)
        {
            edName.Focus();
            //btnOk.Select();
        }
        public string EdName
        {
            get
            {
                return edName.Text;
            }
            set
            {
                edName.Text = value;

            }
        }

        public string Caption
        {
            set 
            {
                this.Text = value;
            }
        }

        public string LblName
        {
            set
            {
                lblName.Text = value;
            }
        }

        public string BtnOk
        {
            set
            {
                 btnOk.Text = value;
            }
        }

        public string BtnCancel
        {
            set
            {
                btnCancel.Text = value;
            }
        }

    }
}
