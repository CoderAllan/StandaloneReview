namespace StandaloneReview
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Windows.Forms;
    using Microsoft.VisualBasic.PowerPacks;
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
        private readonly Dictionary<int, RectangleShape> _navigatorCommentRectangles = new Dictionary<int, RectangleShape>();
        private RectangleShape _navigatorCurrentLineRectangle;

        public FrmStandaloneReview()
        {
            _appState = ApplicationState.ReadApplicationState();
            _systemIO = new SystemIO();
            _baseFormPresenter = new BaseFormPresenter(this);
            _frmStandaloneReviewPresenter = new FrmStandaloneReviewPresenter(this);

            RuntimeLocalizer.ChangeCulture(_appState.ApplicationLocale);

            InitializeComponent();

            var eventArgs = new BaseFormEventArgs
            {
                Height = _appState.FrmStandaloneReviewHeight,
                Width = _appState.FrmStandaloneReviewWidth,
                Location = new Point(_appState.FrmStandaloneReviewPosX, _appState.FrmStandaloneReviewPosY)
            };
            DoFormLoad(this, eventArgs);

            textEditorControlEx1.Document.FoldingManager.FoldingStrategy = new XmlFoldingStrategy();
            navigatorCanvas.Top = textEditorControlEx1.Top;
            navigatorCanvas.Height = textEditorControlEx1.Height - 2;

            textEditorControlEx1.ActiveTextAreaControl.TextArea.MouseClick += ShowSelectionLength;
            textEditorControlEx1.ActiveTextAreaControl.TextArea.MouseDoubleClick += ShowSelectionLength;
            textEditorControlEx1.ActiveTextAreaControl.TextArea.MouseUp += ShowSelectionLength;
            textEditorControlEx1.ActiveTextAreaControl.TextArea.KeyUp += ShowSelectionLength;

            EnableDisableMenuToolstripItems();
        }

        public ISystemIO SystemIO
        {
            get { return _systemIO; }
        }

        public ApplicationState AppState
        {
            get { return _appState; }
        }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<LoadEventArgs> BtnLoadClick;
        public event EventHandler<EventArgs> BtnNewClick;
        public event EventHandler<SaveEventArgs> BtnSaveClick;
        public event EventHandler<CancelEventArgs> BtnExitClick;
        public event EventHandler<CommitCommentEventArgs> CommitComment;
        public event EventHandler<ReviewCommentEventArgs> SetReviewComment;
        public event EventHandler<CaretPositionEventArgs> DeleteComment;
        public event EventHandler<CaretPositionEventArgs> EditComment;
        public event EventHandler<CaretPositionEventArgs> ContextMenuStripOpening;

        public void SetFrmStandaloneReviewTitle(string text)
        {
            Text = text;
        }

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
            switch (fileType.ToLower())
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
            int commentPosition = 0;
            if (_appState.CurrentReview.ReviewedFiles.Count > 0)
            {
                commentPosition = _appState.CurrentReview.ReviewedFiles[_appState.CurrentReviewedFile.Filename].Comments.Max(p => p.Position) + 1;
            }
            var reviewCommentEventArgs = new ReviewCommentEventArgs
            {
                Position = commentPosition,
                Line = editor.ActiveTextAreaControl.Caret.Position.Line + 1,
                LineText = editor.ActiveTextAreaControl.Document.GetText(editor.ActiveTextAreaControl.Document.GetLineSegment(editor.ActiveTextAreaControl.Caret.Position.Line)),
            };
            SetLabelStatusText(statusStrip1, toolStripStatusLblLine, string.Format(Resources.StatusStripLineNumber, reviewCommentEventArgs.Line));
            SetLabelStatusText(statusStrip1, toolStripStatusLblColumn, string.Format(Resources.StatusStripColumnNumber, editor.ActiveTextAreaControl.Caret.Position.Column));
            if (editor.ActiveTextAreaControl.SelectionManager.SelectionCollection.Count > 0)
            {
                var selection = editor.ActiveTextAreaControl.SelectionManager.SelectionCollection[0];
                if (selection != null && selection.Length > 0)
                {
                    SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionLength, string.Format(Resources.StatusStripSelectionLength, selection.Length));
                    reviewCommentEventArgs.SelectionStartLine = selection.StartPosition.Line + 1;
                    reviewCommentEventArgs.SelectionStartColumn = selection.StartPosition.Column;
                    SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionStart, string.Format(Resources.StatusStripSelectionStart, reviewCommentEventArgs.SelectionStartLine, reviewCommentEventArgs.SelectionStartColumn));
                    reviewCommentEventArgs.SelectionEndLine = selection.EndPosition.Line + 1;
                    reviewCommentEventArgs.SelectionEndColumn = selection.EndPosition.Column;
                    SetLabelStatusText(statusStrip1, toolStripStatusLblSelectionEnd, string.Format(Resources.StatusStripSelectionEnd, reviewCommentEventArgs.SelectionEndLine, reviewCommentEventArgs.SelectionEndColumn));
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
            Close();
        }

        public void CloseApplication()
        {
            _appState.PersistFrmStandaloneReview(this);
            ApplicationState.WriteApplicationState(_appState);
        }

        private void FrmStandaloneReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BtnExitClick != null)
            {
                BtnExitClick(sender, e);
            }
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

        private RectangleShape CreateRectangleShape(int line, Color color)
        {
            double correctionFactor = 1;
            if (navigatorCanvas.Height - navigatorCanvas.Top < textEditorControlEx1.Document.TotalNumberOfLines)
            {
                correctionFactor = (navigatorCanvas.Height - navigatorCanvas.Top)/(double) textEditorControlEx1.Document.TotalNumberOfLines;
            }
            var rectangleShape = new RectangleShape
            {
                Height = 2,
                Top = navigatorCanvas.Top + (int)(line * correctionFactor),
                Width = navigatorCanvas.Width - 2,
                Left = Width - 42, // This calculation should be: 'Left = navigatorCanvas.Left + 1', but the navigatorCanvas.Left property never changes when resizing the form. So we have to use the magic number 42 to make the placement of the navigator lines work.
                FillStyle = FillStyle.Solid,
                FillColor = color,
                BorderStyle = DashStyle.Solid,
                BorderColor = color,
                Tag = line,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
            };
            rectangleShape.Click += rectangle_Click;
            navigatorCanvas.SendToBack();
            rectangleShape.BringToFront();
            return rectangleShape;
        }

        public void rectangle_Click(object sender, EventArgs e)
        {
            var rectangle = (RectangleShape) sender;
            var line = (int) rectangle.Tag;
            var editor = GetActiveTextEditor();
            editor.ActiveTextAreaControl.Caret.Line = line - 1;
        }

        public void AddNavigatorCurrentLineMarker(int line)
        {
            if (_navigatorCurrentLineRectangle != null)
            {
                shapeContainer1.Shapes.Remove(_navigatorCurrentLineRectangle);
            }
            _navigatorCurrentLineRectangle = CreateRectangleShape(line, Color.DarkBlue);
            shapeContainer1.Shapes.Add(_navigatorCurrentLineRectangle);
        }

        public void AddNavigatorCommentMarker(int line)
        {
            if (!_navigatorCommentRectangles.ContainsKey(line))
            {
                var rectangle = CreateRectangleShape(line, Color.Goldenrod);
                _navigatorCommentRectangles.Add(line, rectangle);
                shapeContainer1.Shapes.Add(rectangle);
            }
        }

        public void AddGreyedArea()
        {
            var rectangleShape = new RectangleShape
            {
                Height = navigatorCanvas.Height - textEditorControlEx1.Document.TotalNumberOfLines,
                Top = navigatorCanvas.Top + textEditorControlEx1.Document.TotalNumberOfLines + 2,
                Width = navigatorCanvas.Width - 2,
                Left = navigatorCanvas.Left + 1,
                FillStyle = FillStyle.Solid,
                FillColor = Color.LightGray,
                BorderStyle = DashStyle.Solid,
                BorderColor = Color.LightGray,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
            };
            shapeContainer1.Shapes.Add(rectangleShape);
        }

        public void RemoveAllNavigatorShapes()
        {
            shapeContainer1.Shapes.Clear();
        }

        public void RemoveNavigatorCommentMarker(int line)
        {
            if (_navigatorCommentRectangles.ContainsKey(line))
            {
                shapeContainer1.Shapes.Remove(_navigatorCommentRectangles[line]);
                _navigatorCommentRectangles.Remove(line);
            }
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

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmOptions = new FrmOptions(_appState);
            if (frmOptions.ShowDialog() == DialogResult.OK)
            {
                RuntimeLocalizer.ChangeCulture(_appState.ApplicationLocale);
                Refresh();
            }
        }
    }
}
