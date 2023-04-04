using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xilium.CefGlue;
using 贵州省干部在线学习助手.Renderer;

namespace WebHelper.Common
{
    class MyCefApp:CefApp
    {
        private CefRenderProcessHandler _renderProcessHandler = new DemoRenderProcessHandler();
        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {
            //设置代理ip
            commandLine.AppendSwitch("proxy-server", "http://127.0.0.1:" + WebHelper.Helper.Port);
            commandLine.AppendSwitch("ppapi-flash-version", "18.0.0.209");//PepperFlash\manifest.json中的version
            commandLine.AppendSwitch("ppapi-flash-path", "ppflash\\18_0_0_209\\pepflashplayer32_18_0_0_209.dll");
            commandLine.AppendSwitch("--ignore-urlfetcher-cert-requests", "1");//解决证书问题
            commandLine.AppendSwitch("--ignore-certificate-errors", "1");//解决证书问题
            commandLine.AppendSwitch("proxy-bypass-list", "media.ejxjy.com"); 
            base.OnBeforeCommandLineProcessing(processType, commandLine);
        }
        protected override CefRenderProcessHandler GetRenderProcessHandler()
        {
            return _renderProcessHandler;
        }
    }
}
