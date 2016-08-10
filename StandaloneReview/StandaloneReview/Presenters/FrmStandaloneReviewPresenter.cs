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
            _view.CommitComment += DoCommitComment;
            _view.SetReviewComment += DoSetReviewComment;
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
        }

        private void DoSaveClick(object sender, SaveEventArgs e)
        {
            string review = _view.AppState.CurrentReview.ToString();
            _view.SystemIO.WriteAllText(e.Filename, review);
            _view.AppState.CurrentReview.Saved = true;
        }

        private void DoNewClick(object sender, EventArgs e)
        {
            _view.AppState.WorkingComment = new ReviewComment();
            _view.AppState.CurrentReview = new Review
            {
                ReviewTime = DateTime.Now,
                ReviewedFiles = new Dictionary<string, ReviewedFile>(),
                Saved = false
            };
            _view.ResetTextEditor();
        }

        private void DoCommitComment(object sender, EventArgs e)
        {
            if (!_view.AppState.CurrentReview.ReviewedFiles.ContainsKey(_view.AppState.CurrentReviewedFile.Filename))
            {
                _view.AppState.CurrentReview.ReviewedFiles.Add(_view.AppState.CurrentReviewedFile.Filename, _view.AppState.CurrentReviewedFile);
            }
            _view.AppState.CurrentReview.ReviewedFiles[_view.AppState.CurrentReviewedFile.Filename].Comments.Add(_view.AppState.WorkingComment);
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
            _view.AppState.WorkingComment = new ReviewComment();
            _view.AppState.CurrentReview.Saved = false;
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
    }
}
