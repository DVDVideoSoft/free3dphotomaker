using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using DVDVideoSoft.Utils;
using DVDVideoSoft.AppFxApi;
using DVDVideoSoft.AppFx;

namespace DVDVideoSoft.DialogForms
{
    public partial class OptionsFormBase : LocalizableForm
    {
        public event VoidStringDelegate      CultureChanged;
        public event VoidStringDelegate      AppThemeChanged;
        public event EventHandler<EventArgs> CheckUpdatesClicked;

        protected Form parent;
        protected bool m_bCenterToParent = false;
        protected PropsProvider provider;
        
        protected ComboBox cmbLanguageRef;
        protected ComboBox cmbThemeRef;

        protected int m_nPrevLanguageIndex;

        public bool CenterToParentForm
        {
            get { return m_bCenterToParent; }
            set { m_bCenterToParent = value; }
        }

        public OptionsFormBase()
        {
            InitializeComponent();

            m_bCenterToParent = true;
        }

        public OptionsFormBase(Form parent)
        {
            InitializeComponent();

            m_bCenterToParent = true;
            this.parent = parent;
        }

        private void OptionsFormBase_Load(object sender, EventArgs e)
        {
            if (this.parent != null && this.CenterToParentForm)
                WindowUtils.CenterToParent(this, this.parent);
        }

        protected void Init()
        {
            this.cmbLanguageRef = GetLanguageCombo();
            this.cmbThemeRef = GetThemeCombo();
        }

        protected virtual ComboBox GetLanguageCombo()
        {
            return null;
        }

        protected virtual ComboBox GetThemeCombo()
        {
            return null;
        }

        protected virtual ComboBox GetLogLevelCombo()
        {
            return null;
        }

        private new DialogResult ShowDialog(IWin32Window parent) // use ShowModal instead
        {
            return base.ShowDialog(parent);
        }

        protected void OnCheckUpdatesClick()
        {
            if (this.CheckUpdatesClicked != null)
                this.CheckUpdatesClicked(this, null);
        }

        public DialogResult ShowModal(PropsProvider provider)
        {
            this.provider = provider;

            FillLanguageCombo();
            LoadThemes();
            SetControlValues();

            m_nPrevLanguageIndex = this.cmbLanguageRef.SelectedIndex;
            //TODO: this.currentVersionLabel.Text = "Current version: " + this.provider.GetString(OptionsFormBase.PN.VersionStr.ToString());

            DialogResult ret = System.Windows.Forms.DialogResult.Cancel;
            do {
                ret = this.ShowDialog(this.parent);

                if (ret == DialogResult.OK) {
                    RememberControlValues(/*ref bCanClose*/);
                    //if (!bCanClose)
                    //    continue;
                } else if (ret == DialogResult.Cancel) {
                    OnFormClosedByCancel();
                    this.cmbLanguageRef.SelectedIndex = m_nPrevLanguageIndex;
                }
            }
            while (false);

            return ret;
        }

        public virtual void BeforeSetControlValues()
        {
        }

        protected virtual void DoSetControlValues()
        {
        }

        protected void SetControlValues()
        {
            BeforeSetControlValues();
            DoSetControlValues();
        }

        /// <summary>
        /// TODO: consider removing this function
        /// </summary>
        public void UpdateControlValues()
        {
            SetControlValues();
        }

        public virtual void RememberControlValues()
        {
            //bCanClose = true;
        }

        public virtual void OnFormClosedByCancel()
        {
        }

        public override void SetCulture(string culture)
        {
        }

        protected virtual string GetCultureFromCombo()
        {
            if (this.cmbLanguageRef == null || this.cmbLanguageRef.SelectedItem == null)
                return "en-US";
            return ("en-US");
        }

        protected virtual int GetLogLevelFromCombo()
        {
            return GetLogginLevels().Length - this.GetLogLevelCombo().SelectedIndex;
        }

        protected virtual int GetComboIndexByLogLevel(int nLevel)
        {
            if (nLevel == 0)
                return -1;
            return GetLogginLevels().Length - nLevel;
        }

        protected virtual string GetThemeFromCombo()
        {
            if (this.cmbThemeRef == null || this.cmbThemeRef.Items.Count == 0 || this.cmbThemeRef.SelectedItem == null)
                return "";
            return this.cmbThemeRef.SelectedItem.ToString();
        }

        protected void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CultureChanged != null)
                CultureChanged(this.GetCultureFromCombo());
        }

        protected void cmbTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AppThemeChanged != null)
                AppThemeChanged(this.GetThemeFromCombo());
        }

        protected virtual void FillLanguageCombo()
        {
            if (this.cmbLanguageRef == null)
                return;
            this.cmbLanguageRef.Items.Clear();
            IList<string> availableLanguages = LocalizationManager.GetAvailableLanguages(null);

            for (int i = 0; i < availableLanguages.Count; i++)
                this.cmbLanguageRef.Items.Add(new ComboBox ());

            this.cmbLanguageRef.SelectedIndexChanged -= this.cmbLanguage_SelectedIndexChanged;
            this.cmbLanguageRef.SelectedIndex = availableLanguages.IndexOf(Thread.CurrentThread.CurrentUICulture.Name);
            this.cmbLanguageRef.SelectedIndexChanged += this.cmbLanguage_SelectedIndexChanged;
        }

        public void LoadThemes()
        {
            if (this.cmbThemeRef == null || this.provider == null)
                return;

            this.cmbThemeRef.Items.Clear();

            List<string> availableThemes = (List<string>)this.provider[Defs.PN.AvailableThemes.ToString()];

            if (availableThemes.Count == 0)
                return;

            for (int i = 0; i < availableThemes.Count; i++)
                this.cmbThemeRef.Items.Add(availableThemes[i]);

            this.cmbThemeRef.SelectedIndexChanged -= this.cmbTheme_SelectedIndexChanged;
            this.cmbThemeRef.SelectedIndex = this.cmbThemeRef.FindString((string)this.provider[Defs.PN.CurrentTheme.ToString()]);
            this.cmbThemeRef.SelectedIndexChanged += this.cmbTheme_SelectedIndexChanged;
        }

        protected virtual string[] GetLogginLevels()
        {
            return new string[] { "Trace", "Debug", "Info", "Warning", "Error", "Fatal" };
        }
    }
}
