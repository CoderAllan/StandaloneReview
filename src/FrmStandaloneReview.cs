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

            EnableDisableMenuToolstripItems();
        }

        public ISystemIO SystemIO { get { return _systemIO; } }
        public ApplicationState AppState { get { return _appState; } }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<LoadEventArgs> BtnLoadClick;
        public event EventHandler<EventArgs> BtnNewClick;
        public event EventHandler<SaveEventArgs> BtnSaveClick;
        public event EventHandler<EventArgs> BtnExitClick;
        public event EventHandler<CommitCommentEventArgs> CommitComment;
        public event EventHandler<ReviewCommentEventArgs> SetReviewComment;
        public event EventHandler<CaretPositionEventArgs> DeleteComment;
        public event EventHandler<CaretPositionEventArgs> EditComment;
        public event EventHandler<CaretPositionEventArgs> ContextMenuStripOpening;

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
            SetStatusText(GetActiveTextEditor());
        }
        private void ShowSelectionLength(object sender, MouseEventArgs e)
        {
            SetStatusText(GetActiveTextEditor());
        }
        private TextEditorControlEx GetActiveTextEditor()
        {
            return textEditorControlEx1;
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BtnExitClick != null)
            {
                BtnExitClick(sender, e);
            }
        }

        public void CloseApplication()
        {
            Close();
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
            ShowInsertCommentForm(false);
        }

        public void ShowInsertCommentForm(bool editCurrentWorkingComment)
        {
            var frmInsertComment = new FrmInsertComment(_appState, editCurrentWorkingComment)
            {
                Visible = false
            };
            if (frmInsertComment.ShowDialog() == DialogResult.OK && CommitComment != null)
            {
                var commitCommentEventArgs = new CommitCommentEventArgs
                {
                    EditCurrentWorkingComment = editCurrentWorkingComment
                };
                CommitComment(null, commitCommentEventArgs);
            }
            else
            {
                _appState.WorkingComment = new ReviewComment();
            }
        }

        private void editCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditComment != null)
            {
                var editor = GetActiveTextEditor();
                var editCommentEventArgs = new CaretPositionEventArgs
                {
                    Line = editor.ActiveTextAreaControl.Caret.Position.Line + 1,
                    Column = editor.ActiveTextAreaControl.Caret.Position.Column
                };
                EditComment(sender, editCommentEventArgs);
            }
        }

        public int GetTextOffset(int column, int line)
        {
            var editor = GetActiveTextEditor();
            return editor.ActiveTextAreaControl.Document.PositionToOffset(new TextLocation(column, line));
        }

        public void AddMarker(int offset, int length, string tooltipText)
        {
            var marker = new TextMarker(offset, length, TextMarkerType.SolidBlock, Color.Gold)
            {
                ToolTip = tooltipText
            };
            var editor = GetActiveTextEditor();
            editor.Document.MarkerStrategy.AddMarker(marker);
            editor.Refresh();
        }

        public void SetMarkerTooltip(string tooltipText)
        {
            var editor = GetActiveTextEditor();
            var textLocation = new TextLocation(editor.ActiveTextAreaControl.Caret.Position.Column, editor.ActiveTextAreaControl.Caret.Position.Line);
            foreach (var textMarker in editor.Document.MarkerStrategy.GetMarkers(textLocation))
            {
                textMarker.ToolTip = tooltipText;
            }
            editor.Refresh();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BtnLoadClick != null)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var editor = GetActiveTextEditor();
                    var loadEventArgs = new LoadEventArgs
                    {
                        Filename = openFileDialog1.FileName,
                        EditorControlName = editor.Name
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

        private void newReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BtnNewClick != null)
            {
                BtnNewClick(sender, e);
            }
        }

        public bool MessageBoxUnsavedCommentsWarningOkCancel()
        {
            return MessageBox.Show(Resources.FrmStandaloneReview_nytReviewToolStripMenuItem_Click_Unsaved_Changes, Resources.FrmStandaloneReview_nytReviewToolStripMenuItem_Click_Unsaved_Changes_Caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        public void ResetTextEditor()
        {
            var editor = GetActiveTextEditor();
            editor.Document.MarkerStrategy.RemoveAll(marker => true);
            editor.Document.TextContent = "";
            editor.Refresh();
        }

        public void EnableDisableMenuToolstripItems()
        {
            if (_appState != null && _appState.CurrentReviewedFile != null)
            {
                insertCommentToolStripMenuItem1.Enabled = true;
                insertCommentToolStripMenuItem.Enabled = true;
                saveReviewToolStripMenuItem.Enabled = true;
                nytReviewToolStripMenuItem.Enabled = true;
                previewToolStripMenuItem.Enabled = true;
            }
            else
            {
                insertCommentToolStripMenuItem1.Enabled = false;
                insertCommentToolStripMenuItem.Enabled = false;
                saveReviewToolStripMenuItem.Enabled = false;
                nytReviewToolStripMenuItem.Enabled = false;
                deleteCommentToolStripMenuItem.Enabled = false;
                editCommentToolStripMenuItem.Enabled = false;
                previewToolStripMenuItem.Enabled = false;
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ContextMenuStripOpening != null)
            {
                var editor = GetActiveTextEditor();
                var shouldEnableDisableEventArgs = new CaretPositionEventArgs
                {
                    Line = editor.ActiveTextAreaControl.Caret.Position.Line + 1,
                    Column = editor.ActiveTextAreaControl.Caret.Position.Column
                };
                ContextMenuStripOpening(sender, shouldEnableDisableEventArgs);
            }
        }

        public void EnableDisableContextMenuToolsstripItems(bool menuToolStripEnabled)
        {
            deleteCommentToolStripMenuItem.Enabled = menuToolStripEnabled;
            editCommentToolStripMenuItem.Enabled = menuToolStripEnabled;
        }

        private void deleteCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DeleteComment != null)
            {
                var editor = GetActiveTextEditor();
                var deleteCommentEventArgs = new CaretPositionEventArgs
                {
                    Line = editor.ActiveTextAreaControl.Caret.Position.Line + 1,
                    Column = editor.ActiveTextAreaControl.Caret.Position.Column
                };
                DeleteComment(sender, deleteCommentEventArgs);
                var textLocation = new TextLocation(editor.ActiveTextAreaControl.Caret.Position.Column, editor.ActiveTextAreaControl.Caret.Position.Line);
                foreach (var textMarker in editor.Document.MarkerStrategy.GetMarkers(textLocation))
                {
                    editor.Document.MarkerStrategy.RemoveMarker(textMarker);
                }
                editor.Refresh();
            }
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmPreview = new FrmPreview(_appState);
            frmPreview.ShowDialog();
        }
    }
}
