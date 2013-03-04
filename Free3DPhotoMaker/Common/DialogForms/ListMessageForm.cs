using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DVDVideoSoft.DialogForms
{
    public partial class ListMessageForm : Form
    {
        public ListMessageForm( string FormTitle, string Comment, object[] ListItems )
        {
            InitializeComponent();
            buttonOK.Text = Resources.CommonData.OK;
            this.Text = FormTitle;
            this.labelComment.Text = Comment;
            listBoxMessages.Items.AddRange( ListItems );
        }

        private void ListMessageForm_Load( object sender, EventArgs e )
        {

        }

        private void buttonOK_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.OK;
        }
    }
}
