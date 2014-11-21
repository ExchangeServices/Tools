using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;


namespace IMSEnterprise
{
    public partial class UrlInputDialog : Form
    {
        private String currentFilePath;
        public String CurrentFilePath
        {
            get { return currentFilePath; }
            set { currentFilePath = value; }
        }

        private String uri;
        public String Uri
        {
            get { return uri; }
            set { uri = value; }
        }

        private bool recentSaved;
        public bool RecentSaved
        {
            get { return recentSaved; }
            set { recentSaved = value; }
        }

        private List<Object> headers;
        public List<Object> Headers
        {
            get { return headers; }
            set { headers = value; }
        }

        private Stopwatch timer = new Stopwatch();
        public Stopwatch Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        private bool onStartUp = false;
        private bool Continue = false;

        private ProgressBar progressBar;

        public UrlInputDialog(ref String currentFilePath, ref bool recentSaved)
        {
            InitializeComponent();
            this.setAutoCompleteSource();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.currentFilePath = currentFilePath;
            this.recentSaved = recentSaved;
            this.Text = "Load IMS Document from url...";
            this.inputDialogTitleLabel.Text = "Insert the web address here:";
            try
            {
                this.inputDialogComboBox.Text = this.inputDialogComboBox.Items[this.inputDialogComboBox.Items.Count - 1].ToString();
            }
            catch
            { }

            this.progressBar = new ProgressBar();
            this.progressBar.Left = this.inputDialogComboBox.Left;
            this.progressBar.Top = this.inputDialogComboBox.Top + this.inputDialogComboBox.Height + 5;
            this.progressBar.Width = this.inputDialogComboBox.Width;
            this.progressBar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.progressBar.Step = 1;
            this.progressBar.Maximum = 100;
            this.Controls.Add(this.progressBar);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            
          
            
            
            
        }

        public UrlInputDialog(String uri)
        {
            InitializeComponent();
            this.setAutoCompleteSource();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            
            this.progressBar = new ProgressBar();
            this.progressBar.Left = this.inputDialogComboBox.Left;
            this.progressBar.Top = this.inputDialogComboBox.Top + this.inputDialogComboBox.Height + 5;
            this.progressBar.Width = this.inputDialogComboBox.Width;
            this.progressBar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.progressBar.Step = 1;
            this.progressBar.Maximum = 100;
            this.Controls.Add(this.progressBar);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.Text = "loading...";
           
            this.inputDialogTitleLabel.Text = "loading file from url";

            this.inputDialogComboBox.Text = uri;
            this.Uri = uri;
            this.onStartUp = true;

            this.getHttpFile();
            this.loadBtn.Enabled = false;
        }

