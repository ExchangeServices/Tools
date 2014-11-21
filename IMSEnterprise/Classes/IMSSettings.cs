using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace IMSEnterprise
{
    class IMSSettings
    {
        private String filePath;

        private General general = new General();
        internal General General
        {
            get { return general; }
            set { general = value; }
        }

        private Startup startup = new Startup();
        internal Startup Startup
        {
            get { return startup; }
            set { startup = value; }
        }

        public IMSSettings()
        {
            FileInfo fi = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\IMSEnterprise.config");
            if (!fi.Exists)
            {
                this.createSettings(fi);
            }

            this.filePath = fi.FullName;
            this.onLoad();
            
        }

        private void createSettings(FileInfo fi)
        {
            String s = "<?xml version=\"1.0\" encoding=\"utf-8\"?><settings><general><startPoint><root type=\"Unit\">True</root><root type=\"UnitDomain\">False</root><root type=\"SchoolUnit\">False</root><root type=\"Course\">False</root><root type=\"Subject\">False</root><root type=\"EducationGroup\">False</root><root type=\"Class\">False</root><root type=\"Contacts\">False</root><root type=\"Person\">False</root><root type=\"ContactGroup\">False</root></startPoint><problemLister><enabled>True</enabled><warnings>True</warnings><errors>True</errors><maxErrors>0</maxErrors><maxWarnings>0</maxWarnings><log>False</log><logLocation use=\"False\"></logLocation></problemLister><extensions><enabled>True</enabled><distinguish>True</distinguish></extensions></general><startUp><fileToOpen recent=\"3\">-</fileToOpen></startUp></settings>";

            File.WriteAllText(fi.FullName, s);
        }

        private void onLoad()
        {
            XmlDocument xmlD = new XmlDocument();
            try
            {
                xmlD.Load(this.filePath);
                this.setValues((XmlNode)xmlD, "startPoint");
                this.setValues((XmlNode)xmlD, "problemLister");
                this.setValues((XmlNode)xmlD, "extensions");
                this.setValues((XmlNode)xmlD, "fileToOpen");
            }
            catch(XmlException e)
            {
                throw e;
            }
            
        }
        private void setValues(XmlNode node, String find)
        {
            foreach (XmlNode subNode in node.ChildNodes)
            {
                if(subNode.Name == find)
                {
                    foreach(XmlNode rootNode in subNode.ChildNodes)
                    {
                        switch(find)
                        {
                            case "startPoint":
                                Root root = new Root();
                                root.Type = rootNode.Attributes[0].Value;
                                root.Value = Convert.ToBoolean(rootNode.InnerText);
                                this.General.StartPoint.Roots.Add(root);
                                break;

                            case "problemLister":
                                this.General.ProblemLister.Enabled = Convert.ToBoolean(rootNode.ParentNode.ChildNodes[0].InnerText);
                                this.General.ProblemLister.Warnings = Convert.ToBoolean(rootNode.ParentNode.ChildNodes[1].InnerText);
                                this.General.ProblemLister.Errors = Convert.ToBoolean(rootNode.ParentNode.ChildNodes[2].InnerText);
                                this.General.ProblemLister.MaxErrors = int.Parse(rootNode.ParentNode.ChildNodes[3].InnerText);
                                this.General.ProblemLister.MaxWarnings = int.Parse(rootNode.ParentNode.ChildNodes[4].InnerText);
                                this.General.ProblemLister.Log = Convert.ToBoolean(rootNode.ParentNode.ChildNodes[5].InnerText);
                                this.General.ProblemLister.LogLocation.Use = Convert.ToBoolean(rootNode.ParentNode.ChildNodes[6].Attributes[0].Value);
                                this.General.ProblemLister.LogLocation.Location = rootNode.ParentNode.ChildNodes[6].InnerText;
                                break;

                            case "extensions":
                                this.General.Extensions.Enabled = Convert.ToBoolean(rootNode.ParentNode.ChildNodes[0].InnerText);
                                this.General.Extensions.Distinguish = Convert.ToBoolean(rootNode.ParentNode.ChildNodes[1].InnerText);
                                break;

                            case "fileToOpen":
                                FileToOpen fto = new FileToOpen();
                                fto.Choice = int.Parse(rootNode.ParentNode.Attributes[0].Value);
                                fto.Value = rootNode.InnerText;
                                this.Startup.FileToOpen = fto;
                                break;
                        }         
                    }
                }
                else
                {
                    this.setValues(subNode, find);
                }
            }
        }
        public void Save()
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", null));

            XmlNode settingsNode = doc.CreateElement("settings");
                XmlNode generalNode = doc.CreateElement("general");
                    XmlNode startPointNode = doc.CreateElement("startPoint");
                    foreach(Root root in this.General.StartPoint.Roots)
                    {
                        XmlNode rootNode = doc.CreateElement("root");
                        XmlAttribute rootAttr = doc.CreateAttribute("type");
                        rootAttr.Value = root.Type;
                        rootNode.Attributes.Append(rootAttr);
                        rootNode.InnerText = root.Value.ToString();

                        startPointNode.AppendChild(rootNode);  
                    }

                    generalNode.AppendChild(startPointNode);

                        XmlNode pLNode = doc.CreateElement("problemLister");
                            XmlNode enabledNode = doc.CreateElement("enabled");
                            enabledNode.InnerText = this.General.ProblemLister.Enabled.ToString();
                            pLNode.AppendChild(enabledNode);
                            XmlNode warningsNode = doc.CreateElement("warnings");
                            warningsNode.InnerText = this.General.ProblemLister.Warnings.ToString();
                            pLNode.AppendChild(enabledNode);
                            XmlNode errorsNode = doc.CreateElement("errors");
                            errorsNode.InnerText = this.General.ProblemLister.Errors.ToString();
                            XmlNode maxErrorsNode = doc.CreateElement("maxErrors");
                            maxErrorsNode.InnerText = this.General.ProblemLister.MaxErrors.ToString();
                            XmlNode maxWarningsNode = doc.CreateElement("maxWarnings");
                            maxWarningsNode.InnerText = this.General.ProblemLister.MaxWarnings.ToString();
                            XmlNode logNode = doc.CreateElement("log");
                            logNode.InnerText = this.General.ProblemLister.Log.ToString();
                            XmlNode logLocationNode = doc.CreateElement("logLocation");
                            logLocationNode.InnerText = this.General.ProblemLister.LogLocation.Location;
                            XmlAttribute logAttr = doc.CreateAttribute("use");
                            logAttr.Value = this.General.ProblemLister.LogLocation.Use.ToString();
                            logLocationNode.Attributes.Append(logAttr);

                            pLNode.AppendChild(enabledNode);
                            pLNode.AppendChild(warningsNode);
                            pLNode.AppendChild(errorsNode);
                            pLNode.AppendChild(maxErrorsNode);
                            pLNode.AppendChild(maxWarningsNode);
                            pLNode.AppendChild(logNode);
                            pLNode.AppendChild(logLocationNode);

                        generalNode.AppendChild(pLNode);

                        XmlNode iVNode = doc.CreateElement("extensions");
                            XmlNode eEnabled = doc.CreateElement("enabled");
                            eEnabled.InnerText = this.General.Extensions.Enabled.ToString();
                            iVNode.AppendChild(eEnabled);
                            XmlNode disNode = doc.CreateElement("distinguish");
                            disNode.InnerText = this.General.Extensions.Distinguish.ToString();
                            iVNode.AppendChild(disNode);
                        generalNode.AppendChild(iVNode);
                            

                settingsNode.AppendChild(generalNode);
                XmlNode startUpNode = doc.CreateElement("startUp");
                    XmlNode fileToOpenNode = doc.CreateElement("fileToOpen");

                    XmlAttribute fileToOpenAttr = doc.CreateAttribute("recent");
                    fileToOpenAttr.Value = this.Startup.FileToOpen.Choice.ToString();

                    fileToOpenNode.Attributes.Append(fileToOpenAttr);
                    fileToOpenNode.InnerText = this.Startup.FileToOpen.Value;

                    startUpNode.AppendChild(fileToOpenNode);
                settingsNode.AppendChild(startUpNode);
            doc.AppendChild(settingsNode);
            doc.Save(this.filePath);
        }
    }

    class General
    {
        private StartPoint startPoint = new StartPoint();
        public StartPoint StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        private ProblemLister problemLister = new ProblemLister();
        public ProblemLister ProblemLister
        {
            get { return problemLister; }
            set { problemLister = value; }
        }

        private Extensions extensions = new Extensions();
        public Extensions Extensions
        {
            get { return extensions; }
            set { extensions = value; }
        }
    }

    class ProblemLister
    {
        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private bool warnings;
        public bool Warnings
        {
            get { return warnings; }
            set { warnings = value; }
        }

        private bool errors;
        public bool Errors
        {
            get { return errors; }
            set { errors = value; }
        }

        private int maxErrors;
        public int MaxErrors
        {
            get { return maxErrors; }
            set { maxErrors = value; }
        }

        private int maxWarnings;
        public int MaxWarnings
        {
            get { return maxWarnings; }
            set { maxWarnings = value; }
        }

        private bool log;
        public bool Log
        {
            get { return log; }
            set { log = value; }
        }

        private LogLocation logLocation = new LogLocation();
        public LogLocation LogLocation
        {
            get { return logLocation; }
            set { logLocation = value; }
        }
    }
    class LogLocation
    {
        private bool use;
        public bool Use
        {
            get { return use; }
            set { use = value; }
        }

        private String location;
        public String Location
        {
            get { return location; }
            set { location = value; }
        }
    }
    class Extensions
    {
        private bool distinguish;
        public bool Distinguish
        {
            get { return distinguish; }
            set { distinguish = value; }
        }

        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

    }
    class StartPoint
    {
        private List<Root> roots = new List<Root>();
        internal List<Root> Roots
        {
            get { return roots; }
            set { roots = value; }
        }

        public Root FindByType(string type)
        {
            foreach(Root root in Roots)
            {
                if (root.Type == type)
                    return root;
            }
            return null;
        }
    }
    class Root
    {
        private String type;
        public String Type
        {
            get { return type; }
            set { type = value; }
        }

        private bool value;
        public bool Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
    class Startup
    {
        private FileToOpen fileToOpen = new FileToOpen();
        public FileToOpen FileToOpen
        {
            get { return fileToOpen; }
            set { fileToOpen = value; }
        }
    }
    class FileToOpen
    {
        private int choice;
        public int Choice
        {
            get { return choice; }
            set { choice = value; }
        }
        

        private String value;
        public String Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
