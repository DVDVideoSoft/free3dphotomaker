using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DVDVideoSoft.AppFx;
using DVDVideoSoft.Resources;

namespace DVDVideoSoft.DialogForms
{
    public partial class BrowseForFilesAndFolders : LocalizableForm
    {
        

        private List<TreeNode> checkedNodes;
        private int checkedNodesCount = 0;
        private IPreviewCtrl previewWnd=null;
        private bool reopeningNode = false;
        
        public List<ExplorerTreeViewWnd.FilterItem> Filter
        {
            get
            {
                return explorerTreeViewWnd.Filter;
            }
            set
            {
                explorerTreeViewWnd.Filter = value;
                comboBoxFilter.Items.Clear();
                for (int i = 0; i < explorerTreeViewWnd.Filter.Count; i++)
                    comboBoxFilter.Items.Add(explorerTreeViewWnd.Filter[i].ComboText);
                if (explorerTreeViewWnd.Filter.Count > 0)
                {
                    comboBoxFilter.SelectedIndex = 0;
                    //explorerTreeViewWnd.CurFilter = 0;
                }
            }

        }

        public int CheckedNodesCount
        {
            get { return checkedNodesCount; }
            set { checkedNodesCount = value; }
        }

        public List<TreeNode> CheckedNodes
        {
            get
            {
                return checkedNodes;
            }
        }

        public BrowseForFilesAndFolders(IPreviewCtrl preview)
        {
            InitializeComponent();
            ShowInTaskbar = false;
            checkedNodes = new List<TreeNode>();
            previewWnd = preview;
            if (previewWnd != null)
            {
                Control pCtrl = previewWnd.GetControl();
                if (pCtrl != null)
                {
                    panelPreview.Controls.Add(pCtrl);
                    UpdatePreview();
                }
            }
            else
            {
                textBoxSearchPattern.Size = new Size(panelPreview.Right - textBoxSearchPattern.Left, textBoxSearchPattern.Height);
                panelPreview.Visible = false;
            }
            explorerTreeViewWnd.AfterSelect += new TreeViewEventHandler(explorerTreeViewWnd_AfterSelect);
        }

        private void UpdatePreview()
        {
            if (previewWnd == null) return;

            Control pCtrl = previewWnd.GetControl();
            if (pCtrl != null)
            {
                Rectangle newRC = previewWnd.AdjustBounds(panelPreview, panelPreview.ClientRectangle);
                pCtrl.Location = newRC.Location;
                pCtrl.Size = newRC.Size;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdatePreview();
        }

        private TreeNode lastSelectedNode;

        void explorerTreeViewWnd_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (previewWnd != null && (e.Node.Tag as ShellItem) != null && !reopeningNode)
            {
                timerPreview.Stop();
                lastSelectedNode = e.Node;
                timerPreview.Start();
            }
                
        }

        private void BrowseForFilesAndFolders_Load(object sender, EventArgs e)
        {
            SetCulture("");
            explorerTreeViewWnd.IncludeSubFolders = checkBoxInclSubfolders.Checked;
            explorerTreeViewWnd.LoadRootNodes();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            explorerTreeViewWnd.IncludeSubFolders = checkBoxInclSubfolders.Checked;
        }

        public override void SetCulture(string culture)
        {
            Text = CommonData.BrowseForFilesAndFolders_Title;
            checkBoxInclSubfolders.Text = CommonData.BrowseForFilesAndFolders_InclSub;
            labelName.Text = CommonData.Name;
            buttonCancel.Text = CommonData.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            List<TreeNode> tmp = new List<TreeNode>();
 
            for (int i = 0; i < explorerTreeViewWnd.SelectedNodes.Count; i++)
                tmp.Add(explorerTreeViewWnd.SelectedNodes[i]);
            int j = 0;
            while (j < tmp.Count)
            {
                if (!FilterSelectedNode(tmp[j], tmp))
                    j++;
            }

            for (int i = 0; i < tmp.Count; i++)
            {
                explorerTreeViewWnd.OpenNodeRec(tmp[i]);
            }

            j = 0;
            while (j < tmp.Count)
                if (!FilterEmptyNodes(tmp[j]))
                    j++;
                else
                    tmp.Remove(tmp[j]);

            for (int i = 0; i < tmp.Count; i++)
                checkedNodes.Add(tmp[i]);

            CountCheckedFileNodes();

           

            Cursor = Cursors.Default;
        }

        private bool FilterSelectedNode(TreeNode node, List<TreeNode> src)
        {
            TreeNode p = node.Parent;
            if (src.Contains(p))
            {
                src.Remove(node);
                return true;
            }

            return false;
        }


        private void CountCheckedFileNodes()
        {
            for (int i = 0; i < checkedNodes.Count; i++)
                CountCheckedFileNodesRec(checkedNodes[i]);
        }

        private void CountCheckedFileNodesRec(TreeNode node)
        {
            checkedNodesCount++;
            for (int i = 0; i < node.Nodes.Count; i++)
                CountCheckedFileNodesRec(node.Nodes[i]);
        }

        private bool FilterEmptyNodes(TreeNode node)
        {
            int i = 0;
            while(i < node.Nodes.Count)
            {
                if (node.Nodes[i].Nodes.Count == 0 && node.Nodes[i].Tag as ShellItem!=null && (node.Nodes[i].Tag as ShellItem).IsFolder)
                    node.Nodes.Remove(node.Nodes[i]);
                else
                    if (!FilterEmptyNodes(node.Nodes[i]))
                        i++;
            }

            if (node.Nodes.Count == 0 && node.Tag as ShellItem!=null && (node.Tag as ShellItem).IsFolder)
                return true;

            return false;
        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            reopeningNode = true;
            explorerTreeViewWnd.CurFilter = comboBoxFilter.SelectedIndex;
            if (explorerTreeViewWnd.Nodes.Count > 0)
            {
                explorerTreeViewWnd.SuspendLayout();
                for (int i = 0; i < explorerTreeViewWnd.Nodes[0].Nodes.Count; i++)
                    explorerTreeViewWnd.ReOpenNode(explorerTreeViewWnd.Nodes[0].Nodes[i]);
                explorerTreeViewWnd.ResumeLayout();
            }
            reopeningNode = false;
        }

        private void BrowseForFilesAndFolders_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (previewWnd != null)
                previewWnd.ShutDown();
        }

        private void textBoxSearchPattern_TextChanged(object sender, EventArgs e)
        {
            timerFilter.Stop();
            timerFilter.Start();
        }

        private void timerPreview_Tick(object sender, EventArgs e)
        {
            previewWnd.ShowPreview((lastSelectedNode.Tag as ShellItem).Path);
            timerPreview.Stop();
        }

        private void timerFilter_Tick(object sender, EventArgs e)
        {
            if (textBoxSearchPattern.Text.Length > 0)
                explorerTreeViewWnd.SearchPattern = textBoxSearchPattern.Text;
            else
                explorerTreeViewWnd.SearchPattern = "*";

            reopeningNode = true;
            if (explorerTreeViewWnd.Nodes.Count > 0)
            {
                explorerTreeViewWnd.SuspendLayout();
                for (int i = 0; i < explorerTreeViewWnd.Nodes[0].Nodes.Count; i++)
                    explorerTreeViewWnd.ReOpenNode(explorerTreeViewWnd.Nodes[0].Nodes[i]);
                explorerTreeViewWnd.ResumeLayout();
            }
            reopeningNode = false;
            timerFilter.Stop();
        }

    }
}
