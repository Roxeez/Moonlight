using NtCore.Enums;

namespace NtCore.Network.Packets.Maps
{
    [PacketInfo("get", PacketType.Recv)]
    public class GetPacket : Packet
    {
        [PacketIndex(0)]
        public EntityType EntityType { get; set; }
        
        [PacketIndex(1)]
        public int EntityId { get; set; }
        
        [PacketIndex(2)]
        public int DropId { get; set; }
    }
}