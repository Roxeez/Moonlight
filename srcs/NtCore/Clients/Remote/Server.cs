using JetBrains.Annotations;

namespace NtCore.Clients.Remote
{
    public abstract class Server
    {
        public string Ip { get; }
        public short Port { get; }
        
        public Server([NotNull] string ip, short port)
        {
            Ip = ip;
            Port = port;
        }
    }
}