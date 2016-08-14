namespace StandaloneReview.Tests.MockViews
{
    using System;
    
    using Contracts;
    using Model;
    using Views;

    public class MockFrmStandaloneReview : IFrmStandaloneReview
    {
        public ISystemIO SystemIO { get; private set; }
        public ApplicationState AppState { get; private set; }
        public event EventHandler<LoadEventArgs> BtnLoadClick;
        public event EventHandler<EventArgs> BtnNewClick;
        public event EventHandler<SaveEventArgs> BtnSaveClick;
        public event EventHandler<EventArgs> CommitComment;
        public event EventHandler<ReviewCommentEventArgs> SetReviewComment;
        public event EventHandler<CaretPositionEventArgs> DeleteComment;
        public event EventHandler<CaretPositionEventArgs> ContextMenuStripOpening;

        public MockFrmStandaloneReview()
        {
            AppState = new ApplicationState();
        }

        public void FireContextMenuStripOpeningEvent(int line, int column)
        {
            if (ContextMenuStripOpening == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                var caretPositionEventArgs = new CaretPositionEventArgs
                {
                    Line = line,
                    Column = column
                };
                ContextMenuStripOpening(null, caretPositionEventArgs);
            }
        }
        
        public void SetSyntaxHighlighting(string fileType)
        {
            throw new NotImplementedException();
        }

        public void SetTextEditorControlText(string textEditorControlName, string text)
        {
            throw new NotImplementedException();
        }

        public int GetTextOffset(int column, int line)
        {
            throw new NotImplementedException();
        }

        public void AddMarker(int offset, int length, string tooltipText)
        {
            throw new NotImplementedException();
        }

        public void ResetTextEditor()
        {
            throw new NotImplementedException();
        }

        public void EnableDisableMenuToolstripItems()
        {
            throw new NotImplementedException();
        }

        public bool MessageBoxUnsavedCommentsWarningOkCancel()
        {
            throw new NotImplementedException();
        }

        public bool EnableDisableContextMenuToolsstripItemsWasCalled { get; set; }
        public bool EnableDisableContextMenuToolsstripItemsCalledValue { get; set; }
        public void EnableDisableContextMenuToolsstripItems(bool menuToolStripEnabled)
        {
            EnableDisableContextMenuToolsstripItemsWasCalled = true;
            EnableDisableContextMenuToolsstripItemsCalledValue = menuToolStripEnabled;
        }
    }
}
