namespace StandaloneReview
{
    partial class FrmInsertComment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInsertComment));
            this.txtComment = new System.Windows.Forms.RichTextBox();
            this.btnInsertComment = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtComment
            // 
            resources.ApplyResources(this.txtComment, "txtComment");
            this.txtComment.Name = "txtComment";
            // 
            // btnInsertComment
            // 
            resources.ApplyResources(this.btnInsertComment, "btnInsertComment");
            this.btnInsertComment.Name = "btnInsertComment";
            this.btnInsertComment.UseVisualStyleBackColor = true;
            this.btnInsertComment.Click += new System.EventHandler(this.btnInsertComment_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmInsertComment
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnInsertComment);
            this.Controls.Add(this.txtComment);
            this.Name = "FrmInsertComment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmInsertComment_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtComment;
        private System.Windows.Forms.Button btnInsertComment;
        private System.Windows.Forms.Button btnCancel;
    }
}