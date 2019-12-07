using NtCore.API.Enums;

namespace NtCore.Network.Packets.Entities
{
    [PacketInfo("cond", PacketType.Recv)]
    public class CondPacket : Packet
    {
        [PacketIndex(1)]
        public EntityType EntityType { get; set; }

        [PacketIndex(2)]
        public int EntityId { get; set; }

        [PacketIndex(3)]
        public bool IsAttackAllowed { get; set; }

        [PacketIndex(4)]
        public bool IsMovementAllowed { get; set; }

        [PacketIndex(5)]
        public byte Speed { get; set; }
    }
}