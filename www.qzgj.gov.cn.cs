using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;

namespace 贵州省干部在线学习助手
{
    public class qzgj
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if (
                (oSession.url.IndexOf("selfmade/play/player.htm") > 0) ||
                (oSession.url.IndexOf("/index.php") > 0)
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession) {
            if (oSession.url.IndexOf("/linkAICC.js") > 0)
            {
                oSession.utilDecodeResponse();
                oSession.ResponseHeaders["Content-Type"] = "application/javascript; charset=GB2312";
                bool r = oSession.utilReplaceInResponse("setInterval(\"doGetInter(\" + flag + \")\", 200)", "setInterval(\"doGetInter(\" + flag + \")\", 2000)");
            }
            else if (oSession.url.IndexOf("/index.php") > 0)
            {
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("continueStu=confirm(\"请", "continueStu=true;var b=(\"");
                r = oSession.utilReplaceInResponse("document.hasFocus()", "true");
                Console.WriteLine(r + "\r\n");
                r = oSession.utilReplaceInResponse("alert(\"请点击确认继续\");", "");
                Console.WriteLine(r + "\r\n");
                //string html= oSession.GetResponseBodyAsString();
                //oSession.utilSetResponseBody(oSession.GetResponseBodyAsString());
                string js = @"
                            jwplayer(""container"").onPlaylistComplete(function(){
                                    window.open('http://www.gzgbonline.cn/elms/web/lanmu02.jsp');
                                });

                        ";
                string jsstr = @"
                            function jc(){
                                if(jwplayer(""container"").getState()==undefined){
                                    window.open('http://www.gzgbonline.cn/elms/web/lanmu02.jsp');
                                }
                            } 
                            
                        ";
                //r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},20000);</script></body>");
                //r = oSession.utilReplaceInResponse("onReady(e);", "onReady(e);go();");

            }
        }
    }
}
