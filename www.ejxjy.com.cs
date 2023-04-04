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
    /// 湖南师范大学技术人员继续教育网
    /// </summary>
    public class ejxjy
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if (
                (oSession.url == "qdjj.59iedu.com/"|| oSession.url == "qdjj.59iedu.com/index") ||
                (oSession.url.EndsWith("/portal/person")) ||//ejxjy 课程列表
                (oSession.url == "zj.ejxjy.com/") || //zj.ejxjy
                (oSession.url.IndexOf("/myCourseDetail.html?")>0) ||//ejxjy 播放页
                (oSession.url.IndexOf("/personCenter/index")>0) ||//zj.ejxjy 课程列表
                (oSession.url.IndexOf("/personCenter/personCenterStudyDetail") > 0) ||//zj.ejxjy 播放页
                (oSession.url.IndexOf("/_nuxt/")>0) || //zj.ejxjy js https://zj.ejxjy.com/_nuxt/3a30346.js
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
            if (oSession.url == "qdjj.59iedu.com/" || oSession.url == "qdjj.59iedu.com/index")
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
                                if($('a:contains(""进入学习"")').length<=0)
                                {
                                        $('[name=""identify""]').val(username);
                                        $('[name=""passWord""]').val(userpwd);
                                }
                                else{//
                                    $('a:contains(""进入学习"")')[0].click();
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
            else if (oSession.url.EndsWith("/portal/person"))
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
                                if($('#con_a_2').find('a').length>0){
                                    $('#con_a_2').find('a').eq(0)[0].click();
                                }else{
                                    if(!$('#one2').hasClass('hover')){
                                        $('#one2').click();
                                    }else{
                                        alert('学习完成');
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
            else if (oSession.url == "zj.ejxjy.com/" || oSession.url.IndexOf("/personCenter/index") > 0 || oSession.url.IndexOf("/personCenter/personCenterStudyDetail") > 0)
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
                            playtime=0;
                            oldtime=0;
                            oldtimecount=0;
                            //localStorage.setItem(GetQueryString('id'),Number(localStorage.getItem(GetQueryString('id')))+1)
                            function next(){
                                if($('.catalog dd').not('.active,.hover ').length>0){
                                    $('.catalog dd').not('.active,.hover ').find('a')[0].click();
                                }else{
                                    open(localStorage.homeurl);
                                }
                            }
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                if(location.href.indexOf('')>0){
                                    
                                }
                                else if (location.href.indexOf('/personCenter/index') >0){
                                    localStorage.setItem('homeurl',location.href);
                                    playtime=0;
                                    if($('button:contains(""待学习""),button:contains(""学习中"")').length>0){
                                        $('button:contains(""待学习""),button:contains(""学习中"")')[0].click();
                                    }else{
                                        if(!$('#one2').hasClass('hover')){
                                            $('#one2').click();
                                        }else{
                                            alert('学习完成');
                                        }
                                    }
                                    errorcount=0;
                                }
                                else if (location.href.indexOf('/personCenter/personCenterStudyDetail')>0){
                                     if($('.tiny-dialog-body').text()=='课程学习完成，是否立即进入课后测验?')$('.tiny-dialog-certain').click();
                                    if($('video')[0].paused){$('video')[0].play()}
                                    if(playtime==0){
                                        $('video')[0].currentTime=$('video')[0].duration-300;
                                          $('video')[0].addEventListener('ended', function(e) {
                                              console.log('视频播放完了')
                                              playtime=0;
                                                location.reload();
                                            })
                                    }
                                    playtime++;
                                    if($('.courseMenuList .active .f-right').text().trim()=='100%'){
                                        var playnext=true;
	                                    $('.courseMenuList li').not('.active').each(function(i,e){
                                            if($(e).find('.f-right').text().trim()!='100%'){
                                                playtime=0
                                                playnext=false;
		                                        $(e).find('span').click();
		                                        return false;
                                            }
	                                    });
                                        if(playnext){
                                            open(localStorage.homeurl);
                                        }
                                    }
                                    if($('video')[0].currentTime==oldtime){
                                        if(oldtimecount>10)
                                            open(localStorage.homeurl);
                                        oldtimecount++;
                                    }else{oldtimecount=0}
                                    oldtime=$('video')[0].currentTime;
                                    errorcount=0;
                                }
                            }catch(e){
                                    if(errorcount>5)
                                        open(localStorage.homeurl)
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);})  </script></body>");
                oSession.utilReplaceInResponse("top.$.jBox.alert(\"你已学习完该课时！\",\"\");", "next();console.log('你已学习完该课时')");
            }
            else if (oSession.url.IndexOf("/myCourseDetail.html?") > 0)
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
                                if($('.catalog dd').not('.active,.hover ').length>0){
                                    $('.catalog dd').not('.active,.hover ').find('a')[0].click();
                                }else{
                                    open(localStorage.homeurl);
                                }
                            }
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                
                                if($('.tiny-dialog-body').text()=='课程学习完成，是否立即进入课后测验?')$('.tiny-dialog-certain').click();
        
                                if(player.getPosition()==oldtime){
                                    if(oldtimecount>10)
                                        open(localStorage.homeurl)
                                    oldtimecount++;
                                }else{oldtimecount=0}
                                oldtime=player.getPosition();
                                errorcount=0;
                            }catch(e){
                                    if(errorcount>5)
                                        open(localStorage.homeurl)
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + online + "function go(){" + go + "} $(function(){var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000);})  </script></body>");
                oSession.utilReplaceInResponse("top.$.jBox.alert(\"你已学习完该课时！\",\"\");", "next();console.log('你已学习完该课时')");
            }
            else if (oSession.url.IndexOf("/_nuxt/") > 0) {
                oSession.utilDecodeResponse();
                var r = oSession.utilReplaceInResponse("player.onComplete((function(r){", "player.onComplete((function(r){setTimeout(function(){location.reload()},5000);");


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
