namespace 贵州省干部在线学习助手.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xilium.CefGlue;
    //using Xilium.CefGlue.Wrapper;
    class DemoRenderProcessHandler : CefRenderProcessHandler
    {
        internal static bool DumpProcessMessages { get; private set; }

        public DemoRenderProcessHandler()
        {
            //MessageRouter = new CefMessageRouterRendererSide(new CefMessageRouterConfig());
        }
        #region js to obj
        #region 声明常量变量
        /// <summary>
        /// 绑定测试处理器A
        /// </summary>
        private ExampleAv8Handler exampleA;

        #endregion 声明常量变量
        protected override void OnWebKitInitialized()
        {
            #region test
            #region 原生方式注册 ExampleA
            exampleA = new ExampleAv8Handler();
            const string exampleAJavascriptCode = @"function exampleA() {}

            if (!exampleA) exampleA = {};

            (function() {

                exampleA.__defineGetter__('myParam',

                function() {

                    native function GetMyParam();

                    return GetMyParam();

                });

                exampleA.__defineSetter__('myParam',

                function(arg0) {

                    native function SetMyParam(arg0);

                    SetMyParam(arg0);

                });

                exampleA.myFunction = function() {

                    native function MyFunction();

                    return MyFunction();

                };

                exampleA.getMyParam = function() {

                    native function GetMyParam();

                    return GetMyParam();

                };

                exampleA.setMyParam = function(arg0) {

                    native function SetMyParam(arg0);

                    SetMyParam(arg0);

                };
                exampleA.getExamData = function(arg0) {
                    native function getExamData(arg0);
                    return getExamData(arg0);

                };
                exampleA.getExam = function(arg0) {
                    native function getExam(arg0);
                    return getExam(arg0);
                };
                exampleA.setExam = function(arg0,arg1,arg2,arg3) {
                    native function setExam(arg0,arg1,arg2,arg3);
                    return setExam(arg0,arg1,arg2,arg3);
                };

            })();";

            CefRuntime.RegisterExtension("exampleAExtensionName", exampleAJavascriptCode, exampleA);

            #endregion 原生方式注册 ExampleA
            #endregion test
            base.OnWebKitInitialized();
        }
        #endregion js to obj
        //internal CefMessageRouterRendererSide MessageRouter { get; private set; }

        protected override void OnContextCreated(CefBrowser browser, CefFrame frame, CefV8Context context)
        {
            //MessageRouter.OnContextCreated(browser, frame, context);

            // MessageRouter.OnContextCreated doesn't capture CefV8Context immediately,
            // so we able to release it immediately in this call.
            context.Dispose();
        }

        protected override void OnContextReleased(CefBrowser browser, CefFrame frame, CefV8Context context)
        {
            // MessageRouter.OnContextReleased releases captured CefV8Context (if have).
            //MessageRouter.OnContextReleased(browser, frame, context);

            // Release CefV8Context.
            context.Dispose();
        }

        protected override bool OnProcessMessageReceived(CefBrowser browser, CefFrame frame, CefProcessId sourceProcess, CefProcessMessage message)
        {
            if (DumpProcessMessages)
            {
                Console.WriteLine("Render::OnProcessMessageReceived: SourceProcess={0}", sourceProcess);
                Console.WriteLine("Message Name={0} IsValid={1} IsReadOnly={2}", message.Name, message.IsValid, message.IsReadOnly);
                var arguments = message.Arguments;
                for (var i = 0; i < arguments.Count; i++)
                {
                    var type = arguments.GetValueType(i);
                    object value;
                    switch (type)
                    {
                        case CefValueType.Null: value = null; break;
                        case CefValueType.String: value = arguments.GetString(i); break;
                        case CefValueType.Int: value = arguments.GetInt(i); break;
                        case CefValueType.Double: value = arguments.GetDouble(i); break;
                        case CefValueType.Bool: value = arguments.GetBool(i); break;
                        default: value = null; break;
                    }

                    Console.WriteLine("  [{0}] ({1}) = {2}", i, type, value);
                }
            }

            //var handled = MessageRouter.OnProcessMessageReceived(browser, sourceProcess, message);
            //if (handled) return true;

            if (message.Name == "myMessage2") return true;

            var message2 = CefProcessMessage.Create("myMessage2");
            frame.SendProcessMessage(CefProcessId.Renderer, message2);
            Console.WriteLine("Sending myMessage2 to renderer process = {0}");

            var message3 = CefProcessMessage.Create("myMessage3");
            frame.SendProcessMessage(CefProcessId.Browser, message3);
            Console.WriteLine("Sending myMessage3 to browser process = {0}");

            return false;
        }
    }
}
