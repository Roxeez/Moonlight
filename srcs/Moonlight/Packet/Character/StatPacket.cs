using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character
{
    [PacketHeader("stat")]
    internal class StatPacket : Packet
    {
        [PacketIndex(0)]
        public int Hp { get; set; }

        [PacketIndex(1)]
        public int MaxHp { get; set; }

        [PacketIndex(2)]
        public int Mp { get; set; }

        [PacketIndex(3)]
        public int MaxMp { get; set; }
    }
}