using StandaloneReview.Contracts;

namespace StandaloneReview.Tests.MockViews
{
    using System;

    using Model;
    using Views;

    public class MockFrmPreview : IFrmPreview
    {
        public ApplicationState AppState { get; private set; }
        public ISystemIO SystemIO { get; private set; }
        public int LstFilesItemsCount { get; private set; }
        public int LstFilesSelectedIndex { get; private set; }
        public int LstCommentsItemsCount { get; private set; }
        public int LstCommentsSelectedIndex { get; private set; }

        public event EventHandler<EventArgs> FrmPreviewLoad;
        public event EventHandler<SaveEventArgs> BtnSaveClick;
        public event EventHandler<FileMoveEventArgs> BtnMoveFileUpClick;
        public event EventHandler<FileMoveEventArgs> BtnMoveFileDownClick;
        public event EventHandler<CommentMoveEventArgs> BtnMoveCommentUpClick;
        public event EventHandler<CommentMoveEventArgs> BtnMoveCommentDownClick;
        public event EventHandler<SelectedIndexChangedEventArgs> LstFilesSelectedIndexChanged;
        public event EventHandler<SelectedIndexChangedEventArgs> LstCommentsSelectedIndexChanged;

        public MockFrmPreview()
        {
            AppState = new ApplicationState();
        }

        public bool SetTxtPreviewTextWasCalled { get; private set; }
        public string SetTxtPreviewTextValue { get; private set; }
        public void SetTxtPreviewText(string text)
        {
            SetTxtPreviewTextWasCalled = true;
            SetTxtPreviewTextValue = text;
        }


        public void FireFrmPreviewLoad()
        {
            if (FrmPreviewLoad == null)
            {
                throw new NotImplementedException();
            }
            FrmPreviewLoad(null, EventArgs.Empty);
        }

        public bool InsertFilenameInListboxWasCalled { get; private set; }
        public ListboxFilesItem InsertFilenameInListboxValue { get; private set; }
        public void InsertFilenameInListbox(ListboxFilesItem filename)
        {
            InsertFilenameInListboxWasCalled = true;
        }

        public void ClearLstFiles()
        {
            throw new NotImplementedException();
        }

        public bool InsertCommentInListboxWasCalled { get; private set; }
        public string InsertCommentInListboxValue { get; private set; }
        public void InsertCommentInListbox(string comment)
        {
            InsertCommentInListboxWasCalled = true;
            InsertCommentInListboxValue = comment;
        }

        public bool ClearLstCommentsWasCalled { get; private set; }
        public void ClearLstComments()
        {
            ClearLstCommentsWasCalled = true;
        }

        public bool EnableDisableMoveFileButtonsWasCalled { get; private set; }
        public bool BtnMoveFileUpEnabledValue { get; private set; }
        public bool BtnMoveFileDownEnabledValue { get; private set; }
        public void EnableDisableMoveFileButtons(bool btnMoveFileUpEnabled, bool btnMoveFileDownEnabled)
        {
            EnableDisableMoveFileButtonsWasCalled = true;
            BtnMoveFileUpEnabledValue = btnMoveFileUpEnabled;
            BtnMoveFileDownEnabledValue = btnMoveFileDownEnabled;
        }

        public void FireMoveFileUpEvent(string filename, int position)
        {
            if (BtnMoveFileUpClick == null)
            {
                throw new NotImplementedException();
            }
            var fileMoveUpEventArgs = new FileMoveEventArgs
            {
                Filename = filename,
                Position = position
            };
            LstFilesItemsCount = AppState.CurrentReview.ReviewedFiles.Count;
            LstCommentsItemsCount = AppState.CurrentReview.ReviewedFiles[filename].Comments.Count;
            BtnMoveFileUpClick(null, fileMoveUpEventArgs);
        }

        public void FireMoveFileDownEvent(string filename, int position)
        {
            if (BtnMoveFileDownClick == null)
            {
                throw new NotImplementedException();
            }
            var fileMoveDownEventArgs = new FileMoveEventArgs
            {
                Filename = filename,
                Position = position
            };
            LstFilesItemsCount = AppState.CurrentReview.ReviewedFiles.Count;
            LstCommentsItemsCount = AppState.CurrentReview.ReviewedFiles[filename].Comments.Count;
            BtnMoveFileDownClick(null, fileMoveDownEventArgs);
        }

        public bool EnableDisableMoveCommentButtonsWasCalled { get; private set; }
        public bool BtnMoveCommentUpEnabledValue { get; private set; }
        public bool BtnMoveCommentDownEnabledValue { get; private set; }
        public void EnableDisableMoveCommentButtons(bool btnMoveCommentUpEnabled, bool btnMoveCommentDownEnabled)
        {
            EnableDisableMoveCommentButtonsWasCalled = true;
            BtnMoveCommentUpEnabledValue = btnMoveCommentUpEnabled;
            BtnMoveCommentDownEnabledValue = btnMoveCommentDownEnabled;
        }

        public void SavePreview(string filename, SaveAsFormat saveAsFormat)
        {
            throw new NotImplementedException();
        }

        public void FireMoveCommentUpEvent(string filename, int position)
        {
            if (BtnMoveCommentUpClick == null)
            {
                throw new NotImplementedException();
            }
            var commentMoveUpEventArgs = new CommentMoveEventArgs
            {
                Filename = filename,
                Position = position
            };
            LstFilesItemsCount = AppState.CurrentReview.ReviewedFiles.Count;
            LstCommentsItemsCount = AppState.CurrentReview.ReviewedFiles[filename].Comments.Count;
            BtnMoveCommentUpClick(null, commentMoveUpEventArgs);
        }

        public void FireMoveCommentDownEvent(string filename, int position)
        {
            if (BtnMoveCommentDownClick == null)
            {
                throw new NotImplementedException();
            }
            var commentMoveDownEventArgs = new CommentMoveEventArgs
            {
                Filename = filename,
                Position = position
            };
            LstFilesItemsCount = AppState.CurrentReview.ReviewedFiles.Count;
            LstCommentsItemsCount = AppState.CurrentReview.ReviewedFiles[filename].Comments.Count;
            BtnMoveCommentDownClick(null, commentMoveDownEventArgs);
        }

        public void FireLstCommentsSelectedIndexChanged(string filename, int position)
        {
            if (LstCommentsSelectedIndexChanged == null)
            {
                throw new NotImplementedException();
            }
            var selectedIndexChangedEventArgs = new SelectedIndexChangedEventArgs
            {
                Filename = filename,
                Position = position
            };
            LstFilesItemsCount = AppState.CurrentReview.ReviewedFiles.Count;
            LstCommentsItemsCount = AppState.CurrentReview.ReviewedFiles[filename].Comments.Count;
            LstCommentsSelectedIndexChanged(null, selectedIndexChangedEventArgs);
        }

        public void FireLstFilesSelectedIndexChanged(string filename, int position)
        {
            if (LstFilesSelectedIndexChanged == null)
            {
                throw new NotImplementedException();
            }
            var selectedIndexChangedEventArgs = new SelectedIndexChangedEventArgs
            {
                Filename = filename,
                Position = position
            };
            LstFilesItemsCount = AppState.CurrentReview.ReviewedFiles.Count;
            LstCommentsItemsCount = AppState.CurrentReview.ReviewedFiles[filename].Comments.Count;
            LstFilesSelectedIndexChanged(null, selectedIndexChangedEventArgs);
        }
    }
}
