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
                AddNavigatorCommentMarkers(e.Filename);
                _view.AddGreyedArea();
            }
        }

        private void AddNavigatorCommentMarkers(string filename)
        {
            _view.RemoveAllNavigatorShapes();
            var review = _view.AppState.CurrentReview.ReviewedFiles[filename];
            foreach (var comment in review.Comments)
            {
                _view.AddNavigatorCommentMarker(comment.SelectionStartLine > 0 ? comment.SelectionStartLine : comment.Line);
            }
            
        }

        private void DoCloseTabClick(object sender, CloseTabEventArgs e)
        {
            _view.CloseTab(e.Filename);
        }

        private void DoCloseAllTabsClick(object sender, EventArgs e)
        {
            _view.CloseAllTabs();
        }

        private void DoCloseAllTabsButThisClick(object sender, CloseTabEventArgs e)
        {
            _view.CloseAllTabsButThis(e.Filename);
        }
    }
}
