namespace StandaloneReview.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Model;
    using MockViews;
    using Presenters;

    [TestClass]
    public class FrmStandaloneReviewPresenterSelectedTabChangedTests
    {
        [TestMethod]
        public void FrmStandaloneReviewSelectedTabChanged_NoComments_ExpectNoCallsToAddNavigatorCommentMarkerNavigator()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReview = new Review
            {
                ReviewedFiles = new Dictionary<string, ReviewedFile>(),
                Saved = true
            };
            const string filename = "Test";
            var reviewdFile = new ReviewedFile
            {
                Filename = filename,
                Comments = new List<ReviewComment>()
            };
            mockView.AppState.CurrentReview.ReviewedFiles = new Dictionary<string, ReviewedFile>();
            mockView.AppState.CurrentReview.ReviewedFiles.Add(filename, reviewdFile);
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireSelectedTabChanged(filename);

            // Assert
            Assert.IsTrue(mockView.RemoveAllNavigatorShapesWasCalled);
            Assert.IsFalse(mockView.AddNavigatorCommentMarkerWasCalled);
            Assert.IsTrue(mockView.AddGreyedAreaWasCalled);
        }

        [TestMethod]
        public void FrmStandaloneReviewSelectedTabChanged_OneComment_ExpectCallsToAddNavigatorCommentMarkerNavigator()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReview = new Review
            {
                ReviewedFiles = new Dictionary<string, ReviewedFile>(),
                Saved = true
            };
            const string filename = "Test";
            var reviewdFile = new ReviewedFile
            {
                Filename = filename,
                Comments = new List<ReviewComment>
                {
                    new ReviewComment
                    {
                        Line = 5
                    }
                }
            };
            mockView.AppState.CurrentReview.ReviewedFiles = new Dictionary<string, ReviewedFile>();
            mockView.AppState.CurrentReview.ReviewedFiles.Add(filename, reviewdFile);
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireSelectedTabChanged(filename);

            // Assert
            Assert.IsTrue(mockView.RemoveAllNavigatorShapesWasCalled);
            Assert.IsTrue(mockView.AddNavigatorCommentMarkerWasCalled);
            Assert.AreEqual(mockView.AddNavigatorCommentMarkerNumberOfCalls, 1);
            Assert.IsTrue(mockView.AddGreyedAreaWasCalled);
        }

        [TestMethod]
        public void FrmStandaloneReviewSelectedTabChanged_TwoComments_ExpectCallsToAddNavigatorCommentMarkerNavigator()
        {
            // Arrange
            var mockView = new MockFrmStandaloneReview();
            mockView.AppState.CurrentReview = new Review
            {
                ReviewedFiles = new Dictionary<string, ReviewedFile>(),
                Saved = true
            };
            const string filename = "Test";
            var reviewdFile = new ReviewedFile
            {
                Filename = filename,
                Comments = new List<ReviewComment>
                {
                    new ReviewComment { Line = 5 },
                    new ReviewComment { SelectionStartLine = 15 }
                }
            };
            mockView.AppState.CurrentReview.ReviewedFiles = new Dictionary<string, ReviewedFile>();
            mockView.AppState.CurrentReview.ReviewedFiles.Add(filename, reviewdFile);
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireSelectedTabChanged(filename);

            // Assert
            Assert.IsTrue(mockView.RemoveAllNavigatorShapesWasCalled);
            Assert.IsTrue(mockView.AddNavigatorCommentMarkerWasCalled);
            Assert.AreEqual(mockView.AddNavigatorCommentMarkerNumberOfCalls, 2);
            Assert.IsTrue(mockView.AddGreyedAreaWasCalled);
        }
    }
}
