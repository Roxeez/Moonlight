namespace Moonlight.Packet.Core
{
    internal interface IPacketReader
    {
        PacketOutput Read(string packet);
    }
}