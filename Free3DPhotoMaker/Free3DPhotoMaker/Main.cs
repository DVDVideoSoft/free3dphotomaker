using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;

using DVDVideoSoft.Utils;
using DVDVideoSoft.AppFxApi;
using DVDVideoSoft.AppFx;
using DVDVideoSoft.Resources;

namespace Free3DPhotoMaker
{
    public partial class Main: DvsMain
    {
        private StereoProcessor sProcessor;
        private Configuration config;
        private bool processing = false;
        private string buttonConvertText = "";
        private bool notAssign = false; //FIXME: Slov cenzurnih net glyadya na bessmislennie nazvaniya takih flagov. FIXME!!!!!!!!! nikto ne poimet chego Assign i pochmu NOT

        #region Intialization
        
        public Main():
            base()
        {
            InitializeComponent();

            base.Init(false, false);
            base.InitProperties();

            sProcessor = new StereoProcessor();
            sProcessor.MergeProgressChanged += new StereoProcessor.MergeProgressChangedDelegate(sProcessor_MergeProgressChanged);
        }

        public void InitConfig()
        {
            config = new Configuration(Programs.GetRegKey(this.legacyDvsProgId), GetOutputFolder());
            config.InitStorableKeyList();
            config.ConfigHolder.Set(Configuration.MainFormName, Text);
            sProcessor.SetAvailableAlgorithms(config.ConfigHolder);
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CHS");
        }

        public void InitComponents()
        {
            locMgr.ApplyCultureForAll(locMgr.ReadCurrentCulture());

            this.outputFolderText.Text = FileUtils.AppendIndexToFileName(config.ConfigHolder.GetString(Configuration.OutputFile));
            showPreviewCheckBox.Checked = config.ConfigHolder.GetBool(Configuration.ShowPreview);
            useSingleSourceCheckBox.Checked = config.ConfigHolder.GetBool(Configuration.UseSingleImage);
            sProcessor.UseSingle = useSingleSourceCheckBox.Checked;
            swapButton.Enabled = !useSingleSourceCheckBox.Checked;
            openRightButton.Enabled = !useSingleSourceCheckBox.Checked;

            convertButton.Enabled = false;

            if (config.ConfigHolder.GetBool(Configuration.FirstRun))
            {
                sProcessor.OpenImage("SampleLeft.jpg", StereoProcessor.ImageSide.Left);
                sProcessor.OpenImage("SampleRight.jpg", StereoProcessor.ImageSide.Right);

                if (sProcessor.Ready())
                {
                    RefreshImages();
                    convertButton.Enabled = true;
                }
                config.ConfigHolder.Set(Configuration.FirstRun, false);
            }

#if DEBUG
            advancedButton.Enabled = true;
#endif

        }

        #endregion

        #region Overrides

        public override void SetCulture(string culture)
        {
            sProcessor.SetAvailableAlgorithms(config.ConfigHolder);
            int sel = algorithmCombo.SelectedIndex;
            algorithmCombo.Items.Clear();
            List<ComboBoxItem<StereoProcessor.StereoAlgorithm>> algs = (List<ComboBoxItem<StereoProcessor.StereoAlgorithm>>)config.ConfigHolder[Configuration.AvailableAlgorithms];
            for (int i = 0; i < algs.Count; i++)
            {
                algorithmCombo.Items.Add(algs[i]);
            }
            algorithmCombo.SelectedIndex = sel;
            if (sel == -1)
                algorithmCombo.SelectedIndex = 0;

            UpdateTitle();
        }

        protected override string GetLocalizedString(string id)
        {
            string localizedString = "";
            try
            {
                ResourceManager commonDataResMgr = new ResourceManager("DVDVideoSoft.Resources.CommonData", typeof(CommonData).Assembly);
                localizedString = commonDataResMgr.GetString(id);
                //if (string.IsNullOrEmpty(localizedString))
                //{
                //    ResourceManager mainResMgr = new ResourceManager("Free3DPhotoMaker.StringResources", typeof(StringResources).Assembly);
                //    localizedString = mainResMgr.GetString(id);
                //}
            }
            catch { }
            return localizedString;
        }

        protected override void OnFormClosing(object sender, FormClosingEventArgs e)
        {
#if !SHOW_OFFER
            m_bShowOfferScreen = false;
#endif
            sProcessor.Clear();
            config.Save();
            base.OnFormClosing(sender, e);
        }

        #endregion

        #region Form's event handlers

