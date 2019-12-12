using NtCore.Enums;

namespace NtCore.Network.Packets.Entities
{
    [PacketInfo("mv", PacketType.Recv)]
    public class MvPacket : Packet
    {
        [PacketIndex(0)]
        public EntityType EntityType { get; set; }

        [PacketIndex(1)]
        public int EntityId { get; set; }

        [PacketIndex(2)]
        public short X { get; set; }

        [PacketIndex(3)]
        public short Y { get; set; }

        [PacketIndex(4)]
        public byte Speed { get; set; }
    }
}