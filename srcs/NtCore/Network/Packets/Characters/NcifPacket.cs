using NtCore.Enums;

namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("ncif", PacketType.Send)]
    public class NcifPacket : Packet
    {
        [PacketIndex(0)]
        public EntityType EntityType { get; set; }

        [PacketIndex(1)]
        public int EntityId { get; set; }
    }
}