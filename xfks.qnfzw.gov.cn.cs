using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;

namespace 贵州省干部在线学习助手
{/// <summary>
/// 黔南法制网
/// </summary>
    public class qnfzw
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession)
        {
            if (
                (oSession.url.IndexOf("/exam_jm.html?") > 0) ||
                (oSession.url.IndexOf("/vedioplay?") > 0)
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession)
        {
            if (oSession.url.IndexOf("/vedioplay?") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                        function jc(){
                            var lista=$('.row-fluid').find('a');
                            lista[Math.round(Math.random()*lista.length)].click()
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},Math.round(Math.random()*50)*1000+30*1000);</script></body>");
            }
            else if (oSession.url.IndexOf("/exam_jm.html?") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                        function jc(){
                           nextShiti();
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + "</script></body>");
                r=oSession.utilReplaceInResponse("$('#shiti_item').html(shitiItemHtml);", "$('#shiti_item').html(shitiItemHtml);setTimeout(function(){$(\"[value = '\"+anwser+\"']\")[0].click();},1000);setTimeout(function(){jc()},Math.round(Math.random()*10)*1000+10*1000);");



            }
        }
    }
}
