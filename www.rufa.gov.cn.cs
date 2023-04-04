﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;

namespace 贵州省干部在线学习助手
{
    public class rufa
    {
        public static void FiddlerApplication_BeforeRequest(Session oSession)
        {
            if (
                (oSession.url.IndexOf("/fxweb/subpage/legalpublicity/fx_regulations.html?") > 0) ||
                (oSession.url.IndexOf("/onlineanswer/os?") > 0) ||
                (oSession.url.IndexOf("/js/examination.js") > 0)||//http://exam.rufa.gov.cn/rufawebsite/assets/js/examination.js?v=2018082701
                (oSession.url.IndexOf("/goexamination") > 0) //http://exam.rufa.gov.cn/rufawebsite/examination/content/goexamination
                )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
            else if (oSession.url.IndexOf("/mygetquestion?question") > 0) {
                oSession.utilCreateResponseAndBypassServer();
                oSession.oResponse.headers.SetStatus(200, "Ok");
                oSession.oResponse["Content-Type"] = "text/html; charset=UTF-8";
                oSession.oResponse["Cache-Control"] = "private, max-age=0";
                DataTable dt= ExamsDAL.getExams(System.Web.HttpUtility.UrlDecode(oSession.PathAndQuery.Replace("/mygetquestion?question=", "")));
                if (dt.Rows.Count > 0)
                {
                    oSession.utilSetResponseBody(dt.Rows[0]["ExamAnswer"].ToString());
                }
                else {
                    oSession.utilSetResponseBody("");
                }
                

            }
        }

