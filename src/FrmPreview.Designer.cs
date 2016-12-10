namespace StandaloneReview
{
    partial class FrmPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPreview));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.btnMoveFileUp = new System.Windows.Forms.Button();
            this.btnMoveFileDown = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnMoveCommentDown = new System.Windows.Forms.Button();
            this.btnMoveCommentUp = new System.Windows.Forms.Button();
            this.lstComments = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lstFiles
            // 
            resources.ApplyResources(this.lstFiles, "lstFiles");
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
            // 
            // btnMoveFileUp
            // 
            resources.ApplyResources(this.btnMoveFileUp, "btnMoveFileUp");
            this.btnMoveFileUp.Name = "btnMoveFileUp";
            this.btnMoveFileUp.UseVisualStyleBackColor = true;
            this.btnMoveFileUp.Click += new System.EventHandler(this.btnMoveFileUp_Click);
            // 
            // btnMoveFileDown
            // 
            resources.ApplyResources(this.btnMoveFileDown, "btnMoveFileDown");
            this.btnMoveFileDown.Name = "btnMoveFileDown";
            this.btnMoveFileDown.UseVisualStyleBackColor = true;
            this.btnMoveFileDown.Click += new System.EventHandler(this.btnMoveFileDown_Click);
            // 
            // btnMoveCommentDown
            // 
            resources.ApplyResources(this.btnMoveCommentDown, "btnMoveCommentDown");
            this.btnMoveCommentDown.Name = "btnMoveCommentDown";
            this.btnMoveCommentDown.UseVisualStyleBackColor = true;
            this.btnMoveCommentDown.Click += new System.EventHandler(this.btnMoveCommentDown_Click);
            // 
            // btnMoveCommentUp
            // 
            resources.ApplyResources(this.btnMoveCommentUp, "btnMoveCommentUp");
            this.btnMoveCommentUp.Name = "btnMoveCommentUp";
            this.btnMoveCommentUp.UseVisualStyleBackColor = true;
            this.btnMoveCommentUp.Click += new System.EventHandler(this.btnMoveCommentUp_Click);
            // 
            // lstComments
            // 
            resources.ApplyResources(this.lstComments, "lstComments");
            this.lstComments.FormattingEnabled = true;
            this.lstComments.Name = "lstComments";
            this.lstComments.SelectedIndexChanged += new System.EventHandler(this.lstComments_SelectedIndexChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // FrmPreview
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnMoveCommentDown);
            this.Controls.Add(this.btnMoveCommentUp);
            this.Controls.Add(this.lstComments);
            this.Controls.Add(this.btnMoveFileDown);
            this.Controls.Add(this.btnMoveFileUp);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Name = "FrmPreview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPreview_FormClosing);
            this.Load += new System.EventHandler(this.FrmPreview_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Button btnMoveFileUp;
        private System.Windows.Forms.Button btnMoveFileDown;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnMoveCommentDown;
        private System.Windows.Forms.Button btnMoveCommentUp;
        private System.Windows.Forms.ListBox lstComments;
        private System.Windows.Forms.Panel panel1;
    }
}