using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NtCore.Clients.Remote.Network
{
    public interface INetworkClient : IDisposable
    {
        event Action<string> PacketReceived;

        Task<IEnumerable<string>> ReceivePackets();
        Task SendPacket(string packet, bool session = false);
        Task<bool> Connect(string ip, short port);
        void Disconnect();
    }
}