namespace StandaloneReview
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using ICSharpCode.TextEditor;
    
    using Contracts;
    using Model;
    using Views;
    using Presenters;
    using Properties;

    public partial class FrmStandaloneReview : Form, IBaseForm, IFrmStandaloneReview
    {
        private readonly ApplicationState _appState;
        private readonly ISystemIO _systemIO;
        private readonly BaseFormPresenter _baseFormPresenter;
        private readonly FrmStandaloneReviewPresenter _frmStandaloneReviewPresenter;

        public FrmStandaloneReview()
        {
            _appState = ApplicationState.ReadApplicationState();
            _systemIO = new SystemIO();
            _baseFormPresenter = new BaseFormPresenter(this);
            _frmStandaloneReviewPresenter = new FrmStandaloneReviewPresenter(this);

            RuntimeLocalizer.ChangeCulture(_appState.ApplicationLocale);

            InitializeComponent();

            var eventArgs = new BaseFormEventArgs
            {
                Height = _appState.FrmStandaloneReviewHeight,
                Width = _appState.FrmStandaloneReviewWidth,
                Location = new Point(_appState.FrmStandaloneReviewPosX, _appState.FrmStandaloneReviewPosY)
            };
            DoFormLoad(this, eventArgs);
            navigatorCanvas.Top = menuStrip1.Bottom + 2;
            navigatorCanvas.Height = statusStrip1.Top - menuStrip1.Height - 4;

            EnableDisableMenuToolstripItems();
            SetFrmStandaloneReviewTitle();
        }

        public ISystemIO SystemIO
        {
            get { return _systemIO; }
        }

        public ApplicationState AppState
        {
            get { return _appState; }
        }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<LoadEventArgs> BtnLoadClick;
        public event EventHandler<EventArgs> BtnNewClick;
        public event EventHandler<SaveEventArgs> BtnSaveClick;
        public event EventHandler<CancelEventArgs> BtnExitClick;
        public event EventHandler<CommitCommentEventArgs> CommitComment;
        public event EventHandler<ReviewCommentEventArgs> SetReviewComment;
        public event EventHandler<CaretPositionEventArgs> DeleteComment;
        public event EventHandler<CaretPositionEventArgs> EditComment;
        public event EventHandler<CaretPositionEventArgs> ContextMenuStripOpening;
        public event EventHandler<SelectedTabChangedEventArgs> SelectedTabChanged;
        public event EventHandler<CloseTabEventArgs> CloseTabClick;
        public event EventHandler<CloseTabEventArgs> CloseAllTabsButThisClick;
        public event EventHandler<EventArgs> CloseAllTabsClick;
        public event EventHandler<OpenFolderEventArgs> OpenContainingFolder;
        public event EventHandler<CopyFullPathEventArgs> CopyFullPath;

        public void SetFrmStandaloneReviewTitle()
        {
            string isReviewSaved = _appState.CurrentReview.Saved ? "" : " *";
            Text = Resources.MainFormTitle + isReviewSaved;
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void CloseApplication()
        {
            _appState.PersistFrmStandaloneReview(this);
            ApplicationState.WriteApplicationState(_appState);
        }

        public bool MessageBoxUnsavedCommentsWarningOkCancel()
        {
            return MessageBox.Show(Resources.FrmStandaloneReview_nytReviewToolStripMenuItem_Click_Unsaved_Changes,
                                   Resources.FrmStandaloneReview_nytReviewToolStripMenuItem_Click_Unsaved_Changes_Caption,
                                   MessageBoxButtons.YesNoCancel,
                                   MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        
        private void FrmStandaloneReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BtnExitClick != null)
            {
                BtnExitClick(sender, e);
            }
        }

        private void insertCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInsertCommentForm(sender, e);
        }

        private void insertCommentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowInsertCommentForm(sender, e);
        }

        private void ShowInsertCommentForm(object sender, EventArgs e)
        {
            ShowInsertCommentForm(false);
        }

        public void ShowInsertCommentForm(bool editCurrentWorkingComment)
        {
            var frmInsertComment = new FrmInsertComment(_appState, editCurrentWorkingComment)
            {
                Visible = false
            };
            if (frmInsertComment.ShowDialog() == DialogResult.OK && CommitComment != null)
            {
                var commitCommentEventArgs = new CommitCommentEventArgs
                {
                    EditCurrentWorkingComment = editCurrentWorkingComment
                };
                CommitComment(null, commitCommentEventArgs);
            }
            else
            {
                _appState.WorkingComment = new ReviewComment();
            }
        }

        private void editCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditComment != null)
            {
                var editor = GetActiveTextEditor();
                var editCommentEventArgs = new CaretPositionEventArgs
                {
                    Line = editor.ActiveTextAreaControl.Caret.Position.Line + 1,
                    Column = editor.ActiveTextAreaControl.Caret.Position.Column
                };
                EditComment(sender, editCommentEventArgs);
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BtnLoadClick != null)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog1.FileName;
                    var loadEventArgs = new LoadEventArgs
                    {
                        Filename = filename
                    };
                    BtnLoadClick(sender, loadEventArgs);
                }
            }
        }

        private void saveReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BtnSaveClick != null)
            {
                saveFileDialog1.Filter = @"Review (*.txt)|*.txt";
                saveFileDialog1.FileName = "Review-" + _appState.CurrentReview.ReviewTime.ToString("yyyy-MM-dd");
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var args = new SaveEventArgs
                    {
                        Filename = saveFileDialog1.FileName
                    };
                    BtnSaveClick(sender, args);
                }
            }
        }

        private void newReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BtnNewClick != null)
            {
                _filenameTabPageRelation.Clear();
                BtnNewClick(sender, e);
            }
        }

        public void EnableDisableMenuToolstripItems()
        {
            if (_appState != null && _appState.CurrentReviewedFile != null)
            {
                insertCommentToolStripMenuItem1.Enabled = true;
                insertCommentToolStripMenuItem.Enabled = true;
                saveReviewToolStripMenuItem.Enabled = true;
                nytReviewToolStripMenuItem.Enabled = true;
                previewToolStripMenuItem.Enabled = true;
            }
            else
            {
                insertCommentToolStripMenuItem1.Enabled = false;
                insertCommentToolStripMenuItem.Enabled = false;
                saveReviewToolStripMenuItem.Enabled = false;
                nytReviewToolStripMenuItem.Enabled = false;
                deleteCommentToolStripMenuItem.Enabled = false;
                editCommentToolStripMenuItem.Enabled = false;
                previewToolStripMenuItem.Enabled = false;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (ContextMenuStripOpening != null)
            {
                var editor = GetActiveTextEditor();
                var shouldEnableDisableEventArgs = new CaretPositionEventArgs
                {
                    Line = editor.ActiveTextAreaControl.Caret.Position.Line + 1,
                    Column = editor.ActiveTextAreaControl.Caret.Position.Column
                };
                ContextMenuStripOpening(sender, shouldEnableDisableEventArgs);
            }
        }

        public void EnableDisableContextMenuToolsstripItems(bool menuToolStripEnabled)
        {
            deleteCommentToolStripMenuItem.Enabled = menuToolStripEnabled;
            editCommentToolStripMenuItem.Enabled = menuToolStripEnabled;
        }

        private void deleteCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DeleteComment != null)
            {
                var editor = GetActiveTextEditor();
                var deleteCommentEventArgs = new CaretPositionEventArgs
                {
                    Line = editor.ActiveTextAreaControl.Caret.Position.Line + 1,
                    Column = editor.ActiveTextAreaControl.Caret.Position.Column
                };
                DeleteComment(sender, deleteCommentEventArgs);
                var textLocation = new TextLocation(editor.ActiveTextAreaControl.Caret.Position.Column, 
                                                    editor.ActiveTextAreaControl.Caret.Position.Line);
                foreach (var textMarker in editor.Document.MarkerStrategy.GetMarkers(textLocation))
                {
                    editor.Document.MarkerStrategy.RemoveMarker(textMarker);
                }
                editor.Refresh();
            }
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmPreview = new FrmPreview(_appState);
            frmPreview.ShowDialog();
            SetFrmStandaloneReviewTitle();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmOptions = new FrmOptions(_appState);
            if (frmOptions.ShowDialog() == DialogResult.OK)
            {
                RuntimeLocalizer.ChangeCulture(_appState.ApplicationLocale);
                Refresh();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedTabChanged != null && tabControl1.SelectedTab != null)
            {
                var selectedTabChangedArgs = new SelectedTabChangedEventArgs
                {
                    Filename = (string)tabControl1.SelectedTab.Tag
                };
                SelectedTabChanged(sender, selectedTabChangedArgs);
            }
        }

        private void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenContainingFolder != null)
            {
                var filename = (string)tabControl1.SelectedTab.Tag;
                var openFolderEventArgs = new OpenFolderEventArgs
                {
                    Foldername = _systemIO.PathGetFoldername(filename)
                };
                OpenContainingFolder(sender, openFolderEventArgs);
            }
        }

        private void copyFullPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CopyFullPath != null)
            {
                var copyFullPathEventArgs = new CopyFullPathEventArgs
                {
                    Filename = (string)tabControl1.SelectedTab.Tag
                };
                CopyFullPath(sender, copyFullPathEventArgs);
            }
        }


        private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CloseTabClick != null)
            {
                var closeTabEventArgs = new CloseTabEventArgs
                {
                    Filename = (string)tabControl1.SelectedTab.Tag
                };
                CloseTabClick(sender, closeTabEventArgs);
            }
        }

        private void closeAllTabsButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CloseAllTabsButThisClick != null)
            {
                var closeTabEventArgs = new CloseTabEventArgs
                {
                    Filename = (string)tabControl1.SelectedTab.Tag
                };
                CloseAllTabsButThisClick(sender, closeTabEventArgs);
            }
        }

        private void closeAllTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CloseAllTabsClick != null)
            {
                CloseAllTabsClick(sender, EventArgs.Empty);
            }
        }

        private void contextMenuTabpages_Opening(object sender, CancelEventArgs e)
        {
            // We can right click on a tab that is not selected, so we have to select the clicked tab
            // The solution was found here: http://stackoverflow.com/a/10523725/57855
            Point p = tabControl1.PointToClient(Cursor.Position);
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                Rectangle r = tabControl1.GetTabRect(i);
                if (r.Contains(p))
                {
                    tabControl1.SelectedIndex = i; // i is the index of tab under cursor
                    return;
                }
            }
            e.Cancel = true;
        }
    }
}
