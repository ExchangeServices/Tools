using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.InteropServices;
using System.Net;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;




namespace IMSEnterprise
{
    class CommandLineInterface
    {

        private Options _options;

        public Options Options
        {
            get { return _options; }
            set { _options = value; }
        }

        private enterprise _ep;
        public enterprise Ep
        {
            get { return _ep; }
            set { _ep = value; }
        }

        private XmlFile _xmlFile;
        public XmlFile XmlFile
        {
            get { return _xmlFile; }
            set { _xmlFile = value; }
        }

        private string[] _args;
        public string[] Args
        {
            get { return _args; }
            set { _args = value; }
        }

       

        public CommandLineInterface(string[] args)
        {
            this.Args = args;
            this.Options = new Options(args);  
        }
        
        public int Run()
        {
            
            if(this.Options.DownloadArgs != null)
            {
                if (this.Options.DownloadArgs.Length == 2)
                {
                    try
                    {
                        this.Download(this.Options.DownloadArgs[0], this.Options.DownloadArgs[1]);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Error: failed to download file, message: " + e.Message);
                        return 2;
                    }
                }
                    
                else
                {
                    // Wrong argument length, show error message and help text.
                    Console.WriteLine("Error: Wrong input. Did you specify an url to download and a file name?");
                    this.Options.GetUsage();
                    return -1;
                }
            }
            else if (this.Options.ExportArgs != null)
            {
                if (this.Options.ExportArgs.Length == 2)
                    this.Export(this.Options.ExportArgs[0], this.Options.ExportArgs[1]);
                else if (this.Options.ExportArgs.Length == 3)
                    this.Export(this.Options.ExportArgs[0], this.Options.ExportArgs[1], this.Options.ExportArgs[2]);
                else
                {
                    // Wrong argument length, show error message and help text.
                    Console.WriteLine("Error: Wrong input. Did you specify a file to export and a export type?");
                    this.Options.GetUsage();
                    return -1;
                }
            }
            else
                return -1;
            
            // Everything succeded successfully
            return 0; 
        }
        private void Export(String file, String type)
        {
            // Look for the right export types, otherwise show an error message and help text.
            if (!Regex.Match(type, @"\b(persons|groups|memberships)\b", RegexOptions.IgnoreCase).Success)
            {
                Console.WriteLine("Error: Wrong export type provided.");
                this.Options.GetUsage();
            }
            // Read the xml-file and store it.
            try
            {
                // Try to store the value.
                this.Ep = readXmlFile(file);
            }
            catch(Exception e)
            {
                // Print error messages and help text.
                Console.WriteLine("Error: Something went wrong.");
                Console.WriteLine(e.Message);
                this.Options.GetUsage();
            }
            // No specified file name, place it in the executable folder with the same name as the source file.
            String fileName = Path.GetDirectoryName(Application.ExecutablePath) + file + ".csv";

            // Export it.
            try
            {
                if (Regex.Match(type, @"persons", RegexOptions.IgnoreCase).Success)
                    File.WriteAllText(fileName, CSVString.Persons(ref this._ep), Encoding.UTF8);
                else if (Regex.Match(type, @"groups", RegexOptions.IgnoreCase).Success)
                    File.WriteAllText(fileName, CSVString.Groups(ref this._ep), Encoding.UTF8);
                else if (Regex.Match(type, @"memberships", RegexOptions.IgnoreCase).Success)
                    File.WriteAllText(fileName, CSVString.Memberships(ref this._ep), Encoding.UTF8);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: failed to export file, message: " + e.Message);
            }
            
        }
        private void Export(String file, String type, String fileName)
        {

            // Look for the right export types, otherwise show an error message and help text.
            if (!Regex.Match(type, @"\b(persons|groups|memberships)\b", RegexOptions.IgnoreCase).Success)
            {
                Console.WriteLine("Error: Wrong export type provided.");
                this.Options.GetUsage();
            }
            // Read the xml-file and store it.
            try
            {
                // Try to store the value.
                this.Ep = readXmlFile(file);
            }
            catch (Exception e)
            {
                // Print error messages and help text.
                Console.WriteLine("Error: Something went wrong.");
                Console.WriteLine(e.Message);
                this.Options.GetUsage();
            }
            
            // Export it.
            if (Regex.Match(type, @"persons", RegexOptions.IgnoreCase).Success)
                File.WriteAllText(fileName, CSVString.Persons(ref this._ep), Encoding.Default);
            else if (Regex.Match(type, @"groups", RegexOptions.IgnoreCase).Success)
                File.WriteAllText(fileName, CSVString.Groups(ref this._ep), Encoding.Default);
            else if (Regex.Match(type, @"memberships", RegexOptions.IgnoreCase).Success)
                File.WriteAllText(fileName, CSVString.Memberships(ref this._ep), Encoding.Default);
            Console.WriteLine("File successfully exported." + "\nFile exported to: " + fileName);
        }

