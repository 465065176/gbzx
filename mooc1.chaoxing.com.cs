using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;

namespace 贵州省干部在线学习助手
{
    public class chaoxing
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if (
                (oSession.url.IndexOf("/videojs-ext.min.js") > 0) ||
                (oSession.url.IndexOf("/mycourse/studentstudy?") > 0)||
                (oSession.url.IndexOf("/richvideo/initdatawithviewer?") > 0)
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession) {
            if (oSession.url.IndexOf("/videojs-ext.min.js") > 0)
            {
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("e.pause()", "");
                r = oSession.utilReplaceInResponse("preload:\"auto\",", "preload:\"auto\",autoplay:true,");
                r = oSession.utilReplaceInResponse("preload:\"none\",", "preload:\"auto\",autoplay:true,");
                r = oSession.utilReplaceInResponse("g.sendDataLog(\"ended\")", "g.sendDataLog(\"ended\");setInterval(function(){parent.parent.next()},Math.round(Math.random()*10)*1000+30*1000);");
            }
            else if (oSession.url.IndexOf("/mycourse/studentstudy?") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                            var i=0;
                            function next(){
                                for(var i=0;i<$('.onetoone a').length;i++){
                                    if($('.currents a').attr('title')==$('.onetoone a').eq(i).attr('title')){
                                        $('h4 a').eq(i+1)[0].click();
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
                            function jc(){
                                if($('#btn_code').attr('onclick')=='answer()'){
                                    if(i%2==0){
                                       // $('.neiinput').find(""[correct='1']"").click();
                                    }else{
                      //                  $('#btn_code').click();
                                    }
                                    i++;
                                }else{
                                    $('.next').click();
                                }
                            } 
                            
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},Math.round(Math.random()*50)*1000+30*1000);</script></body>");

            }
            else if (oSession.url.IndexOf("/richvideo/initdatawithviewer?") > 0) {
                oSession.utilSetResponseBody("[]");
            }
        }
    }
}
