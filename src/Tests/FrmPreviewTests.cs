
namespace StandaloneReview.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ploeh.AutoFixture;

    using Model;
    using MockViews;
    using Presenters;

    [TestClass]
    public class FrmPreviewTests
    {
        private readonly Fixture _fixture = new Fixture();

        private MockFrmPreview CreateReviewWithTwoComments(out ReviewComment commentTop, out ReviewComment commentSecond, out ReviewedFile reviewedFile)
        {
            reviewedFile = _fixture.Build<ReviewedFile>()
                                   .With(p => p.Comments, new List<ReviewComment>())
                                   .Create();
            commentTop = _fixture.Build<ReviewComment>()
                                 .With(p => p.Position, 0)
                                 .Create();
            commentSecond = _fixture.Build<ReviewComment>()
                                    .With(p => p.Position, 1)
                                    .Create();
            reviewedFile.Comments.Add(commentTop);
            reviewedFile.Comments.Add(commentSecond);
            var reviewedFiles = new Dictionary<string, ReviewedFile>();
            reviewedFiles.Add(reviewedFile.Filename, reviewedFile);
            var mockView = new MockFrmPreview();
            mockView.AppState.CurrentReview = _fixture.Build<Review>()
                                                      .With(p => p.ReviewedFiles, reviewedFiles)
                                                      .Create();
            return mockView;
        }

        [TestMethod]
        public void PreviewMoveComments_MoveTopCommentUp_ExpectCorrectPosition()
        {
            // Arrange
            ReviewComment commentTop;
            ReviewComment commentSecond;
            ReviewedFile reviewedFile;
            MockFrmPreview mockView = CreateReviewWithTwoComments(out commentTop, out commentSecond, out reviewedFile);
            var presenter = new FrmPreviewPresenter(mockView);

            // Act
            mockView.FireMoveCommentUpEvent(reviewedFile.Filename, 0);

            // Assert
            Assert.AreEqual(0, commentTop.Position); // Nothing should be changed when trying to moved top comment up
            Assert.AreEqual(1, commentSecond.Position); // Nothing should be changed when trying to moved top comment up
            Assert.IsFalse(mockView.SetTxtPreviewTextWasCalled);
        }

        [TestMethod]
        public void PreviewMoveComments_MoveSecondCommentUp_ExpectCorrectPosition()
        {
            // Arrange
            ReviewComment commentTop;
            ReviewComment commentSecond;
            ReviewedFile reviewedFile;
            MockFrmPreview mockView = CreateReviewWithTwoComments(out commentTop, out commentSecond, out reviewedFile);
            var presenter = new FrmPreviewPresenter(mockView);

            // Act
            mockView.FireMoveCommentUpEvent(reviewedFile.Filename, 1);

            // Assert
            Assert.AreEqual(1, commentTop.Position); // Top comment up should be moved down
            Assert.AreEqual(0, commentSecond.Position); // Second comment up should be moved up
            Assert.IsTrue(mockView.SetTxtPreviewTextWasCalled);
        }

        [TestMethod]
        public void PreviewMoveComments_MoveBottomCommentDown_ExpectCorrectPosition()
        {
            // Arrange
            ReviewComment commentTop;
            ReviewComment commentSecond;
            ReviewedFile reviewedFile;
            MockFrmPreview mockView = CreateReviewWithTwoComments(out commentTop, out commentSecond, out reviewedFile);
            var presenter = new FrmPreviewPresenter(mockView);

            // Act
            mockView.FireMoveCommentDownEvent(reviewedFile.Filename, 1);

            // Assert
            Assert.AreEqual(0, commentTop.Position); // Nothing should be changed when trying to moved bottom comment down
            Assert.AreEqual(1, commentSecond.Position); // Nothing should be changed when trying to moved bottom comment down
            Assert.IsFalse(mockView.SetTxtPreviewTextWasCalled);
        }

        [TestMethod]
        public void PreviewMoveComments_MoveTopCommentDown_ExpectCorrectPosition()
        {
            // Arrange
            ReviewComment commentTop;
            ReviewComment commentSecond;
            ReviewedFile reviewedFile;
            MockFrmPreview mockView = CreateReviewWithTwoComments(out commentTop, out commentSecond, out reviewedFile);
            var presenter = new FrmPreviewPresenter(mockView);

            // Act
            mockView.FireMoveCommentDownEvent(reviewedFile.Filename, 0);

            // Assert
            Assert.AreEqual(1, commentTop.Position); // Top comment up should be moved down
            Assert.AreEqual(0, commentSecond.Position); // Second comment up should be moved up
            Assert.IsTrue(mockView.SetTxtPreviewTextWasCalled);
        }

        private MockFrmPreview CreateReviewWithThreeComments(out ReviewedFile reviewedFile)
        {
            reviewedFile = _fixture.Build<ReviewedFile>()
                                       .With(p => p.Comments, new List<ReviewComment>())
                                       .Create();
            var commentTop = _fixture.Build<ReviewComment>()
                                     .With(p => p.Position, 0)
                                     .Create();
            var commentSecond = _fixture.Build<ReviewComment>()
                                        .With(p => p.Position, 1)
                                        .Create();
            var commentThird = _fixture.Build<ReviewComment>()
                                       .With(p => p.Position, 2)
                                       .Create();
            reviewedFile.Comments.Add(commentTop);
            reviewedFile.Comments.Add(commentSecond);
            reviewedFile.Comments.Add(commentThird);
            var reviewedFiles = new Dictionary<string, ReviewedFile>();
            reviewedFiles.Add(reviewedFile.Filename, reviewedFile);
            var mockView = new MockFrmPreview();
            mockView.AppState.CurrentReview = _fixture.Build<Review>()
                                                      .With(p => p.ReviewedFiles, reviewedFiles)
                                                      .Create();
            return mockView;
        }

        [TestMethod]
        public void PreviewLstCommentsSelectedIndexChanged_TopCommentClicked_ExpectMoveUpButtonDisableAndMoveDownButtonEnabled()
        {
            // Arrange
            ReviewedFile reviewedFile;
            MockFrmPreview mockView = CreateReviewWithThreeComments(out reviewedFile);
            var presenter = new FrmPreviewPresenter(mockView);

            // Act
            mockView.FireLstCommentsSelectedIndexChanged(reviewedFile.Filename, 0);

            // Assert
            Assert.IsTrue(mockView.EnableDisableMoveCommentButtonsWasCalled);
            Assert.IsFalse(mockView.BtnMoveCommentUpEnabledValue);
            Assert.IsTrue(mockView.BtnMoveCommentDownEnabledValue);
        }

        [TestMethod]
        public void PreviewLstCommentsSelectedIndexChanged_SecondCommentClicked_ExpectMoveUpButtonDisableAndMoveDownButtonEnabled()
        {
            // Arrange
            ReviewedFile reviewedFile;
            MockFrmPreview mockView = CreateReviewWithThreeComments(out reviewedFile);
            var presenter = new FrmPreviewPresenter(mockView);

            // Act
            mockView.FireLstCommentsSelectedIndexChanged(reviewedFile.Filename, 1);

            // Assert
            Assert.IsTrue(mockView.EnableDisableMoveCommentButtonsWasCalled);
            Assert.IsTrue(mockView.BtnMoveCommentUpEnabledValue);
            Assert.IsTrue(mockView.BtnMoveCommentDownEnabledValue);
        }

        [TestMethod]
        public void PreviewLstCommentsSelectedIndexChanged_ThirdCommentClicked_ExpectMoveUpButtonDisableAndMoveDownButtonEnabled()
        {
            // Arrange
            ReviewedFile reviewedFile;
            MockFrmPreview mockView = CreateReviewWithThreeComments(out reviewedFile);
            var presenter = new FrmPreviewPresenter(mockView);

            // Act
            mockView.FireLstCommentsSelectedIndexChanged(reviewedFile.Filename, 2);

            // Assert
            Assert.IsTrue(mockView.EnableDisableMoveCommentButtonsWasCalled);
            Assert.IsTrue(mockView.BtnMoveCommentUpEnabledValue);
            Assert.IsFalse(mockView.BtnMoveCommentDownEnabledValue);
        }

        private MockFrmPreview CreateReviewWithThreeFiles(out ReviewedFile reviewedFileTop, out ReviewedFile reviewedFileSecond, out ReviewedFile reviewedFileThird)
        {
            reviewedFileTop = _fixture.Build<ReviewedFile>()
                                          .With(p => p.Comments, new List<ReviewComment>())
                                          .Create();
            reviewedFileSecond = _fixture.Build<ReviewedFile>()
                                             .With(p => p.Comments, new List<ReviewComment>())
                                             .Create();
            reviewedFileThird = _fixture.Build<ReviewedFile>()
                                            .With(p => p.Comments, new List<ReviewComment>())
                                            .Create();
            var reviewedFiles = new Dictionary<string, ReviewedFile>();
            reviewedFiles.Add(reviewedFileTop.Filename, reviewedFileTop);
            reviewedFiles.Add(reviewedFileSecond.Filename, reviewedFileSecond);
            reviewedFiles.Add(reviewedFileThird.Filename, reviewedFileThird);
            var mockView = new MockFrmPreview();
            mockView.AppState.CurrentReview = _fixture.Build<Review>()
                                                      .With(p => p.ReviewedFiles, reviewedFiles)
                                                      .Create();
            return mockView;
        }

        [TestMethod]
        public void PreviewLstFilesSelectedIndexChanged_TopFileClicked_ExpectMoveUpButtonDisableAndMoveDownButtonEnabled()
        {
            // Arrange
            ReviewedFile reviewedFileTop;
            ReviewedFile reviewedFileSecond;
            ReviewedFile reviewedFileThird;
            MockFrmPreview mockView = CreateReviewWithThreeFiles(out reviewedFileTop, out reviewedFileSecond, out reviewedFileThird);
            var presenter = new FrmPreviewPresenter(mockView);

            // Act
            mockView.FireLstFilesSelectedIndexChanged(reviewedFileTop.Filename, 0);

            // Assert
            Assert.IsTrue(mockView.EnableDisableMoveCommentButtonsWasCalled);
            Assert.IsFalse(mockView.BtnMoveCommentUpEnabledValue);
            Assert.IsTrue(mockView.BtnMoveCommentDownEnabledValue);
        }

        [TestMethod]
        public void PreviewLstFilesSelectedIndexChanged_SecondCommentClicked_ExpectMoveUpButtonDisableAndMoveDownButtonEnabled()
        {
            // Arrange
            ReviewedFile reviewedFileTop;
            ReviewedFile reviewedFileSecond;
            ReviewedFile reviewedFileThird;
            MockFrmPreview mockView = CreateReviewWithThreeFiles(out reviewedFileTop, out reviewedFileSecond, out reviewedFileThird);
            var presenter = new FrmPreviewPresenter(mockView);

            // Act
            mockView.FireLstFilesSelectedIndexChanged(reviewedFileSecond.Filename, 1);

            // Assert
            Assert.IsTrue(mockView.EnableDisableMoveCommentButtonsWasCalled);
            Assert.IsTrue(mockView.BtnMoveCommentUpEnabledValue);
            Assert.IsTrue(mockView.BtnMoveCommentDownEnabledValue);
        }

        [TestMethod]
        public void PreviewLstCommentsSelectedIndexChanged_ThirdFileClicked_ExpectMoveUpButtonDisableAndMoveDownButtonEnabled()
        {
            // Arrange
            ReviewedFile reviewedFileTop;
            ReviewedFile reviewedFileSecond;
            ReviewedFile reviewedFileThird;
            MockFrmPreview mockView = CreateReviewWithThreeFiles(out reviewedFileTop, out reviewedFileSecond, out reviewedFileThird);
            var presenter = new FrmPreviewPresenter(mockView);

            // Act
            mockView.FireLstFilesSelectedIndexChanged(reviewedFileThird.Filename, 2);

            // Assert
            Assert.IsTrue(mockView.EnableDisableMoveCommentButtonsWasCalled);
            Assert.IsTrue(mockView.BtnMoveCommentUpEnabledValue);
            Assert.IsFalse(mockView.BtnMoveCommentDownEnabledValue);
        }
    }
}
