namespace StandaloneReview.Presenters
{
    using System;
    using System.Linq;

    using Model;
    using Views;
    using Contracts;

    public class FrmPreviewPresenter
    {
        private readonly IFrmPreview _view;
        private readonly ISystemIO _systemIO;

        public FrmPreviewPresenter(IFrmPreview view)
        {
            _view = view;
            _systemIO = new SystemIO();

            Initialize();
        }

        private void Initialize()
        {
            _view.FrmPreviewLoad += DoFrmPreviewLoad;
            _view.BtnSaveClick += DoBtnSaveClick;
            _view.BtnMoveFileUpClick += DoBtnMoveFileUpClick;
            _view.BtnMoveFileDownClick += DoBtnMoveFileDownClick;
            _view.BtnMoveCommentUpClick += DoBtnMoveCommentUpClick;
            _view.BtnMoveCommentDownClick += DoBtnMoveCommentDownClick;
            _view.LstFilesSelectedIndexChanged += DoLstFilesSelectedIndexChanged;
            _view.LstCommentsSelectedIndexChanged += DoLstCommentsSelectedIndexChanged;
        }

        private void DoFrmPreviewLoad(object sender, EventArgs e)
        {
            _view.SetTxtPreviewText(_view.AppState.CurrentReview.ToString());
            _view.EnableDisableMoveFileButtons(false, false);
            _view.EnableDisableMoveCommentButtons(false, false);
            foreach (var reviewedFile in _view.AppState.CurrentReview.ReviewedFiles.OrderBy(p => _view.AppState.CurrentReview.ReviewedFiles[p.Key].Position))
            {
                var listboxFilesItem = new ListboxFilesItem
                {
                    Filename = _systemIO.PathGetFilename(_view.AppState.CurrentReview.ReviewedFiles[reviewedFile.Key].Filename),
                    FullFilename = reviewedFile.Key
                };
                _view.InsertFilenameInListbox(listboxFilesItem);
                var selectedIndexChangedEventArgs = new SelectedIndexChangedEventArgs
                {
                    Filename = listboxFilesItem.FullFilename
                };
                DoLstFilesSelectedIndexChanged(null, selectedIndexChangedEventArgs);
            }
        }

        private void DoLstFilesSelectedIndexChanged(object sender, SelectedIndexChangedEventArgs e)
        {
            if (_view.AppState.CurrentReview.ReviewedFiles.ContainsKey(e.Filename))
            {
                _view.ClearLstComments();
                foreach (var comment in _view.AppState.CurrentReview.ReviewedFiles[e.Filename].Comments.OrderBy(p => p.Position))
                {
                    const int maxListboxCommentLength = 20;
                    string trimmedCommentLineText = comment.LineText.Trim();
                    if (trimmedCommentLineText.Length > maxListboxCommentLength)
                    {
                        trimmedCommentLineText = trimmedCommentLineText.Substring(0, maxListboxCommentLength) + "...";
                    }
                    string trimmedComment = comment.Comment.Trim();
                    if (trimmedComment.Length > maxListboxCommentLength)
                    {
                        trimmedComment = trimmedComment.Substring(0, maxListboxCommentLength) + "...";
                    }
                    _view.InsertCommentInListbox(trimmedComment + " - " + trimmedCommentLineText);
                }
            }
            bool btnMoveFileUpEnabled = e.Position != 0 && _view.LstFilesItemsCount > 1 && _view.LstFilesSelectedIndex > -1;
            bool btnMoveFileDownEnabled = e.Position != _view.LstFilesItemsCount - 1 && _view.LstFilesItemsCount > 1 && _view.LstFilesSelectedIndex > -1;
            _view.EnableDisableMoveCommentButtons(btnMoveFileUpEnabled, btnMoveFileDownEnabled);
        }

        private void DoLstCommentsSelectedIndexChanged(object sender, SelectedIndexChangedEventArgs e)
        {
            bool btnMoveCommentUpEnabled = e.Position != 0 && _view.LstCommentsItemsCount > 1 && _view.LstCommentsSelectedIndex > -1;
            bool btnMoveCommentDownEnabled = e.Position != _view.LstCommentsItemsCount - 1 && _view.LstCommentsItemsCount > 1 && _view.LstCommentsSelectedIndex > -1;
            _view.EnableDisableMoveCommentButtons(btnMoveCommentUpEnabled, btnMoveCommentDownEnabled);
        }

        private void DoBtnSaveClick(object sender, SaveEventArgs e)
        {
            string review = _view.AppState.CurrentReview.ToString();
            _systemIO.WriteAllText(e.Filename, review);
            _view.AppState.CurrentReview.Saved = true;
        }

        private void DoBtnMoveFileUpClick(object sender, FileMoveEventArgs e)
        {
            _view.SetTxtPreviewText(_view.AppState.CurrentReview.ToString());
        }

        private void DoBtnMoveFileDownClick(object sender, FileMoveEventArgs e)
        {
            _view.SetTxtPreviewText(_view.AppState.CurrentReview.ToString());
        }

        private void DoBtnMoveCommentUpClick(object sender, CommentMoveEventArgs e)
        {
            if (e.Position > 0)
            {
                var commentToMoveUp = _view.AppState.CurrentReview.ReviewedFiles[e.Filename].Comments.FirstOrDefault(p => p.Position == e.Position);
                var commentToMoveDown = _view.AppState.CurrentReview.ReviewedFiles[e.Filename].Comments.FirstOrDefault(p => p.Position == e.Position - 1);
                if (commentToMoveUp != null)
                {
                    commentToMoveUp.Position = e.Position - 1;
                }
                if (commentToMoveDown != null)
                {
                    commentToMoveDown.Position = e.Position;
                }
                var selectedIndexChangedEventArgs = new SelectedIndexChangedEventArgs
                {
                    Filename = e.Filename
                };
                DoLstFilesSelectedIndexChanged(sender, selectedIndexChangedEventArgs);
                _view.SetTxtPreviewText(_view.AppState.CurrentReview.ToString());
            }
        }

        private void DoBtnMoveCommentDownClick(object sender, CommentMoveEventArgs e)
        {
            if (e.Position < _view.LstCommentsItemsCount - 1)
            {
                var commentToMoveUp = _view.AppState.CurrentReview.ReviewedFiles[e.Filename].Comments.FirstOrDefault(p => p.Position == e.Position + 1);
                var commentToMoveDown = _view.AppState.CurrentReview.ReviewedFiles[e.Filename].Comments.FirstOrDefault(p => p.Position == e.Position);
                if (commentToMoveUp != null)
                {
                    commentToMoveUp.Position = e.Position;
                }
                if (commentToMoveDown != null)
                {
                    commentToMoveDown.Position = e.Position + 1;
                }
                var selectedIndexChangedEventArgs = new SelectedIndexChangedEventArgs
                {
                    Filename = e.Filename
                };
                DoLstFilesSelectedIndexChanged(sender, selectedIndexChangedEventArgs);
                _view.SetTxtPreviewText(_view.AppState.CurrentReview.ToString());
            }
        }
    }
}
