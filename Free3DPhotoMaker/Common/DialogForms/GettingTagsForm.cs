using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using DVDVideoSoft.Utils;

namespace DVDVideoSoft.DialogForms
{
    public partial class GettingTagsForm : Form
    {
        Form parent;
        bool canClose = true;
        public GettingTagsForm(Form parent)
        {
            this.parent = parent;
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private string MinimizeName(string name)
        {
            int namePixSize = 0;

            namePixSize = (int)CreateGraphics().MeasureString(name, this.Font).Width;
            string newPath = name;

            if (namePixSize + 60 > this.Width)
                newPath = name.Substring(0, name.Length - 2);

            while ((namePixSize + 60 > this.Width))
            {
                newPath = newPath.Substring(0, newPath.Length - 1);
                namePixSize = (int)CreateGraphics().MeasureString(newPath + "...", this.Font).Width;
            }

            if (!newPath.Equals(name))
                newPath += "...";

            return newPath;
        }

        public DialogResult ShowModal(string filename)
        {
            this.lblFile.Text = MinimizeName(filename);
            this.timer1.Start(); //wait for a 1 minute and close the window.
            canClose = false;
            return this.ShowDialog(this.parent);
        }

        private void GettingTagsForm_Load(object sender, EventArgs e)
        {
            if (this.parent != null)
                WindowUtils.CenterToParent(this, this.parent);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();
            canClose = true;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void GettingTagsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = !canClose;
        }

        public void CloseForm()
        {
            canClose = true;
            this.Close();
        }
    }
}
