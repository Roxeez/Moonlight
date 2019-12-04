using System;

namespace NtCore.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string message) => Console.WriteLine($"[DEBUG] {message}");

        public void Information(string message) => Console.WriteLine($"[INFO] {message}");

        public void Warning(string message) => Console.WriteLine($"[WARN] {message}");

        public void Error(string message) => Console.WriteLine($"[ERROR] {message}");
    }
}