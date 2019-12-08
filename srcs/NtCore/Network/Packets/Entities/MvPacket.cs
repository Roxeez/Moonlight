using NtCore.API.Enums;

namespace NtCore.Network.Packets.Entities
{
    [PacketInfo("mv", PacketType.Recv)]
    public class MvPacket : Packet
    {
        [PacketIndex(1)]
        public EntityType EntityType { get; set; }

        [PacketIndex(2)]
        public int EntityId { get; set; }

        [PacketIndex(3)]
        public short X { get; set; }

        [PacketIndex(4)]
        public short Y { get; set; }

        [PacketIndex(5)]
        public byte Speed { get; set; }
    }
}