using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;

namespace 贵州省干部在线学习助手
{/// <summary>
/// 开州云枫幼儿园抢购
/// </summary>
    public class bbtree
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession)
        {
            if (
                (oSession.url.IndexOf("/apply.do") > 0) ||
                (oSession.url.IndexOf("/static/apply.js") > 0)
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession)
        {
            if (oSession.url.IndexOf("/apply.do") > 0)
            {
                oSession.utilDecodeResponse();
                if (oSession.url.IndexOf("note") > 0)
                {
                    string js = @"
                        ";
                    string jsstr = @"
                        function jc(){
                            $(""a:contains('同意')"")[0].click();
                        }
                        ";
                    bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + "setInterval(function(){jc()},Math.round(Math.random()*50)+1000);</script></body>");
                }
                else if (oSession.url.IndexOf("input") > 0)
                {
                    string js = @"
                        ";
                    string jsstr = @"
                        function jc(){
                            if($('#check_code').val().length>3){
                            $(""a:contains('报名')"")[0].click();
                                $('#check_code').val('');
                            }
                        }
                        ";
                    bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + "setInterval(function(){jc()},Math.round(Math.random()*50)+1000);</script></body>");
                    dynamic user = new System.Dynamic.ExpandoObject(); ;
                    user.name = "陈妍";
                    user.sex = "女";
                    user.cardNo = "500234201607223707";
                    user.age = "2016-7-22";
                    user.address = "重庆市开州区云枫街道汉丰街303号3幢14-2";
                    user.mark = "";
                    user.gx = "爸爸";
                    user.gxname = "陈龙";
                    user.gxdw = "重庆市开州区义和镇";
                    user.gxmobile = "13295174907";
                    user.gxgg = "重庆开州";
                    user.gxCardNo = "500234199009033698";

                    oSession.utilReplaceInResponse("data-name=\"幼儿姓名\"", "data-name=\"幼儿姓名\" value='"+ user.name + "' ");
                    oSession.utilReplaceInResponse("data-name=\"幼儿身份证号\"", "data-name=\"幼儿身份证号\" value='" + user.cardNo + "' ");
                    oSession.utilReplaceInResponse("data-name=\"幼儿出生日期\"", "data-name=\"幼儿出生日期\" value='" + user.age + "' ");
                    oSession.utilReplaceInResponse("data-name=\"现居住地址\"", "data-name=\"现居住地址\" value='" + user.address + "' ");
                    oSession.utilReplaceInResponse("id=\"mark\">", "id=\"mark\">" + user.mark + "");

                    oSession.utilReplaceInResponse(">" + user.gx + "</option>", " selected ='selected '>" + user.gx+"</option>");

                    oSession.utilReplaceInResponse("data-name=\"家长姓名\"", "data-name=\"家长姓名\" value='" + user.gxname + "' ");
                    oSession.utilReplaceInResponse("id=\"row_cell_4\"", "id=\"row_cell_4\" value='" + user.gxdw + "' ");
                    oSession.utilReplaceInResponse("data-name=\"家长电话\"", "data-name=\"家长电话\" value='" + user.gxmobile + "' ");
                    oSession.utilReplaceInResponse("id=\"row_cell_8\"", "id=\"row_cell_8\" value='" + user.gxgg + "' ");
                    oSession.utilReplaceInResponse("data-name=\"家长身份证号\"", "data-name=\"家长身份证号\" value='" + user.gxCardNo + "' ");
                }
                else
                {
                    string js = @"
                            

                        ";
                    string jsstr = @"
                        function jc(){
                            if(new Date().getHours()*60+new Date().getMinutes()>598){
                                    $(""a:contains('我要报名')"")[2].click();
                            }
                        }
                        ";
                    bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},Math.round(Math.random()*50)+1000);</script></body>");
                }
            }
            else if (oSession.url.IndexOf("/static/apply.js") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                        function jc(){
                            
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + "</script></body>");
                r=oSession.utilReplaceInResponse("ischeck.checked", "true");
                r = oSession.utilReplaceInResponse("alert(", "console.log(new Date()+");
            }
            else if (oSession.url.IndexOf("/static/apply.js") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                        function jc(){
                            //$(""a: contains('同意')"")[0].click();
                        }
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + "</script></body>");
                r = oSession.utilReplaceInResponse("ischeck.checked", "true");
            }
        }
    }
}
