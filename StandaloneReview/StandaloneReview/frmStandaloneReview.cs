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
    using Properties;
    
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
        public ApplicationState AppState { get { return _appState; } }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<LoadEventArgs> BtnLoadClick;
        public event EventHandler<EventArgs> BtnNewClick;
        public event EventHandler<SaveEventArgs> BtnSaveClick;
        public event EventHandler<EventArgs> CommitComment;
        public event EventHandler<ReviewCommentEventArgs> SetReviewComment;

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

        public void SetSyntaxHighlighting(string fileType)
        {
            switch (fileType)
            {
                case ".xml":
                case ".wsdl":
                case ".xsd":
                case ".xsl":
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
            var reviewCommentEventArgs = new ReviewCommentEventArgs
            {
                Line = editor.ActiveTextAreaControl.Caret.Position.Line + 1,
                LineText = editor.ActiveTextAreaControl.Document.GetText(editor.ActiveTextAreaControl.Document.GetLineSegment(editor.ActiveTextAreaControl.Caret.Position.Line))
            };
            SetLabelStatusText(statusStrip1, toolStripStatusLblLine, string.Format("Ln: {0}", reviewCommentEventArgs.Line));
            SetLabelStatusText(statusStrip1, toolStripStatusLblColumn, string.Format("Col: {0}", editor.ActiveTextAreaControl.Caret.Position.Column));
            if (editor.ActiveTextAreaControl.SelectionManager.SelectionCollection.Count > 0)
            {
                var selection = editor.ActiveTextAreaControl.SelectionManager.SelectionCollection[0];
                if (selection != null && selection.Length > 0)
                {
                    SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionLength, string.Format(Resources.StatusStrip1SelectionLength, selection.Length));
                    reviewCommentEventArgs.SelectionStartLine = selection.StartPosition.Line + 1;
                    reviewCommentEventArgs.SelectionStartColumn = selection.StartPosition.Column;
                    SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionStart, string.Format(Resources.StatusStrip1SelectionStart, reviewCommentEventArgs.SelectionStartLine, reviewCommentEventArgs.SelectionStartColumn));
                    reviewCommentEventArgs.SelectionEndLine = selection.EndPosition.Line + 1;
                    reviewCommentEventArgs.SelectionEndColumn = selection.EndPosition.Column;
                    SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionEnd, string.Format(Resources.StatusStrip1SelectionEnd, reviewCommentEventArgs.SelectionEndLine, reviewCommentEventArgs.SelectionEndColumn));
                    reviewCommentEventArgs.SelectedText = selection.SelectedText;
                }
            }
            else
            {
                SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionLength, "");
                SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionStart, "");
                SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionEnd, "");
            }
            if (SetReviewComment != null)
            {
                SetReviewComment(null, reviewCommentEventArgs);
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

        private void insertCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInsertCommentForm(sender, e);
        }

        private void insertCommentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowInsertCommentForm(sender, e);
        }

        private void ShowInsertCommentForm(object sender, EventArgs e)
        {
            var frmInsertComment = new FrmInsertComment(_appState)
            {
                Visible = false
            };
            if (frmInsertComment.ShowDialog() == DialogResult.OK && CommitComment != null)
            {
                CommitComment(sender, e);
            }
        }

        public int GetTextOffset(int column, int line)
        {
            return textEditorControlEx1.ActiveTextAreaControl.Document.PositionToOffset(new TextLocation(column, line));
        }

        public void AddMarker(int offset, int length)
        {
            var marker = new TextMarker(offset, length, TextMarkerType.SolidBlock, Color.Gold);
            textEditorControlEx1.Document.MarkerStrategy.AddMarker(marker);
            textEditorControlEx1.Refresh();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void saveReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BtnSaveClick != null)
            {
                saveFileDialog1.Filter = @"Review (*.review)|*.review";
                saveFileDialog1.FileName = "Review-" + _appState.CurrentReview.ReviewTime.ToString("yyyy-MM-dd");
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var args = new SaveEventArgs
                    {
                        Filename = saveFileDialog1.FileName
                    };
                    BtnSaveClick(sender, args);
                }
            }
        }

        private void nytReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BtnNewClick != null)
            {
                if (_appState.CurrentReview.Saved)
                {
                    BtnNewClick(sender, e);
                }
                else
                {
                    if (MessageBox.Show(Resources.FrmStandaloneReview_nytReviewToolStripMenuItem_Click_Unsaved_Changes, Resources.FrmStandaloneReview_nytReviewToolStripMenuItem_Click_Unsaved_Changes_Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        BtnNewClick(sender, e);
                    }
                }
            }
        }

        public void ResetTextEditor()
        {
            textEditorControlEx1.Document.MarkerStrategy.RemoveAll(marker => true);
            textEditorControlEx1.Document.TextContent = "";
            textEditorControlEx1.Refresh();
        }
    }
}
