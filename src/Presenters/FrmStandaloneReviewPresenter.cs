namespace StandaloneReview.Presenters
{
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;

    using Model;
    using Views;

    public partial class FrmStandaloneReviewPresenter
    {
        private readonly IFrmStandaloneReview _view;

        public FrmStandaloneReviewPresenter(IFrmStandaloneReview view)
        {
            _view = view;

            Initialize();
        }

        public void Initialize()
        {
            _view.BtnLoadClick += DoLoadClick;
            _view.BtnNewClick += DoNewClick;
            _view.BtnSaveClick += DoSaveClick;
            _view.BtnExitClick += DoExitClick;
            _view.ContextMenuStripOpening += DoContextMenuStripOpening;
            _view.OpenContainingFolder += DoOpenContainingFolder;
            _view.CopyFullPath += DoCopyFullPath;

            // FrmStandaloneReviewPresenter.Comment.cs
            _view.CommitComment += DoCommitComment;
            _view.SetReviewComment += DoSetReviewComment;
            _view.DeleteComment += DoDeleteComment;
            _view.EditComment += DoEditComment;

            // FrmStandaloneReviewPresenter.Tabs.cs
            _view.SelectedTabChanged += DoSelectedTabChanged;
            _view.CloseTabClick += DoCloseTabClick;
            _view.CloseAllTabsClick += DoCloseAllTabsClick;
            _view.CloseAllTabsButThisClick += DoCloseAllTabsButThisClick;

            _view.SetFrmStandaloneReviewTitle();
        }

        private void DoLoadClick(object sender, LoadEventArgs args)
        {
            bool openExistingReviewFile = false;
            if (_view.AppState.CurrentReview.ReviewedFiles.ContainsKey(args.Filename))
            {
                if (_view.IsTabOpen(args.Filename))
                {
                    _view.SelectOpenTab(args.Filename);
                    return;
                }
                openExistingReviewFile = true;
            }
            else
            {
                _view.AppState.CurrentReviewedFile = new ReviewedFile
                {
                    Filename = args.Filename,
                    Comments = new List<ReviewComment>(),
                    Position = _view.AppState.CurrentReview.ReviewedFiles.Count
                };
                _view.AppState.CurrentReview.ReviewedFiles.Add(args.Filename, _view.AppState.CurrentReviewedFile);
                _view.RemoveAllNavigatorShapes();
            }
            var text = _view.SystemIO.FileReadAllText(args.Filename);
            string textEditorControlName = _view.AddNewTab(args.Filename);
            _view.SetTextEditorControlText(textEditorControlName, text);
            _view.SetSyntaxHighlighting(_view.SystemIO.PathGetExtension(args.Filename));
            if (openExistingReviewFile)
            {
                foreach (var reviewComment in _view.AppState.CurrentReview.ReviewedFiles[args.Filename].Comments)
                {
                    AddMarkerForComment(reviewComment);
                }
                AddNavigatorCommentMarkers(args.Filename);
            }
            else
            {
                _view.RemoveAllNavigatorShapes();
                _view.AddGreyedArea();
            }
            _view.EnableDisableMenuToolstripItems();
        }

        private void DoExitClick(object sender, CancelEventArgs e)
        {
            if (_view.AppState.CurrentReview.Saved)
            {
                _view.CloseApplication();
            }
            else
            {
                if (_view.AppState.CurrentReviewedFile != null &&
                    _view.AppState.CurrentReviewedFile.Comments != null &&
                    _view.AppState.CurrentReviewedFile.Comments.Count > 0)
                {
                    if (_view.MessageBoxUnsavedCommentsWarningOkCancel())
                    {
                        _view.CloseApplication();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    _view.CloseApplication();
                }
            }            
        }

        private void DoSaveClick(object sender, SaveEventArgs e)
        {
            string review = _view.AppState.CurrentReview.ToString();
            _view.SystemIO.WriteAllText(e.Filename, review);
            _view.AppState.CurrentReview.Saved = true;
            _view.SetFrmStandaloneReviewTitle();
        }

        private void DoContextMenuStripOpening(object sender, CaretPositionEventArgs e)
        {
            var commentAtCaretPosition = GetCommentAtCaretPosition(e.Line, e.Column);
            _view.EnableDisableContextMenuToolsstripItems(commentAtCaretPosition != null);
        }

        private void DoNewClick(object sender, EventArgs e)
        {
            if (_view.AppState.CurrentReview.Saved)
            {
                ResetCurrentReview();
            }
            else
            {
                if (_view.AppState.CurrentReviewedFile != null &&
                    _view.AppState.CurrentReviewedFile.Comments != null &&
                    _view.AppState.CurrentReviewedFile.Comments.Count > 0)
                {
                    if (_view.MessageBoxUnsavedCommentsWarningOkCancel())
                    {
                        ResetCurrentReview();
                    }
                }
                else
                {
                    ResetCurrentReview();
                }
            }
        }

        private void ResetCurrentReview()
        {
            _view.AppState.WorkingComment = new ReviewComment();
            _view.AppState.CurrentReviewedFile = null;
            _view.AppState.CurrentReview = new Review
            {
                ReviewTime = DateTime.Now,
                ReviewedFiles = new Dictionary<string, ReviewedFile>(),
                Saved = true
            };
            _view.RemoveAllOpenTabs();
            _view.EnableDisableMenuToolstripItems();
            _view.SetFrmStandaloneReviewTitle();
        }

        public void DoOpenContainingFolder(object sender, OpenFolderEventArgs e)
        {
            _view.SystemIO.OpenFolderInExplorer(e.Foldername);
        }

        public void DoCopyFullPath(object sender, CopyFullPathEventArgs e)
        {
            _view.SystemIO.CopyToClipboard(e.Filename);
        }

    }
}
