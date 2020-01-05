using NtCore.Enums;

namespace NtCore.Network.Packets.Battle
{
    [PacketInfo("st", PacketType.Recv)]
    public class StPacket : Packet
    {
        [PacketIndex(0)]
        public EntityType EntityType { get; set; }

        [PacketIndex(1)]
        public int EntityId { get; set; }

        [PacketIndex(4)]
        public byte HpPercentage { get; set; }

        [PacketIndex(5)]
        public byte MpPercentage { get; set; }

        [PacketIndex(6)]
        public int CurrentHp { get; set; }

        [PacketIndex(7)]
        public int CurrentMp { get; set; }
    }
}