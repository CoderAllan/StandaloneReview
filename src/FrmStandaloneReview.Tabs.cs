namespace StandaloneReview
{
    using System.Windows.Forms;
    using ICSharpCode.TextEditor;
    using ICSharpCode.TextEditor.Src.Document.FoldingStrategy;

    public partial class FrmStandaloneReview 
    {

        public string AddNewTab(string filename, int newTabPageNumber)
        {
            var tab = new TabPage
            {
                Name = string.Format("tabPage{0}", newTabPageNumber),
                Text = _systemIO.PathGetFilename(filename),
                ToolTipText = filename,
                Tag = filename
            };
            var newtextEditorControl = new TextEditorControlEx
            {
                Name = string.Format("TextEditorControlEx{0}", newTabPageNumber),
                Dock = DockStyle.Fill
            };
            newtextEditorControl.Document.FoldingManager.FoldingStrategy = new XmlFoldingStrategy();
            newtextEditorControl.ActiveTextAreaControl.TextArea.MouseClick += ShowSelectionLength;
            newtextEditorControl.ActiveTextAreaControl.TextArea.MouseDoubleClick += ShowSelectionLength;
            newtextEditorControl.ActiveTextAreaControl.TextArea.MouseUp += ShowSelectionLength;
            newtextEditorControl.ActiveTextAreaControl.TextArea.KeyUp += ShowSelectionLength;
            newtextEditorControl.ContextMenuStrip = contextMenuComment;

            tab.Controls.Add(newtextEditorControl);
            tabControl1.Controls.Add(tab);
            tabControl1.SelectTab(tab);
            return newtextEditorControl.Name;
        }

        public void RemoveAllOpenTabs()
        {
            tabControl1.TabPages.Clear();
        }

        public void SelectOpenTab(string filename)
        {
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                if (tabPage.Tag.Equals(filename))
                {
                    tabControl1.SelectTab(tabPage);
                    break;
                }
            }
        }
    }
}
