using System;
using NtCore.API.Logger;

namespace NtCore.API.Logger
{
    public class ConsoleLogger : ILogger
    {
        private readonly string _prefix;

        public ConsoleLogger(string prefix)
        {
            _prefix = prefix;
        }
        
        public void Debug(string message)
        {
            Console.WriteLine($"[DEBUG][{_prefix}] {message}");
        }

        public void Information(string message)
        {
            Console.WriteLine($"[INFO][{_prefix}] {message}");
        }

        public void Warning(string message)
        {
            Console.WriteLine($"[WARN][{_prefix}] {message}");
        }

        public void Error(string message)
        {
            Console.WriteLine($"[ERROR][{_prefix}] {message}");
        }
    }
}