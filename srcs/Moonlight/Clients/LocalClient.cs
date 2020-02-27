using Moonlight.Clients.Local;
using MoonlightCore;

namespace Moonlight.Clients
{
    public sealed class LocalClient : Client
    {
        public ManagedMoonlightCore ManagedMoonlightCore { get; }
        public Window Window { get; }

        public LocalClient(Window window)
        {
            Window = window;
            ManagedMoonlightCore = new ManagedMoonlightCore();

            ManagedMoonlightCore.SetRecvCallback(OnPacketReceived);
            ManagedMoonlightCore.SetSendCallback(OnPacketSend);
        }

        public override void SendPacket(string packet)
        {
            ManagedMoonlightCore.SendPacket(packet);
        }

        public override void ReceivePacket(string packet)
        {
            ManagedMoonlightCore.ReceivePacket(packet);
        }
    }
}