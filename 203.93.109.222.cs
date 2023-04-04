using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;

namespace 贵州省干部在线学习助手
{/// <summary>
/// 兴义市干部
/// </summary>
    public class cqjcxt
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession)
        {
            if (
                (oSession.url.IndexOf("/Page/studyoffice.aspx") > 0) ||//
                (oSession.url.IndexOf("/kcxx/kcinfo.php") > 0) ||//?
                (oSession.url.IndexOf("/kcxx/index.php") > 0) ||//?
                (oSession.url.IndexOf("content/media.php") > 0)//?
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession)
        {
            if (oSession.url.IndexOf("/Page/studyoffice.aspx") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            function go(){
                                subtime();
                            }
                            setInterval(function(){go()},1000*60*10+Math.round(Math.random()*10)*2000)
                        ";
                string jsstr = @"
                        function jc(){
                            start1();
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + js + "" + jsstr + " setTimeout(function(){jc()},Math.round(Math.random()*10)*1000+1*1000);</script></body>");
                r = oSession.utilReplaceInResponse("function pauseclock() { clearInterval(se); }", "function pauseclock() {  }");
                r = oSession.utilReplaceInResponse("confirm(\"确定继续学习吗？\")", "true");
                r = oSession.utilReplaceInResponse("alert(\"学时\" + sumtime + \"分钟\");", "$('.main_01_left_c ul ').append('<li>学时' + sumtime + '分钟<li>');");
                r = oSession.utilReplaceInResponse(" alert(a[0]);", "$('.main_01_left_c ul ').append('<li>' + a[0] + '<li>');");
            }
            else if (oSession.url.IndexOf("/kcxx/index.php") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                        function jc(){
                            document.querySelector(""#hwxzDiv"").querySelectorAll(""a"")[0].click()
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setTimeout(function(){jc()},Math.round(Math.random()*10)*1000+10*1000);</script></body>");
            }
            else if (oSession.url.IndexOf("/index.php?xxjdID=") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                        function jc(){
                           
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + "</script></body>");
                r=oSession.utilReplaceInResponse("document.hasFocus()","true");
                r = oSession.utilReplaceInResponse("continueStu=confirm(\"请按 [确定] 或 [取消] 继续学习？\");", "continueStu='';");
                r = oSession.utilReplaceInResponse("alert(\"学习结束！\");", "window.top.opener.top.location.reload(); window.top.opener=null; window.top.open('', '_self');window.top.close(); ");
            }
            else if (oSession.url.IndexOf("content/media.php") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            
                        ";
                string jsstr = @"
                        function jc(){
                           
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + "</script></body>");
                r = oSession.utilReplaceInResponse("document.hasFocus()", "true");
                r = oSession.utilReplaceInResponse("continueStu=confirm(\"请按 [确定] 或 [取消] 继续学习？\");", "continueStu='';");
                r = oSession.utilReplaceInResponse("alert(\"学习结束！\");", "window.top.opener.top.location.reload(); window.top.opener=null; window.top.open('', '_self');window.top.close(); ");
            }
        }
    }
}
