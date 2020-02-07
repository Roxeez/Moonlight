using System;

namespace Moonlight.Core.Logging
{
    public interface ILogger
    {
        void Debug(object message);
        void Info(object message);
        void Warn(object message);
        void Error(object message);
        void Error(object message, Exception ex);
        void Fatal(object message, Exception ex);
    }
}