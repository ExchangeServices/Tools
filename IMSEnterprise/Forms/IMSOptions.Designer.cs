namespace IMSEnterprise
{
    partial class IMSOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IMSOptions));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.distinguishingColor = new System.Windows.Forms.CheckBox();
            this.extensionsEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pLLoggingGroupBox = new System.Windows.Forms.GroupBox();
            this.pLLSLOpen = new System.Windows.Forms.Button();
            this.pLLSLocation = new System.Windows.Forms.TextBox();
            this.pLLSLEnabled = new System.Windows.Forms.CheckBox();
            this.pLLEnabled = new System.Windows.Forms.CheckBox();
            this.pLWEGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maxErrors = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.maxWarnings = new System.Windows.Forms.TextBox();
            this.warningsEnabled = new System.Windows.Forms.CheckBox();
            this.errorsEnabled = new System.Windows.Forms.CheckBox();
            this.pLEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.startPointCCLB = new IMSEnterprise.CustomCheckedListBox();
            this.startupTabPage = new System.Windows.Forms.TabPage();
            this.openNone = new System.Windows.Forms.RadioButton();
            this.urlBox = new System.Windows.Forms.TextBox();
            this.openFromUrl = new System.Windows.Forms.RadioButton();
            this.optionOpenRecent = new System.Windows.Forms.RadioButton();
            this.recentFilePathLabel = new System.Windows.Forms.TextBox();
            this.openSpecificFile = new System.Windows.Forms.RadioButton();
            this.openFileDialogBtn = new System.Windows.Forms.Button();
            this.filePathInput = new System.Windows.Forms.TextBox();
            this.saveSettingsBtn = new System.Windows.Forms.Button();
            this.cancelSettingsBtn = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pLLoggingGroupBox.SuspendLayout();
            this.pLWEGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.startupTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.generalTabPage);
            this.tabControl.Controls.Add(this.startupTabPage);
            this.tabControl.Location = new System.Drawing.Point(2, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(440, 361);
            this.tabControl.TabIndex = 7;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.groupBox4);
            this.generalTabPage.Controls.Add(this.groupBox2);
            this.generalTabPage.Controls.Add(this.groupBox1);
            this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Margin = new System.Windows.Forms.Padding(2);
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.Padding = new System.Windows.Forms.Padding(2);
            this.generalTabPage.Size = new System.Drawing.Size(432, 335);
            this.generalTabPage.TabIndex = 0;
            this.generalTabPage.Text = "General";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.extensionsEnabled);
            this.groupBox4.Location = new System.Drawing.Point(5, 246);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(212, 85);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Extensions";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.distinguishingColor);
            this.groupBox5.Location = new System.Drawing.Point(4, 39);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(204, 43);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Settings";
            // 
            // distinguishingColor
            // 
            this.distinguishingColor.AutoSize = true;
            this.distinguishingColor.Location = new System.Drawing.Point(4, 17);
            this.distinguishingColor.Margin = new System.Windows.Forms.Padding(2);
            this.distinguishingColor.Name = "distinguishingColor";
            this.distinguishingColor.Size = new System.Drawing.Size(117, 17);
            this.distinguishingColor.TabIndex = 1;
            this.distinguishingColor.Text = "Distinguishing color";
            this.distinguishingColor.UseVisualStyleBackColor = true;
            // 
            // extensionsEnabled
            // 
            this.extensionsEnabled.AutoSize = true;
            this.extensionsEnabled.Location = new System.Drawing.Point(5, 18);
            this.extensionsEnabled.Margin = new System.Windows.Forms.Padding(2);
            this.extensionsEnabled.Name = "extensionsEnabled";
            this.extensionsEnabled.Size = new System.Drawing.Size(65, 17);
            this.extensionsEnabled.TabIndex = 0;
            this.extensionsEnabled.Text = "Enabled";
            this.extensionsEnabled.UseVisualStyleBackColor = true;
            this.extensionsEnabled.CheckStateChanged += new System.EventHandler(this.extensionsEnabled_CheckStateChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pLLoggingGroupBox);
            this.groupBox2.Controls.Add(this.pLWEGroupBox);
            this.groupBox2.Controls.Add(this.pLEnabled);
            this.groupBox2.Location = new System.Drawing.Point(148, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(284, 227);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Problem Lister";
            // 
            // pLLoggingGroupBox
            // 
            this.pLLoggingGroupBox.Controls.Add(this.pLLSLOpen);
            this.pLLoggingGroupBox.Controls.Add(this.pLLSLocation);
            this.pLLoggingGroupBox.Controls.Add(this.pLLSLEnabled);
            this.pLLoggingGroupBox.Controls.Add(this.pLLEnabled);
            this.pLLoggingGroupBox.Location = new System.Drawing.Point(4, 145);
            this.pLLoggingGroupBox.Name = "pLLoggingGroupBox";
            this.pLLoggingGroupBox.Size = new System.Drawing.Size(277, 77);
            this.pLLoggingGroupBox.TabIndex = 10;
            this.pLLoggingGroupBox.TabStop = false;
            this.pLLoggingGroupBox.Text = "Logging";
            // 
            // pLLSLOpen
            // 
            this.pLLSLOpen.Enabled = false;
            this.pLLSLOpen.Location = new System.Drawing.Point(234, 42);
            this.pLLSLOpen.Margin = new System.Windows.Forms.Padding(2);
            this.pLLSLOpen.Name = "pLLSLOpen";
            this.pLLSLOpen.Size = new System.Drawing.Size(22, 19);
            this.pLLSLOpen.TabIndex = 12;
            this.pLLSLOpen.Text = "...";
            this.pLLSLOpen.UseVisualStyleBackColor = true;
            this.pLLSLOpen.Click += new System.EventHandler(this.pLLSLOpen_Click);
            // 
            // pLLSLocation
            // 
            this.pLLSLocation.Enabled = false;
            this.pLLSLocation.Location = new System.Drawing.Point(119, 42);
            this.pLLSLocation.Margin = new System.Windows.Forms.Padding(2);
            this.pLLSLocation.Name = "pLLSLocation";
            this.pLLSLocation.Size = new System.Drawing.Size(111, 20);
            this.pLLSLocation.TabIndex = 11;
            // 
            // pLLSLEnabled
            // 
            this.pLLSLEnabled.AutoSize = true;
            this.pLLSLEnabled.Location = new System.Drawing.Point(5, 44);
            this.pLLSLEnabled.Name = "pLLSLEnabled";
            this.pLLSLEnabled.Size = new System.Drawing.Size(110, 17);
            this.pLLSLEnabled.TabIndex = 8;
            this.pLLSLEnabled.Text = "Specified location";
            this.pLLSLEnabled.UseVisualStyleBackColor = true;
            this.pLLSLEnabled.CheckStateChanged += new System.EventHandler(this.pLLSLEnabled_CheckStateChanged);
            // 
            // pLLEnabled
            // 
            this.pLLEnabled.AutoSize = true;
            this.pLLEnabled.Location = new System.Drawing.Point(5, 21);
            this.pLLEnabled.Margin = new System.Windows.Forms.Padding(2);
            this.pLLEnabled.Name = "pLLEnabled";
            this.pLLEnabled.Size = new System.Drawing.Size(65, 17);
            this.pLLEnabled.TabIndex = 7;
            this.pLLEnabled.Text = "Enabled";
            this.pLLEnabled.UseVisualStyleBackColor = true;
            this.pLLEnabled.CheckStateChanged += new System.EventHandler(this.pLLEnabled_CheckedChanged);
            // 
            // pLWEGroupBox
            // 
            this.pLWEGroupBox.Controls.Add(this.label4);
            this.pLWEGroupBox.Controls.Add(this.label3);
            this.pLWEGroupBox.Controls.Add(this.label2);
            this.pLWEGroupBox.Controls.Add(this.maxErrors);
            this.pLWEGroupBox.Controls.Add(this.label1);
            this.pLWEGroupBox.Controls.Add(this.maxWarnings);
            this.pLWEGroupBox.Controls.Add(this.warningsEnabled);
            this.pLWEGroupBox.Controls.Add(this.errorsEnabled);
            this.pLWEGroupBox.Location = new System.Drawing.Point(4, 39);
            this.pLWEGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.pLWEGroupBox.Name = "pLWEGroupBox";
            this.pLWEGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.pLWEGroupBox.Size = new System.Drawing.Size(276, 109);
            this.pLWEGroupBox.TabIndex = 9;
            this.pLWEGroupBox.TabStop = false;
            this.pLWEGroupBox.Text = "Warnings and Errors";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label4.Location = new System.Drawing.Point(188, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "0 = unlimited";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Location = new System.Drawing.Point(188, 61);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "0 = unlimited";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Maximum Errors: ";
            // 
            // maxErrors
            // 
            this.maxErrors.Location = new System.Drawing.Point(108, 81);
            this.maxErrors.Margin = new System.Windows.Forms.Padding(2);
            this.maxErrors.Name = "maxErrors";
            this.maxErrors.Size = new System.Drawing.Size(76, 20);
            this.maxErrors.TabIndex = 11;
            this.maxErrors.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.maxInput_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Maximum Warnings: ";
            // 
            // maxWarnings
            // 
            this.maxWarnings.Location = new System.Drawing.Point(108, 61);
            this.maxWarnings.Margin = new System.Windows.Forms.Padding(2);
            this.maxWarnings.Name = "maxWarnings";
            this.maxWarnings.Size = new System.Drawing.Size(76, 20);
            this.maxWarnings.TabIndex = 9;
            this.maxWarnings.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.maxInput_KeyPress);
            // 
            // warningsEnabled
            // 
            this.warningsEnabled.AutoSize = true;
            this.warningsEnabled.Location = new System.Drawing.Point(4, 17);
            this.warningsEnabled.Margin = new System.Windows.Forms.Padding(2);
            this.warningsEnabled.Name = "warningsEnabled";
            this.warningsEnabled.Size = new System.Drawing.Size(113, 17);
            this.warningsEnabled.TabIndex = 7;
            this.warningsEnabled.Text = "Warnings Enabled";
            this.warningsEnabled.UseVisualStyleBackColor = true;
            // 
            // errorsEnabled
            // 
            this.errorsEnabled.AutoSize = true;
            this.errorsEnabled.Location = new System.Drawing.Point(4, 39);
            this.errorsEnabled.Margin = new System.Windows.Forms.Padding(2);
            this.errorsEnabled.Name = "errorsEnabled";
            this.errorsEnabled.Size = new System.Drawing.Size(95, 17);
            this.errorsEnabled.TabIndex = 8;
            this.errorsEnabled.Text = "Errors Enabled";
            this.errorsEnabled.UseVisualStyleBackColor = true;
            // 
            // pLEnabled
            // 
            this.pLEnabled.AutoSize = true;
            this.pLEnabled.Location = new System.Drawing.Point(4, 17);
            this.pLEnabled.Margin = new System.Windows.Forms.Padding(2);
            this.pLEnabled.Name = "pLEnabled";
            this.pLEnabled.Size = new System.Drawing.Size(65, 17);
            this.pLEnabled.TabIndex = 6;
            this.pLEnabled.Text = "Enabled";
            this.pLEnabled.UseVisualStyleBackColor = true;
            this.pLEnabled.CheckStateChanged += new System.EventHandler(this.pLEnabled_CheckStateChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.startPointCCLB);
            this.groupBox1.Location = new System.Drawing.Point(8, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(127, 227);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Start points";
            // 
            // startPointCCLB
            // 
            this.startPointCCLB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.startPointCCLB.CheckOnClick = true;
            this.startPointCCLB.FormattingEnabled = true;
            this.startPointCCLB.Location = new System.Drawing.Point(4, 17);
            this.startPointCCLB.Margin = new System.Windows.Forms.Padding(2);
            this.startPointCCLB.Name = "startPointCCLB";
            this.startPointCCLB.Size = new System.Drawing.Size(118, 198);
            this.startPointCCLB.TabIndex = 6;
            this.startPointCCLB.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.startPointCCLB_ItemCheck);
            // 
            // startupTabPage
            // 
            this.startupTabPage.Controls.Add(this.openNone);
            this.startupTabPage.Controls.Add(this.urlBox);
            this.startupTabPage.Controls.Add(this.openFromUrl);
            this.startupTabPage.Controls.Add(this.optionOpenRecent);
            this.startupTabPage.Controls.Add(this.recentFilePathLabel);
            this.startupTabPage.Controls.Add(this.openSpecificFile);
            this.startupTabPage.Controls.Add(this.openFileDialogBtn);
            this.startupTabPage.Controls.Add(this.filePathInput);
            this.startupTabPage.Location = new System.Drawing.Point(4, 22);
            this.startupTabPage.Margin = new System.Windows.Forms.Padding(2);
            this.startupTabPage.Name = "startupTabPage";
            this.startupTabPage.Padding = new System.Windows.Forms.Padding(2);
            this.startupTabPage.Size = new System.Drawing.Size(432, 335);
            this.startupTabPage.TabIndex = 1;
            this.startupTabPage.Text = "Startup";
            this.startupTabPage.UseVisualStyleBackColor = true;
            // 
            // openNone
            // 
            this.openNone.AutoSize = true;
            this.openNone.Location = new System.Drawing.Point(4, 91);
            this.openNone.Name = "openNone";
            this.openNone.Size = new System.Drawing.Size(51, 17);
            this.openNone.TabIndex = 14;
            this.openNone.TabStop = true;
            this.openNone.Text = "None";
            this.openNone.UseVisualStyleBackColor = true;
            this.openNone.CheckedChanged += new System.EventHandler(this.openNone_CheckedChanged);
            // 
            // urlBox
            // 
            this.urlBox.Location = new System.Drawing.Point(128, 71);
            this.urlBox.Name = "urlBox";
            this.urlBox.Size = new System.Drawing.Size(137, 20);
            this.urlBox.TabIndex = 13;
            // 
            // openFromUrl
            // 
            this.openFromUrl.AutoSize = true;
            this.openFromUrl.Location = new System.Drawing.Point(4, 68);
            this.openFromUrl.Name = "openFromUrl";
            this.openFromUrl.Size = new System.Drawing.Size(88, 17);
            this.openFromUrl.TabIndex = 12;
            this.openFromUrl.TabStop = true;
            this.openFromUrl.Text = "Open from url";
            this.openFromUrl.UseVisualStyleBackColor = true;
            this.openFromUrl.CheckedChanged += new System.EventHandler(this.openFromUrl_CheckedChanged);
            // 
            // optionOpenRecent
            // 
            this.optionOpenRecent.AutoSize = true;
            this.optionOpenRecent.Checked = true;
            this.optionOpenRecent.Location = new System.Drawing.Point(4, 24);
            this.optionOpenRecent.Margin = new System.Windows.Forms.Padding(2);
            this.optionOpenRecent.Name = "optionOpenRecent";
            this.optionOpenRecent.Size = new System.Drawing.Size(100, 17);
            this.optionOpenRecent.TabIndex = 7;
            this.optionOpenRecent.TabStop = true;
            this.optionOpenRecent.Text = "Open recent file";
            this.optionOpenRecent.UseVisualStyleBackColor = true;
            this.optionOpenRecent.CheckedChanged += new System.EventHandler(this.optionOpenRecent_CheckedChanged);
            // 
            // recentFilePathLabel
            // 
            this.recentFilePathLabel.BackColor = System.Drawing.SystemColors.Window;
            this.recentFilePathLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.recentFilePathLabel.Location = new System.Drawing.Point(128, 24);
            this.recentFilePathLabel.Margin = new System.Windows.Forms.Padding(2);
            this.recentFilePathLabel.Name = "recentFilePathLabel";
            this.recentFilePathLabel.ReadOnly = true;
            this.recentFilePathLabel.Size = new System.Drawing.Size(110, 13);
            this.recentFilePathLabel.TabIndex = 11;
            // 
            // openSpecificFile
            // 
            this.openSpecificFile.AutoSize = true;
            this.openSpecificFile.Location = new System.Drawing.Point(4, 46);
            this.openSpecificFile.Margin = new System.Windows.Forms.Padding(2);
            this.openSpecificFile.Name = "openSpecificFile";
            this.openSpecificFile.Size = new System.Drawing.Size(106, 17);
            this.openSpecificFile.TabIndex = 8;
            this.openSpecificFile.Text = "Open specific file";
            this.openSpecificFile.UseVisualStyleBackColor = true;
            this.openSpecificFile.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // openFileDialogBtn
            // 
            this.openFileDialogBtn.Enabled = false;
            this.openFileDialogBtn.Location = new System.Drawing.Point(243, 46);
            this.openFileDialogBtn.Margin = new System.Windows.Forms.Padding(2);
            this.openFileDialogBtn.Name = "openFileDialogBtn";
            this.openFileDialogBtn.Size = new System.Drawing.Size(22, 19);
            this.openFileDialogBtn.TabIndex = 10;
            this.openFileDialogBtn.Text = "...";
            this.openFileDialogBtn.UseVisualStyleBackColor = true;
            this.openFileDialogBtn.Click += new System.EventHandler(this.openFileDialogBtn_Click);
            // 
            // filePathInput
            // 
            this.filePathInput.Enabled = false;
            this.filePathInput.Location = new System.Drawing.Point(128, 46);
            this.filePathInput.Margin = new System.Windows.Forms.Padding(2);
            this.filePathInput.Name = "filePathInput";
            this.filePathInput.Size = new System.Drawing.Size(111, 20);
            this.filePathInput.TabIndex = 9;
            // 
            // saveSettingsBtn
            // 
            this.saveSettingsBtn.Location = new System.Drawing.Point(302, 362);
            this.saveSettingsBtn.Margin = new System.Windows.Forms.Padding(2);
            this.saveSettingsBtn.Name = "saveSettingsBtn";
            this.saveSettingsBtn.Size = new System.Drawing.Size(60, 24);
            this.saveSettingsBtn.TabIndex = 8;
            this.saveSettingsBtn.Text = "Save";
            this.saveSettingsBtn.UseVisualStyleBackColor = true;
            this.saveSettingsBtn.Click += new System.EventHandler(this.saveSettingsBtn_Click);
            // 
            // cancelSettingsBtn
            // 
            this.cancelSettingsBtn.Location = new System.Drawing.Point(366, 362);
            this.cancelSettingsBtn.Margin = new System.Windows.Forms.Padding(2);
            this.cancelSettingsBtn.Name = "cancelSettingsBtn";
            this.cancelSettingsBtn.Size = new System.Drawing.Size(60, 24);
            this.cancelSettingsBtn.TabIndex = 9;
            this.cancelSettingsBtn.Text = "Cancel";
            this.cancelSettingsBtn.UseVisualStyleBackColor = true;
            this.cancelSettingsBtn.Click += new System.EventHandler(this.cancelSettingsBtn_Click);
            // 
            // IMSOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(442, 390);
            this.Controls.Add(this.cancelSettingsBtn);
            this.Controls.Add(this.saveSettingsBtn);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "IMSOptions";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Options";
            this.TopMost = true;
            this.tabControl.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pLLoggingGroupBox.ResumeLayout(false);
            this.pLLoggingGroupBox.PerformLayout();
            this.pLWEGroupBox.ResumeLayout(false);
            this.pLWEGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.startupTabPage.ResumeLayout(false);
            this.startupTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage generalTabPage;
        private System.Windows.Forms.TabPage startupTabPage;
        private System.Windows.Forms.RadioButton optionOpenRecent;
        private System.Windows.Forms.TextBox recentFilePathLabel;
        private System.Windows.Forms.RadioButton openSpecificFile;
        private System.Windows.Forms.Button openFileDialogBtn;
        private System.Windows.Forms.TextBox filePathInput;
        private System.Windows.Forms.Button saveSettingsBtn;
        private System.Windows.Forms.Button cancelSettingsBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomCheckedListBox startPointCCLB;
        private System.Windows.Forms.CheckBox pLEnabled;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox pLWEGroupBox;
        private System.Windows.Forms.CheckBox warningsEnabled;
        private System.Windows.Forms.CheckBox errorsEnabled;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox extensionsEnabled;
        private System.Windows.Forms.CheckBox distinguishingColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox maxErrors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox maxWarnings;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox pLLoggingGroupBox;
        private System.Windows.Forms.CheckBox pLLEnabled;
        private System.Windows.Forms.Button pLLSLOpen;
        private System.Windows.Forms.TextBox pLLSLocation;
        private System.Windows.Forms.CheckBox pLLSLEnabled;
        private System.Windows.Forms.TextBox urlBox;
        private System.Windows.Forms.RadioButton openFromUrl;
        private System.Windows.Forms.RadioButton openNone;

    }
}