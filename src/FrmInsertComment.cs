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

        public ApplicationState AppState { get { return _appState; } }
        public string TxtCommentText { get { return txtComment.Text; } }
        
        public void SetBtnInsertCommentEnabled(bool value)
        {
            btnInsertComment.Enabled = value;
        }

        public FrmInsertComment(ApplicationState appState, bool editWorkingComment)
        {
            _appState = appState;
            _baseFormPresenter = new BaseFormPresenter(this);
            _frmInsertCommentPresenter = new FrmInsertCommentPresenter(this);

            InitializeComponent();

            var eventArgs = new BaseFormEventArgs
            {
                Height = _appState.FrmInsertCommentHeight,
                Width = _appState.FrmInsertCommentWidth,
                Location = new Point(_appState.FrmInsertCommentPosX, _appState.FrmInsertCommentPosY)
            };
            DoFormLoad(this, eventArgs);

            if (editWorkingComment)
            {
                txtComment.Text = _appState.WorkingComment.Comment;
            }
            if (TxtCommentTextChanged != null)
            {
                TxtCommentTextChanged(null, EventArgs.Empty);
            }
        }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<InsertCommentEventArgs> BtnInsertCommentClick;
        public event EventHandler<EventArgs> TxtCommentTextChanged;

        private void btnInsertComment_Click(object sender, EventArgs e)
        {
            if(BtnInsertCommentClick != null)
            {
                var args = new InsertCommentEventArgs
                    {
                        Comment = txtComment.Text
                    };
                BtnInsertCommentClick(sender, args);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void FrmInsertComment_FormClosing(object sender, FormClosingEventArgs e)
        {
            var form = (FrmInsertComment)sender;
            _appState.PersistFrmInsertComment(form);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtComment_TextChanged(object sender, EventArgs e)
        {
            if (TxtCommentTextChanged != null)
            {
                TxtCommentTextChanged(null, EventArgs.Empty);
            }
        }
    }
}
