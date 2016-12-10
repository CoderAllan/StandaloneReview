namespace StandaloneReview
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using Contracts;
    using MarkupConverter;
    using Model;
    using Presenters;
    using Properties;
    using Views;

    using SystemEx = SystemEx.Windows.Forms;

    public partial class FrmPreview : Form, IBaseForm, IFrmPreview
    {
        private readonly ApplicationState _appState;
        private readonly ISystemIO _systemIO;
        private readonly BaseFormPresenter _baseFormPresenter;
        private readonly FrmPreviewPresenter _frmPreviewPresenter;
        private readonly SystemEx.RichTextBoxExtended _txtPreview;

        public FrmPreview(ApplicationState appState)
        {
            _appState = appState;
            _systemIO = new SystemIO();
            _baseFormPresenter = new BaseFormPresenter(this);
            _frmPreviewPresenter = new FrmPreviewPresenter(this);

            InitializeComponent();

            var eventArgs = new BaseFormEventArgs
            {
                Height = _appState.FrmPreviewHeight,
                Width = _appState.FrmPreviewWidth,
                Location = new Point(_appState.FrmPreviewPosX, _appState.FrmPreviewPosY)
            };
            DoFormLoad(this, eventArgs);
            _txtPreview = new SystemEx.RichTextBoxExtended
            {
                Dock = DockStyle.Fill,
                ShowSave = false,
                ShowOpen = false
            };
            panel1.Controls.Add(_txtPreview);
        }

        public ApplicationState AppState { get { return _appState; } }
        public ISystemIO SystemIO { get { return _systemIO; } }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<SaveEventArgs> BtnSaveClick;
        public event EventHandler<FileMoveEventArgs> BtnMoveFileUpClick;
        public event EventHandler<FileMoveEventArgs> BtnMoveFileDownClick;
        public event EventHandler<CommentMoveEventArgs> BtnMoveCommentUpClick;
        public event EventHandler<CommentMoveEventArgs> BtnMoveCommentDownClick;
        public event EventHandler<EventArgs> FrmPreviewLoad;
        public event EventHandler<SelectedIndexChangedEventArgs> LstFilesSelectedIndexChanged;
        public event EventHandler<SelectedIndexChangedEventArgs> LstCommentsSelectedIndexChanged;

        public int LstFilesItemsCount { get { return lstFiles.Items.Count; } }
        public int LstFilesSelectedIndex { get { return lstFiles.SelectedIndex; } }
        public int LstCommentsItemsCount { get { return lstComments.Items.Count; } }
        public int LstCommentsSelectedIndex { get { return lstComments.SelectedIndex; } }

        private void FrmPreview_Load(object sender, EventArgs e)
        {
            if (FrmPreviewLoad != null)
            {
                FrmPreviewLoad(sender, e);
            }
        }

        private void FrmPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            var frmPreview = (FrmPreview)sender;
            _appState.PersistFrmPreview(frmPreview);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BtnSaveClick != null)
            {
                saveFileDialog1.Filter = Resources.SaveDialogFilter;
                saveFileDialog1.FileName = "Review-" + _appState.CurrentReview.ReviewTime.ToString("yyyy-MM-dd");
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var args = new SaveEventArgs
                    {
                        Filename = saveFileDialog1.FileName,
                        SaveAsFormat = (SaveAsFormat)saveFileDialog1.FilterIndex
                    };
                    BtnSaveClick(sender, args);
                }
            }
        }

        public void SavePreview(string filename, SaveAsFormat saveAsFormat)
        {
            switch (saveAsFormat)
            {
                case SaveAsFormat.Html:
                    SystemIO.WriteAllText(filename, RtfToHtmlConverter.ConvertRtfToHtml(_txtPreview.Rtf));
                    break;
                case SaveAsFormat.Richtext:
                    _txtPreview.InnerControl.SaveFile(filename, RichTextBoxStreamType.RichText);
                    break;
                default:
                    _txtPreview.InnerControl.SaveFile(filename, RichTextBoxStreamType.PlainText);
                break;
            }
        }

        private void btnMoveFileUp_Click(object sender, EventArgs e)
        {
            if (BtnMoveFileUpClick != null)
            {
                var fileMoveEventArgs = GetFileMoveEventArgs();
                BtnMoveFileUpClick(sender, fileMoveEventArgs);
            }
        }

        private void btnMoveFileDown_Click(object sender, EventArgs e)
        {
            if (BtnMoveFileDownClick != null)
            {
                var fileMoveEventArgs = GetFileMoveEventArgs();
                BtnMoveFileDownClick(sender, fileMoveEventArgs);
            }
        }

        private FileMoveEventArgs GetFileMoveEventArgs()
        {
            ListboxFilesItem item = GetSelectedFileOrDefault();
            string filename = item.FullFilename;
            var fileMoveEventArgs = new FileMoveEventArgs
            {
                Filename = filename,
                Position = lstFiles.SelectedIndex
            };
            return fileMoveEventArgs;
        }

        private void btnMoveCommentUp_Click(object sender, EventArgs e)
        {
            if (BtnMoveCommentUpClick != null)
            {
                var commentMoveEventArgs = GetCommentMoveEventArgs(); 
                BtnMoveCommentUpClick(sender, commentMoveEventArgs);
            }
        }

        private void btnMoveCommentDown_Click(object sender, EventArgs e)
        {
            if (BtnMoveCommentDownClick != null)
            {
                var commentMoveEventArgs = GetCommentMoveEventArgs();
                BtnMoveCommentDownClick(sender, commentMoveEventArgs);
            }
        }

        private CommentMoveEventArgs GetCommentMoveEventArgs()
        {
            ListboxFilesItem item = GetSelectedFileOrDefault();
            string filename = item.FullFilename;
            var commentMoveEventArgs = new CommentMoveEventArgs
            {
                Filename = filename,
                Position = lstComments.SelectedIndex
            };
            return commentMoveEventArgs;
        }

        public void SetTxtPreviewText(string text)
        {
            _txtPreview.Rtf = text;
        }

        public void ClearLstFiles()
        {
            lstFiles.Items.Clear();
        }

        public void InsertFilenameInListbox(ListboxFilesItem filename)
        {
            lstFiles.Items.Add(filename);
        }

        public void InsertCommentInListbox(string comment)
        {
            lstComments.Items.Add(comment);
        }

        public void ClearLstComments()
        {
            lstComments.Items.Clear();
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstFilesSelectedIndexChanged != null)
            {
                ListboxFilesItem item = GetSelectedFileOrDefault();
                var selectedIndexChangedEventArgs = new SelectedIndexChangedEventArgs
                {
                    Filename = item.FullFilename,
                    Position = lstFiles.SelectedIndex
                };
                LstFilesSelectedIndexChanged(sender, selectedIndexChangedEventArgs);
            }
        }

        private void lstComments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstCommentsSelectedIndexChanged != null)
            {
                ListboxFilesItem item = GetSelectedFileOrDefault();
                var selectedIndexChangedEventArgs = new SelectedIndexChangedEventArgs
                {
                    Filename = item.FullFilename,
                    Position = lstComments.SelectedIndex
                };
                LstCommentsSelectedIndexChanged(sender, selectedIndexChangedEventArgs);
            }
        }

        private ListboxFilesItem GetSelectedFileOrDefault()
        {
            return lstFiles.SelectedIndex >= 0 ? (ListboxFilesItem) lstFiles.SelectedItem : (ListboxFilesItem) lstFiles.Items[0];
        }

        public void EnableDisableMoveFileButtons(bool btnMoveFileUpEnabled, bool btnMoveFileDownEnabled)
        {
            btnMoveFileUp.Enabled = btnMoveFileUpEnabled;
            btnMoveFileDown.Enabled = btnMoveFileDownEnabled;
        }

        public void EnableDisableMoveCommentButtons(bool btnMoveCommentUpEnabled, bool btnMoveCommentDownEnabled)
        {
            btnMoveCommentUp.Enabled = btnMoveCommentUpEnabled;
            btnMoveCommentDown.Enabled = btnMoveCommentDownEnabled;
        }
    }
}
