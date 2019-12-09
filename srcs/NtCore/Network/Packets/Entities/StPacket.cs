using NtCore.Enums;

namespace NtCore.Network.Packets.Entities
{
    [PacketInfo("st", PacketType.Recv)]
    public class StPacket : Packet
    {
        [PacketIndex(1)]
        public EntityType EntityType { get; set; }

        [PacketIndex(2)]
        public int EntityId { get; set; }

        [PacketIndex(3)]
        public byte Level { get; set; }

        [PacketIndex(4)]
        public byte HeroLvl { get; set; }

        [PacketIndex(5)]
        public byte HpPercentage { get; set; }

        [PacketIndex(6)]
        public byte MpPercentage { get; set; }

        [PacketIndex(7)]
        public int Hp { get; set; }

        [PacketIndex(8)]
        public int Mp { get; set; }
    }
}