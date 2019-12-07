using System;

namespace NtCore.API.Logger
{
    public class ConsoleLogger : ILogger
    {
        private readonly string _prefix;

        public ConsoleLogger() : this("NtCore")
        {
        }

        public ConsoleLogger(string prefix) => _prefix = prefix;

        public void Debug(string message)
        {
            Console.WriteLine($"[{_prefix}][DEBUG] {message}");
        }

        public void Information(string message)
        {
            Console.WriteLine($"[{_prefix}][INFO] {message}");
        }

        public void Warning(string message)
        {
            Console.WriteLine($"[WARN][{_prefix}][WARN] {message}");
        }

        public void Error(string message)
        {
            Console.WriteLine($"[{_prefix}][ERROR] {message}");
        }
    }
}