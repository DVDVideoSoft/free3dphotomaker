using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace DVDVideoSoft.Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class ChoupBox : UserControl
    {
        public delegate void TitleCheckedDelegate(bool state);
        public event TitleCheckedDelegate CheckedChanged;

        public Point TitleLocation
        {
            get { return checkBoxTitle.Location; }
            set { checkBoxTitle.Location = value; }
        }

        public string Title
        {
            get { return checkBoxTitle.Text; }
            set { checkBoxTitle.Text = value; }
        }

        public bool Checked
        {
            get { return checkBoxTitle.Checked; }
            set { checkBoxTitle.Checked = value; }
        }

        public ChoupBox()
        {
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Rectangle clipRect;
            if(Application.RenderWithVisualStyles)
                clipRect= new Rectangle(new Point(e.ClipRectangle.X, checkBoxTitle.Location.Y+checkBoxTitle.Height/2),new Size(e.ClipRectangle.Size.Width,e.ClipRectangle.Height-(checkBoxTitle.Location.Y+checkBoxTitle.Height/2)));
            else
                clipRect= new Rectangle(new Point(e.ClipRectangle.X, checkBoxTitle.Location.Y+checkBoxTitle.Height/2),e.ClipRectangle.Size);

            GroupBoxRenderer.DrawGroupBox(e.Graphics,clipRect, Enabled ? System.Windows.Forms.VisualStyles.GroupBoxState.Normal : System.Windows.Forms.VisualStyles.GroupBoxState.Disabled);
        }

        private void checkBoxTitle_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] != checkBoxTitle)
                    Controls[i].Enabled = checkBoxTitle.Checked;
            }

            if (CheckedChanged != null)
                CheckedChanged(checkBoxTitle.Checked);
        }
    }
}
