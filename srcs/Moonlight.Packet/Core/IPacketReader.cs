namespace Moonlight.Packet.Core
{
    public interface IPacketReader
    {
        PacketOutput Read(string packet);
    }
}