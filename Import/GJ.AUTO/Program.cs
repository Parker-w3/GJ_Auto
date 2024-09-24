using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace GJ.AUTO
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string proName = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.ModuleName);
            //Process[] curProcess = Process.GetProcessesByName(proName);
            //if (curProcess.Length > 1)
            //{
            //    MessageBox.Show(proName + "应用程序已打开", proName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WndFrmMain());
        }
    }
}
