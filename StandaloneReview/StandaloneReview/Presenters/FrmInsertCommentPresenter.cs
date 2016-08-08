namespace StandaloneReview.Presenters
{
    using Model;
    using Views;

    public class FrmInsertCommentPresenter
    {
        private readonly IFrmInsertComment _view;

        public FrmInsertCommentPresenter(IFrmInsertComment view)
        {
            _view = view;

            Initialize();
        }

        private void Initialize()
        {
            _view.BtnInsertCommentClick += DoBtnInsertCommentClick;
        }

        private void DoBtnInsertCommentClick(object sender, InsertCommentEventArgs e)
        {
            if (_view.AppState.WorkingComment == null)
            {
                _view.AppState.WorkingComment = new ReviewComment();
            }
            _view.AppState.WorkingComment.Comment = e.Comment;
        }
    }
}
