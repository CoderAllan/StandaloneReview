namespace StandaloneReview.Presenters
{
    using System;
    using System.Collections.Generic;

    using Model;
    using Views;

    public class FrmStandaloneReviewPresenter
    {
        private readonly IFrmStandaloneReview _view;

        public FrmStandaloneReviewPresenter(IFrmStandaloneReview view)
        {
            _view = view;

            Initialize();
        }

        public void Initialize()
        {
            _view.BtnLoadClick += DoLoadClick;
            _view.BtnNewClick += DoNewClick;
            _view.BtnSaveClick += DoSaveClick;
            _view.BtnExitClick += DoExitClick;
            _view.CommitComment += DoCommitComment;
            _view.SetReviewComment += DoSetReviewComment;
            _view.DeleteComment += DoDeleteComment;
            _view.EditComment += DoEditComment;
            _view.ContextMenuStripOpening += DoContextMenuStripOpening;

            DoSetFrmStandaloneReviewTitle();
        }

        private void DoSetFrmStandaloneReviewTitle()
        {
            const string mainFormTitle = "Standalone Review";
            string isReviewSaved = _view.AppState.CurrentReview.Saved ? "" : " *";
            _view.SetFrmStandaloneReviewTitle(mainFormTitle + isReviewSaved);
        }

        private void DoLoadClick(object sender, LoadEventArgs args)
        {
            var text = _view.SystemIO.FileReadAllText(args.Filename);
            _view.SetTextEditorControlText(args.EditorControlName, text);
            _view.SetSyntaxHighlighting(_view.SystemIO.PathGetExtension(args.Filename));
            _view.AppState.CurrentReviewedFile = new ReviewedFile
                {
                    Filename = args.Filename,
                    Comments = new List<ReviewComment>()
                };
            _view.EnableDisableMenuToolstripItems();
        }

        private void DoExitClick(object sender, EventArgs e)
        {
            if (_view.AppState.CurrentReview.Saved)
            {
                _view.CloseApplication();
            }
            else
            {
                if (_view.AppState.CurrentReviewedFile != null &&
                    _view.AppState.CurrentReviewedFile.Comments != null &&
                    _view.AppState.CurrentReviewedFile.Comments.Count > 0)
                {
                    if (_view.MessageBoxUnsavedCommentsWarningOkCancel())
                    {
                        _view.CloseApplication();
                    }
                }
                else
                {
                    _view.CloseApplication();
                }
            }            
        }

        private void DoSaveClick(object sender, SaveEventArgs e)
        {
            string review = _view.AppState.CurrentReview.ToString();
            _view.SystemIO.WriteAllText(e.Filename, review);
            _view.AppState.CurrentReview.Saved = true;
            DoSetFrmStandaloneReviewTitle();
        }

        private void DoNewClick(object sender, EventArgs e)
        {
            if (_view.AppState.CurrentReview.Saved)
            {
                ResetCurrentReviewWorkingComment();
            }
            else
            {
                if (_view.AppState.CurrentReviewedFile != null &&
                    _view.AppState.CurrentReviewedFile.Comments != null &&
                    _view.AppState.CurrentReviewedFile.Comments.Count > 0)
                {
                    if (_view.MessageBoxUnsavedCommentsWarningOkCancel())
                    {
                        ResetCurrentReviewWorkingComment();
                    }
                }
                else
                {
                    ResetCurrentReviewWorkingComment();
                }
            }
        }

        private void ResetCurrentReviewWorkingComment()
        {
            _view.AppState.WorkingComment = new ReviewComment();
            _view.AppState.CurrentReviewedFile = null;
            _view.AppState.CurrentReview = new Review
            {
                ReviewTime = DateTime.Now,
                ReviewedFiles = new Dictionary<string, ReviewedFile>(),
                Saved = true
            };
            _view.ResetTextEditor();
            _view.EnableDisableMenuToolstripItems();
            DoSetFrmStandaloneReviewTitle();
        }

        private void DoCommitComment(object sender, CommitCommentEventArgs e)
        {
            if (!_view.AppState.CurrentReview.ReviewedFiles.ContainsKey(_view.AppState.CurrentReviewedFile.Filename))
            {
                _view.AppState.CurrentReview.ReviewedFiles.Add(_view.AppState.CurrentReviewedFile.Filename, _view.AppState.CurrentReviewedFile);
            }
            if (!e.EditCurrentWorkingComment)
            {
                _view.AppState.CurrentReview.ReviewedFiles[_view.AppState.CurrentReviewedFile.Filename].Comments.Add(_view.AppState.WorkingComment);
            }
            if (!e.EditCurrentWorkingComment)
            {
                int offset;
                int length;
                if (_view.AppState.WorkingComment.SelectionStartLine > 0)
                {
                    offset = _view.GetTextOffset(_view.AppState.WorkingComment.SelectionStartColumn, _view.AppState.WorkingComment.SelectionStartLine - 1);
                    length = _view.AppState.WorkingComment.SelectedText.Length;
                }
                else
                {
                    offset = _view.GetTextOffset(1, _view.AppState.WorkingComment.Line - 1);
                    length = _view.AppState.WorkingComment.LineText.Length;
                }
                _view.AddMarker(offset, length, _view.AppState.WorkingComment.Comment);
            }
            else
            {
                _view.SetMarkerTooltip(_view.AppState.WorkingComment.Comment);
            }
            _view.AppState.WorkingComment = new ReviewComment();
            _view.AppState.CurrentReview.Saved = false;
            DoSetFrmStandaloneReviewTitle();
        }

        private void DoSetReviewComment(object sender, ReviewCommentEventArgs e)
        {
            if (_view.AppState.WorkingComment == null)
            {
                _view.AppState.WorkingComment = new ReviewComment();
            }
            _view.AppState.WorkingComment.Line = e.Line;
            _view.AppState.WorkingComment.LineText = e.LineText;
            _view.AppState.WorkingComment.SelectionStartLine = e.SelectionStartLine;
            _view.AppState.WorkingComment.SelectionStartColumn = e.SelectionStartColumn;
            _view.AppState.WorkingComment.SelectionEndLine = e.SelectionEndLine;
            _view.AppState.WorkingComment.SelectionEndColumn = e.SelectionEndColumn;
            _view.AppState.WorkingComment.SelectedText = e.SelectedText;
        }

        private void DoContextMenuStripOpening(object sender, CaretPositionEventArgs e)
        {
            var commentAtCaretPosition = GetCommentAtCaretPosition(e.Line, e.Column);
            _view.EnableDisableContextMenuToolsstripItems(commentAtCaretPosition != null);
        }

        private void DoDeleteComment(object sender, CaretPositionEventArgs e)
        {
            var commentAtCaretPosition = GetCommentAtCaretPosition(e.Line, e.Column);
            if (commentAtCaretPosition != null)
            {
                _view.AppState.CurrentReviewedFile.Comments.Remove(commentAtCaretPosition);
                _view.AppState.WorkingComment = new ReviewComment();
                _view.AppState.CurrentReview.Saved = false;
            }
            DoSetFrmStandaloneReviewTitle();
        }

        private void DoEditComment(object sender, CaretPositionEventArgs e)
        {
            var commentAtCaretPosition = GetCommentAtCaretPosition(e.Line, e.Column);
            if (commentAtCaretPosition != null)
            {
                _view.AppState.WorkingComment = commentAtCaretPosition;
                _view.ShowInsertCommentForm(true);
            }
        }

        private ReviewComment GetCommentAtCaretPosition(int line, int column)
        {
            ReviewComment commentAtCaretPosition = null;
            if (_view.AppState.CurrentReviewedFile != null)
            {
                foreach (var reviewComment in _view.AppState.CurrentReviewedFile.Comments)
                {
                    if (reviewComment.SelectionStartLine > 0) // Its only when SelectionStartLine > 0 that its a ReviewComment on a text-selection
                    {
                        if (reviewComment.SelectionStartLine < line && reviewComment.SelectionEndLine > line)
                        {
                            commentAtCaretPosition = reviewComment;
                            break;
                        }
                        if (reviewComment.SelectionStartLine == reviewComment.SelectionEndLine &&
                            reviewComment.SelectionStartColumn <= column &&
                            reviewComment.SelectionEndColumn >= column)
                        {
                            commentAtCaretPosition = reviewComment;
                            break;
                        }
                        if ((reviewComment.SelectionStartLine == line && reviewComment.SelectionStartColumn <= column) ||
                            (reviewComment.SelectionEndLine == line && reviewComment.SelectionEndColumn >= column))
                        {
                            commentAtCaretPosition = reviewComment;
                            break;
                        }
                    }
                    else // When its not a ReviewComment on a text-selection, it is a ReviewComment on a given line
                    {
                        if (reviewComment.Line == line)
                        {
                            commentAtCaretPosition = reviewComment;
                            break;
                        }
                    }
                }
            }
            return commentAtCaretPosition;
        }
    }
}
