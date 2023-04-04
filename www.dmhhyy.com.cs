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
    /// 黔南智慧司法
    /// </summary>
    public class dmhhyy
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if (
                (oSession.url.IndexOf("/cms/login.htm")>0) ||//webtrn 登陆
                (oSession.url.IndexOf("/u/student/study/myCourse.action") > 0) ||//webtrn 个人中心
                (oSession.url.IndexOf("/u/student/training/getMyTrainingClass.action") >0) ||//webtrn 培训班列表
                (oSession.url.IndexOf("/u/student/class/getClassDetail.action") > 0) ||//webtrn 培训班课程列表
                (oSession.url.IndexOf("/learnspace/learn/learn/blue/index.action") > 0) ||//webtrn 播放页 //https://newnhc-kfkc.webtrn.cn/learnspace/learn/learn/blue/index.action?params.courseId=ff8080817d8f8ba6017d9026e96b02d9___&params.templateType=1&params.templateStyleType=0&params.template=blue&params.classId=&params.tplRoot=learn
                (oSession.url.IndexOf("/learnspace/learn/learn/blue/course_detail.action") > 0) ||//webtrn 播放简介 //https://newnhc-kfkc.webtrn.cn/learnspace/learn/learn/blue/course_detail.action?params.courseId=ff8080817d3381b6017d35c6d5600004___
                (oSession.url.IndexOf("/learnspace/course/study/learnRecord_stuLearnRecord.action?") > 0) ||//webtrn 播放记录 //https://newnhc-kfkc.webtrn.cn/learnspace/course/study/learnRecord_stuLearnRecord.action?courseId=ff8080817d3381b6017d35c6d5600004___&userId=ff8080817decc1f6017df25c31822456&isShowHistory=1
                (oSession.url.IndexOf("/content_video.action?") >0) ||//webtrn 播放子页 //https://hnssydw-kfkc.webtrn.cn/learnspace/learn/learn/templateeight/content_video.action?params.courseId=2c90a96d7a3944a9017a5d2291a100a4___&params.itemId=402880447a770d68017a7f0c21513c3d&params.templateStyleType=0&params.parentId=402880477a770e40017a7b1a32a31c5f&_t=1629089983339
                (oSession.url.IndexOf("/h5/static/js/") > 0) ||//dmhhyy 自动答题 设置答案 https://www.dmhhyy.com:8082/h5/static/js/3.00cf22cfdd3cb2871ef0.js
                (oSession.url.EndsWith("/exam/")) ||
                (oSession.url.IndexOf("/center/myStudy/goods") > 0) ||
                (oSession.url.IndexOf("/qhdx/qhdxv1/js/modules/myStudy/controllers/goodsDetail-ctrl_") > 0)||
                (oSession.url.IndexOf("/center/qhdx/qhdxv1/js/states/home-state_") > 0) ||
                (oSession.url.IndexOf("/exam/js/modules/fighting/controllers/fighting-ctrl.js") > 0)||
                (oSession.url.IndexOf("/play/js/modules/home/controllers/main-ctrl_") > 0)
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession) {
            if (oSession.url.IndexOf("/cms/login.htm") > 0)
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
                                if($('#isLoginBox').css('display')=='none')
                                {
                                        $('[name=""username""]').val(username);
                                        $('#pwd1').val(userpwd);
                                        $('#doSubmitBtn')[0].click();
                                }
                                else{//
                                    $('a:contains(""进入个人中心"")')[0].click();
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
            else if (oSession.url.IndexOf("/u/student/study/myCourse.action") > 0)
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
                                $('a:contains(""我的网络班级"")')[0].click();
                                errorcount =0;
                            }catch(e){
                                    if(errorcount>2)
                                        location.reload();
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);})  </script></body>");

            }
            else if (oSession.url.IndexOf("/u/student/training/getMyTrainingClass.action") > 0)
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
                                $('a:contains(""进入班级"")')[0].click();
                                errorcount =0;
                            }catch(e){
                                    if(errorcount>2)
                                        location.reload();
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);})  </script></body>");

            }
            else if (oSession.url.IndexOf("/u/student/class/getClassDetail.action") > 0)
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
                                $('.courseLi').each(function(i,e){
                                    if($(e).find('.color-theme').text()!='100%'){
                                        $(e).find('.courseLi-btn a')[0].click();
                                        throw new Error('结束')
                                    }
                                });
                                if($('.icon-dArrright').length>0){
                                    $('.icon-dArrright')[0].click();
                                }
                                errorcount =0;
                            }catch(e){
                                    if(errorcount>2)
                                        location.reload();
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);})  </script></body>");

            }
            else if (oSession.url.IndexOf("/learnspace/learn/learn/blue/index.action") > 0)
            {
                oSession.utilDecodeResponse(); string online = @"
                              
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
                online += $@"localStorage.homeurl='{Form1.startUrl}';";
                    
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                    
                                    errorcount=0;
                            }catch(e){
                                    if(errorcount>5)
                                        open(localStorage.homeurl)
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("<head>", "<head><script type=\"text/javascript\">" + online + "function go(){" + go + "} var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000)  </script>");
            }else if (oSession.url.IndexOf("/learnspace/learn/learn/blue/course_detail.action") > 0) {
                oSession.utilDecodeResponse(); string online = @"
                              
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
                online += $@"localStorage.homeurl='{Form1.startUrl}';";

                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                    window.parent.$('#course_score a')[0].click();
                                    errorcount=0;
                            }catch(e){
                                    if(errorcount>5)
                                        open(localStorage.homeurl)
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("<head>", "<head><script type=\"text/javascript\">" + online + "function go(){" + go + "} var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000)  </script>");

            }
            else if (oSession.url.IndexOf("/learnspace/course/study/learnRecord_stuLearnRecord.action?") > 0) {
                oSession.utilDecodeResponse(); string online = @"
                              
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
                online += $@"localStorage.homeurl='{Form1.startUrl}';";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                  if($('[value=""未完成""]').length>0){
	                                    $('[value=""未完成""]').eq(0).click();
                                    }else{
	                                    open(localStorage.homeurl);
                                    }  
                                    errorcount=0;
                            }catch(e){
                                    if(errorcount>5)
                                        open(localStorage.homeurl);
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("<head>", "<head><script type=\"text/javascript\">" + online + "function go(){" + go + "} var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000)  </script>");


            }
            else if (oSession.url.IndexOf("/content_video.action?") > 0)
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
                                    var video=$('video');
                                jwplayer('container').getPlaybackRate()<=1&&jwplayer('container').setPlaybackRate(2);
                                if(window.parent.$('.select span').hasClass('done')){
                                    window.parent.parent.$('#course_score a')[0].click();
                                }
                                if (video[0].ended&&video[0].paused){
                                    window.parent.parent.$('#course_score a')[0].click();
                                }  
                                var time=video[0].currentTime;
                                if(time==oldtime){
                                    if(oldtimecount>5)
                                       open(localStorage.homeurl);
                                    oldtimecount++;
                                }else{oldtimecount=0}
                                oldtime=time;
                                errorcount=0;
                            }catch(e){
                                    if(errorcount>5)
                                        open(localStorage.homeurl);
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("<head>", "<head><script type=\"text/javascript\">" + online + "function go(){" + go + "} var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000)  </script>");
            }
            else if (oSession.url.IndexOf("/h5/static/js/") > 0)
            {
                oSession.utilDecodeResponse();
                //var r = oSession.utilReplaceInResponse("if(r.clickAuth)", "if (false)");
                //r = oSession.utilReplaceInResponse("if(r.numAuth)", "if (false)");
                //r = oSession.utilReplaceInResponse("if(r.imageAuth)", "if (false)");
                var r = oSession.utilReplaceInResponse("if(n.optionList.map(function(e){e.select", "if(n.optionList.map(function(e){e.isCorrect==1");
                oSession.utilReplaceInResponse(@"select?""bgc-select""",@"isCorrect==1?""bgc-select""");//设置正确答案
                oSession.utilReplaceInResponse(@"onNextTap:function(){", @"onNextTap:function(){this.next=true;");//设置默认可以点击下一页
                oSession.utilReplaceInResponse(@"next:!1,", @"next:!0,");//设置默认可以点击下一页
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
            else if (oSession.url.IndexOf("/qhdx/qhdxv1/js/modules/myStudy/controllers/goodsDetail-ctrl_") > 0)
            {
                oSession.utilDecodeResponse();
                var r = oSession.utilReplaceInResponse("window.open(\"about:blank\",\"_blank\")", "window");
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
