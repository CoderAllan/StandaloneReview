using ICSharpCode.TextEditor.Src.Document.FoldingStrategy;

namespace StandaloneReview
{
    partial class frmStandaloneReview
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.textEditorControlEx1 = new ICSharpCode.TextEditor.TextEditorControlEx();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLblLine = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblColumn = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLblSelectionLength = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1192, 801);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "DoStuff";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.Location = new System.Drawing.Point(14, 801);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLoad.Name = "button2";
            this.btnLoad.Size = new System.Drawing.Size(153, 29);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // textEditorControlEx1
            // 
            this.textEditorControlEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditorControlEx1.EnableFolding = false;
            this.textEditorControlEx1.FoldingStrategy = "XML";
            this.textEditorControlEx1.Font = new System.Drawing.Font("Courier New", 10F);
            this.textEditorControlEx1.Location = new System.Drawing.Point(15, 16);
            this.textEditorControlEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textEditorControlEx1.Name = "textEditorControlEx1";
            this.textEditorControlEx1.Size = new System.Drawing.Size(1331, 778);
            this.textEditorControlEx1.SyntaxHighlighting = "XML";
            this.textEditorControlEx1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLblLine,
            this.toolStripStatusLblColumn,
            this.toolStripStatusLblSelectionLength});
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmStandaloneReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 878);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textEditorControlEx1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmStandaloneReview";
            this.Text = "Standalone Review";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControlEx textEditorControlEx1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblLine;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblColumn;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLblSelectionLength;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

