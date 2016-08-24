namespace StandaloneReview.Views
{
    using System;

    using Model;

    public interface IFrmInsertComment
    {
        ApplicationState AppState { get; }

        event EventHandler<InsertCommentEventArgs> BtnInsertCommentClick;
        event EventHandler<EventArgs> TxtCommentTextChanged;

        string TxtCommentText { get; }
        void SetBtnInsertCommentEnabled(bool value);
    }

    public class InsertCommentEventArgs : EventArgs
    {
        public string Filename { get; set; }
        public string Comment { get; set; }
    }
}
