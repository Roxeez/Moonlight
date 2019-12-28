using JetBrains.Annotations;

namespace NtCore.Clients.Remote
{
    public sealed class WorldServer : Server
    {
        internal WorldServer([NotNull] string ip, short port) : base(ip, port)
        {
        }
    }
}