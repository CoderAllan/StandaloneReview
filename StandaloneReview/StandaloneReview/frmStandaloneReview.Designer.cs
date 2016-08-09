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
            this.textEditorControlEx1 = new ICSharpCode.TextEditor.TextEditorControlEx();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertCommentToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLblLine = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblColumn = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblSelectionLength = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblSelectionStart = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblSelectionEnd = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveReviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.textEditorControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditorControlEx1.ContextMenuStrip = this.contextMenuStrip1;
            this.textEditorControlEx1.EnableFolding = false;
            this.textEditorControlEx1.FoldingStrategy = "XML";
            this.textEditorControlEx1.Font = new System.Drawing.Font("Courier New", 10F);
            this.textEditorControlEx1.Location = new System.Drawing.Point(15, 37);
            this.textEditorControlEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textEditorControlEx1.Name = "textEditorControlEx1";
            this.textEditorControlEx1.ShowVRuler = false;
            this.textEditorControlEx1.Size = new System.Drawing.Size(1331, 807);
            this.textEditorControlEx1.SyntaxHighlighting = "XML";
            this.textEditorControlEx1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertCommentToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(213, 34);
            // 
            // insertCommentToolStripMenuItem1
            // 
            this.insertCommentToolStripMenuItem1.Name = "insertCommentToolStripMenuItem1";
            this.insertCommentToolStripMenuItem1.Size = new System.Drawing.Size(212, 30);
            this.insertCommentToolStripMenuItem1.Text = "Insert Comment";
            this.insertCommentToolStripMenuItem1.Click += new System.EventHandler(this.insertCommentToolStripMenuItem1_Click);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 848);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1359, 30);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLblLine
            // 
            this.toolStripStatusLblLine.Name = "toolStripStatusLblLine";
            this.toolStripStatusLblLine.Size = new System.Drawing.Size(49, 25);
            this.toolStripStatusLblLine.Text = "Ln: 0";
            // 
            // toolStripStatusLblColumn
            // 
            this.toolStripStatusLblColumn.Name = "toolStripStatusLblColumn";
            this.toolStripStatusLblColumn.Size = new System.Drawing.Size(57, 25);
            this.toolStripStatusLblColumn.Text = "Col: 0";
            // 
            // toolStripStatusLblSelectionLength
            // 
            this.toolStripStatusLblSelectionLength.Name = "toolStripStatusLblSelectionLength";
            this.toolStripStatusLblSelectionLength.Size = new System.Drawing.Size(0, 25);
            // 
            // toolStripStatusLblSelectionStart
            // 
            this.toolStripStatusLblSelectionStart.Name = "toolStripStatusLblSelectionStart";
            this.toolStripStatusLblSelectionStart.Size = new System.Drawing.Size(0, 25);
            // 
            // toolStripStatusLblSelectionEnd
            // 
            this.toolStripStatusLblSelectionEnd.Name = "toolStripStatusLblSelectionEnd";
            this.toolStripStatusLblSelectionEnd.Size = new System.Drawing.Size(0, 25);
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
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1359, 33);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveReviewToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(241, 30);
            this.openFileToolStripMenuItem.Text = "&Open file";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveReviewToolStripMenuItem
            // 
            this.saveReviewToolStripMenuItem.Name = "saveReviewToolStripMenuItem";
            this.saveReviewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveReviewToolStripMenuItem.Size = new System.Drawing.Size(241, 30);
            this.saveReviewToolStripMenuItem.Text = "&Save Review";
            this.saveReviewToolStripMenuItem.Click += new System.EventHandler(this.saveReviewToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertCommentToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // insertCommentToolStripMenuItem
            // 
            this.insertCommentToolStripMenuItem.Name = "insertCommentToolStripMenuItem";
            this.insertCommentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.insertCommentToolStripMenuItem.Size = new System.Drawing.Size(265, 30);
            this.insertCommentToolStripMenuItem.Text = "&Insert comment";
            this.insertCommentToolStripMenuItem.Click += new System.EventHandler(this.insertCommentToolStripMenuItem_Click);
            // 
            // FrmStandaloneReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 878);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.textEditorControlEx1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmStandaloneReview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Standalone Review";
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
    }
}

