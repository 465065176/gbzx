using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;

namespace 贵州省干部在线学习助手
{/// <summary>
/// 中国教师研修网
/// </summary>
    public class yanxiu
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession)
        {
            if (
                (oSession.url.IndexOf("/task-center/course/getUserCoursePage?") > 0) || //yanxiu https://ipx-api.yanxiu.com/task-center/course/getUserCoursePage?courseId=5675364940211609623&projectId=5647525133997629471&toolId=5675353326620041256&courseSourceId=22729&isMember=1
                (oSession.url.IndexOf("/train-project-center/trainProject/detail?") > 0) || //yanxiu https://ipx-api.yanxiu.com/train-project-center/trainProject/detail?id=6289627417674080335
                (oSession.url.IndexOf("/detail?") > 0)|| //yanxiu
                (oSession.url.IndexOf("/static/spring-grain/js/") > 0)|| //https://d1.3ren.cn/static/spring-grain/js/5.669f2d2643035d2c7316.js
                (oSession.url.IndexOf("static/spring-cms-web/js/") > 0)//https://d1.3ren.cn/static/spring-cms-web/js/app.80a3898f0c64e0a4aab2.js
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession)
        {
            if (oSession.url.IndexOf("/train-project-center/trainProject/detail?") > 0) {
                oSession.utilDecodeResponse();
                
                bool r = oSession.utilReplaceInResponse("\"indexPage\":0,","\"indexPage\":1,");

            }
            else if (oSession.url.IndexOf("/detail?") > 0)//
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                        function jc(){
                                if($('.alarmClock-wrapper').css('display')!='none'){
                                    $('.alarmClock-wrapper').click();
                                }
                                if($('.scoring-wrapper').css('display')!='none'){    
                                    if($('.commit').length>0){
                                        $('.rate-item').click();
                                        $('.commit button').click();
                                    }
                                }
                            }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},Math.round(Math.random()*10)*1000+30*1000);</script></body>");
                r = oSession.utilReplaceInResponse("</head>", "<script type=\"text/javascript\" src='https://cdnjs.gtimg.com/cdnjs/libs/jquery/1.9.1/jquery.min.js'></script></head>");
            }
            else if (oSession.url.IndexOf("/task-center/course/getUserCoursePage?") > 0)
            {
                oSession.utilDecodeResponse();

                bool r = oSession.utilReplaceInResponse("\"sgmd\":\"3\"", "\"sgmd\":\"1\",\"istanscode\":1");
                r = oSession.utilReplaceInResponse("\"sgmd\":\"5\"", "\"sgmd\":\"1\",\"istanscode\":1");
                r = oSession.utilReplaceInResponse("\"duration\":", "\"duration\":1");
                r = oSession.utilReplaceInResponse("\"totalDuration\":", "\"totalDuration\":1");
                r = oSession.utilReplaceInResponse("\"videoDuration\":", "\"videoDuration\":1");
                r = oSession.utilReplaceInResponse("\"reso\":\"s\"", "\"reso\":\"s\",\"url2\":\"https://preview.yanxiu.com/?url=http%3A%2F%2Fugcqiniu.jsyxw.cn%2F%3Fprefix%3D20210120%2F18%2Fb670d2862cb72b26715888da4ee70ab8%26pageCount%3D2\"");


            }
            else if (oSession.url.IndexOf("/static/spring-grain/js/") > 0)
            {
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("this.isEnableAlarmClock = A,", "this.isEnableAlarmClock = false,");//是否开启防挂机
                r = oSession.utilReplaceInResponse("this.isEnableScoring = l,", "this.isEnableScoring = l,");//是否开启评价
                r = oSession.utilReplaceInResponse("if (isTaskCompleted)", "if (false)"); //若超出课程总时长，则取消心跳、计时等操作
                r = oSession.utilReplaceInResponse("heartbeatPause:function()", "heartbeatPause:function(){console.log('my暂停')},heartbeatPause: function()"); //暂停
                r = oSession.utilReplaceInResponse("n.popupAlarmClock()", "console.log('n.popupAlarmClock()')"); //防挂机
                r = oSession.utilReplaceInResponse("this.popupScoring()", "console.log('this.popupScoring()')"); //触发评价
                


            }
            else if (oSession.url.IndexOf("static/spring-cms-web/js/") > 0) {
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("isShowAll:0", "isShowAll:1"); //显示全部作业
                r = oSession.utilReplaceInResponse("isShowAll:!1", "isShowAll:1"); //显示全部作业
            }


        }
    }
}
