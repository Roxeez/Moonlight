using Moonlight.Core.Import;

namespace Moonlight.Clients
{
    public sealed class LocalClient : Client
    {
        /// <summary>
        ///     Need to keep a reference to both callback to avoid GC
        /// </summary>
        private readonly MoonlightInterop.PacketCallback _recvCallback;
        private readonly MoonlightInterop.PacketCallback _sendCallback;


        public LocalClient()
        {
            _sendCallback = OnPacketSend;
            _recvCallback = OnPacketReceived;

            MoonlightInterop.Initialize();

            MoonlightInterop.SetSendCallback(_sendCallback);
            MoonlightInterop.SetRecvCallback(_recvCallback);
        }

        public override void SendPacket(string packet)
        {
            MoonlightInterop.SendPacket(packet);
        }

        public override void ReceivePacket(string packet)
        {
            MoonlightInterop.RecvPacket(packet);
        }

        public override void Dispose()
        {
            MoonlightInterop.Clean();
        }
    }
}