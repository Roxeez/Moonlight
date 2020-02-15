using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character
{
    [PacketHeader("pairy")]
    internal class PairyPacket : Packet
    {
        [PacketIndex(0)]
        public EntityType EntityType { get; set; }

        [PacketIndex(1)]
        public long EntityId { get; set; }

        [PacketIndex(3)]
        public ElementType Element { get; set; }

        [PacketIndex(4)]
        public short Power { get; set; }
    }
}