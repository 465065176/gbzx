using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace 贵州省干部在线学习助手
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            CefRuntime.Shutdown();
        }
    }
}
