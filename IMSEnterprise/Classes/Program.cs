using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace IMSEnterprise
{
    static class Program
    {
        
        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(args.Length == 0) // Start IMSEnterprise with GUI
                Application.Run(new MainForm());
            else // Start it as a CLI
            {
                if (!AttachConsole(-1))
                    AllocConsole();
                CommandLineInterface CLI = new CommandLineInterface(args);

                int exitCode = CLI.Run();
                
                // Bit of a hack to close the application. Something with attach console prevents it from exiting normally.
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                Environment.Exit(exitCode);
                FreeConsole();
                
            }
            
        }
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(Int32 a);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
    }
}
