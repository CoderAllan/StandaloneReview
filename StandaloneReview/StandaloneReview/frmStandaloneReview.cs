namespace StandaloneReview
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using ICSharpCode.TextEditor.Document;
    using ICSharpCode.TextEditor;
    using ICSharpCode.TextEditor.Src.Document.FoldingStrategy;
    
    using Contracts;
    using Model;
    using Views;
    using Presenters;
    
    public partial class FrmStandaloneReview : Form, IBaseForm, IFrmStandaloneReview
    {
        private readonly ApplicationState _appState;
        private readonly ISystemIO _systemIO;
        private readonly BaseFormPresenter _baseFormPresenter;
        private readonly FrmStandaloneReviewPresenter _frmStandaloneReviewPresenter;

        public FrmStandaloneReview()
        {
            _systemIO = new SystemIO();
            _baseFormPresenter = new BaseFormPresenter(this);
            _frmStandaloneReviewPresenter = new FrmStandaloneReviewPresenter(this);
            _appState = ApplicationState.ReadApplicationState();

            InitializeComponent();

            var eventArgs = new BaseFormEventArgs
            {
                Height = _appState.FrmStandaloneReviewHeight,
                Width = _appState.FrmStandaloneReviewWidth,
                Location = new Point(_appState.FrmStandaloneReviewPosX, _appState.FrmStandaloneReviewPosY)
            };
            DoFormLoad(this, eventArgs);

            textEditorControlEx1.Document.FoldingManager.FoldingStrategy = new XmlFoldingStrategy();

            textEditorControlEx1.ActiveTextAreaControl.TextArea.MouseClick += ShowSelectionLength;
            textEditorControlEx1.ActiveTextAreaControl.TextArea.MouseDoubleClick += ShowSelectionLength;
            textEditorControlEx1.ActiveTextAreaControl.TextArea.MouseUp += ShowSelectionLength;
            textEditorControlEx1.ActiveTextAreaControl.TextArea.KeyUp += ShowSelectionLength;

        }

        public ISystemIO SystemIO { get { return _systemIO; } }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<LoadEventArgs> BtnLoadClick;

        public void SetTextEditorControlText(string textEditorControlName, string text)
        {
            var editControl = (TextEditorControlEx) Controls[textEditorControlName];
            if (editControl != null)
            {
                editControl.Document.TextContent = text;
                editControl.Document.FoldingManager.UpdateFoldings(null, null);
                editControl.Refresh();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (BtnLoadClick != null)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var loadEventArgs = new LoadEventArgs
                        {
                            Filename = openFileDialog1.FileName,
                            EditorControlName = textEditorControlEx1.Name
                        };
                    BtnLoadClick(sender, loadEventArgs);
                }
            }
        }

        private void BtnSaveReview_Click(object sender, EventArgs e)
        {
            //LineSegment lineSegment = textEditorControlEx1.Document.GetLineSegmentForOffset(textEditorControlEx1.ActiveTextAreaControl.Caret.Offset);
            //string line = textEditorControlEx1.Document.GetText(lineSegment);
        }

        public void SetSyntaxHighlighting(string fileType)
        {
            switch (fileType)
            {
                case ".xml":
                case ".wsdl":
                case ".xsd":
                case ".csproj":
                case ".sln":
                case ".config":
                    textEditorControlEx1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
                    break;
                case ".html":
                    textEditorControlEx1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("HTML");
                    break;
                case ".aspx":
                    textEditorControlEx1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("ASPX");
                    break;
                case ".cs":
                    textEditorControlEx1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
                    break;
                case ".sql":
                    textEditorControlEx1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
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
        private void SetStatusText(TextEditorControlEx editor)
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
                SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionStart, "");
                SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionEnd, "");
            }
        }

        private void SetLabelStatusText(StatusStrip toolStrip, ToolStripStatusLabel label, string text)
        {
            label.Text = text;
            label.BorderStyle = Border3DStyle.Flat;
            label.BorderSides = !String.IsNullOrEmpty(label.Text) ? ToolStripStatusLabelBorderSides.Left : ToolStripStatusLabelBorderSides.None;
            toolStrip.Refresh();
        }

        private void FrmStandaloneReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            var frmStandaloneReview = (FrmStandaloneReview) sender;
            _appState.PersistFrmStandaloneReview(frmStandaloneReview);
            ApplicationState.WriteApplicationState(_appState);
        }
    }
}
