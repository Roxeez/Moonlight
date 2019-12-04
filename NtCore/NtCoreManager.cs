using System;
using System.Diagnostics;
using NtCore.Core;
using NtCore.Core.Impl;
using NtCore.Import;
using NtCore.Network;

namespace NtCore
{
    public class ClientFactory
    {
        private readonly IPacketManager _packetManager;
        
        public ClientFactory(IPacketManager packetManager)
        {
            _packetManager = packetManager;
        }
        
        public IClient CreateClient()
        {
            Process process = Process.GetCurrentProcess();

            if (process.MainModule == null)
            {
                throw new InvalidOperationException("Process module can't be null");
            }
            
            
            IClient client = new Client(process.MainModule);

            client.PacketReceived += packet => _packetManager.Handle(client, packet, PacketType.Recv);
            client.PacketSend += packet => _packetManager.Handle(client, packet, PacketType.Send);

            return client;
        }
    }
}