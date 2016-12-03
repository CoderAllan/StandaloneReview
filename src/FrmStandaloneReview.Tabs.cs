namespace StandaloneReview
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using ICSharpCode.TextEditor;
    using ICSharpCode.TextEditor.Src.Document.FoldingStrategy;

    public partial class FrmStandaloneReview 
    {
        private readonly Dictionary<string,string> _filenameTabPageRelation = new Dictionary<string, string>();

        public string AddNewTab(string filename)
        {
            int newTabPageNumber = NextTabPageNumber();
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
            _filenameTabPageRelation.Add(filename, tab.Name);
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

        private int NextTabPageNumber()
        {
            int maxNumber = 0;
            foreach (TabPage tab in tabControl1.TabPages)
            {
                string tabPageNumber = tab.Name.Replace("tabPage", "");
                int currentNumber = int.Parse(tabPageNumber);
                if (currentNumber > maxNumber)
                {
                    maxNumber = currentNumber;
                }
            }
            return maxNumber + 1;
        }

        public void RemoveAllOpenTabs()
        {
            tabControl1.TabPages.Clear();
        }

        public void SelectOpenTab(string filename)
        {
            if (_filenameTabPageRelation.ContainsKey(filename))
            {
                tabControl1.SelectTab(_filenameTabPageRelation[filename]);
            }
        }

        public bool IsTabOpen(string filename)
        {
            return _filenameTabPageRelation.ContainsKey(filename);
        }

        public void CloseTab(string filename)
        {
            if (_filenameTabPageRelation.ContainsKey(filename))
            {
                tabControl1.TabPages.RemoveByKey(_filenameTabPageRelation[filename]);
                _filenameTabPageRelation.Remove(filename);
            }
        }
    }
}
