using Moonlight.Core.Enums.Game;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Map
{
    [PacketHeader("die")]
    public class DiePacket : Packet
    {
        [PacketIndex(0)]
        public EntityType EntityType { get; set; }
        
        [PacketIndex(1)]
        public long EntityId { get; set; }
    }
}