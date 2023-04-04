using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace 贵州省干部在线学习助手.Renderer
{
    /// <summary>

/// ExampleAv8Handler.cs

/// </summary>

public class ExampleAv8Handler : CefV8Handler

{

    #region 声明常量变量

        /// <summary>

        /// 内容

        /// </summary>

        public string MyParam { get; set; }

        #endregion 声明常量变量

    #region 构造函数

    /// <summary>

    /// 默认构造函数

    /// </summary>

    public ExampleAv8Handler()

    {

        MyParam = "ExampleAv8Handlerler : flydoos@vip.qq.com";

    }

    #endregion 构造函数

    #region 事件

    /// <summary>

    /// 网页脚本与后台程序交互方法

    /// 提示一：如果 returnValue = null; 则会导致网页前端出现错误：Cannot read property ’constructor’ of undefined

    /// 提示二：还存在其他的可能，导致导致网页前端出现错误：Cannot read property ’constructor’ of undefined

    /// </summary>

    /// <param name="name">名称</param>

    /// <param name="obj">对象</param>

    /// <param name="arguments">参数</param>

    /// <param name="returnValue">返回值</param>

    /// <param name="exception">返回异常信息</param>

    /// <returns></returns>

    protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)

    {
        string result = string.Empty;
        try
        {
            switch (name)
            {

                case "MyFunction":

                    MyFunction();

                    break;

                case "GetMyParam":

                    result = GetMyParam();

                    break;

                case "SetMyParam":

                    result = SetMyParam(arguments[0].GetStringValue());

                    break;
                case "getExamData":

                    result = getExamData(arguments[0].GetStringValue());

                    break;
                case "getExam":

                    result = getExam(arguments[0].GetStringValue());

                    break;
                case "setExam":
                    result = setExam(arguments[0].GetStringValue(), arguments[1].GetStringValue(), arguments[2].GetStringValue(), arguments[3].GetStringValue());
                    break;

                    default:

                    MessageBox.Show(string.Format("JS调用C# >> {0} >> {1} 返回值", name, obj.GetType()), "系统提示", MessageBoxButtons.OK);

                    break;

            }

            
        }catch(Exception ex){
            MessageBox.Show(ex.Message);
        }
        returnValue = CefV8Value.CreateString(result);
        exception = null;
        return true;

    }

    #endregion 事件

    #region 方法

        /// <summary>

        /// 我的函数

        /// </summary>

        public void MyFunction()

        {

            MessageBox.Show("ExampleAv8Handlerler : JS调用C# >> MyFunction >> 无 返回值", "系统提示", MessageBoxButtons.OK);

        }

        /// <summary>

        /// 取值

        /// </summary>

        /// <returns></returns>

        public string GetMyParam()

        {

            return MyParam;

        }

        /// <summary>

        /// 赋值

        /// </summary>

        /// <param name="value">值</param>

        /// <returns></returns>

        public string SetMyParam(string value)

        {

            MyParam = value;

            return MyParam;

        }
        public string getExamData(string value)
        {
            string json = "";
            List<dynamic> list=new List<dynamic>();
            MyParam = value;
            try {
                
                dynamic d= Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(value);
                int examid =Convert.ToInt32(d.examid.Value);
                foreach (var vd in d.data) {
                    try{
                        //MessageBox.Show(vd.t.Value);
                        var dt= ExamsDAL.getExams(vd.t.Value.Trim());
                        if (dt.Rows.Count>0)
                        {
                            string ExamAnswer = dt.Rows[0]["ExamAnswer"].ToString();
                            int type =Convert.ToInt32(dt.Rows[0]["ExamType"].ToString());
                            bool ExamAnswerVerify = Convert.ToBoolean(dt.Rows[0]["ExamAnswerVerify"]);
                            string ExamItemStr= dt.Rows[0]["ExamItem"].ToString();
                            string[] ExamItem = ExamItemStr.Split(new char[] { '★' },StringSplitOptions.RemoveEmptyEntries);
                            string ExamAnswerNum = ExamAnswer.Replace("A", "0").Replace("B", "1").Replace("C", "2").Replace("D", "3").Replace("E", "4").Replace("F", "5");
                            string ExamAnswerStr = ExamItem[Convert.ToInt32(ExamAnswerNum)];
                            string NewExamItemStr = vd.d.Value.Trim();
                            string[] NewExamItem = NewExamItemStr.Split(new char[] { '★' }, StringSplitOptions.RemoveEmptyEntries);
                            int NewExamAnswerNum= NewExamItem.ToList().IndexOf(ExamAnswerStr);
                            ExamAnswer=NewExamAnswerNum.ToString().Replace("0", "A").Replace("1", "B").Replace("2", "C").Replace("3", "D").Replace("4", "E").Replace("5", "F");
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

                            list.Add(new { ExamAnswer = ExamAnswer, ExamAnswerVerify = ExamAnswerVerify, ExamTitle = dt.Rows[0]["ExamTitle"].ToString() });
                        }
                        else {
                            int type = 0;
                            string ExamAnswer = "A";
                            string typestr=vd.l.Value.ToString();
                            switch (typestr)
                            {
                                case "(单选题)":
                                    type = 1;
                                    break;
                                case "(多选题)":
                                    type = 2;
                                    ExamAnswer = "ABCD";
                                    break;
                                default:
                                    type = 0;
                                    break;
                            }
                            ExamsDAL.AddExam(examid, vd.t.Value.Trim(), vd.d.Value.Trim(), type,ExamAnswer, false, DateTime.Now);
                            list.Add(new { ExamAnswer = ExamAnswer, ExamAnswerVerify = false, ExamTitle = vd.t.Value.Trim() });
                        }

                    }catch(Exception e1){
                        MessageBox.Show(e1.Message + "\r\n" + e1.Source);
                    }
                }
                //MessageBox.Show(list.Count.ToString());
                json = JsonConvert.SerializeObject(list);
                TEMPDAL.UpTemp(json);
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message+"\r\n"+ex.Source);
            }
           
            return json;

        }
        public string getExam(string value)
        {
            string json = "";
            var dt = ExamsDAL.getExams(value);
            if (dt.Rows.Count > 0)
            {
                string ExamAnswer = dt.Rows[0]["ExamAnswer"].ToString();
                string ExamItem = dt.Rows[0]["ExamItem"].ToString();
                string[] Item = ExamItem.Split(',');
                if (ExamAnswer.IndexOf('A') >= 0)
                {
                    json += Item[0] + ",";
                }
                if (ExamAnswer.IndexOf('B') >= 0)
                {
                    json += Item[1] + ",";
                }
                if (ExamAnswer.IndexOf('C') >= 0)
                {
                    json += Item[2] + ",";
                }
                if (ExamAnswer.IndexOf('D') >= 0)
                {
                    json += Item[3] + ",";
                }
                if (ExamAnswer.IndexOf('E') >= 0)
                {
                    json += Item[4] + ",";
                }
                if (ExamAnswer.IndexOf('F') >= 0)
                {
                    json += Item[5] + ",";
                }
            }
            return json;

        }
        public string setExam(string Title, string Item,string Type, string Answer)
        {
            int ret = 0;
            var dt = ExamsDAL.getExams(Title);
            if (dt.Rows.Count > 0)
            {
                ExamsDAL.DelExam(int.Parse(dt.Rows[0][0].ToString()));
            }
            ExamsDAL.AddExam(0, Title, Item, 0, Answer, true, DateTime.Now);


            return ret.ToString();
        }
        #endregion 方法

    }
}
