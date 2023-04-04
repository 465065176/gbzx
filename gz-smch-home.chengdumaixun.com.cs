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
    /// 国家卫生健康委远程教育培训平台
    /// </summary>
    public class chengdumaixun
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if (
                (oSession.url.EndsWith("gz-smch-home.chengdumaixun.com/")) ||//chengdumaixun
                (oSession.url.IndexOf("/u/student/study/myCourse.action") > 0) ||//webtrn 个人中心
                (oSession.url.IndexOf("/u/student/training/getMyTrainingClass.action") >0) ||//webtrn 培训班列表
                (oSession.url.IndexOf("/u/student/class/getClassDetail.action") > 0) ||//webtrn 培训班课程列表
                (oSession.url.IndexOf("/learnspace/learn/learn/blue/index.action") > 0) ||//webtrn 播放页 //https://newnhc-kfkc.webtrn.cn/learnspace/learn/learn/blue/index.action?params.courseId=ff8080817d8f8ba6017d9026e96b02d9___&params.templateType=1&params.templateStyleType=0&params.template=blue&params.classId=&params.tplRoot=learn
                (oSession.url.IndexOf("/learnspace/learn/learn/blue/course_detail.action") > 0) ||//webtrn 播放简介 //https://newnhc-kfkc.webtrn.cn/learnspace/learn/learn/blue/course_detail.action?params.courseId=ff8080817d3381b6017d35c6d5600004___
                (oSession.url.IndexOf("/learnspace/course/study/learnRecord_stuLearnRecord.action?") > 0) ||//webtrn 播放记录 //https://newnhc-kfkc.webtrn.cn/learnspace/course/study/learnRecord_stuLearnRecord.action?courseId=ff8080817d3381b6017d35c6d5600004___&userId=ff8080817decc1f6017df25c31822456&isShowHistory=1
                (oSession.url.IndexOf("/content_video.action?") >0) ||//webtrn 播放子页 //https://hnssydw-kfkc.webtrn.cn/learnspace/learn/learn/templateeight/content_video.action?params.courseId=2c90a96d7a3944a9017a5d2291a100a4___&params.itemId=402880447a770d68017a7f0c21513c3d&params.templateStyleType=0&params.parentId=402880477a770e40017a7b1a32a31c5f&_t=1629089983339
                (oSession.url.IndexOf("/learnspace/resource/common/js/plugins/scrom/studyTime.js") > 0) ||//zxjxjy 禁止弹出30分钟暂停 https://hnssydw-kfkc.webtrn.cn/learnspace/resource/common/js/plugins/scrom/studyTime.js?=20210122
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
            if (oSession.url.EndsWith("gz-smch-home.chengdumaixun.com/"))
            {
                oSession.utilDecodeResponse();
                string online = $@"var username='{Form1.UserName}';
                                    var userpwd='{Form1.UserPwd}';
                                    localStorage.loginName=username;
                                    localStorage.pwd=userpwd;
                                    sessionStorage.visibleifChrome=1;
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
                            indexpagenext=0;
                            //localStorage.setItem(GetQueryString('id'),Number(localStorage.getItem(GetQueryString('id')))+1)
                            
                        ";
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                var list=[{name:'妊娠合并心脏病-周金年',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1639088403034861570'},
                                 {name:'前置性胎盘的手术技巧与输血治...',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1639090054051328001'},
                                 {name:'凶险性前置胎盘的介入治疗-李...',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1639089357176107010'},
                                {name:'2019版呼吸窘迫综合征管理...',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1639091708976885761'},
                                {name:'从规范指南到新生儿医院感染管...',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1639091087905320962'},
                                {name:'新生儿复苏关键点讲解及危机资...',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1639092534709514241'},
                                {name:'常见超声软指标的产诊断',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1641046548341010434'},
                                {name:'会阴切开裂伤与缝合',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1641047513760739329'},
                                {name:'产后出血的预防 - 李权',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1639095152571772929'},
                                {name:'妊娠期高血糖的诊治指南-胡琼',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1639096325567606785'},
                                {name:'常见胎儿畸形及遗传性疾病的咨...',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1639095823442309122'},
                                {name:' 肺泡表面活性物质的规范使用...',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1639098598486110210'},
                                {name:'新生儿败血症-杜书华',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1639098024994729985'},
                                {name:'早产儿无创通气—陈娟',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1639097594113880065'},
                                {name:'新生儿PS临床应用',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1640601737236246530'},
                                {name:'凶险型前置胎盘的处理',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1640608772224905217'},
                                {name:'高龄孕产妇管理及保障母婴安全',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1640602391702859778'},
                                {name:'妊娠期铁缺乏及缺铁性贫血的诊...',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1640599071194017794'},
                                {name:'新生儿常见病培训',url:'https://gz-smch-home.chengdumaixun.com/#/train/detail?id=1640592071877914626'},

                                ];
                                if(location.href.indexOf('/#/login-account')>0){
                                    if($('.tcaptcha-transform').length==0)
                                    $('.btn_do').click();
                                }else if(location.href.indexOf('/#/index')>0){
                                    $('.title:contains(""个人中心"")').click();
                                }
                                else if(location.href.indexOf('/#/train/detail')>0){
                                    if($('button:contains(""立即报名"")').length>0){
                                        $('button:contains(""立即报名"")').click();
                                    }else{
                                        var list=$('.lv1_row').next().not($('.lv1_row').next().find('s.icon-dui').parent().parent());
                                        if(list.length>0){
                                            list.eq(0).prev()[0].click();
                                            throw new Event('return');
                                        }else{
                                            open(localStorage.homeurl);
                                        }
                                    }
                                }
                                else if(location.href.indexOf('/#/me/index')>0){
                                    oldtimecount=0;
                                    localStorage.homeurl=location.href;
                                    $('.ul_px h4').each(function(i,e){
                                        console.log($(e).text());
                                        console.log($(e).parents('li').find('[role=""progressbar""]').attr('aria-valuenow'));
                                        localStorage.setItem($(e).text(),$(e).parents('li').find('[role=""progressbar""]').attr('aria-valuenow'))
                                    })
                                    if ($('.btn-next').length>0&&$('.btn-next').attr('disabled')==null&&indexpagenext<2){
                                        $('.btn-next').click();
                                        indexpagenext++;
                                        return false;
                                    }
                                    okcount=0;
                                    indexpagenext=0;
                                    list.forEach(function(e,i){
                                        var p=localStorage.getItem(e.name)
                                        if (p == '200')
                                        {
                                            
                                        }
                                        else if (p!= '100')
                                        {
                                            open(e.url);
                                            throw new Event('return');
                                        }
                                        else if (p == '100')
                                        {
                                            okcount++;
                                        }
                                        if(okcount==list.length){
                                            $('[placeholder=""推荐搜索""]').val('已学完');
                                        }
                                        console.log(okcount);
                                    })
                                }else if(location.href.indexOf('/#/train/media')>0){
                                    if(location.href=='https://gz-smch-home.chengdumaixun.com/#/train/media?id=1640609163620577282&trainId=1640608772224905217'&&document.querySelector('video').src!='https://smch-gz-media-v2.chengdumaixun.com/tv/8cae18e2-5ca7-78bc-d3b7-9dd42dcf8ac2.mp4?auth_key=1680266459-27002de1315540e29db3e7a0609d2e65-0-8d1dc065ccfe48d35c970d35e90c01f4'){
                                        document.querySelector('video').src='https://smch-gz-media-v2.chengdumaixun.com/tv/8cae18e2-5ca7-78bc-d3b7-9dd42dcf8ac2.mp4?auth_key=1680266459-27002de1315540e29db3e7a0609d2e65-0-8d1dc065ccfe48d35c970d35e90c01f4';
                                    }
                                    document.querySelector('video').paused&&document.querySelector('video').currentTime==0&&document.querySelector('video').play();
                                    (document.querySelector('video').currentTime<document.querySelector('video').duration-60)&&(document.querySelector('video').currentTime=document.querySelector('video').duration-10);
                                    var time=document.querySelector('video').currentTime;
                                    if(time==oldtime){
                                        if(oldtimecount>5)
                                           open(localStorage.homeurl);
                                        oldtimecount++;
                                    }else{oldtimecount=0}
                                    oldtime=time;
                                    if(document.querySelector('video').ended){
                                        open(localStorage.homeurl);
                                    }
                                }
                                errorcount =0;
                            }catch(e){
                                    if(errorcount>2)
                                        location.reload();
                                    errorcount++;
                            }
                           
                    ";

                oSession.utilPrependToResponseBody("<script type=\"text/javascript\">" + online + "function go(){" + go + "} setInterval(function(){go()},Math.round(Math.random()*5)*1000+2000);  </script>");
                oSession.utilPrependToResponseBody("<script type=\"text/javascript\" src='https://cdnjs.gtimg.com/cdnjs/libs/jquery/1.9.1/jquery.min.js'></script>");

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
            else if (oSession.url.IndexOf("/learnspace/resource/common/js/plugins/scrom/studyTime.js") > 0)
            {
                oSession.utilDecodeResponse();
                //var r = oSession.utilReplaceInResponse("if(r.clickAuth)", "if (false)");
                //r = oSession.utilReplaceInResponse("if(r.numAuth)", "if (false)");
                //r = oSession.utilReplaceInResponse("if(r.imageAuth)", "if (false)");
                var r = oSession.utilReplaceInResponse("window.onbeforeunload", "window.onbeforeunload=null;var myonbeforeunload");
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
