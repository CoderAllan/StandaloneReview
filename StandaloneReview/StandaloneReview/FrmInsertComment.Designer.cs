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
            this.txtComment = new System.Windows.Forms.RichTextBox();
            this.btnInsertComment = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Location = new System.Drawing.Point(13, 22);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(709, 486);
            this.txtComment.TabIndex = 0;
            this.txtComment.Text = "";
            // 
            // btnInsertComment
            // 
            this.btnInsertComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsertComment.Location = new System.Drawing.Point(589, 527);
            this.btnInsertComment.Name = "btnInsertComment";
            this.btnInsertComment.Size = new System.Drawing.Size(132, 46);
            this.btnInsertComment.TabIndex = 1;
            this.btnInsertComment.Text = "Insert comment";
            this.btnInsertComment.UseVisualStyleBackColor = true;
            this.btnInsertComment.Click += new System.EventHandler(this.btnInsertComment_Click);
            // 
            // FrmInsertComment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 585);
            this.Controls.Add(this.btnInsertComment);
            this.Controls.Add(this.txtComment);
            this.Name = "FrmInsertComment";
            this.Text = "Insert comment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmInsertComment_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtComment;
        private System.Windows.Forms.Button btnInsertComment;
    }
}