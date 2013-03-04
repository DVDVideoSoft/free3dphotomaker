using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DVDVideoSoft.Controls
{
    public partial class SearchTextBox : UserControl
    {
        public event EventHandler SearchTextChanged;

        private Color SideColor = Color.FromArgb(226, 227, 234);
        private Color UpColor   = Color.FromArgb(171, 173, 179);
        private Color DownColor = Color.FromArgb(227, 233, 239);

        private Color FocusedSideColor = Color.FromArgb(181, 207, 231);
        private Color FocusedUpColor   = Color.FromArgb(61, 123, 173);
        private Color FocusedDownColor = Color.FromArgb(183, 217, 237);
        public SearchTextBox()
        {
            InitializeComponent();
            bool canFocus = this.CanFocus;
        }

        private void SearchTextBox_Enter(object sender, EventArgs e)
        {
            this.lblSearchDownloads.Visible = false;
            this.Focus();
        }

        private void SearchTextBox_Leave(object sender, EventArgs e)
        {
            this.lblSearchDownloads.Visible = string.IsNullOrEmpty(this.txtSearch.Text);
        }

        [Category("Text"),
        DefaultValue(typeof(string), ""),
        Description("The search text box text.")]
        public override string Text
        {
            get { return this.txtSearch.Text;  }
            set { this.txtSearch.Text = value; }
        }

        private void SearchTextBox_Load(object sender, EventArgs e)
        {
            //this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchPictureBox.Left = this.Width - 22;
            this.searchPictureBox.Top = (this.Height - this.searchPictureBox.Height) / 2;
            this.lblSearchDownloads.Top = (this.Height - this.lblSearchDownloads.Height) / 2;
            this.txtSearch.Width = this.searchPictureBox.Left - 5;
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.SearchTextChanged != null)
                SearchTextChanged(sender, e);
        }

        private void lblSearchDownloads_Click(object sender, EventArgs e)
        {
            this.txtSearch.Focus();
        }


        private void SearchTextBox_Paint(object sender, PaintEventArgs e)
        {
            if (!Focused)
            {
                e.Graphics.DrawLine(new Pen(SideColor), new Point(0, 0), new Point(0, this.Height));
                e.Graphics.DrawLine(new Pen(SideColor), new Point(this.Width - 1, 0), new Point(this.Width - 1, this.Height));

                e.Graphics.DrawLine(new Pen(DownColor), new Point(0, this.Height - 1), new Point(this.Width, this.Height - 1));
                e.Graphics.DrawLine(new Pen(UpColor), new Point(0, 0), new Point(this.Width, 0));
            }
            else
            {
                e.Graphics.DrawLine(new Pen(FocusedSideColor), new Point(0, 0), new Point(0, this.Height));
                e.Graphics.DrawLine(new Pen(FocusedSideColor), new Point(this.Width - 1, 0), new Point(this.Width - 1, this.Height));

                e.Graphics.DrawLine(new Pen(FocusedDownColor), new Point(0, this.Height - 1), new Point(this.Width, this.Height - 1));
                e.Graphics.DrawLine(new Pen(FocusedUpColor), new Point(0, 0), new Point(this.Width, 0));
            }
        }
    }
}
