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
    public partial class IMSFileInformationForm : Form
    {
        public IMSFileInformationForm(FileInfo currentFile, String loadingTime, int numberOfPersons, int numberOfGroups, int numberOfMemberships, int downloadProcessTime = 0)
        {
            InitializeComponent();
            currentFilePathLabel.Text = currentFile.FullName;
            Size size = TextRenderer.MeasureText(currentFilePathLabel.Text, currentFilePathLabel.Font);
            currentFilePathLabel.Width = size.Width;
            if(currentFilePathLabel.Width > 90)
                this.Width = currentFilePathLabel.Width + labelForCurrentFilePath.Width + labelForCurrentFilePath.Left + 20;
            
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = currentFile.Length;
            int order = 0;
            while (len >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                len = len / 1024;
            }
            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            currentFileSizeLabel.Text = String.Format("{0:0.##} {1}", len, sizes[order]);


            currentFileLoadingTimeLabel.Text = loadingTime;
            numberOfPersonsLabel.Text = numberOfPersons.ToString();
            numberOfGroupsLabel.Text = numberOfGroups.ToString();
            numberOfMembershipsLabel.Text = numberOfMemberships.ToString();
            downloadAndProcessLabel.Text = (downloadProcessTime/1000).ToString() + " s";
        }
    }
}
