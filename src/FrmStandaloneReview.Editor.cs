namespace StandaloneReview
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ICSharpCode.TextEditor;
    using ICSharpCode.TextEditor.Document;
    
    using Properties;
    using Views;


    public partial class FrmStandaloneReview
    {
        public void SetTextEditorControlText(string textEditorControlName, string text)
        {
            var editControl = GetActiveTextEditor(textEditorControlName);
            if (editControl != null)
            {
                editControl.Document.TextContent = text;
                editControl.Document.FoldingManager.UpdateFoldings(null, null);
                editControl.Refresh();
            }
        }

        public void SetSyntaxHighlighting(string fileType)
        {
            var editControl = GetActiveTextEditor();
            switch (fileType.ToLower())
            {
                case ".xml":
                case ".wsdl":
                case ".xsd":
                case ".xsl":
                case ".csproj":
                case ".sln":
                case ".config":
                    editControl.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
                    break;
                case ".html":
                    editControl.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("HTML");
                    break;
                case ".aspx":
                    editControl.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("ASPX");
                    break;
                case ".cs":
                    editControl.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
                    break;
                case ".sql":
                    editControl.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
                    break;
            }
            editControl.Refresh();
        }

        private void ShowSelectionLength(object sender, KeyEventArgs e)
        {
            SetStatusText(GetActiveTextEditor());
        }

        private void ShowSelectionLength(object sender, MouseEventArgs e)
        {
            SetStatusText(GetActiveTextEditor());
        }

        private TextEditorControlEx GetActiveTextEditor(string textEditorControlName = null)
        {
            string editorControlName = textEditorControlName;
            if (textEditorControlName == null)
            {
                if (tabControl1.SelectedTab == null)
                {
                    return null;
                }
                editorControlName = tabControl1.SelectedTab.Name.Replace("tabPage", "TextEditorControlEx");
            }
            var editor = tabControl1.Controls.Find(editorControlName, true);
            if (editor.Length != 1)
            {
                return null;
            }
            return (TextEditorControlEx)editor[0];
        }

        private void SetStatusText(TextEditorControlEx editor)
        {
            int commentPosition = 0;
            if (_appState.CurrentReview.ReviewedFiles.Count > 0 &&
                _appState.CurrentReview.ReviewedFiles.ContainsKey(_appState.CurrentReviewedFile.Filename) &&
                _appState.CurrentReview.ReviewedFiles[_appState.CurrentReviewedFile.Filename].Comments.Count > 0)
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

    }
}
