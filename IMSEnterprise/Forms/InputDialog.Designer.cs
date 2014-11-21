namespace IMSEnterprise
{
    partial class InputDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputDialog));
            this.inputDialogBtn = new System.Windows.Forms.Button();
            this.inputDialogTitleLabel = new System.Windows.Forms.Label();
            this.inputDialogTextBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // inputDialogBtn
            // 
            this.inputDialogBtn.Location = new System.Drawing.Point(209, 22);
            this.inputDialogBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputDialogBtn.Name = "inputDialogBtn";
            this.inputDialogBtn.Size = new System.Drawing.Size(58, 26);
            this.inputDialogBtn.TabIndex = 1;
            this.inputDialogBtn.Text = "Open";
            this.inputDialogBtn.UseVisualStyleBackColor = true;
            // 
            // inputDialogTitleLabel
            // 
            this.inputDialogTitleLabel.AutoSize = true;
            this.inputDialogTitleLabel.Location = new System.Drawing.Point(16, 10);
            this.inputDialogTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.inputDialogTitleLabel.Name = "inputDialogTitleLabel";
            this.inputDialogTitleLabel.Size = new System.Drawing.Size(35, 13);
            this.inputDialogTitleLabel.TabIndex = 2;
            this.inputDialogTitleLabel.Text = "label1";
            // 
            // inputDialogTextBox
            // 
            this.inputDialogTextBox.FormattingEnabled = true;
            this.inputDialogTextBox.Location = new System.Drawing.Point(19, 26);
            this.inputDialogTextBox.Name = "inputDialogTextBox";
            this.inputDialogTextBox.Size = new System.Drawing.Size(185, 21);
            this.inputDialogTextBox.TabIndex = 3;
            // 
            // InputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 76);
            this.Controls.Add(this.inputDialogTextBox);
            this.Controls.Add(this.inputDialogTitleLabel);
            this.Controls.Add(this.inputDialogBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "InputDialog";
            this.Text = "InputDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button inputDialogBtn;
        protected System.Windows.Forms.Label inputDialogTitleLabel;
        protected System.Windows.Forms.ComboBox inputDialogTextBox;

    }
}