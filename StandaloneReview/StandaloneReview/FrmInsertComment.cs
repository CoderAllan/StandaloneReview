namespace StandaloneReview
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using Model;
    using Presenters;
    using Views;

    public partial class FrmInsertComment: Form, IBaseForm, IFrmInsertComment
    {
        private readonly ApplicationState _appState;
        private readonly BaseFormPresenter _baseFormPresenter;
        private readonly FrmInsertCommentPresenter _frmInsertCommentPresenter;

        public FrmInsertComment(ApplicationState appState)
        {
            _appState = appState;
            _baseFormPresenter = new BaseFormPresenter(this);
            _frmInsertCommentPresenter = new FrmInsertCommentPresenter(this);

            InitializeComponent();

            var eventArgs = new BaseFormEventArgs
            {
                Height = _appState.FrmStandaloneReviewHeight,
                Width = _appState.FrmStandaloneReviewWidth,
                Location = new Point(_appState.FrmStandaloneReviewPosX, _appState.FrmStandaloneReviewPosY)
            };
            DoFormLoad(this, eventArgs);
        }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<EventArgs> BtnInsertCommentClick;

        private void btnInsertComment_Click(object sender, EventArgs e)
        {
            if(BtnInsertCommentClick != null)
            {
                BtnInsertCommentClick(sender, e);
            }
        }

        private void FrmInsertComment_FormClosing(object sender, FormClosingEventArgs e)
        {
            var form = (FrmInsertComment)sender;
            _appState.PersistFrmInsertComment(form);
        }
    }
}
