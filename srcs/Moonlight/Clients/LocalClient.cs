using Moonlight.Core.Import;

namespace Moonlight.Clients
{
    public sealed class LocalClient : Client
    {
        /// <summary>
        ///     Need to keep a reference to both callback to avoid GC
        /// </summary>
        private readonly Moon.PacketCallback _recvCallback;
        private readonly Moon.PacketCallback _sendCallback;


        public LocalClient()
        {
            _sendCallback = OnPacketSend;
            _recvCallback = OnPacketReceived;

            Moon.Initialize();

            Moon.SetSendCallback(_sendCallback);
            Moon.SetRecvCallback(_recvCallback);
        }

        public override void SendPacket(string packet)
        {
            Moon.SendPacket(packet);
        }

        public override void ReceivePacket(string packet)
        {
            Moon.RecvPacket(packet);
        }

        public override void Dispose()
        {
            Moon.Clean();
        }
    }
}