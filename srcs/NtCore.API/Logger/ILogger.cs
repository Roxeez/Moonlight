namespace NtCore.API.Logger
{
    public interface ILogger
    {
        void Debug(string message);
        void Information(string message);
        void Warning(string message);
        void Error(string message);
    }
}