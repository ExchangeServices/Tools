using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Threading;

namespace IMSEnterprise
{
    class XmlFile
    {
        private String path;
        public String Path
        {
            get { return path; }
            set { path = value; }
        }

        private bool consoleLog;
        public bool ConsoleLog
        {
            get { return consoleLog; }
            set { consoleLog = value; }
        }

        public XmlFile()
        {

        }

        public XmlFile(bool consoleLog)
        {
            this.ConsoleLog = consoleLog;
        }

        public static void clearTemp(String path)
        {
            DirectoryInfo tempDir = new DirectoryInfo(path);
            foreach(FileInfo file in tempDir.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
        }

        public void DownloadAsync(String url, Action<Object, DownloadProgressChangedEventArgs> progress, Action<Object, AsyncCompletedEventArgs> complete)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new WebException("The url given is not a well formed Url");

            Random rand = new Random();
            this.checkTempDir();

            string tempDirectory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "IMS Enterprise Diagnostic Tool");
            this.Path = System.IO.Path.Combine(tempDirectory.ToString(), "httpRequestedXml" + Guid.NewGuid() + ".xml.tmp");

            try
            {
                clearTemp(tempDirectory);
            }
            catch(Exception e)
            {
                throw e;
            }

            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progress);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(complete);

            try
            {
                wc.DownloadFileAsync(new Uri(url), this.Path);
            }
            catch (WebException e)
            {
                wc.Dispose();
                throw e;
            }

        }

        private void checkTempDir()
        {
            DirectoryInfo di = new DirectoryInfo(System.IO.Path.GetTempPath() + "\\IMS Enterprise Diagnostic Tool");
            if (!di.Exists)
                di.Create();
        }

        public void Download(String url, Action<Object, DownloadProgressChangedEventArgs> progress, Action<Object, AsyncCompletedEventArgs> complete)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new WebException("The url given is not a well formed Url");

            Random rand = new Random();
            this.checkTempDir();

            string tempDirectory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "IMS Enterprise Diagnostic Tool");
            this.Path = System.IO.Path.Combine(tempDirectory.ToString(), "httpRequestedXml" + rand.Next(0, 1000000000) + ".xml.tmp");

            try
            {
                clearTemp(tempDirectory);
            }
            catch
            {

            }

            WebClient wc = new WebClient();
            
            
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progress);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(complete);

            try
            {
                wc.DownloadFileAsync(new Uri(url), this.Path);
                while(wc.IsBusy)
                {
                    if (consoleLog)
                        Console.Write(".");
                    Thread.Sleep(500);
                }
            }
            catch (WebException e)
            {
                wc.Dispose();
                throw e;
            }
                    
        }

        public void Download(String url, String fileName, Action<Object, DownloadProgressChangedEventArgs> progress, Action<Object, AsyncCompletedEventArgs> complete)
        {
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new Exception("The url given is not a well formed Url");

            this.Path = fileName;

            WebClient wc = new WebClient();

            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progress);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(complete);

            try
            {
                wc.DownloadFileAsync(new Uri(url), this.Path);
                while (wc.IsBusy)
                {
                    if (consoleLog)
                        Console.Write(".");
                    Thread.Sleep(500);
                }
            }
            catch (WebException e)
            {
                wc.Dispose();
                throw e;
            }
        }

        public static enterprise Read(String uri)
        {
            if (!uri.EndsWith(".xml"))
            {
                if (!uri.EndsWith(".xml.tmp"))
                    throw new XmlException("The file can't be read because of wrong type");
            }

            enterprise enterPrise; 
            using (var xs = new XmlTextReader(uri)) // read the xml file from given uri
            {
                try
                {
                    XmlSerializer xser = new XmlSerializer(typeof(enterprise));
                    enterPrise = (enterprise)xser.Deserialize(xs);
                }
                catch(Exception e)
                {
                    throw e;
                }
                finally
                {
                    xs.Close();
                }
            }
            return enterPrise;
        }
        
        public enterprise Read()
        {
            if (this.Path != null)
                return Read(this.Path);
            else
                throw new Exception("A valid path to the XML-file has to be set before reading a XML-file");
        }
    }
}