        private void setAutoCompleteSource()
        {
            AutoCompleteStringCollection urlHistory = new AutoCompleteStringCollection();
            DirectoryInfo di = new DirectoryInfo(System.IO.Path.GetTempPath() + "\\IMS Enterprise Diagnostic Tool\\History");
            if (!di.Exists)
                di.Create();

            string tempDirectory = di.FullName;
            string filePath = System.IO.Path.Combine(tempDirectory.ToString(), "urlhistory.hist");
            

            try
            {
                string[] urlHistoryItems = File.ReadAllLines(filePath, Encoding.Default);
                urlHistory.AddRange(urlHistoryItems);
                this.inputDialogComboBox.Items.AddRange(urlHistoryItems);   
            }
            catch(Exception ex)
            {
                if (ex.HResult == -2147024894)
                    File.Create(filePath).Close();
                else
                    MessageBox.Show(ex.Message);
            }

            this.inputDialogComboBox.AutoCompleteCustomSource = urlHistory;  
        }
        private void saveAutoCompleteSource()
        {

            AutoCompleteStringCollection urlHistory = new AutoCompleteStringCollection();
            DirectoryInfo di = new DirectoryInfo(System.IO.Path.GetTempPath() + "\\IMS Enterprise Diagnostic Tool\\History");
            if (!di.Exists)
                di.Create();

            string tempDirectory = di.FullName;
            string filePath = System.IO.Path.Combine(tempDirectory.ToString(), "urlhistory.hist");
            

            try
            {
               File.WriteAllLines(filePath, this.inputDialogComboBox.Items.Cast<Object>().Select(item => item.ToString()).ToArray(), Encoding.Default);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private String totalBytes;
        private void setProgress(Object sender, DownloadProgressChangedEventArgs e)
        {
            if (this.Continue)
            {
                try
                {
                    this.progressBar.Value = e.ProgressPercentage;
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    this.Continue = false;
                }
                finally
                {
                    if (totalBytes == null)
                    {
                        this.totalBytes = this.getPrefix(Convert.ToDouble(e.TotalBytesToReceive));
                        this.progressBar.Style = ProgressBarStyle.Continuous;
                    }

                    if (e.ProgressPercentage != 100)
                        this.progress.Text = this.getPrefix(Convert.ToDouble(e.BytesReceived)) + " / " + this.totalBytes;
                    else
                    {
                        if(this.totalBytes == "-1 B")
                            this.progress.Text = this.getPrefix(Convert.ToDouble(e.BytesReceived));
                        else
                            this.progress.Text = this.getPrefix(Convert.ToDouble(e.BytesReceived)) + " / " + this.getPrefix(Convert.ToDouble(e.BytesReceived));
                    }

                }
            }
            else
            {
                this.Continue = false;
                WebClient s = (WebClient)sender;
                s.CancelAsync();
                s.Dispose();
            }
        }

        private String getPrefix(double len)
        {
            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (len >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                len = len / 1024;
            }

            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }

        private void progressComplete(Object sender, AsyncCompletedEventArgs e)
        {
            this.Timer.Stop();

            WebClient wc = (WebClient)sender;
            wc.Dispose();
            if (this.Continue)
            {
                try
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.clearProgress();
                    }
                    else
                    {
                        if (!this.onStartUp)
                            MessageBox.Show("The IMS Document was successfully loaded!\n\nProcessing and downloading took: " + (this.Timer.ElapsedMilliseconds/1000).ToString() + " seconds.");

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (WebException we)
                {
                    
                    MessageBox.Show(we.Message);
                }
                finally
                {
                    Application.UseWaitCursor = false;
                }
            }
            else
            {
                this.Continue = false;
                this.clearProgress();
                //this.DialogResult = DialogResult.Cancel;
            }

            Application.UseWaitCursor = false;
        }

        private void clearProgress()
        {
            this.Continue = false;
            this.progressBar.Value = 0;
            this.progressBar.Style = ProgressBarStyle.Continuous;
            this.progress.Text = "";
            this.loadBtn.Enabled = true;
            
        }

        private void getHttpFile()
        {
            this.loadBtn.Enabled = false;
            this.progressBar.Maximum = 100;
            this.progressBar.Value = 1;
            Application.UseWaitCursor = true;

            XmlFile xmlf = new XmlFile();
            try
            {
                this.Timer.Start();
                this.Continue = true;
                xmlf.DownloadAsync(this.Uri, this.setProgress, this.progressComplete);
                this.progressBar.Style = ProgressBarStyle.Marquee;
                this.progress.Text = "Contacting server...";
            }
            catch(Exception e)
            {
                this.Continue = false;
                if (e.HResult == -2147024864)
                    MessageBox.Show("A process is still runing from the first attempt to load a file. Please wait a few seconds before trying again!");
                else
                    MessageBox.Show("Error! " + e.Message);

                Application.UseWaitCursor = false;
                this.clearProgress();
            }
            this.CurrentFilePath = xmlf.Path;
        }
 
        private void inputDialogBtn_Click(object sender, EventArgs e)
        {
            
            try
            {
                this.Uri = inputDialogComboBox.Text;
                if (!this.inputDialogComboBox.Items.Cast<Object>().Any(cbi => cbi.Equals(this.Uri)))
                {
                    this.inputDialogComboBox.Items.Add(this.Uri);
                    this.saveAutoCompleteSource();
                }

                this.getHttpFile();
               
            }
            catch(WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void UrlInputDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.saveAutoCompleteSource();
            this.Continue = false;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = false;
            if (this.Continue)
            {
                this.clearProgress();
            }
            else
            {
                this.Continue = false;
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }    
        }
    }
}
