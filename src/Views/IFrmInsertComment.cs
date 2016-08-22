namespace StandaloneReview.Views
{
    using System;

    using Model;

    public interface IFrmInsertComment
    {
        ApplicationState AppState { get; }

        event EventHandler<InsertCommentEventArgs> BtnInsertCommentClick;
    }

    public class InsertCommentEventArgs : EventArgs
    {
        public string Filename { get; set; }
        public string Comment { get; set; }
    }
}
