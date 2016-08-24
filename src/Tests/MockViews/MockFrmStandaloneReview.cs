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
        public event EventHandler<EventArgs> BtnExitClick;
        public event EventHandler<CommitCommentEventArgs> CommitComment;
        public event EventHandler<ReviewCommentEventArgs> SetReviewComment;
        public event EventHandler<CaretPositionEventArgs> DeleteComment;
        public event EventHandler<CaretPositionEventArgs> EditComment;
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

        public void SetFrmStandaloneReviewTitle(string text)
        {
            throw new NotImplementedException();
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

        public bool MessageBoxUnsavedCommentsWarningOkCancelWasCalled { get; private set; }
        public bool MessageBoxUnsavedCommentsWarningOkCancelValue { get; set; }
        public bool MessageBoxUnsavedCommentsWarningOkCancel()
        {
            MessageBoxUnsavedCommentsWarningOkCancelWasCalled = true;
            return MessageBoxUnsavedCommentsWarningOkCancelValue;
        }

        public bool EnableDisableContextMenuToolsstripItemsWasCalled { get; private set; }
        public bool EnableDisableContextMenuToolsstripItemsCalledValue { get; private set; }
        public void EnableDisableContextMenuToolsstripItems(bool menuToolStripEnabled)
        {
            EnableDisableContextMenuToolsstripItemsWasCalled = true;
            EnableDisableContextMenuToolsstripItemsCalledValue = menuToolStripEnabled;
        }

        public void ShowInsertCommentForm(bool editCurrentWorkingComment)
        {
            throw new NotImplementedException();
        }

        public void SetMarkerTooltip(string tooltipText)
        {
            throw new NotImplementedException();
        }


        public void FireExitMenuClickedEvent(bool discardUnsavedComments = false)
        {
            if (BtnExitClick == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                MessageBoxUnsavedCommentsWarningOkCancelValue = discardUnsavedComments;
                BtnExitClick(null, EventArgs.Empty);
            }
        }

        public bool CloseApplicationWasCalled { get; private set; }
        public void CloseApplication()
        {
            CloseApplicationWasCalled = true;
        }
    }
}
