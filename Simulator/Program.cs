﻿using System;
using System.Windows.Forms;
using System.Threading;
using Simulator.GUI.Forms;

namespace Simulator
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
            using (Mutex mutex = new Mutex(false, "Global\\" + GetGuid()))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("An instance of Open Exam Simulator is already running, select the add button include more exams.","OES Simulator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Application.Run(args.Length == 0 ? new Ui() : new Ui(args[0]));
            }
        }

        static string GetGuid ()
        {
            Guid assemblyGuid = Guid.Empty;
            object[] assemblyObjects = System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), true);
            if (assemblyObjects.Length > 0)
            {
                assemblyGuid = new Guid(((System.Runtime.InteropServices.GuidAttribute)assemblyObjects[0]).Value);
            }
            return assemblyGuid.ToString();
        }
    }
}
