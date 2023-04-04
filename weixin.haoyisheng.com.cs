using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data;

namespace 贵州省干部在线学习助手
{
    /// <summary>
    /// haoyisheng
    /// </summary>
    public class haoyisheng
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if (
                (oSession.url == "qdjj.59iedu.com/"|| oSession.url == "qdjj.59iedu.com/index") ||
                (oSession.url.IndexOf("/appapi/app/loginnew") > 0) ||//https://appapi.haoyisheng.com/appapi/app/loginnew
                (oSession.url.IndexOf("/wx/getCourseInfo?") >0) ||//https://weixin.haoyisheng.com/wx/getCourseInfo?token=www000487&course_id=202100925665&version=6.0.17
                (oSession.url.IndexOf("/wx/getTests?") > 0) ||//https://weixin.haoyisheng.com/wx/getTests?test_id=202100928094-01&type=0&project=cme
                (oSession.url.IndexOf("/courseware_index.action?") > 0) ||//webtrn 播放页 //https://hnssydw-kfkc.webtrn.cn/learnspace/learn/learn/templateeight/courseware_index.action?params.courseId=2c90a96d7a3944a9017a5d2291a100a4___
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
            else if (oSession.url.IndexOf("/appapi/app/loginnew") > 0) {
                oSession.utilDecodeResponse();
                var r = oSession.utilReplaceInResponse("\"phoneVerify\":false,", "\"phoneVerify\":true,");//修改手机号是否验证
            }
            else if (oSession.url.IndexOf("/wx/getCourseInfo?") > 0)
            {
                oSession.utilDecodeResponse();
                var r = oSession.utilReplaceInResponse("\"study_status\":\"0\",", "\"study_status\":\"1\",");
                r = oSession.utilReplaceInResponse("\"face\":\"1\",", "\"face\":\"0\",");

            }
            else if (oSession.url.IndexOf("/wx/getTests?") > 0)
            {
                oSession.utilDecodeResponse();
                try
                {
                    dynamic dy = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(oSession.GetResponseBodyAsString());
                    if (dy.status.Value == 0)
                    {
                        string answers = "";
                        for (int i = 0; i < dy.tests.Count; i++)
                        {
                            try
                            {
                                dynamic d = dy.tests[i];

                                Regex regex = new Regex("\\d+\\.(?<title>.*?)$");
                                string title = d.questionTitle.Value;
                                //string item = d.config.Value.Replace("${answer}", "★");
                                //int type = (int)d.questionStreamType.Value;
                                string answer = d.answer.Value;
                                answers += answer;
                                dy.tests[i].questionTitle.Value = d.questionTitle.Value + "【" + answer + "】";
                                char[] answerc = answer.ToCharArray();
                                foreach (char c in answerc) {
                                    int index = c - 65;
                                    dy.tests[i].options[index].optionContent.Value +="====[对]";


                                }
                                //if (type == 0)
                                //{
                                //    type = (int)QuestionType.Single;
                                //}
                                //DataTable dt = ExamsDAL.getExams(title);
                                //if (dt.Rows.Count == 0)
                                //{
                                //    ExamsDAL.AddExam(0, title, item, type, answer, true, new DateTime());
                                //}
                                //else if (dt.Rows.Count > 0 && !Convert.ToBoolean(dt.Rows[0]["ExamAnswerVerify"]))
                                //{
                                //    ExamsDAL.UpExam(Convert.ToInt32(dt.Rows[0]["ID"]), answer, true);
                                //}
                            }
                            catch (Exception e)
                            {

                            }
                        }
                        //MessageBox.Show(answers);
                    }
                    oSession.utilSetResponseBody(Newtonsoft.Json.JsonConvert.SerializeObject(dy));
                }
                catch { }
            }
            else if (oSession.url.IndexOf("/courseware_index.action?") > 0)
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
                string go = @"
                            try{
                                
                            }catch(e){}
                        ";

                go = @"
                            try{
                                    if($('.s_pointerct').find('.item_done_icon').hasClass('done_icon_show')){
                                        var list=$('.item_done_icon').not('.done_icon_show').parent();
                                        if(list.length>0){
                                            list.eq(0).click();
                                        }else{
                                            open('https://hn.webtrn.cn/u/student/study/myCourse.action')
                                        }
                                        
                                    }
                                    errorcount=0;
                            }catch(e){
                                    if(errorcount>5)
                                        open('https://hn.webtrn.cn/u/student/study/myCourse.action')
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
                                    
                                var time=video[0].currentTime;
                                if(time==oldtime){
                                    if(oldtimecount>5)
                                        open('https://hn.webtrn.cn/u/student/study/myCourse.action')
                                    oldtimecount++;
                                }else{oldtimecount=0}
                                oldtime=time;
                                errorcount=0;
                            }catch(e){
                                    if(errorcount>5)
                                        open('https://hn.webtrn.cn/u/student/study/myCourse.action')
                                    errorcount++;
                            }
                           
                    ";
                var r = oSession.utilReplaceInResponse("<head>", "<head><script type=\"text/javascript\">" + online + "function go(){" + go + "} var mysetInterval= setInterval(function(){go()},Math.round(Math.random()*8)*1000+10000)  </script>");
            }
            else if (oSession.url.IndexOf("/learnspace/resource/common/js/plugins/scrom/studyTime.js") > 0)
            {
                oSession.utilDecodeResponse();
                var r = oSession.utilReplaceInResponse("if(r.clickAuth)", "if (false)");
                r = oSession.utilReplaceInResponse("if(r.numAuth)", "if (false)");
                r = oSession.utilReplaceInResponse("if(r.imageAuth)", "if (false)");
                r = oSession.utilReplaceInResponse("window.onbeforeunload", "window.onbeforeunload=null;var myonbeforeunload");
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
