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
    /// 妇幼营养与健康在线培训及考核
    /// </summary>
    public class mchtraweb
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if (
                (oSession.url.IndexOf("/videoPlay/play?") > 0) || //https://mchtracourse.chinawch.org.cn/videoPlay/play?business_id=gp5&signId=16%2342095043142e4747aab13c529a1f46a7%2393bbb8b2-5995-4174-ac83-d3c54513cd66&courseId=48d5e88d29c648ab967a6a851a66e45c&sectionId=48d5e88d29c648ab967a6a851a66e45c1-1&ifPauseAd=0&ifPauseBlur=1&ifCourseComplete=0&ifDrag=0&proxy_url=https://mchtraweb.chinawch.org.cn/index.html%23/v_proxy%3FtrainplanId=42095043142e4747aab13c529a1f46a7%26courseId=48d5e88d29c648ab967a6a851a66e45c%26sectionId=48d5e88d29c648ab967a6a851a66e45c1-1&receive_course_record_url=https://mchtrasys.chinawch.org.cn/yunapi/course/update_learn_record&version=ckplayerx1&sub_domain_name=16&allow_study_same_time=0
                (oSession.url.IndexOf("/index.html") > 0) ||//https://mchtraweb.chinawch.org.cn/index.html#/v_training_system
                
                (oSession.url.IndexOf("/static/js/") > 0) //https://mchtraweb.chinawch.org.cn/static/js/44.b2683a4514b0fc292ff3.js
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession) {
            if (oSession.url.IndexOf("/videoPlay/play?") > 0) {
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
                            //localStorage.setItem(GetQueryString('id'),Number(localStorage.getItem(GetQueryString('id')))+1)
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                if($('video')[0].playbackRate!=10&&$('video')[0].currentTime<$('video')[0].duration-300){
                                    $('video')[0].playbackRate=10;
                                }else if($('video')[0].currentTime>$('video')[0].duration-180){
                                    $('video')[0].playbackRate=1;
                                }
                                if($('video')[0].currentTime<$('video')[0].duration&&$('video')[0].paused){
                                    $('video')[0].play();
                                }
                                if($('video')[0].currentTime==oldtime){
                                    oldtimecount++;
                                    if(oldtimecount>5)
                                    window.history.back();
                                }else{
                                    oldtimecount=0;
                                }
	                            oldtime=$('video')[0].currentTime;
                                if($('video')[0].ended==true&&oldtimecount>2){
                                    oldtimecount=0;
                                    window.history.back();
                                }
                                errorcount=0;
                            }catch(e){
                                    if(errorcount>2)
                                        location.reload();
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</html>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+5000);})  </script></html>");

                r = oSession.utilReplaceInResponse("player.videoPause();", "console.log('player.videoPause();')");

            }
            else if (oSession.url.IndexOf("/index.html") > 0)
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
                            //localStorage.setItem(GetQueryString('id'),Number(localStorage.getItem(GetQueryString('id')))+1)
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                if(location.href.indexOf('v_training_system')>0){

                                }else if(location.href.indexOf('v_trainplan_list')>0){
    
                                }else if(location.href.indexOf('v_selected_course')>0){
                                    localStorage.setItem('homeurl',location.href);
                                    var list=$('[role=""progressbar""]').not('[aria-valuenow=""100""]');
                                    if(list.length>0){
                                       list.eq(0).parent().parent().find('span.bg').click();
                                    }else{
    
                                    }
                                }else if(location.href.indexOf('v_cdcccourseDetails')>0){
                                    var list=$('a.button:contains(""未学习""),a.button:contains(""学习中"")');
                                    if(list.length>0){
                                        list.eq(0)[0].click();
                                    }else{
                                        open(localStorage.getItem('homeurl'));
                                    }
                                }
                                errorcount=0;
                            }catch(e){
                                    if(errorcount>2)
                                        location.reload();
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+5000);})  </script></body>");

            }
            else if (oSession.url.IndexOf("/static/js/") > 0)
            {
                oSession.utilDecodeResponse();
                
                var r = oSession.utilReplaceInResponse("pageSize:5,", "pageSize:20,");


            }
            else if (oSession.url == "hn.ischinese.cn/"||
                (oSession.url.IndexOf("/secondary") > 0) ||//ischinese 首页 https://hn.ischinese.cn/secondary
                (oSession.url.IndexOf("/learncenter/buycourse") > 0) ||//ischinese 课程列表 https://hn.ischinese.cn/learncenter/buycourse
                (oSession.url.IndexOf("/learncenter/play?") > 0))
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
                            //localStorage.setItem(GetQueryString('id'),Number(localStorage.getItem(GetQueryString('id')))+1)
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
	                                mytime++;
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
                                if(location.href.indexOf('/secondary')>-1){
                                    $('[href=""/learncenter/buycourse""]')[0].click();
                                }else if(location.href.indexOf('/learncenter/buycourse')>-1){
                                    oldtimecount=0;
                                    localStorage.homeurl=location.href;
                                    var list=$('.el-progress').not('[aria-valuenow=""100""]');
                                    if(list.length>0){
	                                    list.eq(0).parents('.buyCourse_listItem').find('.buyCourse_classStudy').click();
                                    }else{
	                                    var next=$('.btn-next');
	                                    if(next.attr('disabled')!='disabled'){
		                                    next.click();
	                                    }else{
    	                                    var prev=$('.cur .active').prev();
		                                    if(prev.length>0){
			                                    prev.click();
		                                    }
	                                    }
                                    }
                                }
                                else if(location.href.indexOf('/learncenter/play?')>-1){
                                    if($('video')[0].currentTime==oldtime){
                                        oldtimecount++;
                                        if(oldtimecount>5)
                                        open(localStorage.homeurl)
                                    }else{
                                        oldtimecount=0;
                                    }
	                                oldtime=$('video')[0].currentTime;
                                    if($('video')[0].ended==true&&oldtimecount>2){
                                        oldtimecount=0;
                                        if($('.class-catlog .active').next().length>0||$('.class-catlog .active').parent().parent().next().length){
		                                    $('.nextdontcheat').click();
	                                    }else{
                                            $('.el-message-box__wrapper button').click();
		                                    //$('[href=""/learncenter/buycourse""]')[0].click();
                                            open(localStorage.homeurl)
	                                    }
                                        ////旧方法
	                                    //if(!($('.el-message-box__wrapper').css('display')=='block'&&$('.el-message-box__wrapper .el-message-box__message').text()=='您正在观看最后一节，已无更多课件')){
		                                   // $('.nextdontcheat').click();
	                                    //}else{
                                     //       $('.el-message-box__wrapper button').click();
		                                   // //$('[href=""/learncenter/buycourse""]')[0].click();
                                     //       open(localStorage.homeurl)
	                                    //}
                                    }
                                }
                                
                                
                                errorcount=0;
                            }catch(e){
                                    if(errorcount>5)
                                        open(localStorage.homeurl)
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</html>", "<script type=\"text/javascript\" src='https://cdnjs.gtimg.com/cdnjs/libs/jquery/1.9.1/jquery.min.js'></script></html>");
                r = oSession.utilReplaceInResponse("</html>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);})  </script></html>");
                
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
            }
            else if (oSession.url.IndexOf("/static/js/vendor.") > 0) {
                oSession.utilDecodeResponse();
                var r = oSession.utilReplaceInResponse("initialize:function(){", "initialize:function(){this.$refs.video.autoplay=true;");//

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
