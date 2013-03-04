using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DVDVideoSoft.Controls
{
    public partial class DvdSizeBar : UserControl, DVDVideoSoft.Utils.IDvsControl
    {
        public DvdSizeBar() 
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, false);

            //this.BackColor = Color.Cyan;
            //this.pictureBox1.Location = new Point(0, 0);
            //this.pictureBox1.BackColor = Color.Red;
            this.backPictureBox.Size = new Size(this.Width - 1, this.Height - 1);//324 - 32, 45 - 8);//
            //this.outputSizePanel.Height = 12;
            this.outputSizePanel.Width = 32;
            //this.label1.Text = "dddddddddddddddd";
            //this.Refresh();
        }

        private void DvdSizeBar_SizeChanged(object sender, EventArgs e)
        {
            //this.backPictureBox.Size = new Size(this.Width - 4, this.Height - 4);//324 - 32, 45 - 8);//
            this.outputSizePanel.Location = new Point(0, 0);
            this.backPictureBox.Location = new Point(0, 0);
            this.outputSizePanel.Height = this.dvdTypesBoundsPictureBox.Height = this.Height - 1;
            this.backPictureBox.Width = this.Width - 1;
            this.backPictureBox.Height = this.Height - 1;
        }

        #region Properties

        #region Localization

        public void OnSystemThemeChanged(bool visualStylesEnabled)
        {
        }

        public void SetBackground(Bitmap background)
        {
        }

        #endregion

        #region Private Variables

        //private bool normalDpiMode = true;

        //private State _state = State.None;
        //private State state
        //{
        //    get { return this._state; }
        //    set { this._state = value; }
        //}

        #endregion

        #region Text

        public void AdjustSize()
        {
            //if (this.AutoSize && System.Reflection.Assembly.GetEntryAssembly() != null)
            //    this.Width = GetTextLeft() +
            //                    GetGraphics().MeasureString(this.text, this.Font).ToSize().Width +
            //                    2 +
            //                    (this.DrawComboButton ? 7 : 0) +
            //                    (this.normalDpiMode ? 0 : 3);
        }

        #endregion
    #endregion

    #region Public methods

        private int dataSize;

        public int DataSize
        {
            get
            {
                return this.dataSize;
            }

            set
            {
                this.dataSize = value;
                this.outputSizeLabel.Text = string.Format("Output size: {0:F2} Mb", (float)value / (1024 * 1024));
            }
        }

    #endregion

    }
}
