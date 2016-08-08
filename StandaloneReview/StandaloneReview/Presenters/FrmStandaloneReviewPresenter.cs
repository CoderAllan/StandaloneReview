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


        private void DoCommitComment(object sender, EventArgs e)
        {
            if (!_view.AppState.CurrentReview.ReviewedFiles.ContainsKey(_view.AppState.CurrentReviewedFile.Filename))
            {
                _view.AppState.CurrentReview.ReviewedFiles.Add(_view.AppState.CurrentReviewedFile.Filename, _view.AppState.CurrentReviewedFile);
            }
            _view.AppState.CurrentReview.ReviewedFiles[_view.AppState.CurrentReviewedFile.Filename].Comments.Add(_view.AppState.WorkingComment);
            _view.AppState.WorkingComment = new ReviewComment();
        }

        private void DoSetReviewComment(object sender, ReviewCommentEventArgs e)
        {
            if (_view.AppState.WorkingComment == null)
            {
                _view.AppState.WorkingComment = new ReviewComment();
            }
            _view.AppState.WorkingComment.Line = e.Line;
            _view.AppState.WorkingComment.SelectionStartLine = e.SelectionStartLine;
            _view.AppState.WorkingComment.SelectionStartColoumn = e.SelectionStartColumn;
            _view.AppState.WorkingComment.SelectionEndLine = e.SelectionEndLine;
            _view.AppState.WorkingComment.SelectionEndColoumn = e.SelectionEndColumn;
        }
    }
}
