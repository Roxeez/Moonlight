using Moonlight.Core.Import;

namespace Moonlight.Clients
{
    public sealed class LocalClient : Client
    {
        private readonly NtNative.PacketCallback _recvCallback;

        /// <summary>
        ///     Need to keep a reference to both callback to avoid GC
        /// </summary>
        private readonly NtNative.PacketCallback _sendCallback;


        public LocalClient()
        {
            _sendCallback = OnPacketSend;
            _recvCallback = OnPacketReceived;

            NtNative.Initialize();

            NtNative.SetSendCallback(_sendCallback);
            NtNative.SetRecvCallback(_recvCallback);
        }

        public override void SendPacket(string packet)
        {
            NtNative.SendPacket(packet);
        }

        public override void ReceivePacket(string packet)
        {
            NtNative.RecvPacket(packet);
        }

        public override void Dispose()
        {
            NtNative.Clean();
        }
    }
}