using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;

namespace 贵州省干部在线学习助手
{/// <summary>
/// 乌鲁木齐人力资源培训网
/// </summary>
    public class wlmqpxw
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession)
        {
            if (
                (oSession.url.IndexOf("/index.php?xxjdID=") > 0) ||
                (oSession.url.IndexOf("/kcxx/kcinfo.php") > 0)||
                (oSession.url.IndexOf("/course/jsfile/left.js") > 0)||
                (oSession.url.IndexOf("/course/jsfile/menuinit.js") > 0) ||
                (oSession.url.IndexOf("/course/mediap2pnew.htm") > 0)
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession)
        {
            if (oSession.url.IndexOf("/kcxx/kcinfo.php") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                        function jc(){
                            xuexi();
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setTimeout(function(){jc()},Math.round(Math.random()*10)*1000+1*1000);</script></body>");
            } else if (oSession.url.IndexOf("/kcxx/index.php") > 0)
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
            else if (oSession.url.IndexOf("/course/jsfile/left.js") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                        function jc(){
                           
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + "</script></body>");
                r=oSession.utilReplaceInResponse("checkFlag=parent.document.hasFocus();", "checkFlag=true;");
                //r = oSession.utilReplaceInResponse("alert("本章节已经学完请点击下一章节学习");", "//这里添加点击一下章");
            }
            else if (oSession.url.IndexOf("/course/jsfile/menuinit.js") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                        function jc(){
                           
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + "</script></body>");
                r = oSession.utilReplaceInResponse("alert(\"本课程学习完成后请在'课程管理/课程测试'下进行考试\");", "");
                //r = oSession.utilReplaceInResponse("alert(\"学习结束！\");", "window.top.opener.top.location.reload(); window.top.opener=null; window.top.open('', '_self');window.top.close(); ");
            }
            else if (oSession.url.IndexOf("/course/mediap2pnew.htm") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            
                        ";
                string jsstr = @"
                        function jc(){
                           
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + "</script></body>");
                r = oSession.utilReplaceInResponse("alert(\"本课程结束\");", "window.top.opener.top.location.reload(); window.top.opener=null; window.top.open('', '_self');window.top.close(); ");
            }
        }
    }
}
