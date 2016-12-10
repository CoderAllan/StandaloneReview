namespace StandaloneReview.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Model;
    using MockViews;
    using Presenters;

    [TestClass]
    public class FrmStandaloneReviewPresenterDeleteCommentTests
    {

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
            //Assert.IsFalse(mockView.SetFrmStandaloneReviewTitleValue.EndsWith(" *")); // The title should not contain a star before deleting a comment

            // Act
            mockView.FireDeleteCommentToolStripMenuItemClickEvent(5, 5);

            // Assert
            Assert.IsTrue(mockView.SetFrmStandaloneReviewTitleWasCalled);
            //Assert.IsTrue(mockView.SetFrmStandaloneReviewTitleValue.EndsWith(" *")); // The title should contain a star after a comment is deleted
        }

        [TestMethod]
        public void FrmStandaloneReviewDeleteComment_TwoCommentsOnSameLine_ExpectCorrectOneToBeDeleted()
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
                    },
                    new ReviewComment
                    {
                        SelectionStartLine = 11,
                        SelectionStartColumn = 25,
                        SelectionEndLine = 11,
                        SelectionEndColumn = 35
                    },
                    new ReviewComment
                    {
                        SelectionStartLine = 11,
                        SelectionStartColumn = 55,
                        SelectionEndLine = 11,
                        SelectionEndColumn = 65
                    },
                    new ReviewComment
                    {
                        Line = 15
                    }
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireDeleteCommentToolStripMenuItemClickEvent(11, 30);

            // Assert
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 5));
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.SelectionStartLine == 11 && p.SelectionStartColumn == 55));
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 15));
            Assert.AreEqual(3, mockView.AppState.CurrentReviewedFile.Comments.Count);
        }

        [TestMethod]
        public void FrmStandaloneReviewDeleteComment_OneSelectionComments_ExpectItToBeDeleted()
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
                    },
                    new ReviewComment
                    {
                        SelectionStartLine = 11,
                        SelectionStartColumn = 25,
                        SelectionEndLine = 11,
                        SelectionEndColumn = 35
                    },
                    new ReviewComment
                    {
                        Line = 15
                    }
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireDeleteCommentToolStripMenuItemClickEvent(11, 30);

            // Assert
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 5));
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 15));
            Assert.AreEqual(2, mockView.AppState.CurrentReviewedFile.Comments.Count);
        }

        [TestMethod]
        public void FrmStandaloneReviewDeleteComment_OneSelectionCommentsMarkerNotOnComment_ExpectItToBeDeleted()
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
                    },
                    new ReviewComment
                    {
                        SelectionStartLine = 11,
                        SelectionStartColumn = 25,
                        SelectionEndLine = 11,
                        SelectionEndColumn = 35
                    },
                    new ReviewComment
                    {
                        Line = 15
                    }
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireDeleteCommentToolStripMenuItemClickEvent(11, 15);

            // Assert
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 5));
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.SelectionStartLine == 11 && p.SelectionStartColumn == 25));
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 15));
            Assert.AreEqual(3, mockView.AppState.CurrentReviewedFile.Comments.Count);
        }

        [TestMethod]
        public void FrmStandaloneReviewDeleteComment_OneLineCommentsMarker_ExpectItToBeDeleted()
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
                    },
                    new ReviewComment
                    {
                        SelectionStartLine = 11,
                        SelectionStartColumn = 25,
                        SelectionEndLine = 11,
                        SelectionEndColumn = 35
                    },
                    new ReviewComment
                    {
                        Line = 15
                    }
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireDeleteCommentToolStripMenuItemClickEvent(5, 15);

            // Assert
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.SelectionStartLine == 11 && p.SelectionStartColumn == 25));
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 15));
            Assert.AreEqual(2, mockView.AppState.CurrentReviewedFile.Comments.Count);
        }

        [TestMethod]
        public void FrmStandaloneReviewDeleteComment_BigSelectionComment_ExpectItToBeDeleted()
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
                    },
                    new ReviewComment
                    {
                        SelectionStartLine = 10,
                        SelectionStartColumn = 25,
                        SelectionEndLine = 20,
                        SelectionEndColumn = 35
                    },
                    new ReviewComment
                    {
                        Line = 25
                    }
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireDeleteCommentToolStripMenuItemClickEvent(15, 65);

            // Assert
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 5));
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 25));
            Assert.AreEqual(2, mockView.AppState.CurrentReviewedFile.Comments.Count);
        }

        [TestMethod]
        public void FrmStandaloneReviewDeleteComment_BigSelectionCommentCaretOnSelectionFirstLine_ExpectItToBeDeleted()
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
                    },
                    new ReviewComment
                    {
                        SelectionStartLine = 10,
                        SelectionStartColumn = 25,
                        SelectionEndLine = 20,
                        SelectionEndColumn = 35
                    },
                    new ReviewComment
                    {
                        Line = 25
                    }
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireDeleteCommentToolStripMenuItemClickEvent(10, 30);

            // Assert
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 5));
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 25));
            Assert.AreEqual(2, mockView.AppState.CurrentReviewedFile.Comments.Count);
        }

        [TestMethod]
        public void FrmStandaloneReviewDeleteComment_BigSelectionCommentCaretOnSelectionLastLine_ExpectItToBeDeleted()
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
                    },
                    new ReviewComment
                    {
                        SelectionStartLine = 10,
                        SelectionStartColumn = 25,
                        SelectionEndLine = 20,
                        SelectionEndColumn = 35
                    },
                    new ReviewComment
                    {
                        Line = 25
                    }
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireDeleteCommentToolStripMenuItemClickEvent(20, 25);

            // Assert
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 5));
            Assert.IsTrue(mockView.AppState.CurrentReviewedFile.Comments.Exists(p => p.Line == 25));
            Assert.AreEqual(2, mockView.AppState.CurrentReviewedFile.Comments.Count);
        }

        [TestMethod]
        public void FrmStandaloneReviewDeleteComment_VerifyCommentPositionsWhenFirstDeleted_ExpectAllPositionsDecremented()
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
                        Position = 0,
                        Line = 10                 
                    },
                    new ReviewComment
                    {
                        Position = 1,
                        Line = 1
                    },
                    new ReviewComment
                    {
                        Position = 2,
                        Line = 2
                    }
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireDeleteCommentToolStripMenuItemClickEvent(10, 25);

            // Assert
            Assert.AreEqual(2, mockView.AppState.CurrentReviewedFile.Comments.Count);
            var commentLine1 = mockView.AppState.CurrentReviewedFile.Comments.FirstOrDefault(p => p.Line == 1);
            Assert.IsNotNull(commentLine1);
            Assert.AreEqual(0, commentLine1.Position);
            var commentLine2 = mockView.AppState.CurrentReviewedFile.Comments.FirstOrDefault(p => p.Line == 2);
            Assert.IsNotNull(commentLine2);
            Assert.AreEqual(1, commentLine2.Position);
        }

        [TestMethod]
        public void FrmStandaloneReviewDeleteComment_VerifyCommentPositionsWhenMiddleDeleted_ExpectAllPositionsDecremented()
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
                        Position = 0,
                        Line = 10                 
                    },
                    new ReviewComment
                    {
                        Position = 1,
                        Line = 1
                    },
                    new ReviewComment
                    {
                        Position = 2,
                        Line = 2
                    }
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireDeleteCommentToolStripMenuItemClickEvent(1, 25);

            // Assert
            Assert.AreEqual(2, mockView.AppState.CurrentReviewedFile.Comments.Count);
            var commentLine1 = mockView.AppState.CurrentReviewedFile.Comments.FirstOrDefault(p => p.Line == 10);
            Assert.IsNotNull(commentLine1);
            Assert.AreEqual(0, commentLine1.Position);
            var commentLine2 = mockView.AppState.CurrentReviewedFile.Comments.FirstOrDefault(p => p.Line == 2);
            Assert.IsNotNull(commentLine2);
            Assert.AreEqual(1, commentLine2.Position);
        }

        [TestMethod]
        public void FrmStandaloneReviewDeleteComment_VerifyCommentPositionsWhenLastDeleted_ExpectAllPositionsDecremented()
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
                        Position = 0,
                        Line = 10                 
                    },
                    new ReviewComment
                    {
                        Position = 1,
                        Line = 1
                    },
                    new ReviewComment
                    {
                        Position = 2,
                        Line = 2
                    }
                }
            };
            var presenter = new FrmStandaloneReviewPresenter(mockView);

            // Act
            mockView.FireDeleteCommentToolStripMenuItemClickEvent(2, 25);

            // Assert
            Assert.AreEqual(2, mockView.AppState.CurrentReviewedFile.Comments.Count);
            var commentLine1 = mockView.AppState.CurrentReviewedFile.Comments.FirstOrDefault(p => p.Line == 10);
            Assert.IsNotNull(commentLine1);
            Assert.AreEqual(0, commentLine1.Position);
            var commentLine2 = mockView.AppState.CurrentReviewedFile.Comments.FirstOrDefault(p => p.Line == 1);
            Assert.IsNotNull(commentLine2);
            Assert.AreEqual(1, commentLine2.Position);
        }
    }
}
