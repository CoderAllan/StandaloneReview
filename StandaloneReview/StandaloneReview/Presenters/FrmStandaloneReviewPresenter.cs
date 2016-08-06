namespace StandaloneReview.Presenters
{
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
        }

        private void DoLoadClick(object sender, LoadEventArgs args)
        {
            var text = _view.SystemIO.FileReadAllText(args.Filename);
            _view.SetTextEditorControlText(args.EditorControlName, text);
            _view.SetSyntaxHighlighting(_view.SystemIO.PathGetExtension(args.Filename));
        }
    }
}
