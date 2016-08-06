namespace StandaloneReview.Presenters
{
    using System;
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

        private void DoBtnInsertCommentClick(object sender, EventArgs e)
        {
            
        }
    }
}