        private void Main_Load(object sender, EventArgs e)
        {
            InitConfig();
            InitComponents();

            if (!config.ConfigHolder.GetBool(Configuration.DoNotShowWelcomeScreen))
            {
                WelcomeForm welcome = new WelcomeForm();
                if (this.splash != null)
                    this.splash.Close();

                welcome.ShowModal(this, config.ConfigHolder);
            }
        }

        private void Main_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string sGuideUrl = LocaleUtils.FormatCultureDependentUrl(DvsUrls.GuideUrlTemplate, 0) + GetProvider().GetString(DVDVideoSoft.AppFxApi.Defs.PN.AppTitle.ToString()).Replace(" ", "-") + ".htm";
            System.Diagnostics.Process.Start(sGuideUrl);
        }

        private void Main_ResizeBegin(object sender, EventArgs e)
        {
            //BackgroundImage = null;
        }

        private void Main_ResizeEnd(object sender, EventArgs e)
        {
        }

        #endregion

        #region Business logic

        private void sProcessor_MergeProgressChanged(int progress, int max)
        {
            if (InvokeRequired)
            {
                Invoke(new StereoProcessor.MergeProgressChangedDelegate(sProcessor_MergeProgressChanged), new Object[] { progress, max });
                return;
            }

            progressBar.Maximum = max;
            progressBar.Value = progress;
            if (progress >= progressBar.Maximum)
            {
                processing = false;
                ShowProcessUI(false);
                progressBar.Value = 0;
            }
        }

        #endregion

        #region UI routines

        private void UpdateTitle()
        {
            Text = this.GetAppTitleWithVersion();
        }

        public void ShowProcessUI(bool show)
        {
            if (show)
            {
                buttonConvertText = convertButton.Text;
                convertButton.Text = DVDVideoSoft.Resources.CommonData.Stop;
            }
            else
                convertButton.Text = buttonConvertText;

            browseButton.Enabled = !show;
            openLeftButton.Enabled = !show;
            openRightButton.Enabled = !show && !sProcessor.UseSingle;
            btnOptions.Enabled = !show;
            swapButton.Enabled = !show && !sProcessor.UseSingle;
            outputFolderText.Enabled = !show;
            algorithmCombo.Enabled = !show;

            labelMerging.Visible = show;
            progressBar.Visible = show;

            if (!show && !processing)
            {
                // обработка статуса процессора. 
                StereoProcessor.ProcessStatus s = sProcessor.Status;
                if (s == StereoProcessor.ProcessStatus.NoError)
                {
                    sProcessor.SaveToFile(outputFolderText.Text);
                    if (showPreviewCheckBox.Checked)
                    {
                        PreviewForm prev = new PreviewForm(sProcessor.ResImage);
                        prev.ShowDialog(this);
                    }

                    CompleteMessage cmpl = new CompleteMessage(config.ConfigHolder, true);
                    cmpl.ShowDialog(this);
                    m_bShowOfferScreen = true;
                }
                else
                    MessageBox.Show(sProcessor.GetStatusMsg());
            }
        }

        private void RefreshImages()
        {
            if (splitContainer1.Visible)
            {
                leftGDViewer.DisplayFromGdPictureImage(sProcessor.LeftImage);
                rightGDViewer.DisplayFromGdPictureImage(sProcessor.RightImage);
            }
            else
                if (advancedPanel.Visible)
                    transparentGDViewer.DisplayFromGdPictureImage(sProcessor.TransparentMergedImage);
        }

        private void InitNumerics()
        {
            numericUpDownHorz.Minimum = -sProcessor.Width / 4;
            numericUpDownHorz.Maximum = sProcessor.Width / 4;

            numericUpDownVert.Minimum = -sProcessor.Height / 4;
            numericUpDownVert.Maximum = sProcessor.Height / 4;
        }

        private void EnableLeftRadio(bool enable)
        {
            leftImageRadio.Enabled = enable;
            EnableAdvancedControls(leftImageRadio.Enabled && rightImageRadio.Enabled);
        }

        private void EnableRightRadio(bool enable)
        {
            rightImageRadio.Enabled = enable;
            EnableAdvancedControls(leftImageRadio.Enabled && rightImageRadio.Enabled);
        }

        private void EnableAdvancedControls(bool enable)
        {
            numericUpDownHorz.Enabled = enable;
            numericUpDownVert.Enabled = enable;
            numericUpDownAngle.Enabled = enable;
            applyButton.Enabled = enable;
            resetButton.Enabled = enable;
        }

        #endregion

        #region Control event handlers

