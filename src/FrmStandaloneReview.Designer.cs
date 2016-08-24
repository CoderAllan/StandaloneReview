using ICSharpCode.TextEditor.Src.Document.FoldingStrategy;

namespace StandaloneReview
{
    partial class FrmStandaloneReview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStandaloneReview));
            this.textEditorControlEx1 = new ICSharpCode.TextEditor.TextEditorControlEx();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertCommentToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editCommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLblLine = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblColumn = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblSelectionLength = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblSelectionStart = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblSelectionEnd = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nytReviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveReviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertCommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textEditorControlEx1
            // 
            resources.ApplyResources(this.textEditorControlEx1, "textEditorControlEx1");
            this.textEditorControlEx1.ContextMenuStrip = this.contextMenuStrip1;
            this.textEditorControlEx1.EnableFolding = false;
            this.textEditorControlEx1.FoldingStrategy = "XML";
            this.textEditorControlEx1.Name = "textEditorControlEx1";
            this.textEditorControlEx1.ShowVRuler = false;
            this.textEditorControlEx1.SyntaxHighlighting = "XML";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertCommentToolStripMenuItem1,
            this.editCommentToolStripMenuItem,
            this.deleteCommentToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // insertCommentToolStripMenuItem1
            // 
            this.insertCommentToolStripMenuItem1.Name = "insertCommentToolStripMenuItem1";
            resources.ApplyResources(this.insertCommentToolStripMenuItem1, "insertCommentToolStripMenuItem1");
            this.insertCommentToolStripMenuItem1.Click += new System.EventHandler(this.insertCommentToolStripMenuItem1_Click);
            // 
            // editCommentToolStripMenuItem
            // 
            resources.ApplyResources(this.editCommentToolStripMenuItem, "editCommentToolStripMenuItem");
            this.editCommentToolStripMenuItem.Name = "editCommentToolStripMenuItem";
            this.editCommentToolStripMenuItem.Click += new System.EventHandler(this.editCommentToolStripMenuItem_Click);
            // 
            // deleteCommentToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteCommentToolStripMenuItem, "deleteCommentToolStripMenuItem");
            this.deleteCommentToolStripMenuItem.Name = "deleteCommentToolStripMenuItem";
            this.deleteCommentToolStripMenuItem.Click += new System.EventHandler(this.deleteCommentToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLblLine,
            this.toolStripStatusLblColumn,
            this.toolStripStatusLblSelectionLength,
            this.toolStripStatusLblSelectionStart,
            this.toolStripStatusLblSelectionEnd});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLblLine
            // 
            this.toolStripStatusLblLine.Name = "toolStripStatusLblLine";
            resources.ApplyResources(this.toolStripStatusLblLine, "toolStripStatusLblLine");
            // 
            // toolStripStatusLblColumn
            // 
            this.toolStripStatusLblColumn.Name = "toolStripStatusLblColumn";
            resources.ApplyResources(this.toolStripStatusLblColumn, "toolStripStatusLblColumn");
            // 
            // toolStripStatusLblSelectionLength
            // 
            this.toolStripStatusLblSelectionLength.Name = "toolStripStatusLblSelectionLength";
            resources.ApplyResources(this.toolStripStatusLblSelectionLength, "toolStripStatusLblSelectionLength");
            // 
            // toolStripStatusLblSelectionStart
            // 
            this.toolStripStatusLblSelectionStart.Name = "toolStripStatusLblSelectionStart";
            resources.ApplyResources(this.toolStripStatusLblSelectionStart, "toolStripStatusLblSelectionStart");
            // 
            // toolStripStatusLblSelectionEnd
            // 
            this.toolStripStatusLblSelectionEnd.Name = "toolStripStatusLblSelectionEnd";
            resources.ApplyResources(this.toolStripStatusLblSelectionEnd, "toolStripStatusLblSelectionEnd");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nytReviewToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveReviewToolStripMenuItem,
            this.previewToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // nytReviewToolStripMenuItem
            // 
            this.nytReviewToolStripMenuItem.Name = "nytReviewToolStripMenuItem";
            resources.ApplyResources(this.nytReviewToolStripMenuItem, "nytReviewToolStripMenuItem");
            this.nytReviewToolStripMenuItem.Click += new System.EventHandler(this.newReviewToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            resources.ApplyResources(this.openFileToolStripMenuItem, "openFileToolStripMenuItem");
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveReviewToolStripMenuItem
            // 
            resources.ApplyResources(this.saveReviewToolStripMenuItem, "saveReviewToolStripMenuItem");
            this.saveReviewToolStripMenuItem.Name = "saveReviewToolStripMenuItem";
            this.saveReviewToolStripMenuItem.Click += new System.EventHandler(this.saveReviewToolStripMenuItem_Click);
            // 
            // previewToolStripMenuItem
            // 
            resources.ApplyResources(this.previewToolStripMenuItem, "previewToolStripMenuItem");
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Click += new System.EventHandler(this.previewToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertCommentToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // insertCommentToolStripMenuItem
            // 
            resources.ApplyResources(this.insertCommentToolStripMenuItem, "insertCommentToolStripMenuItem");
            this.insertCommentToolStripMenuItem.Name = "insertCommentToolStripMenuItem";
            this.insertCommentToolStripMenuItem.Click += new System.EventHandler(this.insertCommentToolStripMenuItem_Click);
            // 
            // FrmStandaloneReview
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.textEditorControlEx1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmStandaloneReview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmStandaloneReview_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControlEx textEditorControlEx1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblLine;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblColumn;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblSelectionLength;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblSelectionStart;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblSelectionEnd;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveReviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertCommentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem insertCommentToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nytReviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCommentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCommentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
    }
}

