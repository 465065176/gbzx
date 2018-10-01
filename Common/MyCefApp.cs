using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xilium.CefGlue;

namespace 贵州省干部在线学习助手.Common
{
    class MyCefApp:CefApp
    {
        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {
            //设置代理ip
            commandLine.AppendSwitch("proxy-server", "http://127.0.0.1:" + Process.GetCurrentProcess().Id);
            commandLine.AppendSwitch("ppapi-flash-version", "18.0.0.209");//PepperFlash\manifest.json中的version
            commandLine.AppendSwitch("ppapi-flash-path", "ppflash\\18_0_0_209\\pepflashplayer32_18_0_0_209.dll");
            commandLine.AppendArgument("disable-click-to-play");
            base.OnBeforeCommandLineProcessing(processType, commandLine);
        }
    }
}
