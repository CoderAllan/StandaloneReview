namespace StandaloneReview.Views
{
    using System;

    using Contracts;
    using Model;

    public interface IFrmStandaloneReview
    {
        ISystemIO SystemIO { get; }
        ApplicationState AppState { get; }

        event EventHandler<LoadEventArgs> BtnLoadClick;
        event EventHandler<SaveEventArgs> BtnSaveClick;
        event EventHandler<EventArgs> CommitComment;
        event EventHandler<ReviewCommentEventArgs> SetReviewComment;

        void SetSyntaxHighlighting(string fileType);
        void SetTextEditorControlText(string textEditorControlName, string text);
        int GetTextOffset(int column, int line);
        void AddMarker(int offset, int length);
    }

    public class LoadEventArgs : EventArgs
    {
        public string Filename { get; set; }
        public string EditorControlName { get; set; }
    }

    public class SaveEventArgs : EventArgs
    {
        public string Filename { get; set; }
    }

    public class ReviewCommentEventArgs : EventArgs
    {
        public int Line { get; set; }
        public string LineText { get; set; }
        public int SelectionStartLine { get; set; }
        public int SelectionStartColumn { get; set; }
        public int SelectionEndLine { get; set; }
        public int SelectionEndColumn { get; set; }
        public string SelectedText { get; set; }
    }
}
