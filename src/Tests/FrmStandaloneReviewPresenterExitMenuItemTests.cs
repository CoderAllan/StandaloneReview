namespace StandaloneReview.Tests
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Model;
    using MockViews;
    using Presenters;

    [TestClass]
    public class FrmStandaloneReviewPresenterExitMenuItemTests
    {

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
    }
}
