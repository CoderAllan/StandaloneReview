namespace StandaloneReview.Presenters
{
    using System;
    using System.ComponentModel;
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
            _view.SelectedTabChanged += DoSelectedTabChanged;

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
            if (_view.AppState.CurrentReview.ReviewedFiles.ContainsKey(args.Filename))
            {
                _view.SelectOpenTab(args.Filename);
                return;
            }
            _view.AppState.CurrentReviewedFile = new ReviewedFile
            {
                Filename = args.Filename,
                Comments = new List<ReviewComment>(),
                Position = _view.AppState.CurrentReview.ReviewedFiles.Count
            };
            _view.AppState.CurrentReview.ReviewedFiles.Add(args.Filename, _view.AppState.CurrentReviewedFile);
            var text = _view.SystemIO.FileReadAllText(args.Filename);
            int newTabPageNumber = _view.AppState.CurrentReview.ReviewedFiles.Count;
            string textEditorControlName = _view.AddNewTab(args.Filename, newTabPageNumber);
            _view.SetTextEditorControlText(textEditorControlName, text);
            _view.SetSyntaxHighlighting(_view.SystemIO.PathGetExtension(args.Filename));
            _view.EnableDisableMenuToolstripItems();
            _view.RemoveAllNavigatorShapes();
            _view.AddGreyedArea();
        }

        private void DoExitClick(object sender, CancelEventArgs e)
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
                    else
                    {
                        e.Cancel = true;
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
                ResetCurrentReview();
            }
            else
            {
                if (_view.AppState.CurrentReviewedFile != null &&
                    _view.AppState.CurrentReviewedFile.Comments != null &&
                    _view.AppState.CurrentReviewedFile.Comments.Count > 0)
                {
                    if (_view.MessageBoxUnsavedCommentsWarningOkCancel())
                    {
                        ResetCurrentReview();
                    }
                }
                else
                {
                    ResetCurrentReview();
                }
            }
        }

        private void ResetCurrentReview()
        {
            _view.AppState.WorkingComment = new ReviewComment();
            _view.AppState.CurrentReviewedFile = null;
            _view.AppState.CurrentReview = new Review
            {
                ReviewTime = DateTime.Now,
                ReviewedFiles = new Dictionary<string, ReviewedFile>(),
                Saved = true
            };
            _view.RemoveAllOpenTabs();
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
                    _view.AddNavigatorCommentMarker(_view.AppState.WorkingComment.SelectionStartLine);
                }
                else
                {
                    offset = _view.GetTextOffset(0, _view.AppState.WorkingComment.Line - 1);
                    length = _view.AppState.WorkingComment.LineText.Length;
                    _view.AddNavigatorCommentMarker(_view.AppState.WorkingComment.Line);
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
            _view.AppState.WorkingComment.Position = e.Position;
            _view.AppState.WorkingComment.Line = e.Line;
            _view.AppState.WorkingComment.LineText = e.LineText;
            _view.AppState.WorkingComment.SelectionStartLine = e.SelectionStartLine;
            _view.AppState.WorkingComment.SelectionStartColumn = e.SelectionStartColumn;
            _view.AppState.WorkingComment.SelectionEndLine = e.SelectionEndLine;
            _view.AppState.WorkingComment.SelectionEndColumn = e.SelectionEndColumn;
            _view.AppState.WorkingComment.SelectedText = e.SelectedText;
            _view.AddNavigatorCurrentLineMarker(e.Line);
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
                _view.AppState.CurrentReviewedFile.RemoveComment(commentAtCaretPosition);
                _view.AppState.WorkingComment = new ReviewComment();
                _view.AppState.CurrentReview.Saved = false;
                if (!_view.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == e.Line))
                {
                    _view.RemoveNavigatorCommentMarker(e.Line);
                }
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
                        if ((reviewComment.SelectionStartLine == line && reviewComment.SelectionStartColumn <= column) &&
                            (reviewComment.SelectionEndLine >= line && reviewComment.SelectionEndColumn >= column))
                        {
                            commentAtCaretPosition = reviewComment;
                            break;
                        }
                        if ((reviewComment.SelectionStartLine <= line && reviewComment.SelectionStartColumn <= column) &&
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

        public void DoSelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            if (_view.AppState.CurrentReview.ReviewedFiles.Count > 0)
            {
                _view.RemoveAllNavigatorShapes();
                var review = _view.AppState.CurrentReview.ReviewedFiles[e.Filename];
                foreach (var comment in review.Comments)
                {
                    _view.AddNavigatorCommentMarker(comment.SelectionStartLine > 0 ? comment.SelectionStartLine : comment.Line);
                }
                _view.AddGreyedArea();
            }
        }
    }
}
