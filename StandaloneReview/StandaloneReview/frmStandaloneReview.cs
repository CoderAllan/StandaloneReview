using System;
using System.IO;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using @STextEditor = ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Src.Document.FoldingStrategy;

namespace StandaloneReview
{
    public partial class frmStandaloneReview : Form
    {
        private ISystemIO _systemIO;

        public frmStandaloneReview()
        {
            _systemIO = new SystemIO();

            InitializeComponent();
            textEditorControlEx1.Document.FoldingManager.FoldingStrategy = new XmlFoldingStrategy();

            textEditorControlEx1.ActiveTextAreaControl.TextArea.MouseClick += ShowSelectionLength;
            textEditorControlEx1.ActiveTextAreaControl.TextArea.MouseDoubleClick += ShowSelectionLength;
            textEditorControlEx1.ActiveTextAreaControl.TextArea.MouseUp += ShowSelectionLength;
            textEditorControlEx1.ActiveTextAreaControl.TextArea.KeyUp += ShowSelectionLength;

        }

        private delegate void SetTextEditorControlTextCallback(@STextEditor.TextEditorControlEx control, string text);
        public void SetTextEditorControlText(@STextEditor.TextEditorControlEx editControl, string text)
        {
            if (editControl.InvokeRequired)
            {
                var callback = new SetTextEditorControlTextCallback(SetTextEditorControlText);
                Invoke(callback, new object[] { editControl, text });
            }
            else
            {
                editControl.Document.TextContent = text;
                editControl.Document.FoldingManager.UpdateFoldings(null, null);
                editControl.Refresh();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                var text = _systemIO.FileReadAllText(filename);
                SetTextEditorControlText(textEditorControlEx1, text);
                SetSyntaxHighlighting(_systemIO.PathGetExtension(filename));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //LineSegment lineSegment = textEditorControlEx1.Document.GetLineSegmentForOffset(textEditorControlEx1.ActiveTextAreaControl.Caret.Offset);
            //string line = textEditorControlEx1.Document.GetText(lineSegment);
        }

        private void SetSyntaxHighlighting(string fileType)
        {
            switch (fileType)
            {
                case ".xml":
                case ".wsdl":
                case ".xsd":
                case ".csproj":
                case ".sln":
                case ".config":
                    textEditorControlEx1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML"); ;
                    break;
                case ".html":
                    textEditorControlEx1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("HTML"); ;
                    break;
                case ".aspx":
                    textEditorControlEx1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("ASPX"); ;
                    break;
                case ".cs":
                    textEditorControlEx1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#"); ;
                    break;
                case ".sql":
                    textEditorControlEx1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("SQL"); ;
                    break;
            }
            textEditorControlEx1.Refresh();
        }

        private void ShowSelectionLength(object sender, KeyEventArgs e)
        {
            SetStatusText(textEditorControlEx1);
        }

        private void ShowSelectionLength(object sender, MouseEventArgs e)
        {
            SetStatusText(textEditorControlEx1);
        }
        private void SetStatusText(@STextEditor.TextEditorControlEx editor)
        {
            SetLabelStatusText(statusStrip1, toolStripStatusLblLine, string.Format("Ln: {0}", editor.ActiveTextAreaControl.Caret.Position.Line + 1));
            SetLabelStatusText(statusStrip1, toolStripStatusLblColumn, string.Format("Col: {0}", editor.ActiveTextAreaControl.Caret.Position.Column));
            if (editor.ActiveTextAreaControl.SelectionManager.SelectionCollection.Count > 0)
            {
                var selection = editor.ActiveTextAreaControl.SelectionManager.SelectionCollection[0];
                if (selection != null && selection.Length > 0)
                {
                    SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionLength, string.Format("Selection length: {0}", selection.Length));
                    SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionStart, string.Format("Selection start: (Ln: {0}, Col: {1})", selection.StartPosition.Line + 1, selection.StartPosition.Column));
                    SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionEnd, string.Format("Selection end: (Ln: {0}, Col: {1})", selection.EndPosition.Line + 1, selection.EndPosition.Column));
                }
            }
            else
            {
                SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionLength, "");
            }
        }

        private delegate void SetLabelStatusTextCallback(StatusStrip toolStrip, ToolStripStatusLabel label, string text);
        private void SetLabelStatusText(StatusStrip toolStrip, ToolStripStatusLabel label, string text)
        {
            if (toolStrip.InvokeRequired)
            {
                var callback = new SetLabelStatusTextCallback(SetLabelStatusText);
                Invoke(callback, new object[] { toolStrip, label, text });
            }
            else
            {
                label.Text = text;
                label.BorderStyle = Border3DStyle.Flat;
                label.BorderSides = !String.IsNullOrEmpty(label.Text) ? ToolStripStatusLabelBorderSides.Left : ToolStripStatusLabelBorderSides.None;
                toolStrip.Refresh();
            }
        }
    }
}
