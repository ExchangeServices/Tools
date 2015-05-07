#region Using Directives
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Timers;
using System.Reflection;
#endregion

namespace IMSEnterprise
{
    public partial class MainForm : Form
    {
        private string currentUrl;
        private enterprise ep;
        private IDictionary<String, Label> labelList = new Dictionary<String, Label>();
        private NewPersonForm editCreatePerson;
        private IMSFileInformationForm fileInformationForm;
        private String currentFilePath;
        private IMSOptions IMSOptions;
        private IMSSettings IMSSettings;
        private int currentFileLoadTime;
        private int downloadAndProcessTime = 0;
        private bool recentSaved = true;
        private bool newFile = false;
        private ListViewColumnSorter lvwColumnSorter, eLvwColumnSorter;

        private List<Error> errorList;
        private WriteProblemLog log;
        private String logPath;

        private BackgroundWorker bgwIMS = new BackgroundWorker();

        private int Errors = 0;
        private int Warnings = 0;

        private bool exportBusy = false;
        private String encoding;
       


        BackgroundWorker bgwSearchPerson = new BackgroundWorker();
        BackgroundWorker bgwSearchGroup = new BackgroundWorker();

        public MainForm()
        {
            InitializeComponent();
            this.onLoad();
        }
        private void onLoad()
        {
            this.errorList = new List<Error>();

            this.errorListView1.SmallImageList = new ImageList();
            this.errorListView1.SmallImageList.TransparentColor = Color.White;

            this.errorListView1.SmallImageList.Images.Add("warning", IMSEnterprise.Properties.Resources.warning);
            this.errorListView1.SmallImageList.Images.Add("error", IMSEnterprise.Properties.Resources.error);

            this.totErrorLabel.Text = "0 error(s)";
            this.totWarningLabel.Text = "0 warning(s)";

            this.eLvwColumnSorter = new ListViewColumnSorter();
            this.lvwColumnSorter = new ListViewColumnSorter();
            this.listView.ListViewItemSorter = this.lvwColumnSorter;
            this.errorListView1.ListViewItemSorter = this.eLvwColumnSorter;

            this.MinimumSize = new Size(1024, this.Size.Height);

            this.searchInput.GotFocus += this.searchInput_Enter;
            this.searchInput.LostFocus += this.searchInput_Leave;
            this.searchInput.Tag = 0;

            this.documentationExists();

            this.KeyPreview = true;

            enterpriseList.ShowNodeToolTips = true;
            saveToolStripMenuItem.Enabled = false;
            sourceIdLabel.Height = 20;
            progressBar.Hide();
            // get IMSEnterprise.config
            this.IMSSettings = new IMSSettings();
            this.initSettings();
            // prepare the bgw for reading files
            bgwIMS.DoWork += bgwIMS_DoWork;
            bgwIMS.RunWorkerCompleted += bgwIMS_RunWorkerCompleted;
            bgwIMS.WorkerSupportsCancellation = true;

            if(this.IMSSettings.Startup.FileToOpen.Choice != 3)
            {
                if (Uri.IsWellFormedUriString(this.IMSSettings.Startup.FileToOpen.Value, UriKind.Absolute)) // file to open is a uri
                {
                    UrlInputDialog url = new UrlInputDialog(this.IMSSettings.Startup.FileToOpen.Value);
                    url.StartPosition = FormStartPosition.CenterScreen;
                    url.Text = "Downloading...";
                    if (url.ShowDialog() == DialogResult.OK)
                    {
                        this.downloadAndProcessTime = Convert.ToInt32(url.Timer.ElapsedMilliseconds);
                        this.currentFilePath = url.CurrentFilePath;
                        this.recentSaved = false;
                        if (this.IMSSettings.Startup.FileToOpen.Choice == 0)
                            this.IMSSettings.Startup.FileToOpen.Value = url.Uri;

                        this.enableLoadingMode();
                        bgwIMS.RunWorkerAsync();
                    }
                    else // download was interupted
                    {
                        this.fileNotFound();
                    }
                }
                else // file to open is a local file
                {
                    if (IMSSettings.Startup.FileToOpen.Value != null) // file to open had no value
                    {
                        FileInfo info = new FileInfo(@IMSSettings.Startup.FileToOpen.Value);
                        if (!info.Exists) // file didnt exist
                        {
                            this.fileNotFound();
                        }
                        else // file did exist
                        {

                            this.currentFilePath = @IMSSettings.Startup.FileToOpen.Value;

                            this.enableLoadingMode();
                            bgwIMS.RunWorkerAsync(); // start processing the file
                        }
                    }
                }
            }
        }
        private bool documentationExists()
        {
            if (File.Exists(Path.GetDirectoryName(Application.ExecutablePath) + "\\IMS Enterprise documentation.pdf"))
            {
                toolStripMenuItem5.Enabled = true;
                return true;
            }
            toolStripMenuItem5.Enabled = false;
            return false;
        }
        private void openDocumentation()
        {
            if (this.documentationExists())
                Process.Start(Path.GetDirectoryName(Application.ExecutablePath) + "\\IMS Enterprise documentation.pdf");
            else
                MessageBox.Show("Sorry, no IMS Enterprise Documentation found.");
        }
        void bgwIMS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (InvokeRequired)
                this.Invoke(new Action(() => this.disableLoadingMode()));
            else
                this.disableLoadingMode();

            if (e.Cancelled)
            {
                this.ep = null;
                this.currentFilePath = null;
                this.enterpriseList.Nodes.Clear();
            }
            else
            {
                if (this.ep != null)
                {
                    FileInfo info = new FileInfo(this.currentFilePath);
                    if (InvokeRequired)
                        this.Invoke(new Action(() => this.Text = "IMS Enterprise Diagnostic Tool - " + currentUrl));
                    else
                        this.Text = "IMS Enterprise Diagnostic Tool - " + currentUrl;
                }
            }
        }

