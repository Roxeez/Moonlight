using NtCore.Enums;

namespace NtCore.Network.Packets.Entities
{
    [PacketInfo("rest", PacketType.Recv)]
    public class RestPacket : Packet
    {
        [PacketIndex(0)]
        public EntityType EntityType { get; set; }
        
        [PacketIndex(1)]
        public int EntityId { get; set; }
        
        [PacketIndex(2)]
        public bool IsResting { get; set; }
    }
}