using NtCore.Enums;

namespace NtCore.Network.Packets.Maps
{
    [PacketInfo("out", PacketType.Recv)]
    public class OutPacket : Packet
    {
        [PacketIndex(0)]
        public EntityType EntityType { get; set; }

        [PacketIndex(1)]
        public int EntityId { get; set; }
    }
}