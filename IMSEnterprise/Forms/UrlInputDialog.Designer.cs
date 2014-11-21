namespace IMSEnterprise
{
    partial class UrlInputDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UrlInputDialog));
            this.infoLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.inputDialogTitleLabel = new System.Windows.Forms.Label();
            this.inputDialogComboBox = new System.Windows.Forms.ComboBox();
            this.loadBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(17, 8);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(142, 13);
            this.infoLabel.TabIndex = 3;
            this.infoLabel.Text = "Type the url you wish to load";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Progress:";
            // 
            // progress
            // 
            this.progress.AutoSize = true;
            this.progress.Location = new System.Drawing.Point(74, 90);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(0, 13);
            this.progress.TabIndex = 5;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.Location = new System.Drawing.Point(826, 80);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(72, 23);
            this.cancelBtn.TabIndex = 6;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // inputDialogTitleLabel
            // 
            this.inputDialogTitleLabel.Location = new System.Drawing.Point(-254, 10);
            this.inputDialogTitleLabel.Name = "inputDialogTitleLabel";
            this.inputDialogTitleLabel.Size = new System.Drawing.Size(0, 13);
            this.inputDialogTitleLabel.TabIndex = 0;
            // 
            // inputDialogComboBox
            // 
            this.inputDialogComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputDialogComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.inputDialogComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.inputDialogComboBox.FormattingEnabled = true;
            this.inputDialogComboBox.Location = new System.Drawing.Point(20, 25);
            this.inputDialogComboBox.Name = "inputDialogComboBox";
            this.inputDialogComboBox.Size = new System.Drawing.Size(800, 21);
            this.inputDialogComboBox.TabIndex = 7;
            // 
            // loadBtn
            // 
            this.loadBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadBtn.Location = new System.Drawing.Point(826, 25);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(75, 23);
            this.loadBtn.TabIndex = 0;
            this.loadBtn.Text = "Load";
            this.loadBtn.Click += new System.EventHandler(this.inputDialogBtn_Click);
            // 
            // UrlInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(903, 112);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.inputDialogComboBox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.infoLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(4000, 150);
            this.MinimumSize = new System.Drawing.Size(400, 150);
            this.Name = "UrlInputDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Load from url";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UrlInputDialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label progress;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label inputDialogTitleLabel;
        private System.Windows.Forms.ComboBox inputDialogComboBox;
        private System.Windows.Forms.Button loadBtn;

    }
}