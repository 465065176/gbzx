using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;

namespace 贵州省干部在线学习助手
{
    public class xjjcjy
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if
               (oSession.url.EndsWith("/desktop-web/login.action") ||
                (oSession.url.IndexOf("/teachertraining/onlineteaching/lessonDetail_courseDetail.action") > 0) || //列表
                (oSession.url.IndexOf("/teachertraining/onlineteaching/lessonLearn_lessonMain.action") > 0)//播放页
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession) {
            if (oSession.url.EndsWith("/desktop-web/login.action")) {
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("placeholder=\"用户名/手机号\"", $@"value=""{WebHelper.Helper.UserName}""");
                r = oSession.utilReplaceInResponse("placeholder=\"请输入密码\"", $@"value=""{WebHelper.Helper.UserPwd}""");
                string js = @"
                ";
                string jsstr = @"
                            var i=0;
                            function jc(){
                                if($('.login-error').text()!=='用户不存在！'&&$('.login-error').text()!=='用户名或密码不正确！'&&$('.login-error').text()!=='请输入正确的手机号或密码！'){
                                    $('#loginBtn').click();
                                }
                            } 
                            
                        ";
                r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},Math.round(Math.random()*10)*1000+10*1000);</script></body>");
            }
            else if (oSession.url.IndexOf("/teachertraining/onlineteaching/lessonLearn_lessonMain.action") > 0)
            {
                oSession.utilDecodeResponse();

                string js = @"
                ";
                string jsstr = @"
                            var i=0;
                            function jc(){
                                $('object').remove();
                                if(i>=10){
                                    $('a:contains(""课程详情"")')[0].click();
                                }
                               i++;
                            } 
                            
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},Math.round(Math.random()*10)*1000+60*1000);</script></body>");
                r= oSession.utilReplaceInResponse("</object>", "</span>");
                r = oSession.utilReplaceInResponse("</object", "</span");
            }
            else if (oSession.url.IndexOf("/teachertraining/onlineteaching/lessonDetail_courseDetail.action") > 0)
            {
                oSession.utilDecodeResponse();

                string js=@"
                ";
                string jsstr = @"
                            var i=0;
                            function jc(){
                               $('.record-munu tr').each(function(i,e){
                                    console.info($(e).find('td:eq(3)').text())
    	                                if($(e).find('td:eq(3)').text()!='学习进度 100%'){
    		                                $(e).find('a')[0].click();return false;
                                        }
                                    })
                            } 
                            
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},Math.round(Math.random()*10)*1000+30*1000);</script></body>");
                



            }
        }
    }
}
