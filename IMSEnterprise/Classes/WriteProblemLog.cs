using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace IMSEnterprise
{
    class WriteProblemLog
    {
        private List<String> problems;
        private bool inProgress = false;

        private String directory;
        public String Directory
        {
            get { return directory; }
            set { directory = value; }
        }

        private String path;
        public String Path
        {
          get { return path; }
          set { path = value; }
        }

        private IMSSettings settings;

        public WriteProblemLog(String path, String directory)
        {
            this.settings = new IMSSettings();
            if (settings.General.ProblemLister.Log)
            {
                this.Path = path;
                this.Directory = directory;

                this.problems = new List<String>();

                DirectoryInfo d = new DirectoryInfo(Directory);
                d.Create();

                FileInfo file = new FileInfo(this.Path);
                FileInfo logfile = new FileInfo(this.Directory + "/" + file.Name + ".log");
                String s = "";
                if (logfile.Exists)
                    s = System.Environment.NewLine;

                File.AppendAllText(this.Directory + "\\" + file.Name + ".log", s + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + System.Environment.NewLine);
            }
        }

        public void appendProblem(String problem)
        {
            if (settings.General.ProblemLister.Log)
                this.problems.Add(problem);
        }

        public void write()
        {
            if (!this.inProgress)
            {
                if (settings.General.ProblemLister.Log)
                {
                    this.inProgress = true;

                    BackgroundWorker bgw = new BackgroundWorker();
                    bgw.DoWork += bgw_DoWork;
                    bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                    bgw.RunWorkerAsync(directory);
                }
            }
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.problems.Count != 0)
            {
                if (settings.General.ProblemLister.Log)
                {
                    BackgroundWorker bgw = new BackgroundWorker();
                    bgw.DoWork += bgw_DoWork;
                    bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                    bgw.RunWorkerAsync(this.Directory);
                }
            }
            else
            {
                this.inProgress = false;
            }
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            run(problems[0], this.directory);
            problems.RemoveAt(0);
        }

        private void run(String problem, String Directory)
        {
            FileInfo file = new FileInfo(this.Path);
            String path = Directory + "/" + file.Name + ".log";
            File.AppendAllText(path, problem);
        }
    }
}
