using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xilium.CefGlue;

namespace 贵州省干部在线学习助手
{
    public sealed class SourceVisitor : CefStringVisitor
    {
        private readonly Action<string> _callback;
        public string value;

        public SourceVisitor(Action<string> callback)
        {
            _callback = callback;
        }

        protected override void Visit(string value)
        {
            this.value = value;
            _callback(value);
        }
    }
}
