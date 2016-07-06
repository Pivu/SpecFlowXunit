using NLog;
using System.Diagnostics;
using TechTalk.SpecFlow.Tracing;

namespace Logging
{
    [DebuggerStepThrough]
    public class SpecFlowTestListener : IThreadSafeTraceListener
    {
        Logger _listener = LogManager.GetCurrentClassLogger();

        public void WriteTestOutput(string message)
        {
            _listener.Trace(message);
        }

        public void WriteToolOutput(string message)
        {
            if (!message.Contains("done: "))
            {
                if (message.Contains("(Session info:"))
                {
                    message = message.Substring(0, message.IndexOf("(Session info:"));
                }
                _listener.Trace(" -> " + message);
            }
        }
    }

    [DebuggerStepThrough]
    public class NLogDebugListener : TraceListener
    {
        Logger _listener = LogManager.GetCurrentClassLogger();

        public override void Write(string message)
        {
            _listener.Info(message);
        }

        public override void WriteLine(string message)
        {
            _listener.Info(message);
        }

        public override void WriteLine(string message, string category)
        {
            _listener.Info(string.Format(message, category));
        }

        public override void WriteLine(object o)
        {
            _listener.Info(o.ToString());
        }
    }
}
