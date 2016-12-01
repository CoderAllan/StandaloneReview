namespace StandaloneReview.Views
{
    using System;
    using System.ComponentModel;

    using Contracts;
    using Model;

    public interface IFrmStandaloneReview
    {
        ISystemIO SystemIO { get; }
        ApplicationState AppState { get; }

        event EventHandler<LoadEventArgs> BtnLoadClick;
        event EventHandler<EventArgs> BtnNewClick;
        event EventHandler<SaveEventArgs> BtnSaveClick;
        event EventHandler<CancelEventArgs> BtnExitClick;
        event EventHandler<CommitCommentEventArgs> CommitComment;
        event EventHandler<ReviewCommentEventArgs> SetReviewComment;
        event EventHandler<CaretPositionEventArgs> DeleteComment;
        event EventHandler<CaretPositionEventArgs> EditComment;
        event EventHandler<CaretPositionEventArgs> ContextMenuStripOpening;
        event EventHandler<SelectedTabChangedEventArgs> SelectedTabChanged;
        event EventHandler<OpenFolderEventArgs> OpenContainingFolder;
        event EventHandler<CopyFullPathEventArgs> CopyFullPath;

        // Main form
        void SetFrmStandaloneReviewTitle(string text);
        void EnableDisableMenuToolstripItems();
        bool MessageBoxUnsavedCommentsWarningOkCancel();
        void EnableDisableContextMenuToolsstripItems(bool menuToolStripEnabled);
        void CloseApplication();

        // Texteditor control
        void SetSyntaxHighlighting(string fileType);
        void SetTextEditorControlText(string textEditorControlName, string text);
        int GetTextOffset(int column, int line);
        void AddMarker(int offset, int length, string tooltipText);
        void SetMarkerTooltip(string tooltipText);
        
        // Tabcontrol
        string AddNewTab(string filename, int newTabPageNumber);
        void RemoveAllOpenTabs();
        void SelectOpenTab(string filename);
        
        // Navigator markers
        void AddNavigatorCurrentLineMarker(int line);
        void AddNavigatorCommentMarker(int line);
        void RemoveNavigatorCommentMarker(int line);
        void AddGreyedArea();
        void RemoveAllNavigatorShapes();
        
        void ShowInsertCommentForm(bool editCurrentWorkingComment);
    }

    public class FilenameEventArgs : EventArgs
    {
        public string Filename { get; set; }
    }

    public class LoadEventArgs : FilenameEventArgs
    {
        public string EditorControlName { get; set; }
    }

    public class SaveEventArgs : FilenameEventArgs
    {
    }

    public class CommitCommentEventArgs : EventArgs
    {
        public bool EditCurrentWorkingComment { get; set; }
    }

    public class ReviewCommentEventArgs : EventArgs
    {
        public int Position { get; set; }
        public int Line { get; set; }
        public string LineText { get; set; }
        public int SelectionStartLine { get; set; }
        public int SelectionStartColumn { get; set; }
        public int SelectionEndLine { get; set; }
        public int SelectionEndColumn { get; set; }
        public string SelectedText { get; set; }
    }

    public class CaretPositionEventArgs : EventArgs
    {
        public int Line { get; set; }
        public int Column { get; set; }
    }

    public class SelectedTabChangedEventArgs : FilenameEventArgs
    {
    }

    public class OpenFolderEventArgs : EventArgs
    {
        public string Foldername { get; set; }
    }

    public class CopyFullPathEventArgs : FilenameEventArgs
    {
    }
}
