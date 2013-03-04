using System;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace DVDVideoSoft.DialogForms
{
    public class ExplorerTreeViewWnd : TreeView
    {
        public class FilterItem
        {
            private string text;
            private List<string> extensions;

            public string Text
            {
                get { return text; }
                set { text = value; }
            }

            public List<string> Extensions
            {
                get { return extensions; }
                set { extensions = value; }
            }

            public string ComboText
            {
                get
                {
                    return text + " (*." + string.Join(",*.", extensions.ToArray()) + ")";
                }
            }

            public FilterItem(string caption, params string[] exts)
            {
                text = caption;
                extensions = new List<string>();
                for (int i = 0; i < exts.Length; i++)
                    extensions.Add(exts[i]);
            }
        }

        private string searchPattern = "*";

        protected System.Collections.Generic.List<FilterItem> _Filter;
        protected int curFilter = -1;

        protected System.Collections.Generic.List<TreeNode> m_coll;
        protected TreeNode m_lastNode, m_firstNode;

        private bool includeSubFolders = true;
        private bool showFiles = true;
        private bool multiSelect = false;

        public string SearchPattern
        {
            get { return searchPattern; }
            set { searchPattern = value; }
        }

        [Browsable(false)]
        public int CurFilter
        {
            get { return curFilter; }
            set { curFilter = value; }
        }

        public bool MultiSelect
        {
            get { return multiSelect; }
            set { multiSelect = value; }
        }

        public bool ShowFiles
        {
            get { return showFiles; }
            set { showFiles = value; }
        }

        public bool IncludeSubFolders
        {
            get { return includeSubFolders; }
            set { includeSubFolders = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Collections.Generic.List<TreeNode> SelectedNodes
        {
            get
            {
                return m_coll;
            }
            set
            {
                removePaintFromNodes();
                m_coll.Clear();
                m_coll = value;
                paintSelectedNodes();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Collections.Generic.List<FilterItem> Filter
        {
            get
            {
                return _Filter;
            }

            set
            {
                _Filter = value;
            }
        }
        
        public ExplorerTreeViewWnd()
        {
            m_coll = new System.Collections.Generic.List<TreeNode>();
            _Filter = new System.Collections.Generic.List<FilterItem>();
        }

        /// <summary>
        /// Loads the root TreeView nodes.
        /// </summary>
        public void LoadRootNodes()
        {
            SystemImageList.SetTVImageList(Handle);
            // Create the root shell item.
            
            ShellItem m_shDesktop = new ShellItem();

            // Create the root node.
            TreeNode tvwRoot = new TreeNode();
            tvwRoot.Text = m_shDesktop.DisplayName;
            tvwRoot.ImageIndex = m_shDesktop.IconIndex;
            tvwRoot.SelectedImageIndex = m_shDesktop.IconIndex;
            tvwRoot.Tag = m_shDesktop;

            // Now we need to add any children to the root node.
            /*ArrayList arrChildren = m_shDesktop.GetSubFolders();
            foreach (ShellItem shChild in arrChildren)
            {
                TreeNode tvwChild = new TreeNode();
                tvwChild.Text = shChild.DisplayName;
                tvwChild.ImageIndex = shChild.IconIndex;
                tvwChild.SelectedImageIndex = shChild.IconIndex;
                tvwChild.Tag = shChild;

                // If this is a folder item and has children then add a place holder node.
                if ((shChild.IsFolder && shChild.HasSubFolder) || (showFiles && shChild.HasFiles))
                    tvwChild.Nodes.Add("PH");
                tvwRoot.Nodes.Add(tvwChild);
            }*/

            tvwRoot.Nodes.Add("PH");
            OpenNode(tvwRoot);

            // Add the root node to the tree.
            Nodes.Clear();
            Nodes.Add(tvwRoot);
            tvwRoot.Expand();
        }

        public void ReOpenNode(TreeNode node)
        {
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "PH")
                return;

            int i = 0;
            while(i < node.Nodes.Count)
            {
                if (!(node.Nodes[i].Tag as ShellItem).IsFolder)
                    node.Nodes.RemoveAt(i);
                else
                {
                    ReOpenNode(node.Nodes[i]);
                    i++;
                }
            }
            ShellItem shNode = node.Tag as ShellItem;
            if (showFiles && shNode.HasFiles)
            {
                ArrayList arrSubFiles = shNode.GetFiles();
                foreach (ShellItem shChild in arrSubFiles)
                {
                    if (curFilter >= 0 && curFilter < _Filter.Count)
                    {
                        string path = shChild.GetPath();

                        for (i = 0; i < _Filter[curFilter].Extensions.Count; i++)
                        {
                            DirectoryInfo dir = new DirectoryInfo(shNode.Path);
                            FileInfo[] files = dir.GetFiles(searchPattern + "." + _Filter[curFilter].Extensions[i]);
                            for(int j=0;j<files.Length;j++)
                                if (files[j].FullName == path)
                                {
                                    TreeNode tvwChild = new TreeNode();
                                    tvwChild.Text = shChild.DisplayName;
                                    tvwChild.ImageIndex = shChild.IconIndex;
                                    tvwChild.SelectedImageIndex = shChild.IconIndex;
                                    tvwChild.Tag = shChild;
                                    tvwChild.Checked = includeSubFolders & node.Checked;

                                    node.Nodes.Add(tvwChild);
                                    break;
                                }
                        }
                    }
                    if (_Filter.Count == 0)
                    {
                        TreeNode tvwChild = new TreeNode();
                        tvwChild.Text = shChild.DisplayName;
                        tvwChild.ImageIndex = shChild.IconIndex;
                        tvwChild.SelectedImageIndex = shChild.IconIndex;
                        tvwChild.Tag = shChild;
                        tvwChild.Checked = includeSubFolders & node.Checked;

                        node.Nodes.Add(tvwChild);
                    }
                }
            }
        }

        private void OpenNode(TreeNode node)
        {
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "PH")
                node.Nodes.Clear();
            else
                return;

            ShellItem shNode = (ShellItem)node.Tag;
            
            if (shNode.IsFolder && !shNode.HasSubFolder && !shNode.HasFiles) return;

            Cursor = Cursors.WaitCursor;
            if (shNode.HasSubFolder)
            {
                ArrayList arrSub = shNode.GetSubFolders();
                foreach (ShellItem shChild in arrSub)
                {
                    TreeNode tvwChild = new TreeNode();
                    tvwChild.Text = shChild.DisplayName;
                    tvwChild.ImageIndex = shChild.IconIndex;
                    tvwChild.SelectedImageIndex = shChild.IconIndex;
                    tvwChild.Tag = shChild;
                    tvwChild.Checked = includeSubFolders & node.Checked;

                    bool hasFiles=true;
                    if (shChild.Path.Length > 0 && Filter.Count>0)
                    {
                        if (curFilter >= 0 && curFilter < _Filter.Count)
                        {
                            try
                            {
                                System.IO.DirectoryInfo inf = new System.IO.DirectoryInfo(shChild.Path);
                                int count = 0;
                                for (int i = 0; i < Filter[curFilter].Extensions.Count; i++)
                                {
                                    try
                                    {
                                        count += inf.GetFiles(searchPattern + "." + Filter[curFilter].Extensions[i]).Length;
                                    }
                                    catch
                                    {
                                    }
                                }
                                hasFiles = count > 0;
                            }
                            catch
                            {
                            }
                        }
                    }

                    // If this is a folder item and has children then add a place holder node.
                    if ((shChild.IsFolder && shChild.HasSubFolder) || (showFiles && hasFiles))
                        tvwChild.Nodes.Add("PH");
                    node.Nodes.Add(tvwChild);
                }
            }

            if (showFiles && shNode.HasFiles)
            {
                ArrayList arrSubFiles = shNode.GetFiles();
                foreach (ShellItem shChild in arrSubFiles)
                {
                    if (curFilter >= 0 && curFilter<_Filter.Count)
                    {
                        for (int i = 0; i < _Filter[curFilter].Extensions.Count; i++)
                        {
                            try
                            {
                                DirectoryInfo dir = new DirectoryInfo(shNode.Path);
                                FileInfo[] files = dir.GetFiles(searchPattern + "." + _Filter[curFilter].Extensions[i]);
                                for (int j = 0; j < files.Length; j++)
                                    if (files[j].FullName == shChild.Path)
                                    {
                                        TreeNode tvwChild = new TreeNode();
                                        tvwChild.Text = shChild.DisplayName;
                                        tvwChild.ImageIndex = shChild.IconIndex;
                                        tvwChild.SelectedImageIndex = shChild.IconIndex;
                                        tvwChild.Tag = shChild;
                                        tvwChild.Checked = includeSubFolders & node.Checked;

                                        node.Nodes.Add(tvwChild);
                                        break;
                                    }
                            }
                            catch { }
                        }
                    }
                    if (_Filter.Count == 0)
                    {
                        TreeNode tvwChild = new TreeNode();
                        tvwChild.Text = shChild.DisplayName;
                        tvwChild.ImageIndex = shChild.IconIndex;
                        tvwChild.SelectedImageIndex = shChild.IconIndex;
                        tvwChild.Tag = shChild;
                        tvwChild.Checked = includeSubFolders & node.Checked;

                        node.Nodes.Add(tvwChild);
                    }
                }
            }
            Cursor = Cursors.Default;
        }

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            OpenNode(e.Node);
            base.OnBeforeExpand(e);
        }

        private bool AlreadyChecked(TreeNode parent, TreeNode node, ref bool res)
        {
            if (parent != node)
            {
                for (int i = 0; i < parent.Nodes.Count; i++)
                    if (AlreadyChecked(parent.Nodes[i], node, ref res) && parent.Nodes[i].Checked)
                        res = true;
                return false;
            }
            else
                if (parent.Checked)
                    return true;
                else
                    return false;
        }

        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            if (includeSubFolders && e.Action != TreeViewAction.Unknown)
            {
                CheckNode(e.Node, e.Node.Checked);
            }

            base.OnAfterCheck(e);
        }

        private void CheckNode(TreeNode node,bool Checked)
        {
            Cursor = Cursors.WaitCursor;
            CheckNodeRec(node, Checked);
            Cursor = Cursors.Default;
        }

        public void CheckNodeRec(TreeNode node, bool nodeChecked)
        {
            OpenNode(node);
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].Checked = nodeChecked;
                CheckNodeRec(node.Nodes[i], nodeChecked);
            }
        }

        public void OpenNodeRec(TreeNode node)
        {
            OpenNode(node);
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                OpenNodeRec(node.Nodes[i]);
            }
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            /*if (e.Node.Bounds.Contains(e.Location))
            {
                e.Node.Checked = !e.Node.Checked;
                CheckNode(e.Node, e.Node.Checked);
            }*/
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space)
                for (int i = 0; i < SelectedNodes.Count; i++)
                    if (SelectedNodes[i] != SelectedNode)
                    {
                        SelectedNodes[i].Checked = !SelectedNodes[i].Checked;
                        CheckNode(SelectedNodes[i], !SelectedNodes[i].Checked);
                    }
        }
        // Triggers
        //
        // (overriden method, and base class called to ensure events are triggered)
        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);
            if (!multiSelect)
                return;

            bool bControl = (ModifierKeys == Keys.Control);
            bool bShift = (ModifierKeys == Keys.Shift);

            // selecting twice the node while pressing CTRL ?
            if (bControl && m_coll.Contains(e.Node))
            {
                // unselect it (let framework know we don't want selection this time)
                e.Cancel = true;

                // update nodes
                removePaintFromNodes();
                m_coll.Remove(e.Node);
                paintSelectedNodes();

                //if (e.Node.Parent != null)
                //{
                //    e.Node.Checked = false;
                //    CheckNode(e.Node, e.Node.Checked);
                //}
                return;
            }
            
            m_lastNode = e.Node;
            if (!bShift) m_firstNode = e.Node; // store begin of shift sequence
        }

        private void EnqueueChildNodes(TreeNode parent, Queue<TreeNode> queue)
        {
            for (int i = 0; i < parent.Nodes.Count; i++)
            {
                if (!m_coll.Contains(parent.Nodes[i])) // new node ?
                    queue.Enqueue(parent.Nodes[i]);
            }

            for (int i = 0; i < parent.Nodes.Count; i++)
            {
                EnqueueChildNodes(parent.Nodes[i], queue);
            }

        }

        private void AddNodeToCollection(TreeNode node)
        {
            if (node.Parent == null) return;

            if(!m_coll.Contains(node))
                m_coll.Add(node);
            for (int i = 0; i < node.Nodes.Count; i++)
                AddNodeToCollection(node.Nodes[i]);
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);
            if (!multiSelect)
                return;

            bool bControl = (ModifierKeys == Keys.Control);
            bool bShift = (ModifierKeys == Keys.Shift);

            if (bControl)
            {
                if (!m_coll.Contains(e.Node)) // new node ?
                {
                    //m_coll.Add(e.Node);
                    AddNodeToCollection(e.Node);
                }
                else  // not new, remove it from the collection
                {
                    removePaintFromNodes();
                    m_coll.Remove(e.Node);
                }

                paintSelectedNodes();
            }
            else
            {
                // SHIFT is pressed
                if (bShift)
                {
                    System.Collections.Generic.Queue<TreeNode> myQueue = new System.Collections.Generic.Queue<TreeNode>();

                    TreeNode uppernode = m_firstNode;
                    TreeNode bottomnode = e.Node;
                    // case 1 : begin and end nodes are parent
                    bool bParent = isParent(m_firstNode, e.Node); // is m_firstNode parent (direct or not) of e.Node
                    if (!bParent)
                    {
                        bParent = isParent(bottomnode, uppernode);
                        if (bParent) // swap nodes
                        {
                            TreeNode t = uppernode;
                            uppernode = bottomnode;
                            bottomnode = t;
                        }
                    }
                    if (bParent)
                    {
                        TreeNode n = bottomnode;
                        while (n != uppernode.Parent)
                        {
                            if (!m_coll.Contains(n)) // new node ?
                                myQueue.Enqueue(n);

                            n = n.Parent;
                        }

                        
                    }
                    // case 2 : nor the begin nor the end node are descendant one another
                    else
                    {
                        if ((uppernode.Parent == null && bottomnode.Parent == null) || (uppernode.Parent != null && uppernode.Parent.Nodes.Contains(bottomnode))) // are they siblings ?
                        {
                            int nIndexUpper = uppernode.Index;
                            int nIndexBottom = bottomnode.Index;
                            if (nIndexBottom < nIndexUpper) // reversed?
                            {
                                TreeNode t = uppernode;
                                uppernode = bottomnode;
                                bottomnode = t;
                                nIndexUpper = uppernode.Index;
                                nIndexBottom = bottomnode.Index;
                            }

                            TreeNode n = uppernode;
                            while (nIndexUpper <= nIndexBottom)
                            {
                                if (!m_coll.Contains(n)) // new node ?
                                    myQueue.Enqueue(n);

                                n = n.NextNode;

                                nIndexUpper++;
                            } // end while

                        }
                        else
                        {
                            if (!m_coll.Contains(uppernode)) AddNodeToCollection(uppernode);//myQueue.Enqueue(uppernode);
                            if (!m_coll.Contains(bottomnode)) AddNodeToCollection(bottomnode); //myQueue.Enqueue(bottomnode);
                        }
                    }

                    
                    m_coll.AddRange(myQueue);
                    AddNodeToCollection(uppernode);
                    paintSelectedNodes();
                    m_firstNode = e.Node; // let us chain several SHIFTs if we like it
                } // end if m_bShift
                else
                {
                    // in the case of a simple click, just add this item
                    if (m_coll != null && m_coll.Count > 0)
                    {
                        removePaintFromNodes();
                        m_coll.Clear();
                    }
                    //m_coll.Add(e.Node);
                    AddNodeToCollection(e.Node);
                    paintSelectedNodes();
                }
            }
        }

        protected bool isParent(TreeNode parentNode, TreeNode childNode)
        {
            if (parentNode == childNode)
                return true;

            TreeNode n = childNode;
            bool bFound = false;
            while (!bFound && n != null)
            {
                n = n.Parent;
                bFound = (n == parentNode);
            }
            return bFound;
        }

        protected void paintSelectedNodes()
        {
            foreach (TreeNode n in m_coll)
            {
                n.BackColor = SystemColors.Highlight;
                n.ForeColor = SystemColors.HighlightText;
            }
        }

        protected void removePaintFromNodes()
        {
            if (m_coll.Count == 0) return;

            TreeNode n0 = m_coll[0];
            Color back = n0.TreeView.BackColor;
            Color fore = n0.TreeView.ForeColor;

            foreach (TreeNode n in m_coll)
            {
                n.BackColor = back;
                n.ForeColor = fore;
            }

        }

    }
}
