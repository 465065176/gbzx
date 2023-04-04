using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;
using System.Text.RegularExpressions;

namespace 贵州省干部在线学习助手
{/// <summary>
/// {/// <summary>
/// 贵州工程应用技术学院继续教育
/// </summary>
/// </summary>
    public class ggcjxjy
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if (
                (oSession.url.IndexOf("/playVideo.js") > 0) ||
                (Regex.IsMatch(oSession.url,@"/course/study/\d+\.html",RegexOptions.Singleline))||
                (oSession.url.IndexOf("/richvideo/initdatawithviewer?") > 0)
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession) {
            if (oSession.url.IndexOf("/playVideo.js") > 0)
            {
                oSession.utilDecodeResponse();
                //bool r = oSession.utilReplaceInResponse("e.pause()", "");
                //r = oSession.utilReplaceInResponse("preload:\"auto\",", "preload:\"auto\",autoplay:true,");
                //r = oSession.utilReplaceInResponse("preload:\"none\",", "preload:\"auto\",autoplay:true,");
                //r = oSession.utilReplaceInResponse("g.sendDataLog(\"ended\")", "g.sendDataLog(\"ended\");setInterval(function(){parent.parent.next()},Math.round(Math.random()*10)*1000+30*1000);");
            }
            else if (Regex.IsMatch(oSession.url, @"/course/study/\d+\.html", RegexOptions.Singleline))
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                            var i=0;
                            function next(){
                                var list=$('.pl-v-ej-name');
                                for(var i=0;i<list.length;i++){
                                    if(document.title==list.eq(i).text()){
                                        list.eq(i+1)[0].click();
                                        break;
                                    }
                                }
                            }
                            function nextold(){
                                if($('.currents').parent().parent().next().children()[0].tagName=='A'){
                                    $('.currents').parent().parent().next().children().children().click();
                                }else if($('.currents').parent().parent().next().children()[0].tagName=='H3'){
                                    $('.currents').parent().parent().next().children().eq(1).children().children().click();
                                }
                            }
                            var t=Math.random()*2000;
                            function jc(){
                                if(player.getCurrentTime()>600+t){
                                    next();
                                }
                            } 
                            
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},Math.round(Math.random()*50)*1000+30*1000);</script></body>");
                r = oSession.utilReplaceInResponse("dialog('提示',\"恭喜你已经学完本课程！\",1);", "next();dialog('提示',\"恭喜你已经学完本课程！\",1);");
                r = oSession.utilReplaceInResponse("autoplay:false,", "autoplay:true,");

            }
            else if (oSession.url.IndexOf("/richvideo/initdatawithviewer?") > 0) {
                oSession.utilSetResponseBody("[]");
            }
        }
    }
}
