using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;

namespace 贵州省干部在线学习助手
{/// <summary>
/// 重庆市江北执法
/// </summary>
    public class cqjbzf
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession)
        {
            if (
                (oSession.url.IndexOf("/videoLearning2.jsp") > 0) || //ok
                (oSession.url.IndexOf("/kcxx/kcinfo.php") > 0) ||//?
                (oSession.url.IndexOf("/jbzf/index.jsp") > 0) ||// jbzf
                (oSession.url.IndexOf("content/media.php") > 0)//?
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession)
        {
            if (oSession.url.IndexOf("/videoLearning2.jsp") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            function go(){
                                //subtime();
                            }
                            setInterval(function(){go()},1000*60*10+Math.round(Math.random()*10)*2000)
                        ";
                string jsstr = @"
                        function jc(){
                            if($('#questionBox').css('display')=='block'){valiAnswer();}
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + js + "" + jsstr + " setInterval(function(){jc()},Math.round(Math.random()*10)*1000+1*1000);</script></body>");
                r = oSession.utilReplaceInResponse("getPlayer().pause();//暂停视频", "//getPlayer().pause();//暂停视频");
                r = oSession.utilReplaceInResponse("$('#alertLeaveBrowserBox').modal('show');", "//$('#alertLeaveBrowserBox').modal('show');");
                r = oSession.utilReplaceInResponse("$(\"#inpAnswer\").val();", "1");
                r = oSession.utilReplaceInResponse("json.data == answer", "true");
                r = oSession.utilReplaceInResponse("alert(\"视频加载错误，将重新加载页面\");", "console.log(\"视频加载错误，将重新加载页面\");"); 
            }
            else if (oSession.url.IndexOf("/jbzf/index.jsp") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                        function jc(){
                            $('[href=""training.jsp""]')[0].click();
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
