using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMSEnterprise
{
    public partial class NewPersonForm : Form
    {
        private bool edit;
        private person newEditPerson;
        private TextBox lastDigitInput = null;
        public NewPersonForm(person editPerson = null)
        {
            InitializeComponent();
            this.TopMost = true;
            this.TopLevel = true;
            if (editPerson == null)
            {
                this.edit = false;
                this.Text = "Create New Person";
                systemRoleTypeBox.SelectedItem = "None";

                sourceIdCurrLabel.Hide();
                //addSourceIdLabel.Hide();
                //addSourceIdInput.Hide();


                bdayPicker.Width = 90;
                this.lastDigitInput = new TextBox();
                this.lastDigitInput.Width = 45;
                this.lastDigitInput.TabIndex = 13;
                this.lastDigitInput.Location = new Point(bdayPicker.Width + 1 + bdayPicker.Left, bdayPicker.Top);
                this.Controls.Add(this.lastDigitInput);

                bdayLabel.Text = "*Social security number:";
                bdayLabel.Left -= 92; 
            }
            else
            {
                this.edit = true;

                this.newEditPerson = editPerson;

                this.Text = "Edit " + editPerson.name.fn;
                createEditButton.Text = "Save";

                sourceIdCurrLabel.Text = editPerson.sourcedid[0].id;
                if (editPerson.sourcedid.ToList().Count == 2)
                    sourceIdOldLabel.Text = editPerson.sourcedid[1].id;


                fnInput.Text = editPerson.name.n.given;
                lnInput.Text = editPerson.name.n.family;

                bdayPicker.Text = editPerson.demographics.bday;
                genderBox.SelectedItem = editPerson.demographics.gender;

                emailInput.Text = editPerson.email;
                hNumberInput.Text = editPerson.tel[0].Value;
                mNumberInput.Text = editPerson.tel[1].Value;

                postcodeInput.Text = editPerson.adr.pcode;
                streetInput.Text = editPerson.adr.street[0];
                localityInput.Text = editPerson.adr.locality;

                institutionTypeBox.SelectedItem = editPerson.institutionrole[0].institutionroletype.ToString();
                institutionPrimaryBox.SelectedItem = editPerson.institutionrole[0].primaryrole.ToString();

                systemRoleTypeBox.SelectedItem = editPerson.systemrole.systemroletype.ToString();
                if (editPerson.userid != null)
                {
                    if (editPerson.userid[0].Value != null)
                        userIdInput.Text = editPerson.userid[0].Value;
                    if (editPerson.userid[0].useridtype != null)
                        userIdTypeBox.SelectedItem = editPerson.userid[0].useridtype;
                }
            }
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            bdayPicker.CustomFormat = "";
        }

        public bool getEdit()
        {
            return this.edit;
        }

        public person getNewPerson()
        {
            return this.newEditPerson;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void createEditButton_Click(object sender, EventArgs e)
        {
            if (this.lastDigitInput != null) //create
            {
                this.newEditPerson = new person();
                // System Role
                newEditPerson.sourcedid[0].id = this.lastDigitInput.Text;
                // check what system role is chosen
                if ((String)systemRoleTypeBox.SelectedItem == "None")
                    newEditPerson.systemrole.systemroletype = systemroleSystemroletype.None;
                else if ((String)systemRoleTypeBox.SelectedItem == "User")
                    newEditPerson.systemrole.systemroletype = systemroleSystemroletype.User;
                else if ((String)systemRoleTypeBox.SelectedItem == "User")
                    newEditPerson.systemrole.systemroletype = systemroleSystemroletype.SysAdmin;
                // Name
                newEditPerson.name.n.family = lnInput.Text;
                newEditPerson.name.n.given = fnInput.Text;
                newEditPerson.name.fn = fnInput.Text + " " + lnInput.Text;
                //Contact info
                newEditPerson.email = emailInput.Text;
                newEditPerson.tel[0].teltype = telTeltype.Item1;
                newEditPerson.tel[0].Value = hNumberInput.Text;
                newEditPerson.tel[1].teltype = telTeltype.Mobile;
                newEditPerson.tel[1].Value = mNumberInput.Text;
                newEditPerson.tel[2].teltype = telTeltype.Item2;
                newEditPerson.tel[2].Value = workNumberInput.Text;
                // Address info
                newEditPerson.adr.pcode = postcodeInput.Text;
                newEditPerson.adr.street[0] = streetInput.Text;
                newEditPerson.adr.locality = localityInput.Text;
                // Demographics
                newEditPerson.demographics.bday = bdayPicker.Text;
                newEditPerson.demographics.gender = genderBox.SelectedItem.ToString();
                // Institution role
                if ((String)institutionTypeBox.SelectedItem == "Student")
                    newEditPerson.institutionrole[0].institutionroletype = institutionroleInstitutionroletype.Student;
                else if ((String)institutionTypeBox.SelectedItem == "Staff")
                    newEditPerson.institutionrole[0].institutionroletype = institutionroleInstitutionroletype.Student;
                else if ((String)institutionTypeBox.SelectedItem == "Contact")
                    newEditPerson.institutionrole[0].institutionroletype = institutionroleInstitutionroletype.Contact;
                else if ((String)institutionTypeBox.SelectedItem == "Child")
                    newEditPerson.institutionrole[0].institutionroletype = institutionroleInstitutionroletype.Other;

                if ((String)institutionPrimaryBox.SelectedValue == "No")
                    newEditPerson.institutionrole[0].primaryrole = institutionrolePrimaryrole.No;
                else
                    newEditPerson.institutionrole[0].primaryrole = institutionrolePrimaryrole.Yes;
            }
            else // edit
            {
                // System Role
               
                // check what system role is chosen
                if ((String)systemRoleTypeBox.SelectedItem == "None")
                    newEditPerson.systemrole.systemroletype = systemroleSystemroletype.None;
                else if ((String)systemRoleTypeBox.SelectedItem == "User")
                    newEditPerson.systemrole.systemroletype = systemroleSystemroletype.User;
                else if ((String)systemRoleTypeBox.SelectedItem == "Administrator")
                    newEditPerson.systemrole.systemroletype = systemroleSystemroletype.SysAdmin;
                // Name
                newEditPerson.name.n.family = lnInput.Text;
                newEditPerson.name.n.given = fnInput.Text;
                newEditPerson.name.fn = fnInput.Text + " " + lnInput.Text;
                //Contact info
                newEditPerson.email = emailInput.Text;
                newEditPerson.tel[0].teltype = telTeltype.Item1;
                newEditPerson.tel[0].Value = hNumberInput.Text;
                if(newEditPerson.tel.Length >= 2)
                {
                    newEditPerson.tel[1].teltype = telTeltype.Mobile;
                    newEditPerson.tel[1].Value = mNumberInput.Text;
                }
                if (newEditPerson.tel.Length >= 3)
                {
                    newEditPerson.tel[2].teltype = telTeltype.Item2;
                    newEditPerson.tel[2].Value = workNumberInput.Text;
                }
                // Address info
                newEditPerson.adr.pcode = postcodeInput.Text;
                newEditPerson.adr.street[0] = streetInput.Text;
                newEditPerson.adr.locality = localityInput.Text;
                // Demographics
                newEditPerson.demographics.bday = bdayPicker.Text;
                newEditPerson.demographics.gender = genderBox.SelectedItem.ToString();
                // Institution role
                if ((String)institutionTypeBox.SelectedItem == "Student")
                    newEditPerson.institutionrole[0].institutionroletype = institutionroleInstitutionroletype.Student;
                else if ((String)institutionTypeBox.SelectedItem == "Staff")
                    newEditPerson.institutionrole[0].institutionroletype = institutionroleInstitutionroletype.Staff;
                else if ((String)institutionTypeBox.SelectedItem == "Contact")
                    newEditPerson.institutionrole[0].institutionroletype = institutionroleInstitutionroletype.Contact;
                else if ((String)institutionTypeBox.SelectedItem == "Child")
                    newEditPerson.institutionrole[0].institutionroletype = institutionroleInstitutionroletype.Other;

                if ((String)institutionPrimaryBox.SelectedItem == "No")
                    newEditPerson.institutionrole[0].primaryrole = institutionrolePrimaryrole.No;
                else
                    newEditPerson.institutionrole[0].primaryrole = institutionrolePrimaryrole.Yes;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }

        private void NewPersonForm_Load(object sender, EventArgs e)
        {

        }

        private void input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                if (e.KeyChar != '-')
                    e.Handled = true;
            }
        }

        private void userIdTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(userIdTypeBox.SelectedItem == (Object)"PID")
            {
                this.userIdInput.Text = this.getPID(this.newEditPerson);
            }
            else
                this.userIdInput.Text = this.getGUID(this.newEditPerson);
        }

        private String getPID(person person)
        {
            foreach (userid uid in person.userid)
            {
                if (uid.useridtype == "PID")
                {
                    return uid.Value;
                }
            }
            return "";
        }

        private String getGUID(person person)
        {
            foreach (userid uid in person.userid)
            {
                if (uid.useridtype == "GUID")
                {
                    return uid.Value;
                }
            }
            return "";
        }
    }
}
