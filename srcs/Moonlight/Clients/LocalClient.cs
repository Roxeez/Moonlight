using System;
using Moonlight.Clients.Local;
using Moonlight.Core.Interop;

namespace Moonlight.Clients
{
    public sealed class LocalClient : Client
    {
        /// <summary>
        ///     Need to keep a reference to both callback to avoid GC
        /// </summary>
        private readonly Native.PacketCallback _recvCallback;
        private readonly Native.PacketCallback _sendCallback;

        public Window Window { get; }

        public LocalClient(Window window)
        {
            Window = window;
            
            _sendCallback = OnPacketSend;
            _recvCallback = OnPacketReceived;

            Native.Initialize();

            Native.SetSendCallback(_sendCallback);
            Native.SetRecvCallback(_recvCallback);
        }

        public override void SendPacket(string packet)
        {
            Native.SendPacket(packet);
        }

        public override void ReceivePacket(string packet)
        {
            Native.RecvPacket(packet);
        }

        public override void Dispose()
        {
            Native.Clean();
        }
    }
}