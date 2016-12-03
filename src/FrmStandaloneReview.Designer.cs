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
            this.contextMenuComment = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertCommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.navigatorCanvas = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.contextMenuTabpages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllTabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllTabsButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyFullPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openContainingFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuComment.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuTabpages.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuComment
            // 
            this.contextMenuComment.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertCommentToolStripMenuItem1,
            this.editCommentToolStripMenuItem,
            this.deleteCommentToolStripMenuItem});
            this.contextMenuComment.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuComment, "contextMenuComment");
            this.contextMenuComment.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
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
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nytReviewToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveReviewToolStripMenuItem,
            this.exportToolStripMenuItem,
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
            // exportToolStripMenuItem
            // 
            resources.ApplyResources(this.exportToolStripMenuItem, "exportToolStripMenuItem");
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
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
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // optionsToolStripMenuItem
            // 
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // shapeContainer1
            // 
            resources.ApplyResources(this.shapeContainer1, "shapeContainer1");
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.navigatorCanvas});
            this.shapeContainer1.TabStop = false;
            // 
            // navigatorCanvas
            // 
            resources.ApplyResources(this.navigatorCanvas, "navigatorCanvas");
            this.navigatorCanvas.Name = "navigatorCanvas";
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.ContextMenuStrip = this.contextMenuTabpages;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // contextMenuTabpages
            // 
            this.contextMenuTabpages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeTabToolStripMenuItem,
            this.closeAllTabsToolStripMenuItem,
            this.closeAllTabsButThisToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyFullPathToolStripMenuItem,
            this.openContainingFolderToolStripMenuItem});
            this.contextMenuTabpages.Name = "contextMenuTabpages";
            resources.ApplyResources(this.contextMenuTabpages, "contextMenuTabpages");
            this.contextMenuTabpages.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuTabpages_Opening);
            // 
            // closeTabToolStripMenuItem
            // 
            resources.ApplyResources(this.closeTabToolStripMenuItem, "closeTabToolStripMenuItem");
            this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
            this.closeTabToolStripMenuItem.Click += new System.EventHandler(this.closeTabToolStripMenuItem_Click);
            // 
            // closeAllTabsToolStripMenuItem
            // 
            this.closeAllTabsToolStripMenuItem.Name = "closeAllTabsToolStripMenuItem";
            resources.ApplyResources(this.closeAllTabsToolStripMenuItem, "closeAllTabsToolStripMenuItem");
            this.closeAllTabsToolStripMenuItem.Click += new System.EventHandler(this.closeAllTabsToolStripMenuItem_Click);
            // 
            // closeAllTabsButThisToolStripMenuItem
            // 
            this.closeAllTabsButThisToolStripMenuItem.Name = "closeAllTabsButThisToolStripMenuItem";
            resources.ApplyResources(this.closeAllTabsButThisToolStripMenuItem, "closeAllTabsButThisToolStripMenuItem");
            this.closeAllTabsButThisToolStripMenuItem.Click += new System.EventHandler(this.closeAllTabsButThisToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // copyFullPathToolStripMenuItem
            // 
            this.copyFullPathToolStripMenuItem.Name = "copyFullPathToolStripMenuItem";
            resources.ApplyResources(this.copyFullPathToolStripMenuItem, "copyFullPathToolStripMenuItem");
            this.copyFullPathToolStripMenuItem.Click += new System.EventHandler(this.copyFullPathToolStripMenuItem_Click);
            // 
            // openContainingFolderToolStripMenuItem
            // 
            resources.ApplyResources(this.openContainingFolderToolStripMenuItem, "openContainingFolderToolStripMenuItem");
            this.openContainingFolderToolStripMenuItem.Name = "openContainingFolderToolStripMenuItem";
            this.openContainingFolderToolStripMenuItem.Click += new System.EventHandler(this.openContainingFolderToolStripMenuItem_Click);
            // 
            // FrmStandaloneReview
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.shapeContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmStandaloneReview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmStandaloneReview_FormClosing);
            this.contextMenuComment.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuTabpages.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.ContextMenuStrip contextMenuComment;
        private System.Windows.Forms.ToolStripMenuItem insertCommentToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nytReviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCommentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCommentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape navigatorCanvas;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuTabpages;
        private System.Windows.Forms.ToolStripMenuItem closeTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllTabsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllTabsButThisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFullPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openContainingFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    }
}

