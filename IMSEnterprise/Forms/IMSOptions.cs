using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace IMSEnterprise
{
    public partial class IMSOptions : Form
    {
        private IMSSettings IMSSettings;
        private String currentFilePath;
        private bool isDone = false;

        private bool maxChanged = false;
        public bool MaxChanged
        {
            get { return maxChanged; }
            set { maxChanged = value; }
        }

        private bool startPointChanged;
        public bool StartPointChanged
        {
            get { return startPointChanged; }
            set { startPointChanged = value; }
        }

        public IMSOptions(String currentFilePath)
        {
            InitializeComponent();
            this.currentFilePath = currentFilePath;
            this.onLoad();
            this.TopLevel = true;
            this.TopMost = true;
            
            #if DEBUG
                this.TopMost = false;
            #endif
        }
        private void onLoad()
        {
            this.StartPointChanged = false;
            this.IMSSettings = new IMSSettings();
           
            //Set the current settings of all the controls
            this.initControls();
            this.isDone = true;
        }
        private void initControls()
        {
            //----Recent file settings-----
            recentFilePathLabel.Text = IMSSettings.Startup.FileToOpen.Value;
            Size size = TextRenderer.MeasureText(recentFilePathLabel.Text, recentFilePathLabel.Font);
            recentFilePathLabel.Width = size.Width;

            if (this.IMSSettings.Startup.FileToOpen.Choice != 2)
                this.urlBox.Enabled = false;
            else
                this.urlBox.Text = this.IMSSettings.Startup.FileToOpen.Value;

            //----Start point settings-------
            int count = 0;
            foreach (Root root in this.IMSSettings.General.StartPoint.Roots)
            {
                this.startPointCCLB.Items.Add(root.Type);
                this.startPointCCLB.SetItemChecked(count, root.Value);
                count++;
            }

            //----Problem Lister settings----
            //Enabled?
            this.pLEnabled.Checked = this.IMSSettings.General.ProblemLister.Enabled;
            
            //Show wEGroupBox if the ProblemLister is enabled.
            this.pLWEGroupBox.Enabled = this.IMSSettings.General.ProblemLister.Enabled;
            this.warningsEnabled.Checked = this.IMSSettings.General.ProblemLister.Warnings;
            this.errorsEnabled.Checked = this.IMSSettings.General.ProblemLister.Errors;
            this.maxWarnings.Text = this.IMSSettings.General.ProblemLister.MaxWarnings.ToString();
            this.maxErrors.Text = this.IMSSettings.General.ProblemLister.MaxErrors.ToString();

            //Show groupBox6 if the ProblemLister is enabled
            this.pLLoggingGroupBox.Enabled = this.IMSSettings.General.ProblemLister.Enabled;
            this.pLLEnabled.Checked = this.IMSSettings.General.ProblemLister.Log;
            this.pLLSLEnabled.Checked = this.IMSSettings.General.ProblemLister.LogLocation.Use;
            this.pLLSLocation.Text = this.IMSSettings.General.ProblemLister.LogLocation.Location;
            if (this.IMSSettings.General.ProblemLister.Log)
            {
                this.pLLSLEnabled.Enabled = true;
                this.pLLSLocation.Enabled = true;
                this.pLLSLOpen.Enabled = true;
            }
            else
            {
                this.pLLSLEnabled.Enabled = false;
                this.pLLSLocation.Enabled = false;
                this.pLLSLOpen.Enabled = false;
            }

            //----Extensions settings-----
            this.groupBox5.Enabled = this.IMSSettings.General.Extensions.Enabled;
            this.extensionsEnabled.Checked = this.IMSSettings.General.Extensions.Enabled;
            this.distinguishingColor.Checked = this.IMSSettings.General.Extensions.Distinguish;

            //------Startup settings-----
            if (this.IMSSettings.Startup.FileToOpen.Choice == 0)
            {
                this.optionOpenRecent.Checked = true;
            }
            else if (this.IMSSettings.Startup.FileToOpen.Choice == 1)
            {
                this.openSpecificFile.Checked = true;
                this.filePathInput.Text = this.IMSSettings.Startup.FileToOpen.Value;
            }
            else if (this.IMSSettings.Startup.FileToOpen.Choice == 2)
            {
                this.openFromUrl.Checked = true;
            }
            else
            {
                this.openNone.Checked = true;
            }


        }
        private void optionOpenRecent_CheckedChanged(object sender, EventArgs e)
        {
            filePathInput.Enabled = false;
            openFileDialogBtn.Enabled = false;
            recentFilePathLabel.Enabled = true;
            urlBox.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            filePathInput.Enabled = true;
            openFileDialogBtn.Enabled = true;
            recentFilePathLabel.Enabled = false;
            urlBox.Enabled = false;
        }

        private void openNone_CheckedChanged(object sender, EventArgs e)
        {
            filePathInput.Enabled = false;
            openFileDialogBtn.Enabled = false;
            recentFilePathLabel.Enabled = false;
            urlBox.Enabled = false;
        }

        private void openFileDialogBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML files (.xml)|*.xml";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                String s = ofd.FileName; // get the path of the file
                if (s.EndsWith("xml") == true) // make sure the file is a xml file
                {
                    filePathInput.Text = ofd.FileName;
                }
                else // the file wasn't a xml file
                {
                    // explain the problem to the user 
                    DialogResult result = MessageBox.Show("The file selected was of the wrong file type, it needs to be an XML-file (.xml)", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry) // person want to retry opening a file
                    {
                        this.openFileDialogBtn_Click(sender, e); // call same function to get same things happening
                    }
                }
            }
        }

        private void cancelSettingsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveSettingsBtn_Click(object sender, EventArgs e)
        {
            if (this.maxErrors.Text != this.IMSSettings.General.ProblemLister.MaxErrors.ToString() || this.maxWarnings.Text != this.IMSSettings.General.ProblemLister.MaxWarnings.ToString())
                this.maxChanged = true;
            applyStartupSettings(sender, e);
            applyProblemListerSettings();
            applyExtensionSettings();
            if(this.startPointCCLB.CheckedItems.Count < 1)
            {
                DialogResult result = MessageBox.Show("Warning no start point is set. Do you wish set Unit as default?", "Warning no start point set", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                if(result == DialogResult.No || result == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    this.startPointChanged = true;
                    this.IMSSettings.General.StartPoint.Roots[0].Value = true;
                }
            }
            try 
            {

                this.IMSSettings.Save();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
            }
            
        }

        private void applyStartupSettings(object sender, EventArgs e)
        {
            //If 'Open recent file' is checked
            if (optionOpenRecent.Checked)
            {
                this.IMSSettings.Startup.FileToOpen.Choice = 0;
                this.IMSSettings.Startup.FileToOpen.Value = this.currentFilePath;
            }
            else if(openSpecificFile.Checked)
            {
                this.IMSSettings.Startup.FileToOpen.Choice = 1;
                if(filePathInput.Text != "" && filePathInput.Text != null)
                {

                    this.IMSSettings.Startup.FileToOpen.Value = filePathInput.Text;
                }
                else
                {
                    DialogResult result = MessageBox.Show("You have to select an IMS-file. Do you want to do this now?", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry) // person want to retry opening a file
                    {
                        this.openFileDialogBtn_Click(sender, e); // call same function to get same things happening
                    }
                }
            }
            else if(openFromUrl.Checked)
            {
                this.IMSSettings.Startup.FileToOpen.Choice = 2;
                if (Uri.IsWellFormedUriString(this.urlBox.Text, UriKind.Absolute) || this.urlBox.Text.EndsWith(".xml") || this.urlBox.Text.EndsWith(".xml/"))
                    this.IMSSettings.Startup.FileToOpen.Value = this.urlBox.Text;
                else
                    MessageBox.Show("Something is wrong with the url", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.IMSSettings.Startup.FileToOpen.Choice = 3;
                
            }
        }
        private void applyProblemListerSettings()
        {
            this.IMSSettings.General.ProblemLister.Enabled = pLEnabled.Checked;

            this.IMSSettings.General.ProblemLister.Warnings = warningsEnabled.Checked;
            this.IMSSettings.General.ProblemLister.Errors = errorsEnabled.Checked;
            if (maxWarnings.Text.IsEmpty())
                maxWarnings.Text = "0";
            this.IMSSettings.General.ProblemLister.MaxWarnings = int.Parse(maxWarnings.Text);
            if (maxErrors.Text.IsEmpty())
                maxErrors.Text = "0";
            this.IMSSettings.General.ProblemLister.MaxErrors = int.Parse(maxErrors.Text);

            this.IMSSettings.General.ProblemLister.Log = pLLEnabled.Checked;
            if (!this.pLLSLEnabled.Enabled)
                this.pLLSLocation.Text = "";
            this.IMSSettings.General.ProblemLister.LogLocation.Location = this.pLLSLocation.Text;
            this.IMSSettings.General.ProblemLister.LogLocation.Use = this.pLLSLEnabled.Checked;
        }
        private void applyExtensionSettings()
        {
            this.IMSSettings.General.Extensions.Enabled = this.extensionsEnabled.Checked;
            this.IMSSettings.General.Extensions.Distinguish = this.distinguishingColor.Checked;
        }

        private void startPointCCLB_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(this.isDone)
            {
                this.startPointChanged = true;
                this.IMSSettings.General.StartPoint.Roots[e.Index].Value = Convert.ToBoolean((int) e.NewValue);
            }
            
        }

        private void pLEnabled_CheckStateChanged(object sender, EventArgs e)
        {
            this.pLWEGroupBox.Enabled = this.pLEnabled.Checked;
            this.pLLoggingGroupBox.Enabled = this.pLEnabled.Checked;    
        }

        private void maxInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

        }

    

        private void extensionsEnabled_CheckStateChanged(object sender, EventArgs e)
        {

            this.groupBox5.Enabled = this.extensionsEnabled.Checked;

        }

        private void pLLSLEnabled_CheckStateChanged(object sender, EventArgs e)
        {

            this.pLLSLocation.Enabled = this.pLLSLEnabled.Checked;
            this.pLLSLOpen.Enabled = this.pLLSLEnabled.Checked;
        }

        private void pLLSLOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.pLLSLocation.Text = fbd.SelectedPath;
                MessageBox.Show("To take effect, this requires a restart of the program!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void openFromUrl_CheckedChanged(object sender, EventArgs e)
        {
            filePathInput.Enabled = false;
            openFileDialogBtn.Enabled = false;
            recentFilePathLabel.Enabled = false;
            urlBox.Enabled = true;
        }

        private void pLLEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (pLLEnabled.Checked)
            {
                this.pLLSLEnabled.Enabled = true;
                this.pLLSLocation.Enabled = true;
                this.pLLSLOpen.Enabled = true;
            }
            else
            {
                this.pLLSLEnabled.Enabled = false;
                this.pLLSLocation.Enabled = false;
                this.pLLSLOpen.Enabled = false;
                this.pLLSLEnabled.Checked = false;
            }
        }

       
    }
}
