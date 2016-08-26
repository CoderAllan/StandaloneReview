using StandaloneReview.Model;

namespace StandaloneReview.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Ploeh.AutoFixture;

    using MockViews;
    using Presenters;

    [TestClass]
    public class FrmInsertCommentPresenterTests
    {
        private readonly Fixture _fixture = new Fixture();

        [TestMethod]
        public void InsertCommentButton_NoTextEntered_ExpectButtonDisabled()
        {
            // Arrange
            var mockView = new MockFrmInsertComment();
            var presenter = new FrmInsertCommentPresenter(mockView);

            // Act
            mockView.SetTxtCommentText("");
            mockView.FireTxtCommentTextChangedEvent();

            // Assert
            Assert.IsTrue(mockView.SetBtnInsertCommentEnabledWasCalled);
            Assert.IsFalse(mockView.SetBtnInsertCommentEnabledValue);
        }

        [TestMethod]
        public void InsertCommentButton_TextEntered_ExpectButtonEnabled()
        {
            // Arrange
            var mockView = new MockFrmInsertComment();
            var presenter = new FrmInsertCommentPresenter(mockView);

            // Act
            mockView.SetTxtCommentText(_fixture.Create<string>());
            mockView.FireTxtCommentTextChangedEvent();

            // Assert
            Assert.IsTrue(mockView.SetBtnInsertCommentEnabledWasCalled);
            Assert.IsTrue(mockView.SetBtnInsertCommentEnabledValue);
        }

        [TestMethod]
        public void InsertComment_InsertCommentWorkingCommentIsNull_ExpectReviewCommentAdded()
        {
            // Arrange
            var mockView = new MockFrmInsertComment();
            mockView.AppState.WorkingComment = null;
            var presenter = new FrmInsertCommentPresenter(mockView);
            var comment = _fixture.Create<string>();

            // Act
            mockView.FireBtnInsertCommentClickEvent(comment);

            // Assert
            Assert.IsNotNull(mockView.AppState.WorkingComment);
            Assert.AreEqual(comment, mockView.AppState.WorkingComment.Comment);
        }
    }
}