        void bgwIMS_DoWork(object sender, DoWorkEventArgs e)
        {
            //read the xml file
            this.ep = this.readXmlFile(this.currentFilePath);
            //log to file if user allows it from settings
            if (this.log == null)
            {
                if (this.IMSSettings.General.ProblemLister.LogLocation.Use)
                    this.logPath = this.IMSSettings.General.ProblemLister.LogLocation.Location;
                else
                    this.logPath = Path.GetDirectoryName(Application.ExecutablePath);

                try
                {
                    this.log = new WriteProblemLog(this.currentFilePath, this.logPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error! " + ex.Message);
                }
            }
            //start creating tree nodes
            this.setupEnterpriseList();
            this.newFile = true;
        }

        private void fileNotFound()
        {
            DialogResult result = MessageBox.Show("File not found. You can not proceed without chosing a IMS Document, do you wish to do that now?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (this.openFile() == DialogResult.Cancel)
                {

                }
            }
        }

        private void enableLoadingMode() // a function to make the Mainform look likes it's loading
        {
            // Disable controls while loading to prohibit possible errors.
            this.exportBusy = true;
            this.previewCurrentFileToolStripMenuItem.Enabled = false;
            this.previewCurrentLogfileToolStripMenuItem.Enabled = false;
            this.previewCurrentLogfileToolStripMenuItem1.Enabled = false;
            if (this.progressBar.InvokeRequired)
            {
                this.progressBar.Invoke(new Action(() => this.progressBar.Show()));
                this.progressBar.Invoke(new Action(() => this.progressBar.Style = ProgressBarStyle.Marquee));
            }
            else
            {
                this.progressBar.Show();
                this.progressBar.Style = ProgressBarStyle.Marquee;
            }
            Application.UseWaitCursor = true;
            this.enterpriseList.Enabled = false;
            this.resetButton.Enabled = false;
            this.searchButton.Enabled = false;
            this.openToolStripMenuItem.Enabled = false;
            this.openFromUrlToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Enabled = false;
            this.exportToolStripMenuItem1.Enabled = false;
        }


        private void disableLoadingMode() // disable the loading mode
        {
            // Enable the disabled controls again.
            this.exportBusy = false;
            this.previewCurrentFileToolStripMenuItem.Enabled = true;

            if (this.IMSSettings.General.ProblemLister.Log)
            {
                this.previewCurrentLogfileToolStripMenuItem.Enabled = true;
                this.previewCurrentLogfileToolStripMenuItem1.Enabled = true;
            }

            this.progressBar.Hide();
            this.progressBar.Style = ProgressBarStyle.Continuous;
            Application.UseWaitCursor = false;
            this.enterpriseList.Invoke(new Action(() => this.enterpriseList.Enabled = true));
            this.resetButton.Enabled = true;
            this.searchButton.Enabled = true;
            this.openToolStripMenuItem.Enabled = true;
            this.openFromUrlToolStripMenuItem.Enabled = true;
            this.saveAsToolStripMenuItem.Enabled = true;
            this.exportToolStripMenuItem1.Enabled = true;
        }

        private void clearOldData() // clear information and disable buttons to let the user know that he/she isn't selecting an item
        {
            this.listView.Items.Clear();
            this.informationLabel.Text = "";
            this.sourceIdLabel.Text = "";
            this.editInforamtionBtn.Enabled = false;
            this.deleteItemBtn.Enabled = false;
            this.editItemToolStripMenuItem1.Enabled = false;
            this.contextMenuStrip1.Enabled = false;
        }

        private void clearErrors() // clears all errors in the list and in the varibles
        {
            if(InvokeRequired)
                this.Invoke(new Action(() => this.errorListView1.Items.Clear() ));
            else
                this.errorListView1.Items.Clear(); // clear all errors and warnings

            this.errorList.Clear();

            if (InvokeRequired)
            {
                this.Invoke(new Action(() => this.totErrorLabel.Text = "0 error(s)"));
                this.Invoke(new Action(() => this.totWarningLabel.Text = "0 warning(s)"));
            }
            else
            {
                this.totErrorLabel.Text = "0 error(s)";
                this.totWarningLabel.Text = "0 warning(s)";
            }
            this.Errors = 0;
            this.Warnings = 0;
        }

        private string getXmlEncoding() // get the xml encoding from the current xml document
        {
            using(XmlReader xr = XmlTextReader.Create(this.currentFilePath))
            {
                xr.Read();
                return xr.GetAttribute("encoding");
            }
        }

        private void setupEnterpriseList() // creates all the tree nodes from current document (this.ep)
        {
            if (this.ep != null)
            {
                this.clearErrors();

                DateTimeConverter dtc = new DateTimeConverter();
                DateTime datetime = (DateTime)dtc.ConvertFromString(this.ep.properties.datetime);
                TimeSpan diff = DateTime.Now - datetime;
                if (diff.TotalHours > 1)
                    addProblem(new Error(-1, "The date of the file is old, is the file saved on a local computer?", "Enterprise", ErrorType.Warning, "IMS Document"));

                if (encoding.IsEmpty())
                    encoding = "none";
                if(encoding != "iso-8859-1")
                    addProblem(new Error(-1, "Wrong encoding(" + encoding + ") on the IMS Document!", "XML-encoding", ErrorType.Warning, "IMS Document"));

                if (this.ep.group.ToList().Count == 0)
                    addProblem(new Error(-1, "Could not find any groups in the IMS Document!", "Group", ErrorType.Error, "IMS Document"));

                if (this.ep.person.ToList().Count == 0)
                    addProblem(new Error(-1, "Could not find any persons in the IMS Document!", "Person", ErrorType.Warning, "IMS Document"));

                if (this.ep.membership == null)
                    addProblem(new Error(-1, "Could not find any memberships in the IMS Document!", "Membership", ErrorType.Error, "IMS Document"));

                if (this.errorListView1.InvokeRequired)
                    this.enterpriseList.Invoke(new Action(() => this.enterpriseList.Nodes.Clear()));
                else
                    this.enterpriseList.Nodes.Clear();

                foreach (Root root in this.IMSSettings.General.StartPoint.Roots)
                {
                    if (root.Value)
                    {
                        if (root.Type != "Person") // root type cannot be Person
                        {
                            List<group> groupList = new List<group>();

                            foreach (group group in ep.group)
                            {
                                if (group.grouptype[0].typevalue[0].Value == root.Type)
                                {
                                    groupList.Add(group);
                                }
                            }
                            if (root.Type == "Unit") // A Unit
                            {
                                if (groupList.Count != 0)
                                {
                                    TreeNode parent = new TreeNode();
                                    parent.Name = "None";
                                    parent.Text = root.Type;
                                    parent.Tag = "DontRemove[!]";
                                    parent.ToolTipText = "Starting point";

                                    foreach (group group in groupList)
                                    {
                                        foreach (membership membership in ep.membership)
                                        {
                                            if (membership.sourcedid.id == group.sourcedid[0].id)
                                            {
                                                int index = ep.group.ToList().IndexOf(group);

                                                TreeNode node = new TreeNode();
                                                node.Name = group.grouptype[0].typevalue[0].Value;

                                                if (group.description.@short.IsEmpty())
                                                    addProblem(new Error(index, "The group is missing short description", "Group", ErrorType.Warning, group.sourcedid[0].id));
                                                if (group.sourcedid[0].id.IsEmpty())
                                                    addProblem(new Error(index, "The group is missing sourcedID", "Group", ErrorType.Error, "No ID"));

                                                node.Text = group.description.@short;
                                                node.Tag = index;
                                                node.Nodes.Add("");
                                                node.ToolTipText = group.grouptype[0].typevalue[0].Value;
                                                parent.Nodes.Add(node);
                                            }
                                        }
                                    }
                                    if (this.enterpriseList.InvokeRequired)
                                        this.enterpriseList.Invoke(new Action(() => enterpriseList.Nodes.Add(parent)));
                                    else
                                        enterpriseList.Nodes.Add(parent);
                                }
                            }
                            else // everything besides Unit
                            {
                                if (groupList.Count != 0)
                                {
                                    TreeNode parent = new TreeNode();
                                    parent.Name = "None";
                                    parent.Text = root.Type;
                                    parent.Tag = "DontRemove[!]";
                                    parent.ToolTipText = "Starting point";

                                    foreach (group group in groupList)
                                    {
                                        int count = 0;
                                        foreach (group group2 in ep.group) // and for every person to find a member with correct sourcedid as a perosn
                                        {
                                            if (group.sourcedid[0].id == group2.sourcedid[0].id) // person found
                                            {
                                                TreeNode node = new TreeNode();

                                                if (group.description.@short.IsEmpty())
                                                    addProblem(new Error(count, "The group short description is empty!", "Group", ErrorType.Warning, group.sourcedid[0].id));
                                                if (group.sourcedid[0].id.IsEmpty())
                                                    addProblem(new Error(count, "The group did not have a sourcedID", "Group", ErrorType.Error, "No ID"));

                                                node.Name = group.grouptype[0].typevalue[0].Value;
                                                node.Text = group.description.@short;
                                                node.Tag = count;
                                                node.Nodes.Add("");
                                                node.ToolTipText = group.grouptype[0].typevalue[0].Value;
                                                parent.Nodes.Add(node);
                                            }
                                            count++;
                                        }
                                    }
                                    if (this.enterpriseList.InvokeRequired)
                                        this.enterpriseList.Invoke(new Action(() => enterpriseList.Nodes.Add(parent)));
                                    else
                                        enterpriseList.Nodes.Add(parent);
                                }
                            }
                        }
                        else // A person
                        {
                            int count = 0;

                            TreeNode parent = new TreeNode();
                            parent.Name = "None";
                            parent.Text = root.Type;
                            parent.Tag = "DontRemove[!]";
                            parent.ToolTipText = "Starting point";

                            foreach (person person in ep.person)
                            {
                                TreeNode node = new TreeNode();
                                node.Name = "Person";
                                node.Text = person.name.fn;

                                if (new Regex(@"[0-9\[\]\^\$\|\?\*\+\(\)\\~`\!@#%&_+={}""<>:;,]").IsMatch(person.name.fn))
                                    addProblem(new Error(count, "The persons includes characters that are not allowed", "Person", ErrorType.Warning, person.sourcedid[0].id));
                                if (person.name.n.given.IsEmpty() || person.name.n.family.IsEmpty() || person.name.fn.IsEmpty())
                                    addProblem(new Error(count, "Something is wrong with the persons name", "Person", ErrorType.Warning, person.sourcedid[0].id));
                                if (person.sourcedid[0].id.IsEmpty())
                                    addProblem(new Error(count, "The person had no sourced ID!", "Person", ErrorType.Error, "No ID"));

                                node.Tag = count;
                                node.ToolTipText = "Person";
                                parent.Nodes.Add(node);

                                count++;
                            }
                            if (this.enterpriseList.InvokeRequired)
                                this.enterpriseList.Invoke(new Action(() => enterpriseList.Nodes.Add(parent)));
                            else
                                enterpriseList.Nodes.Add(parent);
                        }
                    }
                }
            }
        }

        public void insertPerson(person newPerson)
        {
            this.ep.person.ToList().Add(newPerson);
        }

        private enterprise readXmlFile(String uri) // function to the read a xml file from given uri
        {
            if (InvokeRequired)
                this.Invoke(new Action(() => this.clearOldData()));
            else
                this.clearOldData();
                
            Stopwatch timer = new Stopwatch(); // timer to give information about time taken to load IMS document
            timer.Start();
            enterprise enterPrise;
            try
            {
                enterPrise = XmlFile.Read(uri);
            }
            catch(Exception e)
            {
                if (this.IMSSettings.General.ProblemLister.LogLocation.Use)
                    this.logPath = this.IMSSettings.General.ProblemLister.LogLocation.Location;
                else
                    this.logPath = Path.GetDirectoryName(Application.ExecutablePath);

                try
                {
                    this.log = new WriteProblemLog(uri, this.logPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error! " + ex.Message);
                }

                addProblem(new Error(-1, e.Message, "XML syntax", ErrorType.Error, "IMS Document"));
                MessageBox.Show(e.Message);
                enterPrise = null;
                return null;
            }

            timer.Stop();
            this.currentFileLoadTime = (int)timer.ElapsedMilliseconds;
            this.encoding = this.getXmlEncoding();

            return enterPrise;
        }

        private void treeView1_Resize(object sender, EventArgs e)
        {
            //treeView1.Size = new Size(treeView1.Size.Width, this.Size.Height-400);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //enterpriseList.Size = new Size(enterpriseList.Size.Width, this.Size.Height);
        }

        private void enterpriseList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node.Tag != (Object)"DontRemove[!]")
                {
                    this.contextMenuStrip1.Enabled = true;
                    this.enableContextMenu();
                }

                contextMenuStrip1.Show(Cursor.Position); //open a short menu about the selected item
            }
            enterpriseList.SelectedNode = e.Node;
            this.fixNodeInfo(e.Node); // make sure right nodeInfo is presented
        }

        private void fixNodeInfo(TreeNode node) // fix the info about a group/person. A node holds an index of the position in the list of groups/persons (Node.tag)(this.ep.group[]/person[])
        {
            try
            {
                if (node.Name != "None" && node.Tag != (Object)"DontRemove[!]")
                {
                    int index = int.Parse(node.Tag.ToString());
                    if (node.Name == "Person")
                    {
                        informationLabel.Text = "Personal information";

                        this.updatePersonInformationList(index);
                    }
                    else
                    {
                        this.updateGroupInformation(index);
                    }
                    this.contextMenuStrip1.Enabled = true;
                    this.enableContextMenu();
                }
                else
                    this.clearOldData();
            }
            catch(Exception)
            {
                this.clearOldData();
            }
        }

        private void enterpriseList_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void enableContextMenu()
        {
            foreach(ToolStripItem item in this.contextMenuStrip1.Items)
            {
                item.Enabled = true;
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.currentFilePath.IsEmpty())
            {
                if (!this.exportBusy)
                {
                    TreeNode node = enterpriseList.SelectedNode;
                    if (node.Tag.ToString() != "DontRemove[!]")
                    {
                        int index = int.Parse(node.Tag.ToString());
                        String name = enterpriseList.SelectedNode.Name;

                        DialogResult result = MessageBox.Show("Are you sure you want to remove this item?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            enterpriseList.Nodes.Remove(node);
                            if (name == "Person")
                            {
                                ep.person.ToList().RemoveAt(index);
                            }
                            else
                            {
                                ep.group.ToList().RemoveAt(index);
                            }
                            this.recentSaved = false;
                            saveToolStripMenuItem.Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can't remove this item!");
                    }
                }
                else
                    MessageBox.Show("Something is currently progressing. Please wait until the progress is done!");
            }
            else
                MessageBox.Show("No item to remove!");
        }

        private void insertItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void enterpriseList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) // if person is using the keyboard to browse
            {
                this.fixNodeInfo(enterpriseList.SelectedNode); // make sure right nodeInfo is presented
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if the file was edited and saved before opening a new file
            if (this.recentSaved == false)
            {
                DialogResult result = MessageBox.Show("You are about to load another file without saving, do you want to save the file before continuing?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.No)
                {
                    if (this.currentFilePath != "" || this.newFile != true)
                    {
                        this.writeToFile(false);
                    }
                    else
                    {
                        this.saveAsToolStripMenuItem_Click(sender, e);
                    }
                }
            }
            this.openFile();
        }

        private DialogResult openFile() // Open a new xml file to read
        {
            // open a file dialog
            OpenFileDialog fileDia = new OpenFileDialog();
            // filter so that you only can se .xml files or all files
            fileDia.Filter = "XML files (.xml)|*.xml|All Files (*.*)|*.*";
            fileDia.FilterIndex = 1;
            // person should only be allowed to open one file
            fileDia.Multiselect = false;
            fileDia.Title = "Open new IMS Document...";
            DialogResult returnResult = fileDia.ShowDialog();
            if (returnResult == DialogResult.OK)
            {
                String s = fileDia.FileName; // get the path of the file
                if (s.EndsWith("xml") == true) // make sure the file is a xml file
                {
                    this.currentFilePath = @s;
                    this.enableLoadingMode();

                    if (this.IMSSettings.Startup.FileToOpen.Choice == 0)
                    {
                        this.IMSSettings.Startup.FileToOpen.Value = s;
                        this.IMSSettings.Save();
                    }

                    this.downloadAndProcessTime = 0;
                    this.bgwIMS.RunWorkerAsync();
                }
                else // the file wasn't a xml file
                {
                    // explain the problem to the user 
                    DialogResult result = MessageBox.Show("The file you have tried to open was of an incorrect file type, it needs to be an XML-file (.xml)", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry) // person want to retry opening a file
                    {
                        this.openFile(); // call same function to get same things happening
                    }
                }
            }
            return returnResult;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPersonForm newPersonForm = new NewPersonForm();
            newPersonForm.ShowDialog(this);
        }
        
        

        private void updatePersonInformationList(int index) // updates the information list about a person, takes information from current Node.tag
        {
            if (ep.person[index].sourcedid != null)
            {
                if (ep.person[index].sourcedid.ToList().Count > 0 && ep.person[index].sourcedid[0].id != null)
                    sourceIdLabel.Text = ep.person[index].sourcedid[0].id.Replace('_', '\x5f');
                else
                    sourceIdLabel.Text = "";
            }
            else
                sourceIdLabel.Text = "";

                listView.Items.Clear();
            listView.Items.Add(new ListViewItem(new[] { "Name", ep.person[index].name.fn }));
            listView.Items.Add(new ListViewItem(new[] { "GUID", this.getGUID(ep.person[index]) }));
            listView.Items.Add(new ListViewItem(new[] { "PID", this.getPID(ep.person[index]) }));
            //Calculate the age of the person.
            String ageResult = "";
            if(ep.person[index].demographics.bday != null && ep.person[index].demographics.bday != "")
            {
                DateTime now = DateTime.Today;
                DateTime bday = Convert.ToDateTime(ep.person[index].demographics.bday);
                int age = now.Year - bday.Year;
                if (now < bday.AddYears(age)) age--;
                ageResult = age.ToString(); 
            }
            listView.Items.Add(new ListViewItem(new[] { "Age", ageResult }));
            listView.Items.Add(new ListViewItem(new[] { "Gender", ep.person[index].demographics.gender }));
            listView.Items.Add(new ListViewItem(new[] { "Birthday", ep.person[index].demographics.bday }));
            if(ep.person[index].adr.street != null)
                listView.Items.Add(new ListViewItem(new[] { "Address", ep.person[index].adr.street[0] }));
            else
                listView.Items.Add(new ListViewItem(new[] { "Address", "" }));
            listView.Items.Add(new ListViewItem(new[] { "Postcode", ep.person[index].adr.pcode }));
            listView.Items.Add(new ListViewItem(new[] { "Locality", ep.person[index].adr.locality }));
            listView.Items.Add(new ListViewItem(new[] { "E-mail", ep.person[index].email }));
            
            foreach(var t in ep.person[index].tel)
            {
                listView.Items.Add(new ListViewItem(new[] { t.teltype.ToString(), t.Value }));
            }


            listView.Items.Add(new ListViewItem(new[] { "Institution role type", ep.person[index].institutionrole[0].institutionroletype.ToString() }));
            listView.Items.Add(new ListViewItem(new[] { "Primary role", ep.person[index].institutionrole[0].primaryrole.ToString() }));
            listView.Items.Add(new ListViewItem(new[] { "System role", ep.person[index].systemrole.systemroletype.ToString() }));

            if (ep.person[index].extension != null && this.IMSSettings.General.Extensions.Enabled)
            {
                ListViewItem lvi;
                if (ep.person[index].extension.emailwork != null && ep.person[index].extension.emailwork.Any())
                {
                    lvi = new ListViewItem(new[] {"Email work", ep.person[index].extension.emailwork});
                    if (IMSSettings.General.Extensions.Distinguish)
                        lvi.ForeColor = Color.Blue;
                    listView.Items.Add(lvi);
                }
                if (ep.person[index].extension.employmentend != null && ep.person[index].extension.employmentend.Any())
                {
                    lvi = new ListViewItem(new[] {"Employment end", ep.person[index].extension.employmentend});
                    if (IMSSettings.General.Extensions.Distinguish)
                        lvi.ForeColor = Color.Blue;
                    listView.Items.Add(lvi);
                }
                if (ep.person[index].extension.employmentstart != null && ep.person[index].extension.employmentstart.Any())
                {
                    lvi = new ListViewItem(new[] {"Employment start", ep.person[index].extension.employmentstart});
                    if (IMSSettings.General.Extensions.Distinguish)
                        lvi.ForeColor = Color.Blue;
                    listView.Items.Add(lvi);
                }
                if (ep.person[index].extension.municipalitycode != null && ep.person[index].extension.municipalitycode.Any())
                {
                    lvi = new ListViewItem(new[] {"Municipaly code", ep.person[index].extension.municipalitycode});
                    if (IMSSettings.General.Extensions.Distinguish)
                        lvi.ForeColor = Color.Blue;
                    listView.Items.Add(lvi);
                }
                if (ep.person[index].extension.municipalityname != null && ep.person[index].extension.municipalityname.Any())
                {
                    lvi = new ListViewItem(new[] {"Municipality name", ep.person[index].extension.municipalityname});
                    if (IMSSettings.General.Extensions.Distinguish)
                        lvi.ForeColor = Color.Blue;
                    listView.Items.Add(lvi);
                }
                if (ep.person[index].extension.nativelanguage != null && ep.person[index].extension.nativelanguage.Any())
                {
                    lvi = new ListViewItem(new[] {"Native language", ep.person[index].extension.nativelanguage[0]});
                    if (IMSSettings.General.Extensions.Distinguish)
                        lvi.ForeColor = Color.Blue;
                    listView.Items.Add(lvi);
                }
                if (ep.person[index].extension.geographickeycode != null && ep.person[index].extension.geographickeycode.Any())
                {
                    lvi = new ListViewItem(new[] { "Geographic key code", ep.person[index].extension.geographickeycode });
                    if (IMSSettings.General.Extensions.Distinguish)
                        lvi.ForeColor = Color.Blue;
                    listView.Items.Add(lvi);
                }
                if (ep.person[index].extension.privacy != null && ep.person[index].extension.privacy.Any())
                {
                    lvi = new ListViewItem(new[] {"Privacy", ep.person[index].extension.privacy});
                    if (IMSSettings.General.Extensions.Distinguish)
                        lvi.ForeColor = Color.Blue;
                    listView.Items.Add(lvi);
                }
                if (ep.person[index].extension.programcode != null && ep.person[index].extension.programcode.Any())
                {
                    lvi = new ListViewItem(new[] {"Program code", ep.person[index].extension.programcode});
                    if (IMSSettings.General.Extensions.Distinguish)
                        lvi.ForeColor = Color.Blue;
                    listView.Items.Add(lvi);
                }
                if (ep.person[index].extension.signature != null && ep.person[index].extension.signature.Any())
                {
                    lvi = new ListViewItem(new[] {"Signature", ep.person[index].extension.signature});
                    if (IMSSettings.General.Extensions.Distinguish)
                        lvi.ForeColor = Color.Blue;
                    listView.Items.Add(lvi);
                }
                if (ep.person[index].extension.timestampSpecified)
                {
                    lvi = new ListViewItem(new[] {"Timestamp", ep.person[index].extension.timestamp.ToString()});
                    if (IMSSettings.General.Extensions.Distinguish)
                        lvi.ForeColor = Color.Blue;
                    listView.Items.Add(lvi);
                }
                if (ep.person[index].extension.schoolunitcode != null)
                {
                    lvi = new ListViewItem(new[] { "School unit code", ep.person[index].extension.schoolunitcode });
                    if (IMSSettings.General.Extensions.Distinguish)
                        lvi.ForeColor = Color.Blue;
                    listView.Items.Add(lvi);
                }
            }
            else if (this.IMSSettings.General.Extensions.Enabled)
            {
                this.addProblem(new Error(index, "Person is missing extensions", "Person", ErrorType.Warning, ep.person[index].sourcedid[0].id));
            }

        }
        private void updateGroupInformation(int index) // updates the information list about a group, takes information from current Node.tag
        {
            listView.Items.Clear();
            
            informationLabel.Text = ep.group[index].grouptype[0].typevalue[0].Value + " information";
            //Set the source ID label to the right value.
            if (ep.group[index].sourcedid != null)
                if (ep.group[index].sourcedid.Length > 0)
                    sourceIdLabel.Text = ep.group[index].sourcedid[0].id;
                else
                    sourceIdLabel.Text = "";
            else
                sourceIdLabel.Text = "";

            listView.Items.Add(new ListViewItem(new[] { "Name", ep.group[index].description.@short }));
            listView.Items.Add(new ListViewItem(new[] { "Description", ep.group[index].description.@long }));
            listView.Items.Add(new ListViewItem(new[] { "Type Classification", ep.group[index].grouptype[0].typevalue[0].Value }));
            if (ep.group[index].timeframe != null)
            {
                listView.Items.Add(new ListViewItem(new[] { "Begins", ep.group[index].timeframe.begin.Value }));
                listView.Items.Add(new ListViewItem(new[] { "Ends", ep.group[index].timeframe.end.Value }));
            }
           
           if(ep.group[index].extension != null && this.IMSSettings.General.Extensions.Enabled)
           {
               ListViewItem lvi;
               if (ep.group[index].extension.coursecode != null)
               {
                   lvi = new ListViewItem(new[] {"Course code", ep.group[index].extension.coursecode});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.csncode != null && ep.group[index].extension.csncode.Any())
               {
                   lvi = new ListViewItem(new[] {"CSN code", ep.group[index].extension.csncode});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.governedby != null && ep.group[index].extension.governedby.Any())
               {
                   lvi = new ListViewItem(new[] {"Governed by", ep.group[index].extension.governedby});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.groupusage != null && ep.group[index].extension.groupusage.Any())
               {
                   lvi = new ListViewItem(new[] {"Group usage", ep.group[index].extension.groupusage});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.hours != null && ep.group[index].extension.hours.Any())
               {
                   lvi = new ListViewItem(new[] {"Hours", ep.group[index].extension.hours});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.languagecode != null && ep.group[index].extension.languagecode.Any())
               {
                   lvi = new ListViewItem(new[] {"Language code", ep.group[index].extension.languagecode});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.locality != null && ep.group[index].extension.locality.Any())
               {
                   lvi = new ListViewItem(new[] {"Locality", ep.group[index].extension.locality});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.municipalitycode != null && ep.group[index].extension.municipalitycode.Any())
               {
                   lvi = new ListViewItem(new[] {"Municipality code", ep.group[index].extension.municipalitycode});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.municipalityname != null && ep.group[index].extension.municipalityname.Any())
               {
                   lvi = new ListViewItem(new[] {"Municipality name", ep.group[index].extension.municipalityname});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.pcode != null && ep.group[index].extension.pcode.Any())
               {
                   lvi = new ListViewItem(new[] {"Pcode", ep.group[index].extension.pcode});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.phone != null && ep.group[index].extension.phone.Any())
               {
                   lvi = new ListViewItem(new[] {"Phone", ep.group[index].extension.phone});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.point != null && ep.group[index].extension.point.Any())
               {
                   lvi = new ListViewItem(new[] {"Point", ep.group[index].extension.point});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }

               if (ep.group[index].extension.schooltype != null && ep.group[index].extension.schooltype.Any())
               {
                   lvi = new ListViewItem(new [] { "School types", ConvertStringArrayToString(ep.group[index].extension.schooltype) });
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }

               if (ep.group[index].extension.schoolyear != null && ep.group[index].extension.schoolyear.Any())
               {
                   lvi = new ListViewItem(new[] {"School year", ep.group[index].extension.schoolyear});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.street != null && ep.group[index].extension.street.Any())
               {
                   lvi = new ListViewItem(new[] {"Street", ep.group[index].extension.street});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.subjectcode != null && ep.group[index].extension.subjectcode.Any())
               {
                   lvi = new ListViewItem(new[] {"Subject code", ep.group[index].extension.subjectcode});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.timestampSpecified)
               {
                   lvi = new ListViewItem(new[] {"Timestamp", ep.group[index].extension.timestamp.ToString()});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
               if (ep.group[index].extension.web != null && ep.group[index].extension.web.Any())
               {
                   lvi = new ListViewItem(new[] {"Web", ep.group[index].extension.web});
                   if (IMSSettings.General.Extensions.Distinguish)
                       lvi.ForeColor = Color.Blue;
                   listView.Items.Add(lvi);
               }
           }
        }

        static string ConvertStringArrayToString(string[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(',');
            }
            return builder.ToString();
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (editCreatePerson.getEdit() == true)
            {
                int index = int.Parse(enterpriseList.SelectedNode.Tag.ToString());
                ep.person[index] = editCreatePerson.getNewPerson();
                enterpriseList.SelectedNode.Text = ep.person[index].name.fn;
                this.updatePersonInformationList(index);
            }
            else
            {
                ep.person.ToList().Add(editCreatePerson.getNewPerson());
            }
            this.recentSaved = false;
            saveToolStripMenuItem.Enabled = true;
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.exportBusy)
            {
                if (enterpriseList.SelectedNode != null)
                {
                    if (!enterpriseList.SelectedNode.Tag.ToString().IsEmpty() && enterpriseList.SelectedNode.Tag != (Object)"DontRemove[!]")
                    {
                        if (enterpriseList.SelectedNode.Name == "Person")
                        {
                            
                            editCreatePerson = new NewPersonForm(ep.person[int.Parse(enterpriseList.SelectedNode.Tag.ToString())]);
                            editCreatePerson.FormClosing += this.Form1_FormClosing;
                            editCreatePerson.ShowDialog(this);

                            
                        }
                        else
                            MessageBox.Show("Group editor is not supported!");
                    }
                    else
                        MessageBox.Show("Please select a valid item to edit");
                }
                else
                    MessageBox.Show("Please select a item");
            }
            else
                MessageBox.Show("Something is currently progressing. Please wait until the progress is done!");
        }

        private void enterpriseList_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Name != "None")
            {
                enterpriseList.SelectedNode = e.Node;
                if (e.Node.Nodes.Count == 1 || e.Node.Text == "")
                {
                    String nodeName = e.Node.Name; // get the current nodes name
                    {
                        List<membership> membershipList = new List<membership>(); // create a list for membership

                        if (nodeName != "Person")
                        {
                            var openGroupPerson = this.ep.group[int.Parse(e.Node.Tag.ToString())]; // get the group
                            foreach (membership membership in this.ep.membership)
                            {
                                if (membership.sourcedid.id == openGroupPerson.sourcedid[0].id) // check that sourcedid is same with the group
                                {
                                    membershipList.Add(membership);
                                }
                            }
                        }

                        if (membershipList.Count == 0) // if membershipList is 0 clear all subnodes in the node
                        {
                            e.Node.Nodes.Clear();
                        }

                        foreach (membership membership in membershipList) // go through all memberships
                        {
                            if (membership.member != null)
                            {
                                foreach (member member in membership.member) // go through all members
                                {
                                    bool found = false;
                                    int idx = 0;

                                    if (member.idtype == "Person")
                                    {
                                        foreach (person person in ep.person) // find the correct person
                                        {
                                            if (person.sourcedid[0].id == member.sourcedid.id)
                                            {
                                                this.addPersonNode(member, person, idx, e.Node);
                                                found = true;
                                                break;
                                            }

                                            idx++;
                                        }
                                    }
                                    else
                                    {
                                        idx = 0;
                                        foreach (group group in ep.group) // find the correct group
                                        {
                                            if (group.sourcedid[0].id == member.sourcedid.id)
                                            {
                                                this.addGroupNode(member, group, idx, e.Node);
                                                found = true;
                                                break;
                                            }

                                            idx++;
                                        }
                                    }

                                    if(found == false)
                                    {
                                        if (member.idtype == "Group")
                                            addProblem(new Error(-1, "Group not found: " + member.sourcedid.id, "IMSEnterprise", ErrorType.Error, "IMS Document"));
                                        else
                                            addProblem(new Error(-1, "Person not found: " + member.sourcedid.id, "IMSEnterprise", ErrorType.Error, "IMS Document"));

                                        e.Node.Nodes.Add(member.sourcedid.id + " not found!");
                                    }
                                }
                            }
                            else
                                e.Node.Nodes.Clear();
                        }
                    }
                    if (e.Node.Nodes.Count > 1)
                        e.Node.Nodes.RemoveAt(0);
                }
            }
        }

        private void iMSfilePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.fileInformationForm = new IMSFileInformationForm(new FileInfo(this.currentFilePath), this.currentFileLoadTime.ToString() + " ms", this.ep.person.ToList().Count, this.ep.group.ToList().Count, this.ep.membership.ToList().Count, this.downloadAndProcessTime);
                this.fileInformationForm.ShowDialog();
            }
            catch(Exception ex)
            {
                ex.ToString();
                MessageBox.Show("A file is needed to open this menu");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.writeToFile(false);
        }
        private void writeToFile(bool asNewFile) // write to a file with the information from this.ep and then save it and close it
        {
            File.WriteAllText(this.currentFilePath, string.Empty);  //empty the file before writing
            FileMode fm;

            if (asNewFile) fm = FileMode.Open; // save
            else fm = FileMode.OpenOrCreate; // save as

            using (FileStream fs = new FileStream(this.currentFilePath, fm))
            {
                XmlSerializer xmlserializer = new XmlSerializer(typeof(enterprise));

                try
                {
                    xmlserializer.Serialize(fs, ep);
                }
                finally
                {
                    fs.Close();
                    this.recentSaved = true;
                    saveToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "XML files (.xml)|*.xml";
            saveDialog.FilterIndex = 1;
            saveDialog.OverwritePrompt = true;
            saveDialog.Title = "Save IMS Document as...";

            if (saveDialog.ShowDialog() != DialogResult.Cancel)
            {
                if (saveDialog.FileName.EndsWith(".xml"))
                {
                    this.currentFilePath = saveDialog.FileName;
                    FileInfo fi = new FileInfo(this.currentFilePath);
                    this.Text = "IMS Enterprise Diagnostic Tool - " + fi.Name;
                    this.writeToFile(true);
                    this.saveRecentFilePath();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Incorrect file type, the type has be to xml", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry)
                    {
                        this.saveAsToolStripMenuItem_Click(sender, e);
                    }
                }
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.IMSOptions = new IMSOptions(this.currentFilePath);
            this.IMSOptions.FormClosing += this.beforeOptionsCloses;
            this.IMSOptions.ShowDialog(this);
        }

        private void enterpriseList_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag != (Object)"DontRemove[!]")
            {
                e.Node.Nodes.Clear();
                e.Node.Nodes.Add("");
            }
        }

        private void saveRecentFilePath() // save the recent file path
        {
            if (this.IMSSettings.Startup.FileToOpen.Choice == 0)
            {
                this.IMSSettings.Startup.FileToOpen.Value = this.currentFilePath;
                this.IMSSettings.Save();
            }
        }


        private void beforeOptionsCloses(Object sender, FormClosingEventArgs e)
        {
            this.IMSSettings = new IMSSettings();
            if (this.IMSOptions.StartPointChanged || this.IMSOptions.MaxChanged)
                this.setupEnterpriseList();
            this.initSettings();
        }
        private void initSettings() // make sure that the new settings is initialized
        {
            if (this.IMSSettings.General.ProblemLister.Enabled)
            {
                this.splitContainer1.Panel2Collapsed = false;
                this.splitContainer1.Panel2.Show();
            }
            else
            {
                this.splitContainer1.Panel2Collapsed = true;
                this.splitContainer1.Panel2.Hide();
            }
            if (this.IMSSettings.General.ProblemLister.Warnings)
            {
                pictureBox1.Show();
                totWarningLabel.Show();
            }
            else
            {
                this.removeProblem("warning");
                pictureBox1.Hide();
                totWarningLabel.Hide();
            }
            if (this.IMSSettings.General.ProblemLister.Errors)
            {
                pictureBox2.Show();
                totErrorLabel.Show();
            }
            else
            {
                this.removeProblem("error");
                pictureBox2.Hide();
                totErrorLabel.Hide();
            }
            if (this.enterpriseList.SelectedNode != null && this.enterpriseList.SelectedNode.Tag != (Object)"DontRemove[!]")
            {
                if (this.enterpriseList.SelectedNode.Name == "Person")
                    this.updatePersonInformationList(int.Parse(this.enterpriseList.SelectedNode.Tag.ToString()));
                else
                    this.updateGroupInformation(int.Parse(this.enterpriseList.SelectedNode.Tag.ToString()));
            }
            else
                this.clearOldData();

            this.previewCurrentLogfileToolStripMenuItem.Enabled = this.IMSSettings.General.ProblemLister.Log;
            this.previewCurrentLogfileToolStripMenuItem1.Enabled = this.IMSSettings.General.ProblemLister.Log;
            if(!this.IMSSettings.General.ProblemLister.Enabled)
            {
                this.previewCurrentLogfileToolStripMenuItem.Enabled = false;
                this.previewCurrentLogfileToolStripMenuItem1.Enabled = false;
            }         
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.recentSaved == false)
            {
                DialogResult result = MessageBox.Show("You are about to exit without saving, do you want to save the file before closing?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.No)
                {
                    if (this.newFile != true)
                    {
                        this.writeToFile(false);
                    }
                    else
                    {
                        this.saveAsToolStripMenuItem_Click(sender, e);
                    }
                }
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            this.setupEnterpriseList();

            this.searchInput.Text = "";

            if(this.bgwSearchGroup.IsBusy == true)
                this.bgwSearchGroup.CancelAsync();
            if (this.bgwSearchPerson.IsBusy == true)
                this.bgwSearchPerson.CancelAsync();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (this.ep != null)
            {
                progressBar.Style = ProgressBarStyle.Marquee;
                progressBar.MarqueeAnimationSpeed = 230;
                this.enterpriseList.Nodes.Clear();

                if (this.bgwSearchPerson.IsBusy || this.bgwSearchGroup.IsBusy)
                {
                    this.bgwSearchPerson.CancelAsync();
                }
                else if (this.bgwSearchPerson.IsBusy == false && this.bgwSearchGroup.IsBusy == false)
                {
                    progressBar.Show();

                    bgwSearchGroup.DoWork += new DoWorkEventHandler(this.searchGroup);
                    bgwSearchPerson.DoWork += new DoWorkEventHandler(this.searchPerson);

                    bgwSearchPerson.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwComplete);
                    bgwSearchGroup.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwComplete);

                    bgwSearchGroup.WorkerReportsProgress = true;
                    bgwSearchGroup.WorkerSupportsCancellation = true;

                    bgwSearchPerson.WorkerReportsProgress = true;
                    bgwSearchPerson.WorkerSupportsCancellation = true;

                    bgwSearchPerson.RunWorkerAsync();
                }
            }
        }

        private void bgwComplete(object sender,RunWorkerCompletedEventArgs e)
        {
            this.progressBar.Hide();
            this.progressBar.Value = 0;
            if (e.Cancelled == false)
            {
                List<TreeNode> res = (List<TreeNode>)e.Result;

                foreach (TreeNode node in res)
                {
                    if(!this.enterpriseList.Nodes.Contains(node))
                        this.enterpriseList.Nodes.Add(node);
                }
            }
        }

        private void searchGroup(object sender, DoWorkEventArgs e) // search a group from this.ep
        {
            if (e.Cancel != true)
            {
                e.Result = null;
                BackgroundWorker worker = sender as BackgroundWorker;
                List<TreeNode> nodes = new List<TreeNode>();
                String searchWord = this.searchInput.Text;

                int countGroup = 0;
                foreach (group group in this.ep.group)
                {
                    if (group.sourcedid[0].id.Like(searchWord))
                    {
                        TreeNode node = new TreeNode(); // create the new node
                        node.Name = group.grouptype[0].typevalue[0].Value;// set the nam to the grouptype
                        node.Text = group.description.@short; //the text should be the description
                        node.Nodes.Add(""); // add a subnode so user can check for subnodes
                        node.Tag = countGroup; // the tag will be the index of this groups index in ep.group
                        node.ToolTipText = group.grouptype[0].typevalue[0].Value;

                        nodes.Add(node);
                    }
                    else if (group.description.@short.Like(searchWord))
                    {
                        TreeNode node = new TreeNode(); // create the new node
                        node.Name = group.grouptype[0].typevalue[0].Value;// set the nam to the grouptype
                        node.Text = group.description.@short; //the text should be the description
                        node.Nodes.Add(""); // add a subnode so user can check for subnodes
                        node.Tag = countGroup; // the tag will be the index of this groups index in ep.group
                        node.ToolTipText = group.grouptype[0].typevalue[0].Value;

                        nodes.Add(node);
                    }
                    worker.ReportProgress(countGroup);
                    countGroup++;
                }
                e.Result = nodes;
            }
        }

        private void searchPerson(object sender, DoWorkEventArgs e) //search a person from this.ep
        {
            if (e.Cancel != true)
            {
                List<TreeNode> nodes = new List<TreeNode>();
                BackgroundWorker worker = sender as BackgroundWorker;

                String searchWord = this.searchInput.Text;
                int countPerson = 0;
                foreach (person person in this.ep.person)
                {
                    if (this.getPID(person).Like(searchWord)) // check PID
                    {
                        TreeNode node = new TreeNode(); // create new node
                        node.Name = "Person"; // the name is the type of the ep
                        node.Text = person.name.fn; // text will be the persons name
                        node.Tag = countPerson; // the tag will be the index of this persons index in ep.person
                        node.ToolTipText = "Person";
                        node.Nodes.Add(""); // the tag will be the index of this persons index in ep.person

                        nodes.Add(node);
                    }
                    else if (person.name.fn.Like(searchWord)) // check persons fullname
                    {
                        TreeNode node = new TreeNode(); // create new node
                        node.Name = "Person"; // the name is the type of the ep
                        node.Text = person.name.fn; // text will be the persons name
                        node.Tag = countPerson; // the tag will be the index of this persons index in ep.person
                        node.ToolTipText = "Person";
                        node.Nodes.Add(""); // the tag will be the index of this persons index in ep.person

                        nodes.Add(node);
                    }
                    else if (person.email.Like(searchWord)) // check the email
                    {
                        TreeNode node = new TreeNode(); // create new node
                        node.Name = "Person"; // the name is the type of the ep
                        node.Text = person.name.fn; // text will be the persons name
                        node.Tag = countPerson; // the tag will be the index of this persons index in ep.person
                        node.ToolTipText = "Person";
                        node.Nodes.Add(""); // the tag will be the index of this persons index in ep.person

                        nodes.Add(node);
                    }
                    else
                    {
                        foreach (tel telefon in person.tel) // check the phone numbers
                        {
                            if (telefon.Value.Like(searchWord))
                            {
                                TreeNode node = new TreeNode(); // create new node
                                node.Name = "Person"; // the name is the type of the ep
                                node.Text = person.name.fn; // text will be the persons name
                                node.Tag = countPerson; // the tag will be the index of this persons index in ep.person
                                node.ToolTipText = "Person";
                                node.Nodes.Add(""); // the tag will be the index of this persons index in ep.person

                                nodes.Add(node);
                            }
                        }
                    }

                    worker.ReportProgress(countPerson);
                    countPerson++;
                }
                int countGroup = 0;
                foreach (group group in this.ep.group)
                {
                    if (group.sourcedid[0].id.Like(searchWord))
                    {
                        TreeNode node = new TreeNode(); // create the new node
                        node.Name = group.grouptype[0].typevalue[0].Value;// set the nam to the grouptype
                        node.Text = group.description.@short; //the text should be the description
                        node.Nodes.Add(""); // add a subnode so user can check for subnodes
                        node.Tag = countGroup; // the tag will be the index of this groups index in ep.group
                        node.ToolTipText = group.grouptype[0].typevalue[0].Value;

                        nodes.Add(node);
                    }
                    else if (group.description.@short.Like(searchWord))
                    {
                        TreeNode node = new TreeNode(); // create the new node
                        node.Name = group.grouptype[0].typevalue[0].Value;// set the nam to the grouptype
                        node.Text = group.description.@short; //the text should be the description
                        node.Nodes.Add(""); // add a subnode so user can check for subnodes
                        node.Tag = countGroup; // the tag will be the index of this groups index in ep.group
                        node.ToolTipText = group.grouptype[0].typevalue[0].Value;

                        nodes.Add(node);
                    }
                    worker.ReportProgress(countGroup);
                    countGroup++;
                }
                e.Result = nodes;
            }
        }

        private void addGroupNode(member member, group group, int index, TreeNode subNode = null) // add a tree node which is of the type group
        {
            TreeNode node = new TreeNode(); // create the new node
            node.Name = group.grouptype[0].typevalue[0].Value;// set the nam to the grouptype
            if (!group.description.@short.IsEmpty())
                node.Text = group.description.@short; //the text should be the description
            else
            {
                node.Text = "No desc: " + group.sourcedid[0].id;
                addProblem(new Error(index, "The groups short description is empty!", "Group", ErrorType.Warning, group.sourcedid[0].id));
            }
            if (group.sourcedid[0].id.IsEmpty())
                addProblem(new Error(index, "The group did not have a sourcedID", "Group", ErrorType.Error, "No ID"));

            node.Nodes.Add(""); // add a subnode so user can check for subnodes
            node.Tag = index; // the tag will be the index of this groups index in ep.group

            if (member.role != null)
            {
                string tips = member.role[0].roletype.ToString();

                if (member.role[0].timeframe != null)
                    tips += " from " + member.role[0].timeframe.begin.Value + " to " + member.role[0].timeframe.end.Value;
/*
                if (member.role[0].extension != null)
                {
                    var result = member.role[0].extension;
                    foreach (XmlNode ext in result[0].ChildNodes)
                    {
                        if ((ext.Name == "course_code" || ext.Name == "subject_code") && ext.InnerText != string.Empty)
                            tips += " " + ext.InnerText;
                    }
                }
*/
                node.ToolTipText = tips;
            }


            if (subNode != null)
                subNode.Nodes.Add(node); // add the new node to the current nodes subnodes
            else
                enterpriseList.Nodes.Add(node);
        }

        private void addPersonNode(member member, person person, int index, TreeNode subNode = null) // add a tree node which is of the type person
        {
            TreeNode node = new TreeNode(); // create new node
            node.Name = "Person"; // the name is the type of the ep
            node.Text = person.name.fn; // text will be the persons name

            if (new Regex(@"[0-9\[\]\^\$\.\|\?\*\+\(\)\\~`\!@#%&\-_+={}'""<>:;,]").IsMatch(person.name.fn))
                addProblem(new Error(index, "The persons name was incorrect format, had numbers or special characters!", "Person", ErrorType.Warning, person.sourcedid[0].id));
            if (person.name.n.given.IsEmpty() || person.name.n.family.IsEmpty() || person.name.fn.IsEmpty())
                addProblem(new Error(index, "Something is wrong with the persons name, something is missing?", "Person", ErrorType.Warning, person.sourcedid[0].id));
            if (person.sourcedid[0].id.IsEmpty())
                addProblem(new Error(index, "The person had no sourced ID!", "Person", ErrorType.Error, "No ID"));

            node.Tag = index; // the tag will be the index of this persons index in ep.person

            if (member.role != null)
            {
                string tips = member.role[0].roletype.ToString();

                if(member.role[0].timeframe != null)
                   tips += " from " + member.role[0].timeframe.begin.Value + " to " + member.role[0].timeframe.end.Value;

                if (member.role[0].extension.schoolunitcode.Any())
                {
                    tips += ", schoolunitcode=" + member.role[0].extension.schoolunitcode;
                }

                if (member.role[0].extension.schoolyear.Any())
                {
                    tips += ", schoolyear=" + member.role[0].extension.schoolyear;
                }

/*
                if (member.role[0].extension != null)
                {
                    var result = member.role[0].extension.Any;
                    foreach (XmlNode ext in result[0].ChildNodes)
                    {
                        if ((ext.Name == "course_code" || ext.Name == "subject_code") && ext.InnerText != string.Empty)
                            tips += " " + ext.InnerText;
                    }
                }
                */
                node.ToolTipText = tips;
            }

            if (subNode != null)
                subNode.Nodes.Add(node); // add the new node to the current nodes subnodes
            else
                enterpriseList.Nodes.Add(node);
        }

        private void openFromUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if the file was edited and saved before opening the url file.
            if (this.recentSaved == false)
            {
                DialogResult result = MessageBox.Show("You are about to load another file without saving, do you want to save the file before continuing?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.No)
                {
                    if (this.currentFilePath != "" || this.newFile != true)
                    {
                        this.writeToFile(false);
                    }
                    else
                    {
                        this.saveAsToolStripMenuItem_Click(sender, e);
                    }
                }
            }
            
            UrlInputDialog urlDialog = new UrlInputDialog(ref this.currentFilePath, ref this.recentSaved);
            
            var uresult = urlDialog.ShowDialog();
            if (uresult == DialogResult.OK)
            {
                if (this.IMSSettings.Startup.FileToOpen.Choice == 0)
                {
                    this.IMSSettings.Startup.FileToOpen.Value = urlDialog.Uri;
                    this.IMSSettings.Save();
                }

                this.currentUrl = urlDialog.Uri;
                this.downloadAndProcessTime = Convert.ToInt32(urlDialog.Timer.ElapsedMilliseconds);
                this.currentFilePath = urlDialog.CurrentFilePath;

                this.enableLoadingMode();
                this.bgwIMS.RunWorkerAsync();
            }
            
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Text = "IMS Enterprise Diagnostic Tool";
            about.Show();
        }

        private void searchInput_DragDrop(object sender, DragEventArgs e)
        {
            searchInput.Text = e.Data.ToString();
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView.Sort();
        }

        private void errorListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == eLvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (eLvwColumnSorter.Order == SortOrder.Ascending)
                {
                    eLvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    eLvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                eLvwColumnSorter.SortColumn = e.Column;
                eLvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.errorListView1.Sort();
        }

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if(this.listView.SelectedItems[0].Bounds.IntersectsWith(new Rectangle(e.Location,this.listView.SelectedItems[0].Bounds.Size)))
                {
                    if(this.listView.SelectedItems[0].Text != null)
                    {
                        this.contextMenuStrip2.Show(this.listView, e.Location);
                    }
                   
                    
                }
                
            }
        }

        private void copToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!this.listView.SelectedItems[0].SubItems[1].Text.IsEmpty())
                Clipboard.SetText(this.listView.SelectedItems[0].SubItems[1].Text);
        }

        private void searchInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                this.searchButton_Click(sender, e);
            }
        }

        private void searchInput_TextChanged(object sender, EventArgs e)
        {
            if (this.searchInput.Text == "")
            {
                if (this.bgwSearchGroup.IsBusy == false && this.bgwSearchPerson.IsBusy == false)
                    this.setupEnterpriseList();
                
            }
        }

        private void enterpriseList_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            this.enableContextMenu();
            this.editInforamtionBtn.Enabled = true;
            this.deleteItemBtn.Enabled = true;
            this.editItemToolStripMenuItem1.Enabled = true;
            this.fixNodeInfo(e.Node);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.removeToolStripMenuItem_Click(sender, e);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (enterpriseList.SelectedNode.Tag != (Object) "DontRemove[!]")
            {
                Clipboard.SetText(this.copy());
            }
            else
                MessageBox.Show("Can't copy this item!");
        }

        private String copy(bool csv = false) // copies the selected item from information list
        {
            String s = "";
            foreach (ListViewItem item in this.listView.Items)
            {
                int index = 0;
                foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                {
                    if (index != 1)
                    {
                        if(!csv)
                            s += subItem.Text + ": ";
                        else
                            s += subItem.Text + ",";
                        index++;
                    }
                    else
                        s += subItem.Text + "\r\n";
                }
            }

            return s;
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(this.copy());
            }
            catch(Exception ex)
            {
                ex.ToString();
                MessageBox.Show("Can't copy nothing!");
            }
        }

        private void errorListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.errorListView1.SelectedItems.Count > 0)
                {
                    if (this.errorListView1.SelectedItems[0].SubItems[3].Text != "IMS Document" && this.errorListView1.SelectedItems[0].SubItems[2].Text != "-1")
                    {
                        if (this.errorListView1.SelectedItems[0].SubItems[5].Text == "Person")
                        {
                            informationLabel.Text = "Personal information";

                            this.updatePersonInformationList(int.Parse(this.errorListView1.SelectedItems[0].SubItems[2].Text));
                        }
                        else
                        {
                            this.updateGroupInformation(int.Parse(this.errorListView1.SelectedItems[0].SubItems[2].Text));
                        }
                    }
                }
            }
        }
        private void SplitContainer_Paint(object sender, PaintEventArgs e)
        {
            var control = sender as SplitContainer;
            //paint the three dots'
            Point[] points = new Point[3];
            var w = control.Width;
            var h = control.Height;
            var d = control.SplitterDistance;
            var sW = control.SplitterWidth;

            //calculate the position of the points'
            if (control.Orientation == Orientation.Horizontal)
            {
                points[0] = new Point((w / 2), d + (sW / 2));
                points[1] = new Point(points[0].X - 10, points[0].Y);
                points[2] = new Point(points[0].X + 10, points[0].Y);
            }
            else
            {
                points[0] = new Point(d + (sW / 2), (h / 2));
                points[1] = new Point(points[0].X, points[0].Y - 10);
                points[2] = new Point(points[0].X, points[0].Y + 10);
            }

            foreach (Point p in points)
            {
                p.Offset(-2, -2);
                e.Graphics.FillEllipse(SystemBrushes.ControlDark,
                    new Rectangle(p, new Size(3, 3)));

                p.Offset(1, 1);
                e.Graphics.FillEllipse(SystemBrushes.ControlLight,
                    new Rectangle(p, new Size(3, 3)));
            }
        }

        private void errorListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (this.errorListView1.SelectedItems[0].Bounds.IntersectsWith(new Rectangle(e.Location, this.errorListView1.SelectedItems[0].Bounds.Size)))
                {
                    if (this.errorListView1.SelectedItems[0].SubItems[3].Bounds.IntersectsWith(new Rectangle(e.Location, Cursor.Size)))
                    {
                        if (this.errorListView1.SelectedItems[0].Text != null)
                        {
                            this.contextMenuStrip3.Show(this.errorListView1, e.Location);
                        }
                    }
                    else if (this.errorListView1.SelectedItems[0].SubItems[4].Bounds.IntersectsWith(new Rectangle(e.Location, Cursor.Size)))
                    {
                        if (this.errorListView1.SelectedItems[0].Text != null)
                        {
                            this.contextMenuStrip4.Show(this.errorListView1, e.Location);
                        }
                    }


                }

            }
        }

        private void copyToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.errorListView1.SelectedItems[0].SubItems[4].Text);
        }

        private void copyIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.errorListView1.SelectedItems[0].SubItems[3].Text);
        }

        private void previewCurrentFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!this.currentFilePath.IsEmpty())
            {
                Application.UseWaitCursor = true;
                try
                {
                    PreviewCurrentFile pcf = new PreviewCurrentFile(this.currentFilePath);
                    pcf.Show();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Application.UseWaitCursor = false;
                }
            }
           
        }

        private void addProblem(Error error) // an function to reasure some important things about the error before adding. For example, make sure the error doesnt already exists.
        {
            Error find = null;
            foreach(Error findError in errorList)
            {
                if (findError.Id == error.Id)
                    find = findError;
            }
            if (find != null)
            {
                if (error.Description != find.Description)
                {
                    if (InvokeRequired)
                        this.Invoke(new Action(() => this.addItem(error)));
                    else 
                        this.addItem(error);
                }
            }
            else
            {
                if (InvokeRequired)
                    this.Invoke(new Action(() => this.addItem(error)));
                else
                    this.addItem(error);
            }
        }

        private void addItem(Error value) // adds an error to the list and to varibles
        {

            ListViewItem item = new ListViewItem();
            item.SubItems.Add((this.errorListView1.Items.Count + 1).ToString());
            item.SubItems.Add(value.Index.ToString());
            item.SubItems.Add(value.Id);
            item.SubItems.Add(value.Description);
            item.SubItems.Add(value.Type);

            bool checkWarning = (IMSSettings.General.ProblemLister.MaxWarnings != 0 && this.Warnings > IMSSettings.General.ProblemLister.MaxWarnings);
            bool checkError = (IMSSettings.General.ProblemLister.MaxErrors != 0 && this.Errors > IMSSettings.General.ProblemLister.MaxErrors);

            if (value.ErrorType == ErrorType.Warning && this.IMSSettings.General.ProblemLister.Warnings)
            {
                errorList.Add(value);

                item.ImageKey = "warning";
                item.ToolTipText = "Warning: " + value.Description;

                this.Warnings++;
                this.totWarningLabel.Text = this.Warnings.ToString() + " warnings(s)";

                this.errorListView1.Items.Add(item);
                if (checkWarning)
                {
                    this.errorListView1.Items.RemoveAt(this.IMSSettings.General.ProblemLister.MaxWarnings - 1);
                    this.errorList.RemoveAt(this.IMSSettings.General.ProblemLister.MaxWarnings - 1);
                }
            }
            else if (value.ErrorType == ErrorType.Error && this.IMSSettings.General.ProblemLister.Errors)
            {
                errorList.Add(value);
                    
                item.ImageKey = "error";
                item.ToolTipText = "Error: " + value.Description;

                this.Errors++;
                this.totErrorLabel.Text = this.Errors.ToString() + " error(s)";

                this.errorListView1.Items.Add(item);
                if (checkError)
                {
                    this.errorListView1.Items.RemoveAt(this.IMSSettings.General.ProblemLister.MaxErrors - 1);
                    errorList.RemoveAt(this.IMSSettings.General.ProblemLister.MaxErrors - 1);
                }
            }
            //log problem
            if (this.IMSSettings.General.ProblemLister.Log && this.IMSSettings.General.ProblemLister.Enabled)
            {
                String problem;
                if (value.ErrorType == ErrorType.Error && this.IMSSettings.General.ProblemLister.Errors)
                {
                    problem = "Error, Order:" + (this.errorListView1.Items.Count).ToString() + ", index:" + value.Index.ToString() + ", ID:" + value.Id.ToString() + ", Description:" + value.Description + ", Type:" + value.Type + "\r\n";
                    this.log.appendProblem(problem);
                    this.log.write();
                }
                else if (this.IMSSettings.General.ProblemLister.Warnings)
                {
                    problem = "Warning, Order:" + (this.errorListView1.Items.Count).ToString() + ", index:" + value.Index.ToString() + ", ID:" + value.Id.ToString() + ", Description:" + value.Description + ", Type:" + value.Type + "\r\n";
                    this.log.appendProblem(problem);
                    this.log.write();
                }
            }
        }

        private void asCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (enterpriseList.SelectedNode != null)
            {
                if (enterpriseList.SelectedNode.Tag.ToString() != "DontRemove[!]")
                {
                    SaveFileDialog fileDia = new SaveFileDialog();
                    String s;
                    if (enterpriseList.SelectedNode.Name == "Person")
                        s = CSVString.Person(this.ep.person[int.Parse(this.enterpriseList.SelectedNode.Tag.ToString())]);
                    else
                        s = CSVString.Group(this.ep.group[int.Parse(this.enterpriseList.SelectedNode.Tag.ToString())]);


                    fileDia.Filter = "CSV files (.csv)|*.csv";
                    fileDia.FilterIndex = 1;
                    fileDia.FileName = this.enterpriseList.SelectedNode.Text;

                    if (fileDia.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(fileDia.FileName, s, Encoding.Default);
                    }
                }
                else
                    MessageBox.Show("Can't export this item!");
            }
            else
                MessageBox.Show("No item selected to export!");
        }

        private void linkLabel1_MouseClick(object sender, MouseEventArgs e)
        {
            this.clearErrors();
        }

        private void removeProblem(string type) // removes a problem from the problem list
        {
            foreach(ListViewItem item in this.errorListView1.Items)
            {
                if(item.ImageKey == type)
                {
                    this.errorListView1.Items.Remove(item);
                }
            }
        }

        private void removeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int index = this.errorListView1.SelectedItems[0].Index;

            if (this.errorListView1.SelectedItems[0].ImageKey == "warning")
                this.Warnings--;
            else
                this.Errors--;

            this.errorList.RemoveAt(index);
            this.errorListView1.SelectedItems[0].Remove();
        }

        private void errorListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.clearErrors();
        }

        private void exportAsCsvfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.errorList.Count != 0)
            {
                String problems = "";
                foreach (Error value in this.errorList)
                {
                    if (value.ErrorType == ErrorType.Error)
                        problems += "Error, Order:" + (this.errorListView1.Items.Count + 1).ToString() + ", index:" + value.Index.ToString() + ", ID:" + value.Id.ToString() + ", Description:" + value.Description + ", Type:" + value.Type + "\r\n";
                    else
                        problems += "Warning, Order:" + (this.errorListView1.Items.Count + 1).ToString() + ", index:" + value.Index.ToString() + ", ID:" + value.Id.ToString() + ", Description:" + value.Description + ", Type:" + value.Type + "\r\n";
                }

                SaveFileDialog fileDia = new SaveFileDialog();

                fileDia.Filter = "CSV files (.csv)|*.csv";
                fileDia.FilterIndex = 1;

                if (fileDia.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(fileDia.FileName, problems, Encoding.Default);
                }
            }
            else
                MessageBox.Show("No problems listed");
        }

        private void previewCurrentLogfileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!this.currentFilePath.IsEmpty())
            {
                FileInfo fInfo = new FileInfo(this.currentFilePath);
                PreviewCurrentLogFile logfile = new PreviewCurrentLogFile(this.logPath + "\\" + fInfo.Name + ".log");
                logfile.Show();
            }
            else
                MessageBox.Show("No file is yet open");
        }

        private void asCSVToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (!this.currentFilePath.IsEmpty())
            {
                if (!this.exportBusy)
                {
                    BackgroundWorker bgw = new BackgroundWorker();
                    bgw.DoWork += bgw_DoWork;
                    bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                    SaveFileDialog fileDia = new SaveFileDialog();

                    fileDia.Filter = "CSV files (.csv)|*.csv";
                    fileDia.FilterIndex = 1;
                    FileInfo fi = new FileInfo(this.currentFilePath);
                    fileDia.FileName = fi.Name.Remove(fi.Name.IndexOf(".xml")) + " - Groups";

                    if (fileDia.ShowDialog() == DialogResult.OK)
                    {
                        List<String> a = new List<String>();
                        a.Add(fileDia.FileName);
                        a.Add("groups");
                        this.exportBusy = true;

                        this.enableLoadingMode();
                        this.progressBar.Show();
                        this.progressBar.Style = ProgressBarStyle.Continuous;
                        bgw.RunWorkerAsync(a);
                    }
                }
                else
                    MessageBox.Show("Export is currently busy with writing to a file, please wait");
            }
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.exportBusy = false;
            this.progressBar.Value = 0;
            this.progressBar.Hide();
            this.disableLoadingMode();
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            List<String> a = (List<String>)e.Argument;
            if (a[1] == "persons")
                File.WriteAllText(a[0], CSVString.Persons(ref this.ep, this.progressBar), Encoding.Default);
            else if (a[1] == "groups")
                File.WriteAllText(a[0], CSVString.Groups(ref this.ep, this.progressBar), Encoding.Default);
            else // Memberships
                File.WriteAllText(a[0], CSVString.Memberships(ref this.ep, this.progressBar), Encoding.Default);
        }

        private void asCSVToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!this.currentFilePath.IsEmpty())
            {
                if (!this.exportBusy)
                {
                    BackgroundWorker bgw = new BackgroundWorker();
                    bgw.DoWork += bgw_DoWork;
                    bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                    SaveFileDialog fileDia = new SaveFileDialog();

                    fileDia.Filter = "CSV files (.csv)|*.csv";
                    fileDia.FilterIndex = 1;
                    FileInfo fi = new FileInfo(this.currentFilePath);
                    fileDia.FileName = fi.Name.Remove(fi.Name.IndexOf(".xml")) + " - Memberships";

                    if (fileDia.ShowDialog() == DialogResult.OK)
                    {
                        List<String> a = new List<String>();
                        a.Add(fileDia.FileName);
                        a.Add("memberships");
                        this.exportBusy = true;

                        this.enableLoadingMode();
                        this.progressBar.Show();
                        this.progressBar.Style = ProgressBarStyle.Continuous;
                        bgw.RunWorkerAsync(a);
                    }
                }
                else
                    MessageBox.Show("Export is currently busy with writing to a file, please wait");
            }
        }

        private void asCSVToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!this.currentFilePath.IsEmpty())
            {
                if (!this.exportBusy)
                {
                    BackgroundWorker bgw = new BackgroundWorker();
                    bgw.DoWork += bgw_DoWork;
                    bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                    SaveFileDialog fileDia = new SaveFileDialog();

                    fileDia.Filter = "CSV files (.csv)|*.csv";
                    fileDia.FilterIndex = 1;
                    FileInfo fi = new FileInfo(this.currentFilePath);
                    fileDia.FileName = fi.Name.Remove(fi.Name.IndexOf(".xml")) + " - Persons";

                    if (fileDia.ShowDialog() == DialogResult.OK)
                    {
                        List<String> a = new List<String>();
                        a.Add(fileDia.FileName);
                        a.Add("persons");
                        this.exportBusy = true;

                        this.enableLoadingMode();
                        this.progressBar.Show();
                        this.progressBar.Style = ProgressBarStyle.Continuous;
                        bgw.RunWorkerAsync(a);
                    }
                }
                else
                    MessageBox.Show("Export is currently busy with writing to a file, please wait");
            }
        }
        private String getPID(person person) // get a PID from a person
        {
            foreach(userid uid in person.userid)
            {
                if(uid.useridtype == "PID")
                {
                    return uid.Value;
                }
            }
            return "";
        }

        private String getGUID(person person) // get a GUID from a person
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

        private void searchInput_Enter(object sender, EventArgs e)
        {
            if ((int)searchInput.Tag == 0)
            {
                searchInput.Text = "";
                searchInput.Tag = 1;
            }
        }

        private void searchInput_Leave(object sender, EventArgs e)
        {
            if ((int)searchInput.Tag == 1)
            {
                searchInput.Text = "Search...";
                searchInput.Tag = 0;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                this.searchInput.Focus();
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.openDocumentation();
        }
    }
}
