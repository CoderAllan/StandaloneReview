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
    }
}
