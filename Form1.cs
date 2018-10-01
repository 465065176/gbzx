using Fiddler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Xilium.CefGlue;
using 贵州省干部在线学习助手.Common;

namespace 贵州省干部在线学习助手
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CreatePorxy();
            InitializeComponent();
            Name= ini.ReadString("name", "name", "");
            Pwd = ini.ReadString("pwd", "pwd", "");
        }
        FXNET.INI.IniFiles ini = new FXNET.INI.IniFiles(Application.StartupPath + "/config/zh.ini");
        public string Name { get; set; }
        public string Pwd { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {
            Text = Name;
        }
        private void CreatePorxy()
        {
            //设置别名  
            Fiddler.FiddlerApplication.SetAppDisplayName("FiddlerCoreDemoApp");
            //启动方式  
            FiddlerCoreStartupFlags oFCSF = FiddlerCoreStartupFlags.Default;

            //定义http代理端口  
            int iPort = 8888;
            //启动代理程序，开始监听http请求  
            //端口,是否使用windows系统代理（如果为true，系统所有的http访问都会使用该代理）  
            Fiddler.FiddlerApplication.Startup(iPort, false, false, false);
            Fiddler.FiddlerApplication.BeforeRequest += FiddlerApplication_BeforeRequest;
            Fiddler.FiddlerApplication.BeforeResponse += FiddlerApplication_BeforeResponse;
        }

        void FiddlerApplication_BeforeRequest(Session oSession)
        {
            if (oSession.fullUrl.IndexOf("www.qzgj.gov.cn") > 0) {//晴镇市干部
                qzgj.FiddlerApplication_BeforeRequest(oSession);
            }else 
            if (oSession.fullUrl.IndexOf("videoadmin.chinahrt.com.cn") > 0)//重庆人社干部
            {
                chinahrt.FiddlerApplication_BeforeRequest(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("rufa.gov.cn") > 0)//如法网
            {
                rufa.FiddlerApplication_BeforeRequest(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("www.zgzjzj.com") > 0)//专技天下
            {
                zgzjzj.FiddlerApplication_BeforeRequest(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("xfks.qnfzw.gov.cn") > 0)//黔南法制网
            {
                qnfzw.FiddlerApplication_BeforeRequest(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("123.249.79.44") > 0)//兴义市干部
            {
                xysgb.FiddlerApplication_BeforeRequest(oSession);
            }
            else
            if (
                (oSession.url.IndexOf("selfmade/play/player.htm") > 0) ||
                (oSession.url.IndexOf("/play.htm") > 0) ||
                (oSession.url.IndexOf("/elms/web/login.action") > 0) ||
                (oSession.url.IndexOf("/elms/web/re_login.jsp") > 0) ||
                (oSession.url.IndexOf("jquery.course.framework.js") > 0)||
                (oSession.url.IndexOf("controlFunction.js") > 0)||
                (oSession.url.IndexOf("jquery.course.player.flowplayer.js") > 0)||
                (oSession.url.IndexOf("/data/data.xml") > 0) ||
                (oSession.url.IndexOf("jquery.course.test.js") > 0)||
                (oSession.url.IndexOf("index.htm") > 0)||
                (oSession.url.IndexOf("/elms/web/course/getExamQuestion.action") > 0)||
                (oSession.url.IndexOf("/elms/web/course/getPaperResult.action") > 0)||
                (oSession.url.IndexOf("/elms/web/course/questionTempSave.action")>0)||
                (oSession.url.IndexOf("/elms/web/course/getCourseExam.action") > 0)||
                (oSession.url.IndexOf("/elms/web/course/listMyCourses.action") > 0)
                ){
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
                }
            else if ((oSession.url.IndexOf("/linkAICC.js") > 0))
            {
                oSession.bBufferResponse = true;
            }
            else if ((oSession.url.IndexOf(".mp4") > 0)) {
                oSession.bBufferResponse = false;
            }
        }

        void FiddlerApplication_BeforeResponse(Session oSession)
        {
            if (oSession.fullUrl.IndexOf("www.qzgj.gov.cn") > 0)
            {
                qzgj.FiddlerApplication_BeforeResponse(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("videoadmin.chinahrt.com.cn") > 0)
            {
                chinahrt.FiddlerApplication_BeforeResponse(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("rufa.gov.cn") > 0)
            {
                rufa.FiddlerApplication_BeforeResponse(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("www.zgzjzj.com") > 0)
            {
                zgzjzj.FiddlerApplication_BeforeResponse(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("xfks.qnfzw.gov.cn") > 0)
            {
                qnfzw.FiddlerApplication_BeforeResponse(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("123.249.79.44") > 0)//兴义市干部
            {
                xysgb.FiddlerApplication_BeforeResponse(oSession);
            }
            else

            if (oSession.url.IndexOf("/linkAICC.js") > 0)
            {
                oSession.utilDecodeResponse();
                oSession.ResponseHeaders["Content-Type"] = "application/javascript; charset=GB2312";
                bool r = oSession.utilReplaceInResponse("setInterval(\"doGetInter(\" + flag + \")\", 200)", "setInterval(\"doGetInter(\" + flag + \")\", 2000)");
            }else if (oSession.url.IndexOf("/selfmade/play/player.htm") > 0)
            {
                oSession.ResponseHeaders["Content-Type"] = "text/html; charset=GB2312";
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("window.setInterval(\"focusInterval()\", 1000);", "");
                // r = oSession.utilReplaceInResponse(" jwplayer(\"container\").setup({", " jwplayer(\"container\").setup({\r\nflashplayer:\"jwplayer.flash.swf\",\r\nautostart:true,");
                Console.WriteLine(r + "\r\n");
                r = oSession.utilReplaceInResponse("alert(\"请点击确认继续\");", "");
                Console.WriteLine(r + "\r\n");
                //string html= oSession.GetResponseBodyAsString();
                //oSession.utilSetResponseBody(oSession.GetResponseBodyAsString());
                string js = @"
                            jwplayer(""container"").onPlaylistComplete(function(){
                                    window.open('http://www.gzgbonline.cn/elms/web/lanmu02.jsp');
                                });

                        ";
                string jsstr = @"
                            function jc(){
                                if(jwplayer(""container"").getState()==undefined){
                                    window.open('http://www.gzgbonline.cn/elms/web/lanmu02.jsp');
                                }
                            } 
                            
                        ";
                r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},20000);</script></body>");
                r = oSession.utilReplaceInResponse("onReady(e);", "onReady(e);go();");

            }
            else if ((oSession.url.IndexOf("/play.htm") > 0)) {
                oSession.utilDecodeResponse();
                string jsstr = @"
                            function jc(){
                                if(player.realy()==false){
                                    window.open('http://www.gzgbonline.cn/elms/web/lanmu02.jsp');
                                }
                            } 
                            
                        ";
                bool r = oSession.utilReplaceInResponse("<div id=\"course_player\">", "<div id=\"course_player\" style=\"width: 398px; height: 298px;\">");
                r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">" + jsstr + " setInterval(function(){jc()},20000);</script></body>");
            }
            else if (oSession.url.IndexOf("/elms/web/re_login.jsp") > 0)
            {
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("<input type=\"text\" name=\"loginId\" size=\"12\" class=\"input1\" value=\"\"", "<input type=\"text\" name=\"loginId\" size=\"12\" class=\"input1\" value=\"" + Name + "\"");
                r = oSession.utilReplaceInResponse("<input type=\"password\" name=\"password\" size=\"12\" class=\"input1\" value=\"\"", "<input type=\"password\" name=\"password\" size=\"12\" class=\"input1\" value=\"" + Pwd + "\"");
                //r = oSession.utilReplaceInResponse("</body>", "<script>login_onsubmit(0);</script></body>");
                r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){login_onsubmit(0); } setTimeout(function(){go()},Math.round(Math.random()*10)*1000+3000);</script></body>");
            }

            else if (oSession.url.IndexOf("/elms/web/login.action") > 0)
            {
                if (oSession.GetResponseBodyAsString().IndexOf("<title>用户重复</title>") > 0)
                {
                    oSession.utilDecodeResponse();
                    //bool r = oSession.utilReplaceInResponse("</body>", "<script>login.submit();</script></body>");
                    bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){login.submit(); } setTimeout(function(){go()},Math.round(Math.random()*5)*1000+3000);</script></body>");
                }

            }
            //三分屏下需要修改的，
            else if (oSession.url.IndexOf("jquery.course.framework.js") > 0)
            {
                //!!document.createElement('video').canPlayType 源码中在此处判断是用 falsh还是html5
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("!!document.createElement('video').canPlayType", "false");
                
            }
            else if (oSession.url.IndexOf("controlFunction.js") > 0)
            {
                //!!设置失去焦点也播放
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("switch_focus = true;", "switch_focus = false;");
                oSession.utilReplaceInResponse("continueF=false;", "continueF=true;");
            }
            else if ((oSession.url.IndexOf("jquery.course.player.flowplayer.js") > 0))
            {
                //!!设置自动播放
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("autoPlay: false,", "autoPlay: true,");
                r = oSession.utilReplaceInResponse("player.init();", "player.init();go()");
                string js = @"
                            onFinish: function() {
                                 window.open('http://www.gzgbonline.cn/elms/web/lanmu02.jsp');
                            }

                        ";
                r = oSession.utilReplaceInResponse("url:/*player.src*/player.getURL()", "url:/*player.src*/player.getURL(),\r\n" + js);
            }
            else if (oSession.url.IndexOf("/data/data.xml") > 0)
            {
                //!!设置pre="1"为pre="" 就不会定时出题了，//此处设置了  只是不会出题，但到出题的时候还是会暂停。所以还是要修改出题定时器http://219.151.4.130/gjxzxy/tl1601182/js/jquery.course.test.js
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("pre=\"1\"", "pre=\"\"");
            }
            else if (oSession.url.IndexOf("jquery.course.test.js") > 0)
            {
                //修改出题定时器
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("testListener();", "");
            }
            else if (oSession.url.IndexOf("index.htm") > 0)
            {
                oSession.utilDecodeResponse();
                //自动进入play.html
                if (oSession.GetResponseBodyAsString().IndexOf("进入课程") > 0)
                {
                    bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){$(\".learn\").click();} setTimeout(function(){go()},5000); </script></body>");
                }
            }
            else if (oSession.url.IndexOf("/elms/web/course/listMyCourses.action") > 0)
            {
                
                string js = @"
                            var tr=document.getElementsByClassName('secondrow');
                            if(tr.length!=0)
                            {
                                var ix=Math.round(Math.random()*10);
                                for(var i=ix;i<tr.length;i++){
                                    if(document.getElementsByClassName('secondrow')[i].cells[3].innerText.replace('%','')>=100){
                                        document.getElementsByClassName('secondrow')[i].cells[5].childNodes[0].click()
                                    }else{
                                        document.getElementsByClassName('secondrow')[i].cells[4].childNodes[1].click()
                                    }
                                }
                            }else{
                                parent.$(""p:eq(1)"").click()
                            }
                        ";
                string htmlstr = oSession.GetResponseBodyAsString();
                htmlstr = htmlstr.Replace("</body>", "<script type=\"text/javascript\">function go(){" + js + " } setInterval(function(){go()},Math.round(Math.random()*10)*1000+5000);</script></body>");
                oSession.utilSetResponseBody(htmlstr);
                //var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + " } setTimeout(function(){go()},Math.round(Math.random()*10)*1000+5000);</script></body>");
                oSession.ResponseHeaders.Remove("Transfer-Encoding");
            }
            else if ((oSession.url.IndexOf("/elms/web/course/getCourseExam.action") > 0))
            {
                oSession.utilReplaceInResponse("confirm(\"您确定要进入考试吗？确定后将进入并开始计时\")", "1==1");
                string js = @"
                            if(document.getElementsByClassName('image').length!=0)
                            {
                           document.getElementsByClassName('image')[0].click()
                            }else{
                                window.open('/elms/web/lanmu02.jsp');
                            }
                        ";
                var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + " } setTimeout(function(){go()},Math.round(Math.random()*10)*1000+5000);</script></body>");
            }
            else if (oSession.url.IndexOf("/elms/web/course/getExamQuestion.action") > 0 || oSession.url.IndexOf("/elms/web/course/questionTempSave.action") > 0)
            {
                oSession.utilDecodeResponse();
                try
                {
                    Regex r1 = new Regex(@"<td height=""25"" colspan=""2"">(?<t>.*?)</td>.*?<table class=""areaborder"" width=""100%"">(?<d>.*?)</table>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    MatchCollection mc1 = r1.Matches(oSession.GetResponseBodyAsString());
                    if (mc1 != null)
                    {
                        for (int i = 0; i < mc1.Count; i++)
                        {
                            try
                            {
                                string tt = mc1[i].Groups["t"].Value;
                                string t = tt.Substring(tt.IndexOf("、") + 1, (tt.LastIndexOf("。") < 0 ? tt.LastIndexOf("(") - 1 : tt.LastIndexOf("。")) - tt.IndexOf("、"));
                                var dt = ExamsDAL.getExams(t.Trim());
                                if (dt.Rows.Count > 0)
                                {
                                    string ExamAnswer = dt.Rows[0]["ExamAnswer"].ToString();
                                    int type = Convert.ToInt32(dt.Rows[0]["ExamType"].ToString());
                                    bool ExamAnswerVerify = Convert.ToBoolean(dt.Rows[0]["ExamAnswerVerify"]);
                                    if (!ExamAnswerVerify)
                                    {
                                        switch (type)
                                        {
                                            case 1://单选
                                                ExamAnswer = ExamAnswer.Equals("A") ? "B" : ExamAnswer.Equals("B") ? "C" : ExamAnswer.Equals("C") ? "D" : "A";
                                                break;
                                            case 2://多选
                                                ExamAnswer = ExamAnswer.Equals("ABCD") ? "ABC" :
                                                    ExamAnswer.Equals("ABC") ? "ABD" :
                                                    ExamAnswer.Equals("ABD") ? "ACD" :
                                                    ExamAnswer.Equals("ACD") ? "BCD" :
                                                    ExamAnswer.Equals("BCD") ? "AB" :
                                                    ExamAnswer.Equals("AB") ? "AC" :
                                                    ExamAnswer.Equals("AC") ? "AD" :
                                                    ExamAnswer.Equals("AD") ? "BC" :
                                                    ExamAnswer.Equals("BC") ? "BD" :
                                                    ExamAnswer.Equals("BD") ? "A" :
                                                    ExamAnswer.Equals("A") ? "B" :
                                                    ExamAnswer.Equals("B") ? "C" :
                                                    ExamAnswer.Equals("C") ? "D" : "ABCD";
                                                break;
                                            default://是否
                                                ExamAnswer = ExamAnswer.Equals("A") ? "B" : "A";
                                                break;
                                        }

                                        ExamsDAL.UpExam(Convert.ToInt32(dt.Rows[0]["ID"]), ExamAnswer);
                                    }
                                    char[] ExamAnswers = ExamAnswer.ToCharArray();
                                    foreach (char c in ExamAnswers)
                                    {
                                        if (type == 2)
                                            oSession.utilReplaceInResponse("<input name=\"item_" + i + "\" type=\"checkbox\" value=\"" + c + "\"", "<input name=\"item_" + i + "\" type=\"checkbox\" value=\"" + c + "\"  checked=\"checked\" ");
                                        else
                                            oSession.utilReplaceInResponse("<input name=\"item_" + i + "\" type=\"radio\" value=\"" + c + "\"", "<input name=\"item_" + i + "\" type=\"radio\" value=\"" + c + "\"  checked=\"checked\" ");
                                    }
                                }
                                else
                                {
                                    int type = 0;
                                    string ExamAnswer = "A";
                                    if (oSession.GetResponseBodyAsString().IndexOf("单选题") > 0)
                                    {
                                        type = 1;
                                    }
                                    else if (oSession.GetResponseBodyAsString().IndexOf("多选题") > 0)
                                    {
                                        type = 2;
                                        ExamAnswer = "ABCD";
                                    }
                                    else
                                    {
                                        type = 0;
                                    }
                                    Regex r2 = new Regex(@"value=""(?<d>.*?)"".*?>(?<dt>.*?)</div>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                                    string td = "";
                                    try
                                    {
                                        MatchCollection mc2 = r2.Matches(mc1[i].Groups["d"].Value);
                                        foreach (Match m in mc2)
                                        {
                                            td += m.Groups["d"].Value + "-" + m.Groups["dt"].Value + "|";
                                        }
                                        td = td.TrimEnd('|');
                                    }
                                    catch { }
                                    ExamsDAL.AddExam(0, t, td, type, ExamAnswer, false, DateTime.Now);
                                    char[] ExamAnswers = ExamAnswer.ToCharArray();
                                    foreach (char c in ExamAnswers)
                                    {
                                        if (type == 2)
                                            oSession.utilReplaceInResponse("<input name=\"item_" + i + "\" type=\"checkbox\" value=\"" + c + "\"", "<input name=\"item_" + i + "\" type=\"checkbox\" value=\"" + c + "\"  checked=\"checked\" ");
                                        else
                                            oSession.utilReplaceInResponse("<input name=\"item_" + i + "\" type=\"radio\" value=\"" + c + "\"", "<input name=\"item_" + i + "\" type=\"radio\" value=\"" + c + "\"  checked=\"checked\" ");
                                    }
                                }
                            }
                            catch (Exception e1)
                            {
                                MessageBox.Show(e1.Message + "\r\n" + e1.Source);
                            }
                        }
                    }
                    if (oSession.url.IndexOf("Index=0") > 0)
                    {
                        string js = @"
                            changed();
                            if(parent.left.document.body.innerHTML.indexOf('第2部分')>0)
                            {
                                parent.left.next(1);
                            }else{
                                submitExam();
                            }
                        ";
                        var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + " } setTimeout(function(){go()},Math.round(Math.random()*20)*1000+20000);</script></body>");
                    }
                    else if (oSession.url.IndexOf("Index=1") > 0)
                    {
                        string js = @"
                            changed();
                            if(parent.left.document.body.innerHTML.indexOf('第3部分')>0)
                            {
                                parent.left.next(2);}
                            else{
                                submitExam();
                            }
                        ";
                        var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + " } setTimeout(function(){go()},Math.round(Math.random()*20)*1000+20000);</script></body>");
                    }
                    else if (oSession.url.IndexOf("Index=2") > 0)
                    {
                        string js = @"
                            changed();
                            submitExam();
                            //parent.left.next(1);
                        ";
                        var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + " } setTimeout(function(){go()},Math.round(Math.random()*20)*1000+20000);</script></body>");
                    }
                    else if (oSession.url.IndexOf("=submitExam") > 0)//考试提交结果也面
                    {
                        string js = @"
                            if(document.getElementsByClassName('content').length!=0)
                            {
                           document.getElementsByClassName('content')[0].click()
                            }else{
                                window.open('/elms/web/lanmu02.jsp');
                            }
                        ";
                        var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + " } setTimeout(function(){go()},Math.round(Math.random()*10)*1000+5000);</script></body>");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n" + ex.Source);
                }

            }

            else if ((oSession.url.IndexOf("/elms/web/course/getPaperResult.action") > 0))
            {
                oSession.utilDecodeResponse();
                try
                {
                    Regex r1 = new Regex(@"<img src=""/elms/images/(wrong|right).gif""/>(?<t>.*?)\(.*?正确答案：(?<td>.*?)<", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    MatchCollection mc1 = r1.Matches(oSession.GetResponseBodyAsString());
                    if (mc1 != null)
                    {
                        for (int i = 0; i < mc1.Count; i++)
                        {
                            try
                            {
                                string tt = mc1[i].Groups["t"].Value;
                                string t = tt;
                                string ExamAnswer = mc1[i].Groups["td"].Value;
                                var dt = ExamsDAL.getExams(t.Trim());
                                if (dt.Rows.Count > 0)
                                {
                                    int type = Convert.ToInt32(dt.Rows[0]["ExamType"].ToString());
                                    bool ExamAnswerVerify = Convert.ToBoolean(dt.Rows[0]["ExamAnswerVerify"]);
                                    if (!ExamAnswerVerify)
                                    {
                                        ExamsDAL.UpExam(Convert.ToInt32(dt.Rows[0]["ID"]), ExamAnswer, true);
                                    }
                                }
                                else
                                {
                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message + "\r\n" + e.Source);
                            }
                        }
                    }
                    string js = @"
                           window.open('/elms/web/lanmu02.jsp');
                        ";
                    var r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + " } setTimeout(function(){go()},Math.round(Math.random()*10)*1000+5000);</script></body>");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n" + ex.Source);
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Fiddler.FiddlerApplication.Shutdown();
            Thread.Sleep(500);  
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private class DevToolsWebClient : CefClient
        {
        }
    }
}
