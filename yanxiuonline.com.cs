using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;

namespace 贵州省干部在线学习助手
{
    public class yanxiuonline
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if
               (oSession.url.EndsWith(".com/") ||
                (oSession.url.IndexOf("/admin/lock/Study?") > 0) || //播放页
                (oSession.url.IndexOf("/js/player/adksplayer.js?") > 0)
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession) {
            if (oSession.url.EndsWith(".com/")) {
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("placeholder=\"请输入用户名\"", $@"placeholder=""请输入用户名"" value=""{WebHelper.Helper.UserName}""");
                r = oSession.utilReplaceInResponse("placeholder=\"请输入密码\"", $@"placeholder=""请输入密码"" value=""{WebHelper.Helper.UserPwd}""");
            }
            else if (oSession.url.IndexOf("/js/player/adksplayer.js?") > 0)
            {
                oSession.utilDecodeResponse();
                //bool r = oSession.utilReplaceInResponse("//CKobject.getObjectById(playerId).addListener('loadComplete','loadCompleteHandler');", "CKobject.getObjectById(playerId).addListener('loadComplete','loadCompleteHandler');");
            }
            else if (oSession.url.IndexOf("/admin/lock/Study?") > 0)
            {
                oSession.utilDecodeResponse();

                string js=@"
                ";
                string jsstr = @"
                            var i=0;
                            function jc(){
                                if($('.layui-layer-btn1').length>0){$('.layui-layer-btn1').click()}
                                if(window.player.getStatus()=='ready'||window.player.getStatus()=='pause'||window.player.getStatus()=='ended'){window.player.play()}
                                if($('.time').text().indexOf('不再加分')>0){
                                    open('/online/learn');
                                }
                                if($('.layui-layer-btn0').length>0){layer.closeAll();num_study=0;}
                            } 
                            
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},Math.round(Math.random()*10)*1000+30*1000);</script></body>");
                r = oSession.utilReplaceInResponse("id: \"video\",", "id: \"video\",\"useFlashPrism\": true,");//使用flash
                r = oSession.utilReplaceInResponse("value == value1", "value1 == value1");//直接设置为正确答案



            }
        }
    }
}
