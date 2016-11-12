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
    }
}
