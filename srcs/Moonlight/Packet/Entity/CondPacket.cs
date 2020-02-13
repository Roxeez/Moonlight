using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Entity
{
    [PacketHeader("cond")]
    internal class CondPacket : Packet
    {
        [PacketIndex(0)]
        public EntityType EntityType { get; set; }

        [PacketIndex(1)]
        public int EntityId { get; set; }

        [PacketIndex(2)]
        public bool IsAttackAllowed { get; set; }

        [PacketIndex(3)]
        public bool IsMovementAllowed { get; set; }

        [PacketIndex(4)]
        public byte Speed { get; set; }
    }
}