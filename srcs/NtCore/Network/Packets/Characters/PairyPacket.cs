using NtCore.Enums;

namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("pairy", PacketType.Recv)]
    public class PairyPacket : Packet
    {
        [PacketIndex(0)]
        public EntityType EntityType { get; set; }

        [PacketIndex(1)]
        public int EntityId { get; set; }

        [PacketIndex(4)]
        public Element Element { get; set; }

        [PacketIndex(5)]
        public short Power { get; set; }
    }
}