namespace StandaloneReview.Tests
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Model;
    using MockViews;
    using Presenters;

    [TestClass]
    public class FrmStandaloneReviewPresenterTests
    {
        [TestMethod]
        public void ContextMenuStripOpening_LineSelectionBeforeLine_ExpectNoCall()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReviewedFile = GetReviewFileCommentOnSingleLine(17);
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireContextMenuStripOpeningEvent(13, 19);

            // Assert
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsWasCalled);
            Assert.IsFalse(mockView.EnableDisableContextMenuToolsstripItemsCalledValue);
        }

        [TestMethod]
        public void ContextMenuStripOpening_LineSelectionOnTheLine_ExpectCall()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReviewedFile = mockView.AppState.CurrentReviewedFile = GetReviewFileCommentOnSingleLine(17);
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireContextMenuStripOpeningEvent(17, 19);

            // Assert
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsWasCalled);
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsCalledValue);
        }

        [TestMethod]
        public void ContextMenuStripOpening_LineSelectionAfteLine_ExpectNoCall()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReviewedFile = GetReviewFileCommentOnSingleLine(17);
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireContextMenuStripOpeningEvent(19, 19);

            // Assert
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsWasCalled);
            Assert.IsFalse(mockView.EnableDisableContextMenuToolsstripItemsCalledValue);
        }

        private ReviewedFile GetReviewFileCommentOnSingleLine(int line)
        {
            return new ReviewedFile
            {
                Comments = new List<ReviewComment>
                {
                    new ReviewComment
                    {
                        Line = line
                    }
                }
            };
        }

        [TestMethod]
        public void ContextMenuStripOpening_SelectionCaretBefore_ExpectNoCall()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReviewedFile = GetReviewFileCommentSelection(17, 14, 21, 29);
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireContextMenuStripOpeningEvent(13, 19);

            // Assert
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsWasCalled);
            Assert.IsFalse(mockView.EnableDisableContextMenuToolsstripItemsCalledValue);
        }

        [TestMethod]
        public void ContextMenuStripOpening_CaretInSelection_ExpectCall()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReviewedFile = GetReviewFileCommentSelection(17, 14, 21, 29);
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireContextMenuStripOpeningEvent(18, 19);

            // Assert
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsWasCalled);
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsCalledValue);
        }

        [TestMethod]
        public void ContextMenuStripOpening_CaretInSelectionOnStartLine_ExpectCall()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReviewedFile = GetReviewFileCommentSelection(17, 14, 21, 29);
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireContextMenuStripOpeningEvent(17, 19);

            // Assert
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsWasCalled);
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsCalledValue);
        }

        [TestMethod]
        public void ContextMenuStripOpening_CaretInSelectionOnEndLine_ExpectCall()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReviewedFile = GetReviewFileCommentSelection(17, 14, 21, 29);
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireContextMenuStripOpeningEvent(21, 19);

            // Assert
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsWasCalled);
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsCalledValue);
        }

        [TestMethod]
        public void ContextMenuStripOpening_CaretInSingleLineSelection_ExpectCall()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReviewedFile = GetReviewFileCommentSelection(17, 14, 17, 29);
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireContextMenuStripOpeningEvent(17, 19);

            // Assert
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsWasCalled);
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsCalledValue);
        }

        [TestMethod]
        public void ContextMenuStripOpening_SelectionCaretAfter_ExpectNoCall()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReviewedFile = GetReviewFileCommentSelection(17, 14, 21, 29);
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireContextMenuStripOpeningEvent(33, 19);

            // Assert
            Assert.IsTrue(mockView.EnableDisableContextMenuToolsstripItemsWasCalled);
            Assert.IsFalse(mockView.EnableDisableContextMenuToolsstripItemsCalledValue);
        }

        private ReviewedFile GetReviewFileCommentSelection(int selectionLineStart, int selectionColumnStart, int selectionLineEnd, int selectionColumnEnd)
        {
            return new ReviewedFile
            {
                Comments = new List<ReviewComment>
                {
                    new ReviewComment
                    {
                        SelectionStartLine = selectionLineStart,
                        SelectionStartColumn = selectionColumnStart,
                        SelectionEndLine = selectionLineEnd,
                        SelectionEndColumn = selectionColumnEnd
                    }
                }
            };
        }

        [TestMethod]
        public void ExitMenuItem_ClickedReviewSaved_ExpectNoMessageBox()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReview = new Review { Saved = true };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireExitMenuClickedEvent();

            // Assert
            Assert.IsTrue(mockView.CloseApplicationWasCalled);
            Assert.IsFalse(mockView.MessageBoxUnsavedCommentsWarningOkCancelWasCalled);
        }

        [TestMethod]
        public void ExitMenuItem_ClickedReviewUnsavedNoComments_ExpectNoMessageBox()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReview = new Review { Saved = false };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireExitMenuClickedEvent();

            // Assert
            Assert.IsTrue(mockView.CloseApplicationWasCalled);
            Assert.IsFalse(mockView.MessageBoxUnsavedCommentsWarningOkCancelWasCalled);
        }

        [TestMethod]
        public void ExitMenuItem_ClickedReviewUnsavedWithCommentsCancel_ExpectMessageBox()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReview = new Review { Saved = false };
            mockView.AppState.CurrentReviewedFile = new ReviewedFile
            {
                Comments = new List<ReviewComment>
                {
                    new ReviewComment()
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireExitMenuClickedEvent();

            // Assert
            Assert.IsFalse(mockView.CloseApplicationWasCalled);
            Assert.IsTrue(mockView.MessageBoxUnsavedCommentsWarningOkCancelWasCalled);
        }

        [TestMethod]
        public void ExitMenuItem_ClickedReviewUnsavedWithCommentsOkButtonPressed_ExpectMessageBox()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReview = new Review { Saved = false };
            mockView.AppState.CurrentReviewedFile = new ReviewedFile
            {
                Comments = new List<ReviewComment>
                {
                    new ReviewComment()
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireExitMenuClickedEvent(true);

            // Assert
            Assert.IsTrue(mockView.CloseApplicationWasCalled);
            Assert.IsTrue(mockView.MessageBoxUnsavedCommentsWarningOkCancelWasCalled);
        }

        [TestMethod]
        public void FrmStandaloneReviewTitle_ApplicationStarted_ExpectNoStar()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();

            // Act
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Assert
            Assert.IsTrue(mockView.SetFrmStandaloneReviewTitleWasCalled);
            Assert.IsFalse(mockView.SetFrmStandaloneReviewTitleValue.EndsWith(" *"));
        }

        [TestMethod]
        public void FrmStandaloneReviewTitle_CommentAdded_ExpectStar()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReview = new Review
            {
                ReviewedFiles = new Dictionary<string, ReviewedFile>(),
                Saved = true
            };
            mockView.AppState.CurrentReviewedFile = new ReviewedFile
            {
                Filename = "Test",
                Comments = new List<ReviewComment>()
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);
            Assert.IsTrue(mockView.SetFrmStandaloneReviewTitleWasCalled);
            Assert.IsFalse(mockView.SetFrmStandaloneReviewTitleValue.EndsWith(" *")); // The title should not contain a star before adding a comment
            mockView.AppState.WorkingComment = new ReviewComment
            {
                Comment = "Test",
                LineText = "Test",
                Line = 5
            };

            // Act
            mockView.FireCommitCommentToolStripMenuItemClickEvent(false);

            // Assert
            Assert.IsTrue(mockView.SetFrmStandaloneReviewTitleWasCalled);
            Assert.IsTrue(mockView.SetFrmStandaloneReviewTitleValue.EndsWith(" *")); 
        }

        [TestMethod]
        public void FrmStandaloneReviewTitle_CommentDeleted_ExpectStar()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReview = new Review
            {
                ReviewedFiles = new Dictionary<string, ReviewedFile>(),
                Saved = true
            };
            mockView.AppState.CurrentReviewedFile = new ReviewedFile
            {
                Filename = "Test",
                Comments = new List<ReviewComment>
                {
                    new ReviewComment
                    {
                        Line = 5
                    }
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);
            Assert.IsTrue(mockView.SetFrmStandaloneReviewTitleWasCalled);
            Assert.IsFalse(mockView.SetFrmStandaloneReviewTitleValue.EndsWith(" *")); // The title should not contain a star before deleting a comment

            // Act
            mockView.FireDeleteCommentToolStripMenuItemClickEvent(5, 5);

            // Assert
            Assert.IsTrue(mockView.SetFrmStandaloneReviewTitleWasCalled);
            Assert.IsTrue(mockView.SetFrmStandaloneReviewTitleValue.EndsWith(" *")); // The title should contain a star after a comment is deleted
        }
    }
}