        // Read the IMS-file via the generated enterprise class.
        private enterprise readXmlFile(string uri)
        {
            enterprise enterPrise;
            try
            {
                enterPrise = XmlFile.Read(uri);
            }
            catch (Exception e)
            {
                throw e;
            }

            return enterPrise;
        }

        // 
        private void Download(String url)
        {
            this.XmlFile = new XmlFile(true);
            this.XmlFile.Download(url, this.downloadProgress, this.downloadDidComplete);
        }
        private void Download(String url, String fileName)
        {
            this.XmlFile = new XmlFile(true);
            this.XmlFile.Download(url, fileName, this.downloadProgress, this.downloadDidComplete);
        }

        private void downloadProgress(Object sender, DownloadProgressChangedEventArgs e)
        {
           // Do nothing.
        }

        private void downloadDidComplete(Object sender, AsyncCompletedEventArgs e)
        {
            WebClient wc = (WebClient)sender;
            wc.Dispose();
            Console.WriteLine("\nDownload complete." + "\nFile saved at: " + Options.DownloadArgs[1]);
        }
        
    }
    class Options
    {
        private string[] _downloadArgs;
        public string[] DownloadArgs
        {
            get { return _downloadArgs; }
            set { _downloadArgs = value; }
        }

        private string[] _exportArgs;
        public string[] ExportArgs
        {
            get { return _exportArgs; }
            set { _exportArgs = value; }
        }

        public Options(String[] args)
        {
            var e = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            Console.WriteLine("\n" + e.Name + " " + e.Version);
            Console.WriteLine(GetCopyright() + " " + GetCompany() + "\n\n");
            if (args[0] == "-d" || args[0] == "/d" || args[0] == "--download")
            {
                this.DownloadArgs = new String[args.Length - 1];
                for (int i = 1; i < args.Length; i++)
                {
                    this.DownloadArgs[i - 1] = args[i];
                }
            }
            else if (args[0] == "-e" || args[0] == "/e" || args[0] == "--export")
            {
                this.ExportArgs = new String[args.Length - 1];
                for (int i = 1; i < args.Length; i++)
                {
                    this.ExportArgs[i - 1] = args[i];
                }
            }
            else if (args[0] == "-h" || args[0] == "/h" || args[0] == "--help")
                this.GetUsage();
            else
            {
                Console.WriteLine("Error: invalid program arguments passed.");
                this.GetUsage();
            }
            
        }
        
        public void GetUsage()
        {
            
            Console.WriteLine("\n\nPossible command attributes:\n");
            Console.WriteLine("/d -d --download\t\tDownload a file from internet");
            Console.WriteLine("\t\t\t\t/d 'url' 'fileName'");
            Console.WriteLine("\t\t\t\turl - e.g. http://myserver.com/myfile.xml");
            Console.WriteLine("\t\t\t\tfileName - e.g. myCustomFileName.xml");
            Console.WriteLine("\n");
            Console.WriteLine("/e -e --export\t\t\tExport to csv file");
            Console.WriteLine("\t\t\t\t/e 'file' 'type' 'filename'");
            Console.WriteLine("\t\t\t\tfile - e.g. file.xml");
            Console.WriteLine("\t\t\t\ttype - e.g. persons, groups, memberships");
            Console.WriteLine("\t\t\t\tfileName - e.g. newFile.csv");
        }

        private string GetCopyright()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            object[] obj = asm.GetCustomAttributes(false);
            foreach (object o in obj)
            {
                if (o.GetType() ==
                    typeof(System.Reflection.AssemblyCopyrightAttribute))
                {
                    AssemblyCopyrightAttribute aca =
            (AssemblyCopyrightAttribute)o;
                    return aca.Copyright;
                }
            }
            return string.Empty;
        }
        private string GetCompany()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            object[] obj = asm.GetCustomAttributes(false);
            foreach (object o in obj)
            {
                if (o.GetType() ==
                    typeof(System.Reflection.AssemblyCompanyAttribute))
                {
                    AssemblyCompanyAttribute aca =
            (AssemblyCompanyAttribute)o;
                    return aca.Company;
                }
            }
            return string.Empty;
        }
        
    } 
}
