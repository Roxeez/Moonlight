using NtCore.API.Client;

namespace NtCore.Network
{
    internal interface IPacketHandler
    {
        void Handle(IClient client, IPacket packet);
    }

    public abstract class PacketHandler<TPacket> : IPacketHandler where TPacket : IPacket
    {
        public void Handle(IClient client, IPacket packet) => Handle(client, (TPacket) packet);
        public abstract void Handle(IClient client, TPacket packet);
    }
}