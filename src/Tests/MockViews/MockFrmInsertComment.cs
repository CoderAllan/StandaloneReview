namespace StandaloneReview.Tests.MockViews
{
    using System;

    using Model;
    using Views;

    public class MockFrmInsertComment : IFrmInsertComment
    {
        public ApplicationState AppState { get; private set; }
        public event EventHandler<InsertCommentEventArgs> BtnInsertCommentClick;
        public event EventHandler<EventArgs> TxtCommentTextChanged;
        public string TxtCommentText { get; private set; }

        public bool SetBtnInsertCommentEnabledWasCalled { get; private set; }
        public bool SetBtnInsertCommentEnabledValue { get; private set; }
        public void SetBtnInsertCommentEnabled(bool value)
        {
            SetBtnInsertCommentEnabledWasCalled = true;
            SetBtnInsertCommentEnabledValue = value;
        }

        public void SetTxtCommentText(string text)
        {
            TxtCommentText = text;
        }

        public void FireTxtCommentTextChangedEvent()
        {
            if (TxtCommentTextChanged == null)
            {
                throw new NotImplementedException();
            }
            TxtCommentTextChanged(null, EventArgs.Empty);
        }
    }
}
