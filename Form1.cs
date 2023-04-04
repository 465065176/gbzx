using Fiddler;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WebHelper;
using WebHelper.Common;
using Xilium.CefGlue;

namespace 贵州省干部在线学习助手
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CreatePorxy();
            //Helper helper = new Helper(true);
            //helper.Initialize();//初始化
            //helper.FiddlerApplication_BeforeRequest += FiddlerApplication_BeforeRequest;
            //helper.FiddlerApplication_BeforeResponse += FiddlerApplication_BeforeResponse;
            ChromePath = ini.ReadString("chromepath", "chromepath", "chrome").Replace("\0", "");
            UserName = ini.ReadString("name", "name", "").Replace("\0", "").Trim();
            UserPwd = ini.ReadString("pwd", "pwd", "").Replace("\0", "").Trim();
            UserExtend1 = ini.ReadString("extend1", "extend1", "").Replace("\0", "");
            UserExtend2 = ini.ReadString("extend2", "extend2", "").Replace("\0", "");
            UserExtend3 = ini.ReadString("extend3", "extend3", "").Replace("\0", "");
            RunInternal(null);
            InitializeComponent();
            
        }
        static string path = new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase).LocalPath;
        FXNET.INI.IniFiles ini = new FXNET.INI.IniFiles(System.IO.Path.GetDirectoryName(path) + "/config/zh.ini");
        FXNET.INI.IniFiles portini = new FXNET.INI.IniFiles(Path.GetPathRoot(path) + "chromePort/port.ini");
        public static string UserName { get; set; }
        public static string UserPwd { get; set; }
        public static string UserExtend1 { get; set; }
        public static string UserExtend2 { get; set; }
        public static string UserExtend3 { get; set; }
        public static string ChromePath { get; set; }
        static Proxy oSecureEndpoint;
        static string sSecureEndpointHostname = "localhost";
        static int iSecureEndpointPort = 7777;
        private bool isstart = false;
        public static string startUrl="";
        public void CreatePorxy()
        {
            //设置别名  
            Fiddler.FiddlerApplication.SetAppDisplayName("FiddlerCoreDemoApp");
            //启动方式
            string name = Path.GetFileName(path).Replace(Path.GetExtension(path), "");
            int port = portini.ReadInteger("ports", name, 0);
            if (port == 0)
            {
                NameValueCollection sc = new NameValueCollection();
                portini.ReadSectionValues("ports", sc);
                port = 10000 + sc.Count * 100 + 100;
                portini.WriteInteger("ports", name, port);
            }
            //定义http代理端口  
            int iPort = PortHelper.GetFirstAvailablePort(port);
            Helper.Port = iPort;
            //忽略服务器证书错误
            CONFIG.IgnoreServerCertErrors = true;
            FiddlerApplication.Prefs.SetBoolPref("fiddler.network.streaming.abortifclientaborts", true);
            //信任证书
            //if (!CertMaker.rootCertExists())
            //{
            //    if (!CertMaker.createRootCert())
            //    { }

            //    if (!CertMaker.trustRootCert())
            //    { }

            //    string Cert = FiddlerApplication.Prefs.GetStringPref("fiddler.certmaker.bc.cert", null);
            //    string Key = FiddlerApplication.Prefs.GetStringPref("fiddler.certmaker.bc.key", null);
            //}
            #region -----------处理证书-----------
            ////伪造的证书
            //X509Certificate2 oRootCert;
            ////如果没有伪造过证书并把伪造的证书加入本机证书库中
            //if (null == CertMaker.GetRootCertificate())
            //{
            //    //创建伪造证书
            //    CertMaker.createRootCert();

            //    //重新获取
            //    oRootCert = CertMaker.GetRootCertificate();

            //    //打开本地证书库
            //    X509Store certStore = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            //    certStore.Open(OpenFlags.ReadWrite);
            //    try
            //    {
            //        //将伪造的证书加入到本地的证书库
            //        certStore.Add(oRootCert);
            //    }
            //    finally
            //    {
            //        certStore.Close();
            //    }
            //}
            //else
            //{
            //    //以前伪造过证书，并且本地证书库中保存过伪造的证书
            //    oRootCert = CertMaker.GetRootCertificate();
            //}

            ////-----------------------------

            ////指定伪造证书
            //FiddlerApplication.oDefaultClientCertificate = oRootCert;
            ////忽略服务器证书错误
            //CONFIG.IgnoreServerCertErrors = true;
            ////信任证书
            //CertMaker.trustRootCert();
            ////看字面意思知道是啥，但实际起到啥作用。。。鬼才知道，官方例程里有这句，加上吧，管它呢。
            //FiddlerApplication.Prefs.SetBoolPref("fiddler.network.streaming.abortifclientaborts", true);
            #endregion
            //启动代理程序，开始监听http请求  
            FiddlerCoreStartupSettings startupSettings =
                new FiddlerCoreStartupSettingsBuilder()
                    .ListenOnPort((ushort)iPort)
                    //.RegisterAsSystemProxy()
                    .DecryptSSL()
                    .AllowRemoteClients()
                    //.ChainToUpstreamGateway()
                    //.MonitorAllConnections()
                    //.HookUsingPACFile()
                    //.CaptureLocalhostTraffic()
                    //.CaptureFTP()
                    .OptimizeThreadPool()
                    //.SetUpstreamGatewayTo("http=CorpProxy:80;https=SecureProxy:443;ftp=ftpGW:20")
                    .Build();
            FiddlerApplication.Startup(startupSettings);
            //oSecureEndpoint = FiddlerApplication.CreateProxyEndpoint(iSecureEndpointPort, true, sSecureEndpointHostname);
            //if (null != oSecureEndpoint)
            //{
            //    FiddlerApplication.Log.LogFormat("Created secure endpoint listening on port {0}, using a HTTPS certificate for '{1}'", iSecureEndpointPort, sSecureEndpointHostname);
            //}

            Fiddler.FiddlerApplication.BeforeRequest += FiddlerApplication_BeforeRequest;
            Fiddler.FiddlerApplication.BeforeResponse += FiddlerApplication_BeforeResponse;
            //CONFIG.bDebugSpew = true;控制台显示调试信息log
        }
        private int RunInternal(string[] args)
        {
            CefRuntime.Load(ChromePath);//cef路径

            var settings = new CefSettings();
            //如果不开启好多用那种JSUI的控件的网站 什么的全死   
            settings.WindowlessRenderingEnabled = true;
            settings.MultiThreadedMessageLoop = CefRuntime.Platform == CefRuntimePlatform.Windows;
            
            settings.LogSeverity = CefLogSeverity.Error;
            string path = new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase).LocalPath;
            string logpath = Path.GetPathRoot(path) + "ChromeCache/" + Path.GetFileName(path).Replace(Path.GetExtension(path), "") + "/Log/" + UserName;
            if (!Directory.Exists(logpath))
            {
                Directory.CreateDirectory(logpath);
            }
            settings.LogFile = logpath + "/cef.log";
            settings.Locale = "zh_CN";
            //settings.ResourcesDirPath = System.IO.Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase).LocalPath);
            settings.RemoteDebuggingPort = 20480;
            settings.NoSandbox = true;
            settings.UserDataPath = Path.GetPathRoot(path) + "ChromeCache/" + Path.GetFileName(path).Replace(Path.GetExtension(path), "") + "/UserData/" + UserName + "";
            settings.CachePath = Path.GetPathRoot(path) + "ChromeCache/" + Path.GetFileName(path).Replace(Path.GetExtension(path), "") + "/Cache/" + UserName + "";
            //settings.UserDataPath = System.IO.Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase).LocalPath) + "/UserData/" + UserName + "/UserData";
            //settings.CachePath = System.IO.Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase).LocalPath) + "/UserData/" + UserName + "/Cache";
            //settings.CachePath = null;//设置为null,隐身模式，内存中缓存
            settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36";
            var argv = args;
            if (CefRuntime.Platform != CefRuntimePlatform.Windows)
            {
                argv = new string[args.Length + 1];
                Array.Copy(args, 0, argv, 1, args.Length);
                argv[0] = "-";
            }

            var mainArgs = new CefMainArgs(argv);
            dynamic app = new MyCefApp();
            var exitCode = CefRuntime.ExecuteProcess(mainArgs, app, IntPtr.Zero);
            Console.WriteLine("CefRuntime.ExecuteProcess() returns {0}", exitCode);
            if (exitCode != -1)
                return exitCode;

            // guard if something wrong
            //foreach (var arg in args) { if (arg.StartsWith("--type=")) { return -2; } }

            CefRuntime.Initialize(mainArgs, settings, app, IntPtr.Zero);

            return 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Text = UserName;
        }

        void FiddlerApplication_BeforeRequest(Session oSession)
        {
            if (oSession.fullUrl.IndexOf("www.qzgj.gov.cn") > 0)
            {//晴镇市干部
                qzgj.FiddlerApplication_BeforeRequest(oSession);
            }
            else
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
            if (oSession.fullUrl.IndexOf("202.98.201.161") > 0)//兴义市干部
            {
                xysgb.FiddlerApplication_BeforeRequest(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("www.wlmqpxw.com") > 0)//乌鲁木齐人力资源培训网
            {
                wlmqpxw.FiddlerApplication_BeforeRequest(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("203.93.109.222") > 0)//重庆质检系统网络学习平台
            {
                cqjcxt.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf(".hngbjy.com") > 0)//湖南干部教育
            {
                hngbjy.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf(".chaoxing.com") > 0)//贵州工程应用技术学院-国家级专业技术人员继续教育网络学习平台
            {
                chaoxing.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf(".ggcjxjy.cn") > 0)//贵州工程应用技术学院-国家级专业技术人员继续教育网络学习平台
            {
                ggcjxjy.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("site.bbtree.com") > 0)//开州云枫幼儿园抢购
            {
                bbtree.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("61.128.194.102") > 0)//重庆市江北执法
            {
                cqjbzf.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("yanxiuonline.com") > 0)//研修在线
            {
                yanxiuonline.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("api.qingshuxuetang.com") > 0)//青书堂
            {
                qinshutang.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("edu.xjcxedu.com") > 0)//新疆基础教育资源公共服务平台edu.xjcxedu.com
            {
                xjjcjy.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf(".59iedu.com") > 0)//青海大学继续教育学院专业技术人员继续教育平台
            {
                qdjj.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("teacheredu.cn") > 0)//教师教育网
            {
                teacheredu.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("wlcb.zgrsw.cn") > 0)//乌兰察布专业技术人员继续教育在线学习
            {
                wlcb.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("ejxjy.com") > 0)//湖南师范大学技术人员继续教育网
            {
                ejxjy.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("guizhou.zxjxjy.com") > 0)//贵州继续教育网
            {
                zxjxjy.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("hn.webtrn.cn") > 0)//湖南事业单位工作人员培训网络平台
            {
                webtrn.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("haoyisheng.com") > 0)//好医生app
            {
                haoyisheng.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("91huayi.com") > 0)//91huayi app
            {
                huayi.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("hnsydwpx.cn") > 0)//hnsydwpx.cn 湖南省事业单位工作人员培训网
            {
                hnsydwpx.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("gzwy.gov.cn") > 0)//gzwy.gov.cn 贵州省党员干部网络学院
            {
                gzwy.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("hn.ischinese.cn") > 0)//hn.ischinese.cn 湖南省事业单位工作人员培训网
            {
                ischinese.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("newnhc-p.webtrn.cn") > 0)//newnhc-p.webtrn.cn 国家卫生健康委远程教育培训平台
            {
                newnhcpwebtrn.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("newnhc-kfkc.webtrn.cn") > 0)//newnhc-p.webtrn.cn 国家卫生健康委远程教育培训平台
            {
                newnhcpwebtrn.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("gzhkzy.cjnep.net") > 0)//http://gzhkzy.cjnep.net/ 贵州航天职业技术学院学习平台
            {
                cjnep.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("www.dmhhyy.com") > 0)//www.dmhhyy.com 黔南智慧司法
            {
                dmhhyy.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("yanxiu.com") > 0 | oSession.fullUrl.IndexOf("3ren.cn") > 0)//yanxiu.com 中国教师研修网
            {
                yanxiu.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("mchtraweb.chinawch.org.cn") > 0)//https://mchtraweb.chinawch.org.cn/
            {
                mchtraweb.FiddlerApplication_BeforeRequest(oSession);
            }
            else if (oSession.fullUrl.IndexOf("gz-smch-home.chengdumaixun.com") > 0)//https://gz-smch-home.chengdumaixun.com/
            {
                chengdumaixun.FiddlerApplication_BeforeRequest(oSession);
            }
            else
           if (
               (oSession.url.IndexOf("222.133.41.36:8013/jpwspx/kj.asp") >= 0) ||//德州市营运车辆从业人员网上培训系统
               (oSession.url.IndexOf("yanxiu.jsyxsq.com/proj/studentwork/study.htm?") >= 0) ||//教师研修社区（国培）
               (oSession.url.IndexOf("selfmade/play/player.htm") > 0) ||
               (oSession.url.IndexOf("/play.htm") > 0) ||
               (oSession.url.IndexOf("/elms/web/login.action") > 0) ||
               (oSession.url.IndexOf("/elms/web/re_login.jsp") > 0) ||
               (oSession.url.IndexOf("jquery.course.framework.js") > 0) ||
               (oSession.url.IndexOf("controlFunction.js") > 0) ||
               (oSession.url.IndexOf("jquery.course.player.flowplayer.js") > 0) ||
               (oSession.url.IndexOf("/data/data.xml") > 0) ||
               (oSession.url.IndexOf("jquery.course.test.js") > 0) ||
               (oSession.url.IndexOf("index.htm") > 0) ||
               (oSession.url.IndexOf("/elms/web/course/getExamQuestion.action") > 0) ||
               (oSession.url.IndexOf("/elms/web/course/getPaperResult.action") > 0) ||
               (oSession.url.IndexOf("/elms/web/course/questionTempSave.action") > 0) ||
               (oSession.url.IndexOf("/elms/web/course/getCourseExam.action") > 0) ||
               (oSession.url.IndexOf("/elms/web/course/listMyCourses.action") > 0)
               )
            {
                //词句代码必须，不然无法修改返回数据
                oSession.bBufferResponse = true;
            }
            else if ((oSession.url.IndexOf("/linkAICC.js") > 0))
            {
                oSession.bBufferResponse = true;
            }
            else if ((oSession.url.IndexOf(".mp4") > 0))
            {
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
            if (oSession.fullUrl.IndexOf("zgzjzj.com") > 0)
            {
                zgzjzj.FiddlerApplication_BeforeResponse(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("xfks.qnfzw.gov.cn") > 0)
            {
                qnfzw.FiddlerApplication_BeforeResponse(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("202.98.201.161") > 0)//兴义市干部
            {
                xysgb.FiddlerApplication_BeforeResponse(oSession);
            }
            else
            if (oSession.fullUrl.IndexOf("www.wlmqpxw.com") > 0)//乌鲁木齐人力资源培训网
            {
                wlmqpxw.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf(".chaoxing.com") > 0)//贵州工程应用技术学院-国家级专业技术人员继续教育网络学习平台
            {
                chaoxing.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf(".ggcjxjy.cn") > 0)//贵州工程应用技术学院-国家级专业技术人员继续教育网络学习平台
            {
                ggcjxjy.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.url.IndexOf("222.133.41.36:8013/jpwspx/kj.asp") >= 0)
            {
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("alert('您刚才的10分钟学时以记录！')", "");
            }
            else if (oSession.url.IndexOf("yanxiu.jsyxsq.com/proj/studentwork/study.htm?") >= 0)//教师研修社区（国培）
            {
                oSession.utilDecodeResponse();
                bool r = oSession.utilReplaceInResponse("alert(\"已经学习了\"+tishiTime+\",点击确定更新学习时间.\")", "console.log('已经学习了'+tishiTime+',点击确定更新学习时间.')");
            }
            else if (oSession.fullUrl.IndexOf("203.93.109.222") > 0)//重庆质检系统网络学习平台
            {
                cqjcxt.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf(".hngbjy.com") > 0)//湖南干部教育
            {
                hngbjy.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("site.bbtree.com") > 0)//开州云枫幼儿园抢购
            {
                bbtree.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("61.128.194.102") > 0)//重庆市江北执法
            {
                cqjbzf.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("yanxiuonline.com") > 0)//研修在线
            {
                yanxiuonline.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("api.qingshuxuetang.com") > 0)//青书堂
            {
                qinshutang.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("edu.xjcxedu.com") > 0)//新疆基础教育资源公共服务平台edu.xjcxedu.com
            {
                xjjcjy.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf(".59iedu.com") > 0)//青海大学继续教育学院专业技术人员继续教育平台
            {
                qdjj.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("teacheredu.cn") > 0)//教师教育网
            {
                teacheredu.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("wlcb.zgrsw.cn") > 0)//乌兰察布专业技术人员继续教育在线学习
            {
                wlcb.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("ejxjy.com") > 0)//湖南师范大学技术人员继续教育网
            {
                ejxjy.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("guizhou.zxjxjy.com") > 0)//贵州继续教育网
            {
                zxjxjy.FiddlerApplication_BeforeResponse(oSession);
            }

            else if (oSession.fullUrl.IndexOf("hn.webtrn.cn") > 0)//湖南事业单位工作人员培训网络平台
            {
                webtrn.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("haoyisheng.com") > 0)//好医生app
            {
                haoyisheng.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("91huayi.com") > 0)//91huayi app
            {
                huayi.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("hnsydwpx.cn") > 0)//hnsydwpx.cn 湖南省事业单位工作人员培训网
            {
                hnsydwpx.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("gzwy.gov.cn") > 0)//gzwy.gov.cn 贵州省党员干部网络学院
            {
                gzwy.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("hn.ischinese.cn") > 0)//hn.ischinese.cn 湖南省事业单位工作人员培训网
            {
                ischinese.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("newnhc-kfkc.webtrn.cn") > 0)//newnhc-kfkc.webtrn.cn 国家卫生健康委远程教育培训平台
            {
                newnhcpwebtrn.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("newnhc-p.webtrn.cn") > 0)//newnhc-p.webtrn.cn 国家卫生健康委远程教育培训平台
            {
                newnhcpwebtrn.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("gzhkzy.cjnep.net") > 0)//http://gzhkzy.cjnep.net/ 贵州航天职业技术学院学习平台
            {
                cjnep.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("www.dmhhyy.com") > 0)//www.dmhhyy.com 黔南智慧司法
            {
                dmhhyy.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("yanxiu.com") > 0|| oSession.fullUrl.IndexOf("3ren.cn") > 0)//yanxiu.com 中国教师研修网
            {
                yanxiu.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("mchtraweb.chinawch.org.cn") > 0)//https://mchtraweb.chinawch.org.cn/
            {
                mchtraweb.FiddlerApplication_BeforeResponse(oSession);
            }
            else if (oSession.fullUrl.IndexOf("gz-smch-home.chengdumaixun.com") > 0)//https://gz-smch-home.chengdumaixun.com/
            {
                chengdumaixun.FiddlerApplication_BeforeResponse(oSession);
            }
            //
            else

           if (oSession.url.IndexOf("/linkAICC.js") > 0)
            {
                oSession.utilDecodeResponse();
                oSession.ResponseHeaders["Content-Type"] = "application/javascript; charset=GB2312";
                bool r = oSession.utilReplaceInResponse("setInterval(\"doGetInter(\" + flag + \")\", 200)", "setInterval(\"doGetInter(\" + flag + \")\", 2000)");
            }
            else if (oSession.url.IndexOf("/selfmade/play/player.htm") > 0)
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
            else if ((oSession.url.IndexOf("/play.htm") > 0))
            {
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
                bool r = oSession.utilReplaceInResponse("<input type=\"text\" name=\"loginId\" size=\"12\" class=\"input1\" value=\"\"", "<input type=\"text\" name=\"loginId\" size=\"12\" class=\"input1\" value=\"" + Helper.UserName + "\"");
                r = oSession.utilReplaceInResponse("<input type=\"password\" name=\"password\" size=\"12\" class=\"input1\" value=\"\"", "<input type=\"password\" name=\"password\" size=\"12\" class=\"input1\" value=\"" + Helper.UserPwd + "\"");
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
                r = oSession.utilReplaceInResponse("switch_focus = true", "switch_focus = false");

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
                                        //ExamsDAL.UpExam(Convert.ToInt32(dt.Rows[0]["ID"]), ExamAnswer, true);
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
            isstart = false;
            Thread.Sleep(500);  
        }

        private void cefWebBrowser1_BeforePopup(object sender, Xilium.CefGlue.WindowsForms.BeforePopupEventArgs e)
        {
            try
            {
                this.cefWebBrowser1.Browser.GetMainFrame().LoadUrl(e.TargetUrl);
            }
            catch { }
            e.Handled = true;//结束创建新窗口的流程

        }
        private void button1_Click(object sender, EventArgs e)
        {
            cefWebBrowser1.Browser.GetMainFrame().LoadUrl(comboBox1.Text);
            startUrl = comboBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var host = cefWebBrowser1.Browser.GetHost();
            var wi = CefWindowInfo.Create();
            wi.SetAsPopup(IntPtr.Zero, "DevTools");
            host.ShowDevTools(wi, new DevToolsWebClient(), new CefBrowserSettings(), new CefPoint(0, 0));
        }
        private class DevToolsWebClient : CefClient
        {
        }
        Random random = new Random();
        private void cefWebBrowser1_BrowserCreated(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(delegate
            {
                int errorCount = 0;
                while (isstart)
                {
                    try
                    {
                        BeginInvoke(new Action(() =>
                        {
                            if (this.cefWebBrowser1.Browser.GetMainFrame().Url !=null)
                            {
                                if (this.cefWebBrowser1.Browser.GetMainFrame().Url.IndexOf("qdjj.59iedu.com/play/") > 0||
                                    this.cefWebBrowser1.Browser.GetMainFrame().Url.IndexOf("hwexamv1.59iedu.com/exam/") > 0
                                    )
                                this.cefWebBrowser1.Browser.GetMainFrame().ExecuteJavaScript("go();", "", 0);
                            }
                            var visitor = new SourceVisitor(text =>
                            {
                                string h = text;h=h == null ? "" : h;
                                if (h == "<html><head></head><body></body></html>")
                                {
                                    errorCount++;
                                }
                                else if (h.IndexOf("h1>503 Service Temporarily Unavailable</h1>") > 0)
                                {
                                    errorCount++;
                                }
                                else if (h.IndexOf("\"msg\":\"运行时异常:Index: 0, Size: 0\"") > 0)
                                {
                                    errorCount++;
                                }
                                else
                                {
                                    errorCount = 0;
                                }
                                if (errorCount == 10)
                                {
                                    errorCount = 0;
                                    button1_Click(null, null);
                                }
                            });
                            cefWebBrowser1.Browser.GetMainFrame().GetSource(visitor);

                        }));
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        Thread.Sleep(1000 * random.Next(10, 15));
                    }
                }
            }));
            isstart = true;
            t.Start();
        }

        private void cefWebBrowser1_Click(object sender, EventArgs e)
        {

        }
    }
}
