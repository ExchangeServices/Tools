using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IMSEnterprise
{
    public partial class PreviewCurrentFile : Form
    {
        public PreviewCurrentFile(string currentFilePath)
        {
            InitializeComponent();
            if(!currentFilePath.IsEmpty())
            {
                try
                {
                    this.richTextBox1.Text = File.ReadAllText(currentFilePath,Encoding.Default);
                }
                catch (Exception e)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    throw e;
                }
                Application.UseWaitCursor = false;
            }
            else
            {
                MessageBox.Show("No source to show.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
