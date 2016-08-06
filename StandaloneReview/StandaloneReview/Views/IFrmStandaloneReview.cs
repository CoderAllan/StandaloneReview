namespace StandaloneReview.Views
{
    using System;
    using Contracts;

    public interface IFrmStandaloneReview
    {
        ISystemIO SystemIO { get; }

        event EventHandler<LoadEventArgs> BtnLoadClick;

        void SetSyntaxHighlighting(string fileType);
        void SetTextEditorControlText(string textEditorControlName, string text);
    }

    public class LoadEventArgs : EventArgs
    {
        public string Filename { get; set; }
        public string EditorControlName { get; set; }
    }
}
