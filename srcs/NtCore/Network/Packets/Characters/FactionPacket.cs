using NtCore.Enums;

namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("fs", PacketType.Recv)]
    public class FactionPacket : Packet
    {
        [PacketIndex(0)]
        public Faction Faction { get; set; }
    }
}