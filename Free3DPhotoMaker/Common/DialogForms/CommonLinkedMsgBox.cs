using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DVDVideoSoft.Utils;

namespace DVDVideoSoft.DialogForms
{
    public partial class CommonLinkedMsgBox : Form
    {
        //private IList<Control> controls;
        //private IList<Point>   ctrlLocations;
        private Form parent;
        private Buttons btns = Buttons.Ok;

        public event EventHandler onBtnOkClicked;
        public event EventHandler onBtnLaterClicked;


        public enum Buttons
        {
            Ok,
            OkLater
        }

        public CommonLinkedMsgBox()
        {
            InitializeComponent();
        }

        public CommonLinkedMsgBox(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        public DialogResult Show(IList<Control> controls)//, IList<Point> ctrlLocations)
        {
            //if (controls.Count != ctrlLocations.Count)
            //    return DialogResult.Cancel;

            //int index = 0;
            //this.iconPictureBox.Top = (this.pnlDown.Top - this.iconPictureBox.Height) / 2;
            foreach (Control ctrl in controls)
            {
                this.Controls.Add(ctrl);
                ctrl.Left += this.iconPictureBox.Left + this.iconPictureBox.Width;
            }
            this.Width += this.iconPictureBox.Left + this.iconPictureBox.Width;

            if (btns == Buttons.Ok)
            {
                this.btnOk.Location = this.btnLater.Location;
                this.btnLater.Visible = false;
                this.CancelButton = this.btnOk;
            }

            return ShowDialog(parent);
        }

        public DialogResult Show(IList<Control> controls, Buttons buttons)
        {
            this.btns = buttons;
            return Show(controls);
        }

        private void CommonLinkedMsgBox_Load(object sender, EventArgs e)
        {
            WindowUtils.CenterToParent(this, this.parent);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (onBtnOkClicked != null)
                onBtnOkClicked(sender, e);
        }

        private void btnLater_Click(object sender, EventArgs e)
        {
            if (onBtnLaterClicked != null)
                onBtnLaterClicked(sender, e);
        } 
    }
}