        private void buttonOpenLeft_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = DVDVideoSoft.ImageUtils.ImageUtils.GetOpenDialogFilter();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                string fn = dlg.FileName;
                dlg.Dispose();
                Cursor = Cursors.WaitCursor;
                int img = sProcessor.OpenImage(fn, StereoProcessor.ImageSide.Left);
                if (img > 0)
                {
                    InitNumerics();
                    EnableLeftRadio(true);

                    if (splitContainer1.Visible)
                        leftGDViewer.DisplayFromGdPictureImage(img);
                    else
                    {
                        transparentGDViewer.DisplayFromGdPictureImage(sProcessor.MergeTransparentPreview(new StereoProcessor.ImageSettings(), transparentGDViewer.Width, transparentGDViewer.Height));
                    }
                }
                if (sProcessor.Ready())
                    convertButton.Enabled = true;
                Cursor = Cursors.Default;
            }

            outputFolderText.Text = FileUtils.AppendIndexToFileName(config.ConfigHolder.GetString(Configuration.OutputFile));
        }

        private void buttonOpenRight_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = DVDVideoSoft.ImageUtils.ImageUtils.GetOpenDialogFilter();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                string fn = dlg.FileName;
                dlg.Dispose();
                Cursor = Cursors.WaitCursor;
                int img = sProcessor.OpenImage(fn, StereoProcessor.ImageSide.Right);
                if (img > 0)
                {
                    InitNumerics();
                    EnableRightRadio(true);

                    if (splitContainer1.Visible)
                        rightGDViewer.DisplayFromGdPictureImage(img);
                    else
                    {
                        transparentGDViewer.DisplayFromGdPictureImage(sProcessor.MergeTransparentPreview(new StereoProcessor.ImageSettings(), transparentGDViewer.Width, transparentGDViewer.Height));
                    }
                }
                if (sProcessor.Ready())
                    convertButton.Enabled = true;
                Cursor = Cursors.Default;
            }

            outputFolderText.Text = FileUtils.AppendIndexToFileName(config.ConfigHolder.GetString(Configuration.OutputFile));
        }

        private void buttonSwap_Click(object sender, EventArgs e)
        {
            sProcessor.Swap();
            RefreshImages();
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            if (System.IO.Path.GetFileName(outputFolderText.Text).Length == 0)
            {
                MessageBox.Show(this, "Please enter file name.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            System.IO.Directory.CreateDirectory(config.ConfigHolder.GetString(Defs.PN.OutputFolder.ToString()));

            if (processing)
            {
                sProcessor.Stop();
                ShowProcessUI(false);
                processing = false;
            }
            else
            {
                outputFolderText.Text = FileUtils.AppendIndexToFileName(config.ConfigHolder.GetString(Configuration.OutputFile));
                processing = true;
                ShowProcessUI(true);
                sProcessor.Merge();
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            
           SaveFileDialog dlg = new SaveFileDialog();
           string strOutFile = config.ConfigHolder.GetString(Configuration.OutputFile);
           
           if ( strOutFile != "" ) {
               dlg.InitialDirectory = System.IO.Path.GetDirectoryName( strOutFile );//config.ConfigHolder.GetString(Configuration.OutputFile);
               dlg.FileName = System.IO.Path.GetFileName( strOutFile );
               dlg.CheckFileExists = false;
           }
           dlg.Filter = DVDVideoSoft.ImageUtils.ImageUtils.GetSaveDialogFilter();
           if (dlg.ShowDialog(this) == DialogResult.OK)
               outputFolderText.Text = dlg.FileName;
           dlg.Dispose();
            
        }

        private void comboBoxAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            sProcessor.Algorithm = ((ComboBoxItem<StereoProcessor.StereoAlgorithm>)algorithmCombo.SelectedItem).Value;
        }

        private void checkBoxShowPreview_CheckedChanged(object sender, EventArgs e)
        {
            config.ConfigHolder.Set(Configuration.ShowPreview, showPreviewCheckBox.Checked);
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            OptionsForm opt = new OptionsForm(this);
            opt.CheckUpdatesClicked += new EventHandler<EventArgs>(opt_CheckUpdatesClicked);
            opt.CultureChanged += new VoidStringDelegate(opt_CultureChanged);
            locMgr.Add(opt);
            opt.ShowModal(config.ConfigHolder);
            locMgr.Remove(opt);
        }

        private void opt_CultureChanged(string culture)
        {
            (locMgr as DVDVideoSoft.AppFx.LocalizationManager).ApplyCultureForAll(culture);
            (locMgr as DVDVideoSoft.AppFx.LocalizationManager).WriteCurrentCulture();
        }

        private void opt_CheckUpdatesClicked(object sender, EventArgs e)
        {
            CheckForUpdates(false);
        }

        private void buttonAdvanced_Click(object sender, EventArgs e)
        {
            advancedPanel.Location = splitContainer1.Location;
            advancedPanel.Size = splitContainer1.Size;

            splitContainer1.Visible = false;
            advancedPanel.Visible = true;

            Cursor = Cursors.WaitCursor;
            sProcessor.MergeTransparentPreview(new StereoProcessor.ImageSettings(), transparentGDViewer.Width, transparentGDViewer.Height);
            RefreshImages();
            Cursor = Cursors.Default;

            buttonBasic.Location = advancedButton.Location;
            buttonBasic.Visible = true;
            advancedButton.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            transparentGDViewer.DisplayFromGdPictureImage(sProcessor.MergeTransparentPreview(sProcessor.ImageSetts, transparentGDViewer.Width, transparentGDViewer.Height));

            Cursor = Cursors.Default;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            numericUpDownVert.Value = numericUpDownHorz.Value = numericUpDownAngle.Value = 0;

            transparentGDViewer.DisplayFromGdPictureImage(sProcessor.MergeTransparentPreview(sProcessor.ImageSetts, transparentGDViewer.Width, transparentGDViewer.Height));

            Cursor = Cursors.Default;
        }

        private void numericUpDownVert_ValueChanged(object sender, EventArgs e)
        {
            if (leftImageRadio.Checked)
            {
                sProcessor.ImageSetts.LeftOffset = new Point((int)numericUpDownHorz.Value, (int)numericUpDownVert.Value);
            }

            if (rightImageRadio.Checked)
            {
                sProcessor.ImageSetts.RightOffset = new Point((int)numericUpDownHorz.Value, (int)numericUpDownVert.Value);
            }
        }

        private void numericUpDownHorz_ValueChanged(object sender, EventArgs e)
        {
            if (notAssign) return;

            if (leftImageRadio.Checked)
            {
                sProcessor.ImageSetts.LeftOffset = new Point((int)numericUpDownHorz.Value, (int)numericUpDownVert.Value);
            }

            if (rightImageRadio.Checked)
            {
                sProcessor.ImageSetts.RightOffset = new Point((int)numericUpDownHorz.Value, (int)numericUpDownVert.Value);
            }
        }

        private void radioButtonLeft_CheckedChanged(object sender, EventArgs e)
        {
            if (leftImageRadio.Checked)
            {
                notAssign = true;
                numericUpDownHorz.Value = sProcessor.ImageSetts.LeftOffset.X;
                numericUpDownVert.Value = sProcessor.ImageSetts.LeftOffset.Y;
                numericUpDownAngle.Value = sProcessor.ImageSetts.LeftAngle;
                notAssign = false;
            }
        }

        private void radioButtonRight_CheckedChanged(object sender, EventArgs e)
        {
            if (rightImageRadio.Checked)
            {
                notAssign = true;
                numericUpDownHorz.Value = sProcessor.ImageSetts.RightOffset.X;
                numericUpDownVert.Value = sProcessor.ImageSetts.RightOffset.Y;
                numericUpDownAngle.Value = sProcessor.ImageSetts.RightAngle;
                notAssign = false;
            }
        }

        private void buttonBasic_Click(object sender, EventArgs e)
        {
            splitContainer1.Location = advancedPanel.Location;
            splitContainer1.Size = advancedPanel.Size;

            splitContainer1.Visible = true;
            advancedPanel.Visible = false;

            RefreshImages();

            advancedButton.Location = buttonBasic.Location;
            buttonBasic.Visible = false;
            advancedButton.Visible = true;
        }

        private void numericUpDownAngle_ValueChanged(object sender, EventArgs e)
        {
            if (notAssign) return;

            if (leftImageRadio.Checked)
            {
                sProcessor.ImageSetts.LeftAngle = (int)numericUpDownAngle.Value;
            }

            if (rightImageRadio.Checked)
            {
                sProcessor.ImageSetts.RightAngle = (int)numericUpDownAngle.Value;
            }
        }

        private void textboxOutputFolder_TextChanged(object sender, EventArgs e)
        {
            config.ConfigHolder.Set(Configuration.OutputFile, outputFolderText.Text);
        }

        private void checkSingleImage_CheckedChanged(object sender, EventArgs e)
        {
            config.ConfigHolder.Set(Configuration.UseSingleImage, useSingleSourceCheckBox.Checked);
            swapButton.Enabled = !useSingleSourceCheckBox.Checked;
            openRightButton.Enabled = !useSingleSourceCheckBox.Checked;
            sProcessor.UseSingle = useSingleSourceCheckBox.Checked;
            splitContainer1.Panel2Collapsed = useSingleSourceCheckBox.Checked;
            convertButton.Enabled = sProcessor.Ready();
        }
        
        #endregion
    }
}
