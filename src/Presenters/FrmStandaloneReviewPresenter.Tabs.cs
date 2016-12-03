namespace StandaloneReview.Presenters
{
    using System;

    using Views;

    public partial class FrmStandaloneReviewPresenter
    {

        public void DoSelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
        {
            if (_view.AppState.CurrentReview.ReviewedFiles.Count > 0)
            {
                _view.AppState.CurrentReviewedFile = _view.AppState.CurrentReview.ReviewedFiles[e.Filename];
                _view.RemoveAllNavigatorShapes();
                var review = _view.AppState.CurrentReview.ReviewedFiles[e.Filename];
                foreach (var comment in review.Comments)
                {
                    _view.AddNavigatorCommentMarker(comment.SelectionStartLine > 0 ? comment.SelectionStartLine : comment.Line);
                }
                _view.AddGreyedArea();
            }
        }

        private void DoCloseTabClick(object sender, CloseTabEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DoCloseAllTabsClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DoCloseAllTabsButThisClick(object sender, CloseTabEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
