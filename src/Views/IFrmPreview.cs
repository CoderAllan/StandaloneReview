using StandaloneReview.Contracts;

namespace StandaloneReview.Views
{
    using System;
    using Model;

    public interface IFrmPreview
    {
        event EventHandler<SaveEventArgs> BtnSaveClick;
        event EventHandler<FileMoveEventArgs> BtnMoveFileUpClick;
        event EventHandler<FileMoveEventArgs> BtnMoveFileDownClick;
        event EventHandler<CommentMoveEventArgs> BtnMoveCommentUpClick;
        event EventHandler<CommentMoveEventArgs> BtnMoveCommentDownClick;
        event EventHandler<EventArgs> FrmPreviewLoad;
        event EventHandler<SelectedIndexChangedEventArgs> LstFilesSelectedIndexChanged;
        event EventHandler<SelectedIndexChangedEventArgs> LstCommentsSelectedIndexChanged;
        
        ApplicationState AppState { get; }
        ISystemIO SystemIO { get; }

        int LstFilesItemsCount { get; }
        int LstFilesSelectedIndex { get; }
        int LstCommentsItemsCount { get; }
        int LstCommentsSelectedIndex { get; }

        void SetTxtPreviewText(string text);
        void InsertFilenameInListbox(ListboxFilesItem filename);
        void ClearLstFiles();
        void InsertCommentInListbox(string comment);
        void ClearLstComments();
        void EnableDisableMoveFileButtons(bool btnMoveFileUpEnabled, bool btnMoveFileDownEnabled);
        void EnableDisableMoveCommentButtons(bool btnMoveCommentUpEnabled, bool btnMoveCommentDownEnabled);
        void SavePreview(string filename, SaveAsFormat saveAsFormat);
    }

    public class SelectedIndexChangedEventArgs : EventArgs
    {
        public string Filename { get; set; }
        public int Position { get; set; }
    }

    public class FileMoveEventArgs : EventArgs
    {
        public string Filename { get; set; }
        public int Position { get; set; }
    }

    public class CommentMoveEventArgs : EventArgs
    {
        public string Filename { get; set; }
        public int Position { get; set; }
    }

    public enum SaveAsFormat { Text = 1, Richtext = 2, Html = 3 }
    public class SaveEventArgs : FilenameEventArgs
    {
        public SaveAsFormat SaveAsFormat { get; set; }
    }
}
