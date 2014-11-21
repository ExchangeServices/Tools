namespace IMSEnterprise
{
    partial class NewPersonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewPersonForm));
            this.createEditButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.firstNameLabel = new System.Windows.Forms.Label();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.fnInput = new System.Windows.Forms.TextBox();
            this.lnInput = new System.Windows.Forms.TextBox();
            this.demographicsLabel = new System.Windows.Forms.Label();
            this.genderLabel = new System.Windows.Forms.Label();
            this.bdayLabel = new System.Windows.Forms.Label();
            this.bdayPicker = new System.Windows.Forms.DateTimePicker();
            this.genderBox = new System.Windows.Forms.ComboBox();
            this.mobileNumberLabel = new System.Windows.Forms.Label();
            this.homeNumberLabel = new System.Windows.Forms.Label();
            this.contactInfoLabel = new System.Windows.Forms.Label();
            this.hNumberInput = new System.Windows.Forms.TextBox();
            this.mNumberInput = new System.Windows.Forms.TextBox();
            this.emailInput = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.postcodeInput = new System.Windows.Forms.TextBox();
            this.postcodeLabel = new System.Windows.Forms.Label();
            this.localityInput = new System.Windows.Forms.TextBox();
            this.streetInput = new System.Windows.Forms.TextBox();
            this.localityLabel = new System.Windows.Forms.Label();
            this.streetLabel = new System.Windows.Forms.Label();
            this.addressLabel = new System.Windows.Forms.Label();
            this.institutionTypeLabel = new System.Windows.Forms.Label();
            this.institutionPrimaryLabel = new System.Windows.Forms.Label();
            this.institutionLabel = new System.Windows.Forms.Label();
            this.institutionTypeBox = new System.Windows.Forms.ComboBox();
            this.institutionPrimaryBox = new System.Windows.Forms.ComboBox();
            this.systemRoleTypeBox = new System.Windows.Forms.ComboBox();
            this.systemRoleTypeLabel = new System.Windows.Forms.Label();
            this.systemRoleLabel = new System.Windows.Forms.Label();
            this.workNumberInput = new System.Windows.Forms.TextBox();
            this.workNumberLabel = new System.Windows.Forms.Label();
            this.sourceIdCurrLabel = new System.Windows.Forms.Label();
            this.sourceIdOldLabel = new System.Windows.Forms.Label();
            this.currentLabel = new System.Windows.Forms.Label();
            this.userIdInput = new System.Windows.Forms.TextBox();
            this.userIdLabel = new System.Windows.Forms.Label();
            this.userIdTypeLabel = new System.Windows.Forms.Label();
            this.userIdTypeBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // createEditButton
            // 
            this.createEditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createEditButton.Location = new System.Drawing.Point(417, 390);
            this.createEditButton.Margin = new System.Windows.Forms.Padding(2);
            this.createEditButton.Name = "createEditButton";
            this.createEditButton.Size = new System.Drawing.Size(64, 24);
            this.createEditButton.TabIndex = 18;
            this.createEditButton.Text = "Create";
            this.createEditButton.UseVisualStyleBackColor = true;
            this.createEditButton.Click += new System.EventHandler(this.createEditButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(349, 390);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(64, 24);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(39, 150);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(45, 17);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "Name";
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Location = new System.Drawing.Point(51, 174);
            this.firstNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Size = new System.Drawing.Size(58, 13);
            this.firstNameLabel.TabIndex = 3;
            this.firstNameLabel.Text = "First name:";
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Location = new System.Drawing.Point(51, 197);
            this.lastNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Size = new System.Drawing.Size(59, 13);
            this.lastNameLabel.TabIndex = 4;
            this.lastNameLabel.Text = "Last name:";
            // 
            // fnInput
            // 
            this.fnInput.Location = new System.Drawing.Point(116, 174);
            this.fnInput.Margin = new System.Windows.Forms.Padding(2);
            this.fnInput.Name = "fnInput";
            this.fnInput.Size = new System.Drawing.Size(104, 20);
            this.fnInput.TabIndex = 3;
            this.fnInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_KeyPress);
            // 
            // lnInput
            // 
            this.lnInput.Location = new System.Drawing.Point(116, 197);
            this.lnInput.Margin = new System.Windows.Forms.Padding(2);
            this.lnInput.Name = "lnInput";
            this.lnInput.Size = new System.Drawing.Size(104, 20);
            this.lnInput.TabIndex = 4;
            this.lnInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_KeyPress);
            // 
            // demographicsLabel
            // 
            this.demographicsLabel.AutoSize = true;
            this.demographicsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.demographicsLabel.Location = new System.Drawing.Point(286, 194);
            this.demographicsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.demographicsLabel.Name = "demographicsLabel";
            this.demographicsLabel.Size = new System.Drawing.Size(99, 17);
            this.demographicsLabel.TabIndex = 7;
            this.demographicsLabel.Text = "Demographics";
            // 
            // genderLabel
            // 
            this.genderLabel.AutoSize = true;
            this.genderLabel.Location = new System.Drawing.Point(311, 245);
            this.genderLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.genderLabel.Name = "genderLabel";
            this.genderLabel.Size = new System.Drawing.Size(45, 13);
            this.genderLabel.TabIndex = 9;
            this.genderLabel.Text = "Gender:";
            // 
            // bdayLabel
            // 
            this.bdayLabel.AutoSize = true;
            this.bdayLabel.Location = new System.Drawing.Point(308, 222);
            this.bdayLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.bdayLabel.Name = "bdayLabel";
            this.bdayLabel.Size = new System.Drawing.Size(48, 13);
            this.bdayLabel.TabIndex = 8;
            this.bdayLabel.Text = "Birthday:";
            // 
            // bdayPicker
            // 
            this.bdayPicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bdayPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.bdayPicker.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bdayPicker.Location = new System.Drawing.Point(362, 222);
            this.bdayPicker.Margin = new System.Windows.Forms.Padding(2);
            this.bdayPicker.Name = "bdayPicker";
            this.bdayPicker.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bdayPicker.Size = new System.Drawing.Size(104, 19);
            this.bdayPicker.TabIndex = 12;
            // 
            // genderBox
            // 
            this.genderBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genderBox.FormattingEnabled = true;
            this.genderBox.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Other"});
            this.genderBox.Location = new System.Drawing.Point(362, 245);
            this.genderBox.Margin = new System.Windows.Forms.Padding(2);
            this.genderBox.Name = "genderBox";
            this.genderBox.Size = new System.Drawing.Size(104, 21);
            this.genderBox.TabIndex = 14;
            // 
            // mobileNumberLabel
            // 
            this.mobileNumberLabel.AutoSize = true;
            this.mobileNumberLabel.Location = new System.Drawing.Point(38, 333);
            this.mobileNumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.mobileNumberLabel.Name = "mobileNumberLabel";
            this.mobileNumberLabel.Size = new System.Drawing.Size(79, 13);
            this.mobileNumberLabel.TabIndex = 16;
            this.mobileNumberLabel.Text = "Mobile number:";
            // 
            // homeNumberLabel
            // 
            this.homeNumberLabel.AutoSize = true;
            this.homeNumberLabel.Location = new System.Drawing.Point(40, 310);
            this.homeNumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.homeNumberLabel.Name = "homeNumberLabel";
            this.homeNumberLabel.Size = new System.Drawing.Size(76, 13);
            this.homeNumberLabel.TabIndex = 15;
            this.homeNumberLabel.Text = "Home number:";
            // 
            // contactInfoLabel
            // 
            this.contactInfoLabel.AutoSize = true;
            this.contactInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contactInfoLabel.Location = new System.Drawing.Point(45, 263);
            this.contactInfoLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.contactInfoLabel.Name = "contactInfoLabel";
            this.contactInfoLabel.Size = new System.Drawing.Size(83, 17);
            this.contactInfoLabel.TabIndex = 14;
            this.contactInfoLabel.Text = "Contact info";
            // 
            // hNumberInput
            // 
            this.hNumberInput.Location = new System.Drawing.Point(122, 310);
            this.hNumberInput.Margin = new System.Windows.Forms.Padding(2);
            this.hNumberInput.Name = "hNumberInput";
            this.hNumberInput.Size = new System.Drawing.Size(104, 20);
            this.hNumberInput.TabIndex = 6;
            this.hNumberInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_KeyPress);
            // 
            // mNumberInput
            // 
            this.mNumberInput.Location = new System.Drawing.Point(122, 333);
            this.mNumberInput.Margin = new System.Windows.Forms.Padding(2);
            this.mNumberInput.Name = "mNumberInput";
            this.mNumberInput.Size = new System.Drawing.Size(104, 20);
            this.mNumberInput.TabIndex = 7;
            this.mNumberInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_KeyPress);
            // 
            // emailInput
            // 
            this.emailInput.Location = new System.Drawing.Point(122, 288);
            this.emailInput.Margin = new System.Windows.Forms.Padding(2);
            this.emailInput.Name = "emailInput";
            this.emailInput.Size = new System.Drawing.Size(104, 20);
            this.emailInput.TabIndex = 5;
            this.emailInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_KeyPress);
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(77, 288);
            this.emailLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(38, 13);
            this.emailLabel.TabIndex = 19;
            this.emailLabel.Text = "E-mail:";
            // 
            // postcodeInput
            // 
            this.postcodeInput.Location = new System.Drawing.Point(362, 113);
            this.postcodeInput.Margin = new System.Windows.Forms.Padding(2);
            this.postcodeInput.Name = "postcodeInput";
            this.postcodeInput.Size = new System.Drawing.Size(104, 20);
            this.postcodeInput.TabIndex = 9;
            this.postcodeInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_KeyPress);
            // 
            // postcodeLabel
            // 
            this.postcodeLabel.AutoSize = true;
            this.postcodeLabel.Location = new System.Drawing.Point(303, 112);
            this.postcodeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.postcodeLabel.Name = "postcodeLabel";
            this.postcodeLabel.Size = new System.Drawing.Size(55, 13);
            this.postcodeLabel.TabIndex = 26;
            this.postcodeLabel.Text = "Postcode:";
            // 
            // localityInput
            // 
            this.localityInput.Location = new System.Drawing.Point(362, 158);
            this.localityInput.Margin = new System.Windows.Forms.Padding(2);
            this.localityInput.Name = "localityInput";
            this.localityInput.Size = new System.Drawing.Size(104, 20);
            this.localityInput.TabIndex = 11;
            this.localityInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_KeyPress);
            // 
            // streetInput
            // 
            this.streetInput.Location = new System.Drawing.Point(362, 136);
            this.streetInput.Margin = new System.Windows.Forms.Padding(2);
            this.streetInput.Name = "streetInput";
            this.streetInput.Size = new System.Drawing.Size(104, 20);
            this.streetInput.TabIndex = 10;
            this.streetInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_KeyPress);
            // 
            // localityLabel
            // 
            this.localityLabel.AutoSize = true;
            this.localityLabel.Location = new System.Drawing.Point(311, 158);
            this.localityLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.localityLabel.Name = "localityLabel";
            this.localityLabel.Size = new System.Drawing.Size(46, 13);
            this.localityLabel.TabIndex = 23;
            this.localityLabel.Text = "Locality:";
            // 
            // streetLabel
            // 
            this.streetLabel.AutoSize = true;
            this.streetLabel.Location = new System.Drawing.Point(320, 135);
            this.streetLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.streetLabel.Name = "streetLabel";
            this.streetLabel.Size = new System.Drawing.Size(38, 13);
            this.streetLabel.TabIndex = 22;
            this.streetLabel.Text = "Street:";
            // 
            // addressLabel
            // 
            this.addressLabel.AutoSize = true;
            this.addressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addressLabel.Location = new System.Drawing.Point(286, 89);
            this.addressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.addressLabel.Name = "addressLabel";
            this.addressLabel.Size = new System.Drawing.Size(87, 17);
            this.addressLabel.TabIndex = 21;
            this.addressLabel.Text = "Address info";
            // 
            // institutionTypeLabel
            // 
            this.institutionTypeLabel.AutoSize = true;
            this.institutionTypeLabel.Location = new System.Drawing.Point(260, 332);
            this.institutionTypeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.institutionTypeLabel.Name = "institutionTypeLabel";
            this.institutionTypeLabel.Size = new System.Drawing.Size(98, 13);
            this.institutionTypeLabel.TabIndex = 33;
            this.institutionTypeLabel.Text = "Institution role type:";
            // 
            // institutionPrimaryLabel
            // 
            this.institutionPrimaryLabel.AutoSize = true;
            this.institutionPrimaryLabel.Location = new System.Drawing.Point(243, 354);
            this.institutionPrimaryLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.institutionPrimaryLabel.Name = "institutionPrimaryLabel";
            this.institutionPrimaryLabel.Size = new System.Drawing.Size(111, 13);
            this.institutionPrimaryLabel.TabIndex = 29;
            this.institutionPrimaryLabel.Text = "Institution role primary:";
            // 
            // institutionLabel
            // 
            this.institutionLabel.AutoSize = true;
            this.institutionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.institutionLabel.Location = new System.Drawing.Point(286, 307);
            this.institutionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.institutionLabel.Name = "institutionLabel";
            this.institutionLabel.Size = new System.Drawing.Size(96, 17);
            this.institutionLabel.TabIndex = 28;
            this.institutionLabel.Text = "Institution role";
            // 
            // institutionTypeBox
            // 
            this.institutionTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.institutionTypeBox.FormattingEnabled = true;
            this.institutionTypeBox.Items.AddRange(new object[] {
            "Student",
            "Staff",
            "Contact",
            "Child"});
            this.institutionTypeBox.Location = new System.Drawing.Point(362, 332);
            this.institutionTypeBox.Margin = new System.Windows.Forms.Padding(2);
            this.institutionTypeBox.Name = "institutionTypeBox";
            this.institutionTypeBox.Size = new System.Drawing.Size(103, 21);
            this.institutionTypeBox.TabIndex = 15;
            // 
            // institutionPrimaryBox
            // 
            this.institutionPrimaryBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.institutionPrimaryBox.FormattingEnabled = true;
            this.institutionPrimaryBox.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.institutionPrimaryBox.Location = new System.Drawing.Point(362, 354);
            this.institutionPrimaryBox.Margin = new System.Windows.Forms.Padding(2);
            this.institutionPrimaryBox.Name = "institutionPrimaryBox";
            this.institutionPrimaryBox.Size = new System.Drawing.Size(103, 21);
            this.institutionPrimaryBox.TabIndex = 16;
            // 
            // systemRoleTypeBox
            // 
            this.systemRoleTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.systemRoleTypeBox.FormattingEnabled = true;
            this.systemRoleTypeBox.Items.AddRange(new object[] {
            "None",
            "User",
            "Administrator"});
            this.systemRoleTypeBox.Location = new System.Drawing.Point(116, 111);
            this.systemRoleTypeBox.Margin = new System.Windows.Forms.Padding(2);
            this.systemRoleTypeBox.Name = "systemRoleTypeBox";
            this.systemRoleTypeBox.Size = new System.Drawing.Size(103, 21);
            this.systemRoleTypeBox.TabIndex = 2;
            // 
            // systemRoleTypeLabel
            // 
            this.systemRoleTypeLabel.AutoSize = true;
            this.systemRoleTypeLabel.Location = new System.Drawing.Point(23, 113);
            this.systemRoleTypeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.systemRoleTypeLabel.Name = "systemRoleTypeLabel";
            this.systemRoleTypeLabel.Size = new System.Drawing.Size(87, 13);
            this.systemRoleTypeLabel.TabIndex = 37;
            this.systemRoleTypeLabel.Text = "System role type:";
            // 
            // systemRoleLabel
            // 
            this.systemRoleLabel.AutoSize = true;
            this.systemRoleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.systemRoleLabel.Location = new System.Drawing.Point(39, 89);
            this.systemRoleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.systemRoleLabel.Name = "systemRoleLabel";
            this.systemRoleLabel.Size = new System.Drawing.Size(82, 17);
            this.systemRoleLabel.TabIndex = 36;
            this.systemRoleLabel.Text = "System role";
            // 
            // workNumberInput
            // 
            this.workNumberInput.Location = new System.Drawing.Point(122, 356);
            this.workNumberInput.Margin = new System.Windows.Forms.Padding(2);
            this.workNumberInput.Name = "workNumberInput";
            this.workNumberInput.Size = new System.Drawing.Size(104, 20);
            this.workNumberInput.TabIndex = 8;
            this.workNumberInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.input_KeyPress);
            // 
            // workNumberLabel
            // 
            this.workNumberLabel.AutoSize = true;
            this.workNumberLabel.Location = new System.Drawing.Point(44, 354);
            this.workNumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.workNumberLabel.Name = "workNumberLabel";
            this.workNumberLabel.Size = new System.Drawing.Size(74, 13);
            this.workNumberLabel.TabIndex = 39;
            this.workNumberLabel.Text = "Work number:";
            // 
            // sourceIdCurrLabel
            // 
            this.sourceIdCurrLabel.AutoSize = true;
            this.sourceIdCurrLabel.BackColor = System.Drawing.SystemColors.Control;
            this.sourceIdCurrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceIdCurrLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.sourceIdCurrLabel.Location = new System.Drawing.Point(59, 17);
            this.sourceIdCurrLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.sourceIdCurrLabel.Name = "sourceIdCurrLabel";
            this.sourceIdCurrLabel.Size = new System.Drawing.Size(50, 13);
            this.sourceIdCurrLabel.TabIndex = 41;
            this.sourceIdCurrLabel.Text = "source id";
            // 
            // sourceIdOldLabel
            // 
            this.sourceIdOldLabel.AutoSize = true;
            this.sourceIdOldLabel.BackColor = System.Drawing.SystemColors.Control;
            this.sourceIdOldLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.sourceIdOldLabel.Location = new System.Drawing.Point(48, 28);
            this.sourceIdOldLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.sourceIdOldLabel.Name = "sourceIdOldLabel";
            this.sourceIdOldLabel.Size = new System.Drawing.Size(0, 13);
            this.sourceIdOldLabel.TabIndex = 44;
            // 
            // currentLabel
            // 
            this.currentLabel.AutoSize = true;
            this.currentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentLabel.Location = new System.Drawing.Point(9, 14);
            this.currentLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentLabel.Name = "currentLabel";
            this.currentLabel.Size = new System.Drawing.Size(46, 17);
            this.currentLabel.TabIndex = 45;
            this.currentLabel.Text = "GUID:";
            // 
            // userIdInput
            // 
            this.userIdInput.Location = new System.Drawing.Point(389, 10);
            this.userIdInput.Margin = new System.Windows.Forms.Padding(2);
            this.userIdInput.Name = "userIdInput";
            this.userIdInput.Size = new System.Drawing.Size(76, 20);
            this.userIdInput.TabIndex = 1;
            // 
            // userIdLabel
            // 
            this.userIdLabel.AutoSize = true;
            this.userIdLabel.Location = new System.Drawing.Point(332, 14);
            this.userIdLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.userIdLabel.Name = "userIdLabel";
            this.userIdLabel.Size = new System.Drawing.Size(46, 13);
            this.userIdLabel.TabIndex = 48;
            this.userIdLabel.Text = "User ID:";
            // 
            // userIdTypeLabel
            // 
            this.userIdTypeLabel.AutoSize = true;
            this.userIdTypeLabel.Location = new System.Drawing.Point(310, 32);
            this.userIdTypeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.userIdTypeLabel.Name = "userIdTypeLabel";
            this.userIdTypeLabel.Size = new System.Drawing.Size(69, 13);
            this.userIdTypeLabel.TabIndex = 50;
            this.userIdTypeLabel.Text = "User ID type:";
            // 
            // userIdTypeBox
            // 
            this.userIdTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userIdTypeBox.FormattingEnabled = true;
            this.userIdTypeBox.Items.AddRange(new object[] {
            "PID",
            "GUID"});
            this.userIdTypeBox.Location = new System.Drawing.Point(389, 32);
            this.userIdTypeBox.Margin = new System.Windows.Forms.Padding(2);
            this.userIdTypeBox.Name = "userIdTypeBox";
            this.userIdTypeBox.Size = new System.Drawing.Size(76, 21);
            this.userIdTypeBox.TabIndex = 2;
            this.userIdTypeBox.SelectedIndexChanged += new System.EventHandler(this.userIdTypeBox_SelectedIndexChanged);
            // 
            // NewPersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 422);
            this.ControlBox = false;
            this.Controls.Add(this.userIdTypeBox);
            this.Controls.Add(this.userIdTypeLabel);
            this.Controls.Add(this.userIdInput);
            this.Controls.Add(this.userIdLabel);
            this.Controls.Add(this.currentLabel);
            this.Controls.Add(this.sourceIdOldLabel);
            this.Controls.Add(this.sourceIdCurrLabel);
            this.Controls.Add(this.workNumberInput);
            this.Controls.Add(this.workNumberLabel);
            this.Controls.Add(this.systemRoleTypeBox);
            this.Controls.Add(this.systemRoleTypeLabel);
            this.Controls.Add(this.systemRoleLabel);
            this.Controls.Add(this.institutionPrimaryBox);
            this.Controls.Add(this.institutionTypeBox);
            this.Controls.Add(this.institutionTypeLabel);
            this.Controls.Add(this.institutionPrimaryLabel);
            this.Controls.Add(this.institutionLabel);
            this.Controls.Add(this.postcodeInput);
            this.Controls.Add(this.postcodeLabel);
            this.Controls.Add(this.localityInput);
            this.Controls.Add(this.streetInput);
            this.Controls.Add(this.localityLabel);
            this.Controls.Add(this.streetLabel);
            this.Controls.Add(this.addressLabel);
            this.Controls.Add(this.emailInput);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.mNumberInput);
            this.Controls.Add(this.hNumberInput);
            this.Controls.Add(this.mobileNumberLabel);
            this.Controls.Add(this.homeNumberLabel);
            this.Controls.Add(this.contactInfoLabel);
            this.Controls.Add(this.genderBox);
            this.Controls.Add(this.bdayPicker);
            this.Controls.Add(this.genderLabel);
            this.Controls.Add(this.bdayLabel);
            this.Controls.Add(this.demographicsLabel);
            this.Controls.Add(this.lnInput);
            this.Controls.Add(this.fnInput);
            this.Controls.Add(this.lastNameLabel);
            this.Controls.Add(this.firstNameLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.createEditButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "NewPersonForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "NewPersonForm";
            this.Load += new System.EventHandler(this.NewPersonForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createEditButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label firstNameLabel;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.TextBox fnInput;
        private System.Windows.Forms.TextBox lnInput;
        private System.Windows.Forms.Label demographicsLabel;
        private System.Windows.Forms.Label genderLabel;
        private System.Windows.Forms.Label bdayLabel;
        private System.Windows.Forms.DateTimePicker bdayPicker;
        private System.Windows.Forms.ComboBox genderBox;
        private System.Windows.Forms.Label mobileNumberLabel;
        private System.Windows.Forms.Label homeNumberLabel;
        private System.Windows.Forms.Label contactInfoLabel;
        private System.Windows.Forms.TextBox hNumberInput;
        private System.Windows.Forms.TextBox mNumberInput;
        private System.Windows.Forms.TextBox emailInput;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.TextBox postcodeInput;
        private System.Windows.Forms.Label postcodeLabel;
        private System.Windows.Forms.TextBox localityInput;
        private System.Windows.Forms.TextBox streetInput;
        private System.Windows.Forms.Label localityLabel;
        private System.Windows.Forms.Label streetLabel;
        private System.Windows.Forms.Label addressLabel;
        private System.Windows.Forms.Label institutionTypeLabel;
        private System.Windows.Forms.Label institutionPrimaryLabel;
        private System.Windows.Forms.Label institutionLabel;
        private System.Windows.Forms.ComboBox institutionTypeBox;
        private System.Windows.Forms.ComboBox institutionPrimaryBox;
        private System.Windows.Forms.ComboBox systemRoleTypeBox;
        private System.Windows.Forms.Label systemRoleTypeLabel;
        private System.Windows.Forms.Label systemRoleLabel;
        private System.Windows.Forms.TextBox workNumberInput;
        private System.Windows.Forms.Label workNumberLabel;
        private System.Windows.Forms.Label sourceIdCurrLabel;
        private System.Windows.Forms.Label sourceIdOldLabel;
        private System.Windows.Forms.Label currentLabel;
        private System.Windows.Forms.TextBox userIdInput;
        private System.Windows.Forms.Label userIdLabel;
        private System.Windows.Forms.Label userIdTypeLabel;
        private System.Windows.Forms.ComboBox userIdTypeBox;
    }
}