        public static void FiddlerApplication_BeforeResponse(Session oSession)
        {
            if (oSession.url.IndexOf("/onlineanswer/os?") > 0)
            {
                oSession.utilDecodeResponse();
                using (StreamWriter tw = new StreamWriter(Application.StartupPath + @"\1.txt", true))
                {
                    tw.WriteLine(oSession.GetResponseBodyAsString());
                }
            }
            else if (oSession.url.IndexOf("/fxweb/subpage/legalpublicity/fx_regulations.html?") > 0)
            {
                oSession.utilDecodeResponse();
                string js = @"
                            

                        ";
                string jsstr = @"
                            var i=0;
                            function jc(){
                                if($('#btn_code').length>0&&$('#btn_code').attr('disabled')==undefined){
                                    if(i%2==0){
                                        $('.neiinput').find(""[correct='1']"").click();
                                    }else{
                                          $('#btn_code').click();
                                    }
                                    i++;
                                }else{
                                    $('.next').click();
                                }
                            } 
                            
                        ";
                bool r = oSession.utilReplaceInResponse("</body>", "<script type=\"text/javascript\">function go(){" + js + "}" + jsstr + " setInterval(function(){jc()},Math.round(Math.random()*50)*1000+30*1000);</script></body>");



            }
            else if (oSession.url.IndexOf("/js/examination.js") > 0) {
                string js= @"
                            function mydati(question_detail){
                                $.get(""/mygetquestion?question=""+question_detail.question,function(data,status){
                                        $(""input[name = 'answer']"").each(function(){
                                            if (data.indexOf($(this).val())>-1) {
						                        $(this).checked = true;
					                        }
                                          });
                                    });
                                }
                                    ";
                bool r = oSession.utilReplaceInResponse("setButtonDisable(question_no);", "setButtonDisable(question_no);mydati(question_detail);");
            }
            else if (oSession.url.IndexOf("/goexamination") > 0)
            {
                string html = oSession.GetResponseBodyAsString();
                Regex reg = new Regex("<input type='hidden' value = '(?<text>.*?)' id='questoinList'/>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match m = reg.Match(html);
                if (m.Success)
                {
                    html = m.Groups["text"].Value;
                    //if (true) {
                    //html = "[{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037985245044932608\",\"lpQuestionAnswerList\":[{\"contentType\":\"要求\",\"id\":\"1037985245074292736\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"机会\",\"id\":\"1037985245107847168\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"权利\",\"id\":\"1037985245141401600\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"权力\",\"id\":\"1037985245170761728\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"1\",\"question\":\"中华人民共和国公民对于任何国家机关和国家工作人员，有提出批评和建议的————。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037256636021866496\",\"lpQuestionAnswerList\":[{\"contentType\":\"对\",\"id\":\"1037256636047032320\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"错\",\"id\":\"1037256636063809536\",\"isCorrect\":\"0\",\"optionNum\":\"B\"}],\"num\":\"2\",\"question\":\"党的十九大报告指出，任何组织和个人都不得有超越宪法法律的特权，绝不允许以言代法、以权压法、逐利违法、徇私枉法。（ ）\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037898639491465216\",\"lpQuestionAnswerList\":[{\"contentType\":\"对\",\"id\":\"1037898639512436736\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"错\",\"id\":\"1037898639537602560\",\"isCorrect\":\"0\",\"optionNum\":\"B\"}],\"num\":\"3\",\"question\":\"经营者应当保证其提供的商品或者服务符合保障人身、财产安全的要求。（ ）\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037982713283346432\",\"lpQuestionAnswerList\":[{\"contentType\":\"二年\",\"id\":\"1037982713304317952\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"三年\",\"id\":\"1037982713325289472\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"四年\",\"id\":\"1037982713346260992\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"五年\",\"id\":\"1037982713371426816\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"4\",\"question\":\"党员受到撤销党内职务处分，或者依照前款规定受到严重警告处分的，————内不得在党内担任和向党外组织推荐担任与其原任职务相当或者高于其原任职务的职务。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"201806200722Q640255146290\",\"lpQuestionAnswerList\":[{\"contentType\":\"赔偿\",\"id\":\"201806200722F163099197673\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"补偿\",\"id\":\"201806200722E206781547216\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"平反\",\"id\":\"201806200722L793121821278\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"胜诉\",\"id\":\"201806200722F311151701548\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"5\",\"question\":\"由于国家机关和国家工作人员侵犯公民权利而受到损失的人，有依照法律规定取得————的权利。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038626921627254784\",\"lpQuestionAnswerList\":[{\"contentType\":\"党中央\",\"id\":\"1038626921648226304\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"国务院\",\"id\":\"1038626921673392128\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"外交部\",\"id\":\"1038626921694363648\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"外事办\",\"id\":\"1038626921711140864\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"6\",\"question\":\"《国歌法》 第九条 外交活动中奏唱国歌的场合和礼仪，由————规定。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037538841705054208\",\"lpQuestionAnswerList\":[{\"contentType\":\"五\",\"id\":\"1037538841726025728\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"三\",\"id\":\"1037538841742802944\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"二\",\"id\":\"1037538841763774464\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"七\",\"id\":\"1037538841784745984\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"7\",\"question\":\"《刑法》第一百三十三条　违反交通运输管理法规，因而发生重大事故，致人重伤、死亡或者使公私财产遭受重大损失的，处——————年以下有期徒刑或者拘役。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038717775800958976\",\"lpQuestionAnswerList\":[{\"contentType\":\"对\",\"id\":\"1038717775821930496\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"错\",\"id\":\"1038717775838707712\",\"isCorrect\":\"0\",\"optionNum\":\"B\"}],\"num\":\"8\",\"question\":\"国务院每届任期同全国人民代表大会每届任期相同。（ ）\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038627273248342016\",\"lpQuestionAnswerList\":[{\"contentType\":\"刑事责任\",\"id\":\"1038627273269313536\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"政治责任\",\"id\":\"1038627273290285056\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"法律责任\",\"id\":\"1038627273307062272\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"行政责任\",\"id\":\"1038627273323839488\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"9\",\"question\":\"在公共场合，故意篡改国歌歌词、曲谱，以歪曲、 贬损方式奏唱国歌，或者以其他方式侮辱国歌的，由公安机关处以警告或者拘留；构成犯罪的，依法追究————。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"201805301019Y324235707019\",\"lpQuestionAnswerList\":[{\"contentType\":\"党章\",\"id\":\"201805301019I634076601386\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"宪法\",\"id\":\"201805301019T668956739934\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"政策\",\"id\":\"201805301019V393066437111\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"决议\",\"id\":\"201805301019G198312342613\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"10\",\"question\":\"一切法律、行政法规和地方性法规都不得同——————相抵触。(单选题)\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038702500976263168\",\"lpQuestionAnswerList\":[{\"contentType\":\"代行职务\",\"id\":\"1038702500997234688\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"推选\",\"id\":\"1038702501014011904\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"任命\",\"id\":\"1038702501034983424\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"补选\",\"id\":\"1038702501051760640\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"11\",\"question\":\"中华人民共和国副主席缺位的时候，由全国人民代表大会————。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037981569416953856\",\"lpQuestionAnswerList\":[{\"contentType\":\"对\",\"id\":\"1037981569437925376\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"错\",\"id\":\"1037981569458896896\",\"isCorrect\":\"0\",\"optionNum\":\"B\"}],\"num\":\"12\",\"question\":\"不得使用或者变相使用中华人民共和国的国旗、国歌、国徽，军旗、军歌、军徽做商品的商标。（ ）\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037619705508003840\",\"lpQuestionAnswerList\":[{\"contentType\":\"十\",\"id\":\"1037619705528975360\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"七\",\"id\":\"1037619705549946880\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"五\",\"id\":\"1037619705566724096\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"十五\",\"id\":\"1037619705583501312\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"13\",\"question\":\"《刑法》第二百三十四条规定，故意伤害他人身体，致人重伤的，处三年以上————年以下有期徒刑。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038717530056687616\",\"lpQuestionAnswerList\":[{\"contentType\":\"对\",\"id\":\"1038717530077659136\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"错\",\"id\":\"1038717530094436352\",\"isCorrect\":\"0\",\"optionNum\":\"B\"}],\"num\":\"14\",\"question\":\"全国人民代表大会常务委员会有权修改宪法，解释宪法，监督宪法的实施。（ ）\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"201805301010O699263028770\",\"lpQuestionAnswerList\":[{\"contentType\":\"宪法\",\"id\":\"201805301010K444541856745\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"人民意志\",\"id\":\"201805301010K433433032833\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"国家意志\",\"id\":\"201805301010G938547848133\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"党内法规\",\"id\":\"201805301010K313917956038\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"15\",\"question\":\".党要领导立法，善于使党的主张通过法定程序成为——————。(单选题)\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037893557333000192\",\"lpQuestionAnswerList\":[{\"contentType\":\"对\",\"id\":\"1037893557404303360\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"错\",\"id\":\"1037893557479800832\",\"isCorrect\":\"0\",\"optionNum\":\"B\"}],\"num\":\"16\",\"question\":\"居民委员会应当开展便民利民的社区服务活动，可以兴办有关的服务事业。（ ）\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037899116543213568\",\"lpQuestionAnswerList\":[{\"contentType\":\"对\",\"id\":\"1037899116559990784\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"错\",\"id\":\"1037899116580962304\",\"isCorrect\":\"0\",\"optionNum\":\"B\"}],\"num\":\"17\",\"question\":\"《行政处罚法》第七条规定，公民、法人或者其他组织违法行为构成犯罪，应当依法追究刑事责任，也可以行政处罚代替刑事处罚。（ ）\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037252730822852608\",\"lpQuestionAnswerList\":[{\"contentType\":\"对\",\"id\":\"1037252730848018432\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"错\",\"id\":\"1037252730864795648\",\"isCorrect\":\"0\",\"optionNum\":\"B\"}],\"num\":\"18\",\"question\":\"自治区人民代表大会制定的自治条例和单行条例，要报全国人民代表大会批准后生效。（ ）\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037899189847064576\",\"lpQuestionAnswerList\":[{\"contentType\":\"对\",\"id\":\"1037899189868036096\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"错\",\"id\":\"1037899189889007616\",\"isCorrect\":\"0\",\"optionNum\":\"B\"}],\"num\":\"19\",\"question\":\"对行政违法人根据情况可以暂扣或者吊销许可证、暂扣或者吊销执照。（ ）\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037176778080124928\",\"lpQuestionAnswerList\":[{\"contentType\":\"对\",\"id\":\"1037176778101096448\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"错\",\"id\":\"1037176778122067968\",\"isCorrect\":\"0\",\"optionNum\":\"B\"}],\"num\":\"20\",\"question\":\"中国社会主义革命的胜利和共产主义事业的成就，是中国共产党领导中国各族人民，在马克思列宁主义、毛泽东思想的指引下，坚持真理，修正错误，战胜许多艰难险阻而取得的。（ ）\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"2\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"201804200526U210814193227\",\"lpQuestionAnswerList\":[{\"contentType\":\"会议机构\",\"id\":\"201804200526I829781553080\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"常设机构\",\"id\":\"201804200526Q224784455791\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"政治机关\",\"id\":\"201804200526B772678065255\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"最高国家权力机关\",\"id\":\"201804200526A892068803720\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"21\",\"question\":\"中华人民共和国全国人民代表大会是最高———。(单选题)\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037989591203708928\",\"lpQuestionAnswerList\":[{\"contentType\":\"党的意志\",\"id\":\"1037989591224680448\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"国家的意志\",\"id\":\"1037989591245651968\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"人民的意志\",\"id\":\"1037989591262429184\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"人们内心自觉\",\"id\":\"1037989591287595008\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"22\",\"question\":\"习近平总书记指出，“再多再好的法律，必须转化为——————才能真正为人们所遵行”。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038715411505348608\",\"lpQuestionAnswerList\":[{\"contentType\":\"对\",\"id\":\"1038715411526320128\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"错\",\"id\":\"1038715411543097344\",\"isCorrect\":\"0\",\"optionNum\":\"B\"}],\"num\":\"23\",\"question\":\"任何组织或者个人都不得有超越宪法和法律的特权。（ ）\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037269304657051648\",\"lpQuestionAnswerList\":[{\"contentType\":\"主任\",\"id\":\"1037269304678023168\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"副主任\",\"id\":\"1037269304698994688\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"委员长\",\"id\":\"1037269304715771904\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"书记\",\"id\":\"1037269304736743424\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"24\",\"question\":\"监察委员会的————同本级人民代表大会的任期相同，特别规定国家监察委员会主任连续任职不得超过两届。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037503607479468032\",\"lpQuestionAnswerList\":[{\"contentType\":\"刑事犯罪\",\"id\":\"1037503607500439552\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"侵权\",\"id\":\"1037503607517216768\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"不法侵害\",\"id\":\"1037503607533993984\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"侵害\",\"id\":\"1037503607554965504\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"25\",\"question\":\"为了使国家、公共利益、本人或者他人 的人身、财产和其他权利免受正在进行的————，而采取的制止不法侵害的行为，对不法侵害人造成损害的，属于正当防卫，不负刑事责任。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037258214338461696\",\"lpQuestionAnswerList\":[{\"contentType\":\"对\",\"id\":\"1037258214359433216\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"错\",\"id\":\"1037258214376210432\",\"isCorrect\":\"0\",\"optionNum\":\"B\"}],\"num\":\"26\",\"question\":\"监察机关行使的调查权相当于刑事侦查权，等同于司法机关的强制措施。（ ）\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"201805301007F183857298151\",\"lpQuestionAnswerList\":[{\"contentType\":\"繁荣昌盛\",\"id\":\"201805301007R897073021010\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"伟大复兴\",\"id\":\"201805301007E811180334720\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"千年梦想\",\"id\":\"201805301007B165121319067\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"伟大中国梦\",\"id\":\"201805301007L526166939404\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"27\",\"question\":\"宪法规定，要把我国建设成为富强民主文明和谐美丽的社会主义现代化强国，实现中华民族————————。(单选题)\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"201805301012U805282891959\",\"lpQuestionAnswerList\":[{\"contentType\":\"监督制度\",\"id\":\"201805301012D185549689751\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"企业制度\",\"id\":\"201805301012C691094364096\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"法人制度\",\"id\":\"201805301012J281994385055\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"法律顾问制度\",\"id\":\"201805301012X108517439904\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"28\",\"question\":\"党的十八届三中全会提出普遍建立————————，为法治发展注入了新的动力。(单选题)\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"201806200746P260046603340\",\"lpQuestionAnswerList\":[{\"contentType\":\"国家决策权\",\"id\":\"201806200746P806913018860\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"国家立法权\",\"id\":\"201806200746A424173818025\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"国家行政权\",\"id\":\"201806200746L901446462986\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"国家司法权\",\"id\":\"201806200746M668818157858\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"29\",\"question\":\"全国人民代表大会和全国人民代表大会常务委员会行使——————。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038703946383753216\",\"lpQuestionAnswerList\":[{\"contentType\":\"批准\",\"id\":\"1038703946404724736\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"审查\",\"id\":\"1038703946421501952\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"研究\",\"id\":\"1038703946442473472\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"备案\",\"id\":\"1038703946459250688\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"30\",\"question\":\"直辖市的人民代表大会和它们的常务委员会，在不同宪法、法律、行政法规相抵触的前提下，可以制定地方性法规，报全国人民代表大会常务委员会————。\",\"questionDifficulty\":\"0\",\"questionType\":\"0\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038688954028654592\",\"lpQuestionAnswerList\":[{\"contentType\":\"党的中央组织、地方组织、基层组织\",\"id\":\"1038688954049626112\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"党的纪律检查机关\",\"id\":\"1038688954066403328\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"党的工作机关、党委派出机关\",\"id\":\"1038688954087374848\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"党委直属事业单位、党组等党的组织\",\"id\":\"1038688954104152064\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"31\",\"question\":\"党务公开主体可以分为以下几类：——————————\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037623228144549888\",\"lpQuestionAnswerList\":[{\"contentType\":\"全面建成小康社会\",\"id\":\"1037623228165521408\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"全面深化改革\",\"id\":\"1037623228182298624\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"全面依法治国\",\"id\":\"1037623228203270144\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"全面从严治党\",\"id\":\"1037623228224241664\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"32\",\"question\":\"跨入新世纪，必须按照中国特色社会主义事业“五位一体”总体布局和“四个全面”战略布局，“四个全面”是指——————————————。\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038711491156508672\",\"lpQuestionAnswerList\":[{\"contentType\":\"主任\",\"id\":\"1038711491173285888\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"副主任若干\",\"id\":\"1038711491194257408\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"委员若干人\",\"id\":\"1038711491211034624\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"党组书记\",\"id\":\"1038711491232006144\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"33\",\"question\":\"监察委员会由下列人员组成：————————\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038690848008568832\",\"lpQuestionAnswerList\":[{\"contentType\":\"监督\",\"id\":\"1038690848029540352\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"调查\",\"id\":\"1038690848046317568\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"处置\",\"id\":\"1038690848067289088\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"双规\",\"id\":\"1038690848084066304\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"34\",\"question\":\"监察机关依法独立行使监察权，有———————三大职责。\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038691973239668736\",\"lpQuestionAnswerList\":[{\"contentType\":\"国家公祭仪式\",\"id\":\"1038691973256445952\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"重大外交活动\",\"id\":\"1038691973277417472\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"重大体育赛事\",\"id\":\"1038691973298388992\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"宪法宣誓仪式\",\"id\":\"1038691973315166208\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"35\",\"question\":\"根据《国歌法》规定，应当奏唱国歌的场合包括：————。\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037600318365368320\",\"lpQuestionAnswerList\":[{\"contentType\":\"是马克思主义中国化的最新成果\",\"id\":\"1037600318390534144\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"是党和人民实践经验和集体智慧的结晶\",\"id\":\"1037600318407311360\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"是党的十八大以来党和国家事业取得历史性成就、发生历史性变革的根本的理论指引\",\"id\":\"1037600318428282880\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"已经被载入宪法\",\"id\":\"1037600318445060096\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"36\",\"question\":\"习近平新时代中国特色社会主义思想——————。\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"2\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"201804280305T415526325805\",\"lpQuestionAnswerList\":[{\"contentType\":\"不得用于或者变相用于商标、商业广告\",\"id\":\"201804280305M374361849225\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"不得在私人丧事活动等不适宜的场合使用\",\"id\":\"201804280305U88054218457\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"不得作为公共场所的背景音乐等。\",\"id\":\"201804280305O997792209048\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"不得随便哼唱\",\"id\":\"201804280305I424487274844\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"37\",\"question\":\"国歌_______________(多选题)\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037889255130529792\",\"lpQuestionAnswerList\":[{\"contentType\":\"申请行政许可，行政机关拒绝或者在法定期限内不予答复，或者对行政机关作出的有关行政许可的其他决定不服的\",\"id\":\"1037889255151501312\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"对行政机关作出的关于确认土地、矿藏、水流、森林、山岭、草原、荒地、滩涂、海域等自然资源的所有权或者使用权的决定不服的\",\"id\":\"1037889255172472832\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"对征收、征用决定及其补偿决定不服的\",\"id\":\"1037889255193444352\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"申请行政机关履行保护人身权、财产权等合法权益的法定职责，行政机关拒绝履行或者不予答复的\",\"id\":\"1037889255214415872\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"38\",\"question\":\"《行政诉讼法》第十二条  人民法院受理公民、法人或者其他组织提起的下列诉讼：\t————————\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037623228144549888\",\"lpQuestionAnswerList\":[{\"contentType\":\"全面建成小康社会\",\"id\":\"1037623228165521408\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"全面深化改革\",\"id\":\"1037623228182298624\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"全面依法治国\",\"id\":\"1037623228203270144\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"全面从严治党\",\"id\":\"1037623228224241664\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"39\",\"question\":\"跨入新世纪，必须按照中国特色社会主义事业“五位一体”总体布局和“四个全面”战略布局，“四个全面”是指——————————————。\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037623228144549888\",\"lpQuestionAnswerList\":[{\"contentType\":\"全面建成小康社会\",\"id\":\"1037623228165521408\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"全面深化改革\",\"id\":\"1037623228182298624\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"全面依法治国\",\"id\":\"1037623228203270144\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"全面从严治党\",\"id\":\"1037623228224241664\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"40\",\"question\":\"跨入新世纪，必须按照中国特色社会主义事业“五位一体”总体布局和“四个全面”战略布局，“四个全面”是指——————————————。\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038686619198029824\",\"lpQuestionAnswerList\":[{\"contentType\":\"案件事实清楚，证据确实、充分，依据法律认定被告人有罪的，应当作出有罪判决\",\"id\":\"1038686619219001344\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"依据法律认定被告人无罪的，应当作出无罪判决\",\"id\":\"1038686619239972864\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"证据不足，不能认定被告人有罪的，应当作出证据不足、指控的犯罪不能成立的无罪判决\",\"id\":\"1038686619256750080\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"以民事处罚代替刑罚处罚\",\"id\":\"1038686619277721600\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"41\",\"question\":\"根据刑事诉讼法规定，人民法院在审理案件后应该分别情况作出以下判决：——————\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038689973101920256\",\"lpQuestionAnswerList\":[{\"contentType\":\"党的中央纪律检查机关\",\"id\":\"1038689973122891776\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"党中央有关工作机关\",\"id\":\"1038689973139668992\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"县级以上地方党委以及地方纪律检查机关\",\"id\":\"1038689973160640512\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"地方党委有关工作机关\",\"id\":\"1038689973177417728\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"42\",\"question\":\"————————————应当建立和完善党委新闻发言人制度，逐步建立例行发布制度，及时准确发布重要党务信息。\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"2\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"201804280335G699040463658\",\"lpQuestionAnswerList\":[{\"contentType\":\"公开\",\"id\":\"201804280335E323857713006\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"公平\",\"id\":\"201804280335X384490405779\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"公正\",\"id\":\"201804280335H969850402020\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"公有\",\"id\":\"201804280335Z946642821931\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"43\",\"question\":\"设定和实施行政许可，应当遵循_______________的原则。(多选题)\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"2\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"201804200530S766185809809\",\"lpQuestionAnswerList\":[{\"contentType\":\"中华人民共和国国旗是五星红旗。\",\"id\":\"201804200530P795121801159\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"中华人民共和国国歌是《义勇军进行曲》。\",\"id\":\"201804200530O332922669722\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"中华人民共和国国徽， 中间是五星照耀下的天安门， 周围是谷穗和齿轮。\",\"id\":\"201804200530L825040978168\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"中华人民共和国首都是北京\",\"id\":\"201804200530Q384068802570\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"44\",\"question\":\"下列说法正确的是——————(多选题)\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1038683797647785984\",\"lpQuestionAnswerList\":[{\"contentType\":\"宪法首先应该是常规的底线,培育宪法思维就是要形成底线思维。\",\"id\":\"1038683797664563200\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"培育宪法思维就是要形成约束意识，只有在宪法法律限定的框架内活动，坚持“法定职责必须为，法无授权不可为”。\",\"id\":\"1038683797685534720\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"宪法作为根本法具有法律的最高威慑性，一切违反宪法的行为都是危险的，都必须予以追究。\",\"id\":\"1038683797702311936\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"宪法指导党章的修订\",\"id\":\"1038683797723283456\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"45\",\"question\":\"必须解决领导干部的宪法思维问题,激活宪法思维，必须认识到——————————\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"201805301050R648015638028\",\"lpQuestionAnswerList\":[{\"contentType\":\"国家主权\",\"id\":\"201805301050J238890336590\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"安全\",\"id\":\"201805301050I959849602310\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"发展利益\",\"id\":\"201805301050Y703243707413\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"改革开放\",\"id\":\"201805301050B629595118751\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"46\",\"question\":\"党章指出要坚持总体国家安全观，坚决维护—————————————(多选题)\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037865096249868288\",\"lpQuestionAnswerList\":[{\"contentType\":\"制定与国家、省环境保护法律、法规和规定相违背的文件及规定的\",\"id\":\"1037865096270839808\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"因决策失误或者违反国家产业政策，造成本辖区环境质量下降，产生重大环境污染或者对生态环境产生破坏性影响和不可恢复利用的\",\"id\":\"1037865096291811328\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"未按要求完成本地区节能减排年度目标任务，或者其他环境保护目标任务的\",\"id\":\"1037865096316977152\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"年度改善生态环境专项考核不合格的\",\"id\":\"1037865096333754368\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"47\",\"question\":\"党委、政府在组织领导和决策过程中有下列行为之一的，应当问责：————————————\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"201805301048M218527268097\",\"lpQuestionAnswerList\":[{\"contentType\":\"民主法治\",\"id\":\"201805301048J663318256804\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"公平正义\",\"id\":\"201805301048X623932968684\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"诚信友爱\",\"id\":\"201805301048R816395865519\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"充满活力\",\"id\":\"201805301048U831678966477\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"48\",\"question\":\"中国共产党领导人民构建社会主义和谐社会。按照————————————、安定有序、人与自然和谐相处的总要求和共同建设、共同享有的原则，以保障和改善民生为重点，解决好人民最关心、最直接、最现实的利益问题。(多选题)\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037620489209511936\",\"lpQuestionAnswerList\":[{\"contentType\":\"科学立法\",\"id\":\"1037620489230483456\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"严格执法\",\"id\":\"1037620489247260672\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"公正司法\",\"id\":\"1037620489268232192\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"全民守法\",\"id\":\"1037620489289203712\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"49\",\"question\":\"组建中央全面依法治国委员会，要统筹推进————————，协调推进中国特色社会主义法治体系和社会主义法治国家建设等。\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"},{\"contentType\":\"\",\"depTypeCode\":\"0\",\"depTypeName\":\"全行业\",\"id\":\"1037623228144549888\",\"lpQuestionAnswerList\":[{\"contentType\":\"全面建成小康社会\",\"id\":\"1037623228165521408\",\"isCorrect\":\"0\",\"optionNum\":\"A\"},{\"contentType\":\"全面深化改革\",\"id\":\"1037623228182298624\",\"isCorrect\":\"0\",\"optionNum\":\"B\"},{\"contentType\":\"全面依法治国\",\"id\":\"1037623228203270144\",\"isCorrect\":\"0\",\"optionNum\":\"C\"},{\"contentType\":\"全面从严治党\",\"id\":\"1037623228224241664\",\"isCorrect\":\"0\",\"optionNum\":\"D\"}],\"num\":\"50\",\"question\":\"跨入新世纪，必须按照中国特色社会主义事业“五位一体”总体布局和“四个全面”战略布局，“四个全面”是指——————————————。\",\"questionDifficulty\":\"0\",\"questionType\":\"1\",\"trueAnswer\":\"\"}]";
                    dynamic d = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(html);
                    foreach (var d1 in d)
                    {
                        try
                        {
                            ExamsDAL.AddExam(2018, d1.question.ToString(), d1.lpQuestionAnswerList.ToString(), Convert.ToInt32(d1.questionType), " ", false, DateTime.Now);
                        }
                        catch(Exception ex) { }
                    }

                }

                
                string js = @"
                            function mydati(question_detail){
                                $.get(""/mygetquestion?question=""+question_detail.question,function(data,status){
                                        $(""input[name = 'answer']"").each(function(){
                                            if (data.indexOf($(this).val())>-1) {
						                        $(this).checked = true;
					                        }
                                          });
                                    });
                                }
                                    ";

                bool r = oSession.utilReplaceInResponse("setButtonDisable(question_no);", "setButtonDisable(question_no);mydati(question_detail);");
            }
        }
    }
}
