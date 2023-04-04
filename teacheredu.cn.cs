using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace 贵州省干部在线学习助手
{
    public class teacheredu
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession) {
            if (
                (oSession.url.IndexOf("/v8_6/quiz/detail?") > 0) ||
                (oSession.url.IndexOf("/proj/studentwork/study.htm") > 0)||
                (oSession.url.IndexOf("/richvideo/initdatawithviewer?") > 0)
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession) {
            if (oSession.url.IndexOf("/v8_6/quiz/detail?") > 0)//自动修改错误答案为下一项
            {
                oSession.utilDecodeResponse();
                dynamic d = JsonConvert.DeserializeObject<dynamic>(oSession.GetResponseBodyAsString());
                if (d.data.studentScore > 70) {
                    MessageBox.Show("成绩："+ d.data.studentScore);
                }
                if (d != null && d.data != null && d.data.studentQuestions != null) {
                    foreach (dynamic item in d.data.studentQuestions) {
                        if (item.questionTypeName == "单选题")
                        {
                            if (item.studentScore != item.score)
                            {
                                item.studentAnswer = item.studentAnswer == "A" ? "B" : item.studentAnswer == "B" ? "C" : item.studentAnswer == "C" ? "D" : item.studentAnswer == "D" && item.options.Count > 4 ? "E" : "A";
                            }

                        }
                        else if (item.questionTypeName == "多选题"&& item.studentScore != item.score-1) {
                            item.studentAnswer = item.studentAnswer == ("ABCD") ? "ABC" :
                                            item.studentAnswer== ("ABC") ? "ABD" :
                                            item.studentAnswer== ("ABD") ? "ACD" :
                                            item.studentAnswer== ("ACD") ? "BCD" :
                                            item.studentAnswer== ("BCD") ? "AB" :
                                            item.studentAnswer== ("AB") ? "AC" :
                                            item.studentAnswer== ("AC") ? "AD" :
                                            item.studentAnswer== ("AD") ? "BC" :
                                            item.studentAnswer== ("BC") ? "BD" :
                                            item.studentAnswer== ("BD") ? "A" :
                                            item.studentAnswer== ("A") ? "B" :
                                            item.studentAnswer== ("B") ? "C" :
                                            item.studentAnswer == ("C") ? "D" : "ABCD";
                        }
                        else if (item.questionTypeName == "判断题" && item.studentScore != item.score)
                        {
                            item.studentAnswer = item.studentAnswer == "A" ? "B" : "A";
                        }
                    }
                }
                string html = JsonConvert.SerializeObject(d);
                oSession.utilSetResponseBody(html);
            }
            else if (oSession.url.IndexOf("/proj/studentwork/study.htm") > 0)
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
                               if(!videoifplayFlag){
                                    videoifplayFlag=true;
                                }
                            } 
                            
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},Math.round(Math.random()*50)*1000+30*1000);</script></body>");
                r = oSession.utilReplaceInResponse("alert(\"已经学习了\"+tishiTime+\",点击确定更新学习时间.\");", "console.log(\"已经学习了\"+tishiTime+\",点击确定更新学习时间.\");");

            }
            else if (oSession.url.IndexOf("/richvideo/initdatawithviewer?") > 0) {
                oSession.utilSetResponseBody("[]");
            }
        }
    }
}
