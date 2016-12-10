namespace StandaloneReview.Presenters
{
    using Model;
    using Views;

    public partial class FrmStandaloneReviewPresenter
    {

        private void DoCommitComment(object sender, CommitCommentEventArgs e)
        {
            if (!_view.AppState.CurrentReview.ReviewedFiles.ContainsKey(_view.AppState.CurrentReviewedFile.Filename))
            {
                _view.AppState.CurrentReview.ReviewedFiles.Add(_view.AppState.CurrentReviewedFile.Filename, _view.AppState.CurrentReviewedFile);
            }
            if (!e.EditCurrentWorkingComment)
            {
                var comment = _view.AppState.WorkingComment;
                _view.AppState.CurrentReview.ReviewedFiles[_view.AppState.CurrentReviewedFile.Filename].Comments.Add(comment);
                AddMarkerForComment(comment);
            }
            else
            {
                _view.SetMarkerTooltip(_view.AppState.WorkingComment.Comment);
            }
            _view.AppState.WorkingComment = new ReviewComment();
            _view.AppState.CurrentReview.Saved = false;
            _view.SetFrmStandaloneReviewTitle();
        }

        private void AddMarkerForComment(ReviewComment comment)
        {
            int offset;
            int length;
            if (_view.AppState.WorkingComment.SelectionStartLine > 0)
            {
                offset = _view.GetTextOffset(comment.SelectionStartColumn, comment.SelectionStartLine - 1);
                length = comment.SelectedText.Length;
                _view.AddNavigatorCommentMarker(comment.SelectionStartLine);
            }
            else
            {
                offset = _view.GetTextOffset(0, comment.Line - 1);
                length = comment.LineText.Length;
                _view.AddNavigatorCommentMarker(comment.Line);
            }
            _view.AddMarker(offset, length, comment.Comment);
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
            _view.SetFrmStandaloneReviewTitle();
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

    }
}
