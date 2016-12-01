namespace StandaloneReview.Tests.MockViews
{
    using System;
    using System.ComponentModel;
    
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
        public event EventHandler<CancelEventArgs> BtnExitClick;
        public event EventHandler<CommitCommentEventArgs> CommitComment;
        public event EventHandler<ReviewCommentEventArgs> SetReviewComment;
        public event EventHandler<CaretPositionEventArgs> DeleteComment;
        public event EventHandler<CaretPositionEventArgs> EditComment;
        public event EventHandler<CaretPositionEventArgs> ContextMenuStripOpening;
        public event EventHandler<SelectedTabChangedEventArgs> SelectedTabChanged;
        public event EventHandler<OpenFolderEventArgs> OpenContainingFolder;
        public event EventHandler<CopyFullPathEventArgs> CopyFullPath;

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
            var caretPositionEventArgs = new CaretPositionEventArgs
            {
                Line = line,
                Column = column
            };
            ContextMenuStripOpening(null, caretPositionEventArgs);
        }

        public void FireDeleteCommentToolStripMenuItemClickEvent(int line, int column)
        {
            if (DeleteComment == null)
            {
                throw new NotImplementedException();
            }
            var caretPositionEventArgs = new CaretPositionEventArgs
            {
                Line = line,
                Column = column
            };
            DeleteComment(null, caretPositionEventArgs);
        }

        public void FireCommitCommentToolStripMenuItemClickEvent(bool editCurrentWorkingComment)
        {
            if (CommitComment == null)
            {
                throw new NotImplementedException();
            }
            var commitCommentEventArgs = new CommitCommentEventArgs
            {
                EditCurrentWorkingComment = editCurrentWorkingComment
            };
            CommitComment(null, commitCommentEventArgs);
        }


        public bool SetFrmStandaloneReviewTitleWasCalled { get; private set; }
        public string SetFrmStandaloneReviewTitleValue { get; private set; }
        public void SetFrmStandaloneReviewTitle(string text)
        {
            SetFrmStandaloneReviewTitleWasCalled = true;
            SetFrmStandaloneReviewTitleValue = text;
        }

        public void SetSyntaxHighlighting(string fileType)
        {
            throw new NotImplementedException();
        }

        public void SetTextEditorControlText(string textEditorControlName, string text)
        {
            throw new NotImplementedException();
        }

        public string AddNewTab(string filename, int newTabPageNumber)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllOpenTabs()
        {
            throw new NotImplementedException();
        }

        public void SelectOpenTab(string tabName)
        {
            throw new NotImplementedException();
        }

        public bool GetTextOffsetWasCalled { get; private set; }
        public int GetTextOffsetColumnValue { get; private set; }
        public int GetTextOffsetLineValue { get; private set; }
        public int GetTextOffsetReturnValue { get; set; }
        public int GetTextOffset(int column, int line)
        {
            GetTextOffsetWasCalled = true;
            GetTextOffsetColumnValue = column;
            GetTextOffsetLineValue = line;
            return GetTextOffsetReturnValue;
        }

        public bool AddNavigatorCurrentLineMarkerWasCalled { get; private set; }
        public int AddNavigatorCurrentLineMarkerLineValue { get; private set; }
        public void AddNavigatorCurrentLineMarker(int line)
        {
            AddNavigatorCurrentLineMarkerWasCalled = true;
            AddNavigatorCurrentLineMarkerLineValue = line;
        }

        public bool AddNavigatorCommentMarkerWasCalled { get; private set; }
        public int AddNavigatorCommentMarkerLineValue { get; private set; }
        public int AddNavigatorCommentMarkerNumberOfCalls { get; private set; }
        public void AddNavigatorCommentMarker(int line)
        {
            AddNavigatorCommentMarkerWasCalled = true;
            AddNavigatorCommentMarkerLineValue = line;
            AddNavigatorCommentMarkerNumberOfCalls++;
        }

        public bool RemoveNavigatorCommentMarkerWasCalled { get; private set; }
        public int RemoveNavigatorCommentMarkerLineValue { get; private set; }
        public void RemoveNavigatorCommentMarker(int line)
        {
            RemoveNavigatorCommentMarkerWasCalled = true;
            RemoveNavigatorCommentMarkerLineValue = line;
        }

        public bool AddMarkerWasCalled { get; private set; }
        public int AddMarkerOffsetValue { get; private set; }
        public int AddMarkerLengthValue { get; private set; }
        public string AddMarkerTooltipTextValue { get; private set; }
        public void AddMarker(int offset, int length, string tooltipText)
        {
            AddMarkerWasCalled = true;
            AddMarkerOffsetValue = offset;
            AddMarkerLengthValue = length;
            AddMarkerTooltipTextValue = tooltipText;
        }

        public bool AddGreyedAreaWasCalled { get; private set; }
        public void AddGreyedArea()
        {
            AddGreyedAreaWasCalled = true;
        }

        public bool RemoveAllNavigatorShapesWasCalled { get; private set; }
        public void RemoveAllNavigatorShapes()
        {
            RemoveAllNavigatorShapesWasCalled = true;
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
            MessageBoxUnsavedCommentsWarningOkCancelValue = discardUnsavedComments;
            var e = new CancelEventArgs
            {
                Cancel = false
            };
            BtnExitClick(null, e);
        }

        public bool CloseApplicationWasCalled { get; private set; }
        public void CloseApplication()
        {
            CloseApplicationWasCalled = true;
        }

        public void FireSelectedTabChanged(string filename)
        {
            if (SelectedTabChanged == null)
            {
                throw new NotImplementedException();
            }
            var e = new SelectedTabChangedEventArgs()
            {
                Filename = filename
            };
            SelectedTabChanged(null, e);
        }
    }
}
