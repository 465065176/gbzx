using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace 贵州省干部在线学习助手
{
    /// <summary>
    /// 贵州省党员干部网络学院
    /// </summary>
    public class gzwy
    {
        public static string Yzm { get; set; }
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if (
                (oSession.url.IndexOf("/logins/views/login.html") > 0) || //gzwy https://gzwy.gov.cn/dsfa/nc/pc/logins/views/login.html
                (oSession.url.IndexOf("/dsfa/code/image?") > 0) ||// gzwy yzm https://gzwy.gov.cn/dsfa/code/image?t=1640617861261
                (oSession.url.IndexOf("/logins/scripts/login.js") > 0) || //gzwy https://gzwy.gov.cn/dsfa/nc/pc/logins/scripts/login.js
                (oSession.url.IndexOf("/pageclasscourse/getClassCourse?")>0) ||//hnsydwpx 课程列表
                (oSession.url.IndexOf("/main/views/main.html") > 0) ||//gzwy 主页 https://gzwy.gov.cn/dsfa/nc/pc/main/views/main.html
                (oSession.url.IndexOf("/mhkc/views/kclb.html?") > 0) ||//gzwy 课程列表 https://gzwy.gov.cn/dsfa/nc/kck/mhkc/views/kclb.html?foot=1
                (oSession.url.IndexOf("/main/views/mainClass.html?") > 0) ||//gzwy 个人中心 https://gzwy.gov.cn/dsfa/nc/pc/main/views/mainClass.html?defaultMenu=1
                (oSession.url.IndexOf("/course/views/course.html?") >0) ||//gzwy 播放页 https://gzwy.gov.cn/dsfa/nc/pc/course/views/course.html?id=dd7b9aa96eed409db6e5792734f07798&type=1&dwid=85c5a50576dd494f9e11144e6da460fe
                (oSession.url.EndsWith("/exam/")) ||
                (oSession.url.IndexOf("/center/myStudy/goods") > 0) ||
                (oSession.url.IndexOf("/course/scripts/course.js") > 0) || //gzwy https://gzwy.gov.cn/dsfa/nc/pc/course/scripts/course.js
                (oSession.url.IndexOf("/course/scripts/ckplayer/dCkPlayer.js") > 0) || //https://gzwy.gov.cn/dsfa/nc/pc/course/scripts/ckplayer/dCkPlayer.js
                (oSession.url.IndexOf("/center/qhdx/qhdxv1/js/states/home-state_") > 0) ||
                (oSession.url.IndexOf("/exam/js/modules/fighting/controllers/fighting-ctrl.js") > 0)||
                (oSession.url.IndexOf("/play/js/modules/home/controllers/main-ctrl_") > 0)
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
            else if ((oSession.url.IndexOf("getyzm.php") > 0))
            {
                oSession.utilCreateResponseAndBypassServer();
                oSession.oResponse.headers.SetStatus(200, "Ok");
                oSession.oResponse["Content-Type"] = "text/html; charset=UTF-8";
                oSession.oResponse["Cache-Control"] = "private, max-age=0";
                oSession.utilSetResponseBody(Yzm);
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession) {
            if (oSession.url.IndexOf("/logins/views/login.html") > 0)
            {
                oSession.utilDecodeResponse();
                string online = $@"var username='{Form1.UserName}';
                                    var userpwd='{Form1.UserPwd}';
                                ";
                online += @"
                            function GetQueryString(name)
                            {
                                 var reg = new RegExp(""(^|&)""+ name +""=([^&]*)(&|$)"");
                                 var r = window.location.search.substr(1).match(reg);
                                 if(r!=null)return  unescape(r[2]); return null;
                            }
                            function alert(str){
                                console.log(str);
                                if(str=='请您登录！'){
                                   open('/')
                                }else if(str=='请输入验证码'){
                                    location.reload();
                                }
                            }
                            function confirm(str){
                                console.log(str);return true;
                            }
                            var loginmessage='';
                            function mymessage(str){
                                loginmessage=str;
                                if(str.indexOf('登录失败')>=0){
                                    exampleA.UpLoginState(username,str);
                                }
                            }
                            okcount=0;
                            errorcount=0;
                            oldtime=0;
                            oldtimecount=0;
                            index=0;
                            //localStorage.setItem(GetQueryString('id'),Number(localStorage.getItem(GetQueryString('id')))+1)
                            
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                    if(loginmessage==''){
                                        $('#username').val(username);
                                        $('#password').val(userpwd);
                                         $.get('/getyzm.php',function(data,status){
                                            $('#vertifyCode').val(data);
                                            $('#LoginBtn').click();
                                        });
                                    }
                                errorcount =0;
                            }catch(e){
                                    if(errorcount>2)
                                        location.reload();
                                    errorcount++;
                            }
                           
                    ";

                oSession.utilPrependToResponseBody("<script type=\"text/javascript\">" + online + "function go(){" + go + "} setInterval(function(){go()},Math.round(Math.random()*5)*1000+1000);  </script>");

            }
            else if (oSession.url.IndexOf("/dsfa/code/image?") > 0)
            {
                oSession.utilDecodeResponse();
                try
                {
                    验证码识别.Form1 ocr = new 验证码识别.Form1();
                    Yzm = ocr.ocrZhiJie(oSession.ResponseBody).Trim(new char[] { '\n', ' ' }).Replace(" ", "");
                }
                catch (Exception exyzm)
                {
                    //MessageBox.Show(exyzm.Message);
                }
            }
            else if (oSession.url.IndexOf("/logins/scripts/login.js") > 0) {
                oSession.utilDecodeResponse();
                var r = oSession.utilReplaceInResponse("dsf.layer.message(res.message, false);", "dsf.layer.message(res.message, false);mymessage(res.message)");

            }
            else if (oSession.url.IndexOf("/pageclasscourse/getClassCourse?") > 0)
            {
                oSession.utilDecodeResponse();
                string online = @"
                            localStorage.setItem('homeurl',location.href)
                            function GetQueryString(name)
                            {
                                 var reg = new RegExp(""(^|&)""+ name +""=([^&]*)(&|$)"");
                                 var r = window.location.search.substr(1).match(reg);
                                 if(r!=null)return  unescape(r[2]); return null;
                            }
                            function alert(str){
                                console.log(str);
                                if(str=='请您登录！'){
                                   open('/')
                                }else if(str=='请输入验证码'){
                                    location.reload();
                                }
                            }
                            function confirm(str){
                                console.log(str);return true;
                            }    
                            okcount=0;
                            errorcount=0;
                            oldtime=0;
                            oldtimecount=0;
                            //localStorage.setItem(GetQueryString('id'),Number(localStorage.getItem(GetQueryString('id')))+1)
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                if($('span:contains(""学习中""),span:contains(""未学习"")').length>0){
                                    $('span:contains(""学习中""),span:contains(""未学习"")').eq(0).parent().next().find('a')[0].click()
                                }
                                else{
                                        $('a:contains(""下页"")')[0].click();
                                }
                                errorcount=0;
                            }catch(e){
                                    if(errorcount>2)
                                        location.reload();
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);})  </script></body>");

            }
            else if (oSession.url.IndexOf("/main/views/main.html") > 0)
            {
                oSession.utilDecodeResponse();
                string online = @"
                            function GetQueryString(name)
                            {
                                 var reg = new RegExp(""(^|&)""+ name +""=([^&]*)(&|$)"");
                                 var r = window.location.search.substr(1).match(reg);
                                 if(r!=null)return  unescape(r[2]); return null;
                            }
                            function alert(str){
                                console.log(str);
                                if(str=='请您登录！'){
                                   open('/')
                                }else if(str=='请输入验证码'){
                                    location.reload();
                                }
                            }
                            function confirm(str){
                                console.log(str);return true;
                            }    
                            okcount=0;
                            errorcount=0;
                            oldtime=0;
                            oldtimecount=0;
                            //localStorage.setItem('homeurl',location.href);
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                if($('iframe[src=""/dsfa/nc/pc/logins/views/login.html""]').length==0){
                                    if(!$('li:contains(""资源中心"")').hasClass('nav-item-sel')){
	                                    $('li:contains(""资源中心"")').click();
                                    }
                                }
                                errorcount=0;
                            }catch(e){
                                    if(errorcount>2)
                                        location.reload();
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);})  </script></body>");


            }
            else if (oSession.url.IndexOf("/mhkc/views/kclb.html?") > 0)
            {
                oSession.utilDecodeResponse();
                string online = @"
                            function GetQueryString(name)
                            {
                                 var reg = new RegExp(""(^|&)""+ name +""=([^&]*)(&|$)"");
                                 var r = window.location.search.substr(1).match(reg);
                                 if(r!=null)return  unescape(r[2]); return null;
                            }
                            function alert(str){
                                console.log(str);
                                if(str=='请您登录！'){
                                   open('/')
                                }else if(str=='请输入验证码'){
                                    location.reload();
                                }
                            }
                            function confirm(str){
                                console.log(str);return true;
                            }    
                            okcount=0;
                            errorcount=0;
                            oldtime=0;
                            oldtimecount=0;
                            //localStorage.setItem('homeurl',location.href);
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                    var isnext=true;
                                    $('span:contains(""马上学习"")').each(function(i,e){
                                        var count=localStorage.getItem($(e).attr('id'));
                                        if(count==null){
                                            count=0;
                                        }
                                        console.log(count);
                                        if(count<5){
                                            $(e).click();
                                            isnext=false;
                                            return false;
                                        }
                                    });
                                    if(isnext){
                                        $('.icon-right').click();
                                    }
                                errorcount=0;
                            }catch(e){
                                    if(errorcount>2)
                                        location.reload();
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);})  </script></body>");


            }

            else if (oSession.url.IndexOf("/main/views/mainClass.html?") > 0)
            {
                oSession.utilDecodeResponse();
                string online = @"
                            function GetQueryString(name)
                            {
                                 var reg = new RegExp(""(^|&)""+ name +""=([^&]*)(&|$)"");
                                 var r = window.location.search.substr(1).match(reg);
                                 if(r!=null)return  unescape(r[2]); return null;
                            }
                            function alert(str){
                                console.log(str);
                                if(str=='请您登录！'){
                                   open('/')
                                }else if(str=='请输入验证码'){
                                    location.reload();
                                }
                            }
                            function confirm(str){
                                console.log(str);return true;
                            }    
                            okcount=0;
                            errorcount=0;
                            oldtime=0;
                            oldtimecount=0;
                            localStorage.setItem('homeurl',location.href);
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                open('https://gzwy.gov.cn/dsfa/nc/pc/main/views/main.html');
                                errorcount=0;
                            }catch(e){
                                    if(errorcount>2)
                                        location.reload();
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);})  </script></body>");


            }
            else if (oSession.url.IndexOf("/course/views/course.html?") > 0)
            {
                oSession.utilDecodeResponse();
                string online = @"
                              
                            function GetQueryString(name)
                            {
                                 var reg = new RegExp(""(^|&)""+ name +""=([^&]*)(&|$)"");
                                 var r = window.location.search.substr(1).match(reg);
                                 if(r!=null)return  unescape(r[2]); return null;
                            }
                            function alert(str){
                                console.log(str);
                                if(str=='请您登录！'){
                                   open('/')
                                }else if(str=='请输入验证码'){
                                    location.reload();
                                }
                            }
                            function confirm(str){
                                console.log(str);return true;
                            }    
                            okcount=0;
                            errorcount=0;
                            oldtime=0;
                            oldtimecount=0;
                            localStorage.setItem(GetQueryString('id'),Number(localStorage.getItem(GetQueryString('id')))+1);
                                var myplayRate=2
                                $(function(){
                                    window.playbackRate([myplayRate])
                                });
                                function next(){
                                var timertimeHandler=null;
                                var mytime=0;
                                var timer=null;
                                if (timer) clearInterval(timer);
	                                timer = setInterval(function() {
                                    keepCourseProgress(true);
                                }, 1000 * 10);
                                if (timertimeHandler) clearInterval(timertimeHandler);
	                                timertimeHandler = setInterval(function() {
	                                if(window.kjjd!=null&&window.kjjd>=100){
		                                mytime=0;
                                        window.kjjd=0
		                                window.nextCourse();
	                                }
	                                mytime+=myplayRate;
                                    window.timeHandler(mytime);
                                }, 1000);
                            }
                            
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                if($('canvas.com-canvas[data-percent!=""100""]').length==0){
                                    localStorage.setItem(GetQueryString('id'),5);
                                    open(localStorage.homeurl);
                                }
                                errorcount =0;
                            }catch(e){
                                    if(errorcount>5)
                                        open(localStorage.homeurl);
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);})  </script></body>");
                oSession.utilReplaceInResponse("top.$.jBox.alert(\"你已学习完该课时！\",\"\");", "next();console.log('你已学习完该课时')");
            }
            else if (oSession.url.EndsWith("/exam/"))
            {
                oSession.utilDecodeResponse();
                string online = @"
                            function GetQueryString(name)
                            {
                                 var reg = new RegExp(""(^|&)""+ name +""=([^&]*)(&|$)"");
                                 var r = window.location.search.substr(1).match(reg);
                                 if(r!=null)return  unescape(r[2]); return null;
                            }
                            function alert(str){
                                console.log(str);
                                if(str=='请您登录！'){
                                   open('/')
                                }else if(str=='请输入验证码'){
                                    location.reload();
                                }
                            }
                            function confirm(str){
                                console.log(str);return true;
                            }    
                            okcount=0;
                            errorcount=0;
                            oldtime=0;
                            oldtimecount=0;
                            index=0;
                            //localStorage.setItem(GetQueryString('id'),Number(localStorage.getItem(GetQueryString('id')))+1)
                            
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                if(location.href.indexOf('/exam/#/fighting/')>0)
                                {
                                    if($('.exam-tit').length>index){
                                        var ee=$('.exam-tit').eq(index);
                                        var data=window.examinfo.answerQuestionDtos[index];
                                        console.log(data);
                                        if(data.questionType==1)
                                        {debugger;
    	                                    var truei=data.correct?0:1
    	                                    ee.next().find('input')[(truei)].click();
    	                                    console.info(data.correct)
                                        }else if(data.questionType==2)
                                         {
    	                                    var truei=data.correct=='A'?0:data.correct=='B'?1:data.correct=='C'?2:3;
    	                                    ee.next().find('input').eq(truei).click();
    	                                    console.info(data.correct)
                                        }else if(data.questionType==3)
                                         {
    	                                    $.each(data.correctAnswers,function(ii,e2){
    		                                    var truei=e2=='A'?0:e2=='B'?1:e2=='C'?2:3;
                                                ee.next().find('input').eq(truei).click();
                                                console.info(data.correct)
    
    
    	                                    })
    	
                                        }
                                    }else{
                                        if($('button[ng-click=""confirmCertain();""]').length<=0){
                                            $('a:contains(""我要交卷"")').trigger('click');
                                        }else{
                                            $('button[ng-click=""confirmCertain();""]').click();
                                        }
                                    }
                                    index++;
                                }
                                else if(location.href.indexOf('/exam/#/meeting/')>0){//
                                    history.back();
                                }
                                errorcount =0;
                            }catch(e){
                                    console.error(e);
                                    if(errorcount>2)
                                        open('https://qdjj.59iedu.com')
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);  </script></body>");
                oSession.utilPrependToResponseBody("<script type=\"text/javascript\">" + online + "function go(){" + go + "} /*setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);*/  </script>");
            }
            else if (oSession.url.IndexOf("/center/myStudy/goods") > 0)
            {
                oSession.utilDecodeResponse();
                string online = @"
                            localStorage.setItem('homeurl',location.href)
                            function GetQueryString(name)
                            {
                                 var reg = new RegExp(""(^|&)""+ name +""=([^&]*)(&|$)"");
                                 var r = window.location.search.substr(1).match(reg);
                                 if(r!=null)return  unescape(r[2]); return null;
                            }
                            function alert(str){
                                console.log(str);
                                if(str=='请您登录！'){
                                   open('/')
                                }else if(str=='请输入验证码'){
                                    location.reload();
                                }
                            }
                            function confirm(str){
                                console.log(str);return true;
                            }    
                            okcount=0;
                            errorcount=0;
                            oldtime=0;
                            oldtimecount=0;
                            //localStorage.setItem(GetQueryString('id'),Number(localStorage.getItem(GetQueryString('id')))+1)
                            
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                if(location.href.endsWith('/center/myStudy/goods'))
                                    $('a:contains(""进入学习"")').click();
                                else if(location.href.indexOf('/center/myStudy/goods/detail')>0){//https://qdjj.59iedu.com/center/myStudy/goods/detail?year=2020
                                    $('table tr[ng-repeat=""item in model.notPass.list""]').each(function(i,e){
                                        if ($(e).find('span:contains(""课程进度"")').text().match(/\d+/)[0] != '100'){
    		                                $(e).find('.i-play').click();
                                            return false;
                                        }else{
    		                                $(e).find('.i-write').click();
                                            return false;
                                        }
                                    })
                                }
                                errorcount =0;
                            }catch(e){
                                    if(errorcount>2)
                                        location.reload();
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);  </script></body>");
            }
            else if (oSession.url.IndexOf("/course/scripts/course.js") > 0)
            {
                oSession.utilDecodeResponse();
                var r = oSession.utilReplaceInResponse("hintTime = 5;", "hintTime = 5000;");
                r = oSession.utilReplaceInResponse("isAutoPlay = 0,", "isAutoPlay = 1,");
                r = oSession.utilReplaceInResponse("isLoop = 0,", "isLoop = 0,");
                r = oSession.utilReplaceInResponse("autoplay: false,", "autoplay: true,");
                r = oSession.utilReplaceInResponse("isAutoPlay = res.data.isAutoPlay;", "isAutoPlay = 1;");
                r = oSession.utilReplaceInResponse("isLoop = res.data.yxxhbf || 0;", "isLoop = 0;");
                r = oSession.utilReplaceInResponse("hintTime = res.data.hinttime || 5;", "hintTime = 500;");
                r = oSession.utilReplaceInResponse("window.onblur = function()", "window.onblur = function(){}\r\nvar myonblur=function()");
                r = oSession.utilReplaceInResponse("dsf.layer.alert(\"请至章节学习下一个课程\",", "window.nextCourse();var myalertttt=(");
                r = oSession.utilReplaceInResponse("playbackrate: isSpeed == 0 ? false : 1,", "playbackrate:2,flashplayer:true,");
                r = oSession.utilReplaceInResponse("html5m3u8: true,", "html5m3u8: false,");
                r = oSession.utilReplaceInResponse("keepCourseProgress(true);", "keepCourseProgress(true);\r\nif($('video')[0].playbackRate!=2||playRate!=2){$('video')[0].playbackRate=2;playRate=2;}\r\nif($('video').css('display')!='none'){$('video').css('display','none')}");
                r = oSession.utilReplaceInResponse("function formatSeconds(value) {", " window.formatSeconds=function(value) {");
                r = oSession.utilReplaceInResponse("function timeHandler(time) {", " window.timeHandler=function(time) {");
                r = oSession.utilReplaceInResponse("function keepCourseProgress(state) {", " window.keepCourseProgress=function(state) {");
                r = oSession.utilReplaceInResponse("function playbackRate(s) {", " window.playbackRate=function(s) {");
                r = oSession.utilReplaceInResponse("var p = res.kjjd ? Number(res.kjjd) : 0;", "var p = res.kjjd ? Number(res.kjjd) : 0;window.kjjd=p;");
                r = oSession.utilReplaceInResponse("window.loadedHandler = function() {", "window.loadedHandler = function() {next();\r\n");
                r = oSession.utilReplaceInResponse("var st = (sumPlayTime += 10);", "var st = (sumPlayTime += 10);\r\nif(myplayRate==2){st = (sumPlayTime += 10);}");
            }
            else if (oSession.url.IndexOf("/course/scripts/ckplayer/dCkPlayer.js") > 0)
            {
                oSession.utilDecodeResponse();
                var r = oSession.utilReplaceInResponse("this.embedSWF();", "this.embedSWF();this.playerLoad();");//


            }
            else if (oSession.url.IndexOf("/center/qhdx/qhdxv1/js/states/home-state_") > 0)
            {
                oSession.utilDecodeResponse();
                var r = oSession.utilReplaceInResponse("window.open(\"about:blank\",\"_blank\")", "window");
            }
            else if (oSession.url.IndexOf("/exam/js/modules/fighting/controllers/fighting-ctrl.js") > 0)
            {
                oSession.utilDecodeResponse();
                var r = oSession.utilReplaceInResponse("if(F(b.info)", "debugger;window.examinfo=b.info;if(F(b.info)");
            }
            else if (oSession.url.IndexOf("/play/js/modules/home/controllers/main-ctrl_") > 0)
            {
                oSession.utilDecodeResponse();
                var r = oSession.utilReplaceInResponse("window.open(\"about:blank\",\"_blank\")", "window");
            }
                Console.WriteLine(oSession.url);
                Console.WriteLine(oSession.url.IndexOf("/play/js/modules/home/controllers/main-ctrl_"));
        }
    }
}
