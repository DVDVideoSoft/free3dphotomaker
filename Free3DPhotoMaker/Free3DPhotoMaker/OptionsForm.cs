using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DVDVideoSoft.AppFxApi;

namespace Free3DPhotoMaker
{
    public partial class OptionsForm : DVDVideoSoft.DialogForms.OptionsFormBase
    {
        public OptionsForm(Form parent): base(parent)
        {
            InitializeComponent();
            base.Init();
        }

        protected override ComboBox GetLanguageCombo()
        {
            return cmbLanguage;
        }

        protected override void DoSetControlValues()
        {
            useAllCPUsCheckBox.Checked = provider.GetBool(Configuration.UseMultiCore);
        }

        public override void RememberControlValues()
        {
            provider.Set(Configuration.UseMultiCore, useAllCPUsCheckBox.Checked);
            provider.Set(Defs.PN.CurrentCulture.ToString(), GetCultureFromCombo());
        }

        public override void SetCulture(string culture)
        {
            base.SetCulture(culture);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OnCheckUpdatesClick();
        }
    }
}
