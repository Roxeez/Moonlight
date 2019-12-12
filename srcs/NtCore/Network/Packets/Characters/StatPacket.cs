namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("stat", PacketType.Recv)]
    public class StatPacket : Packet
    {
        [PacketIndex(0)]
        public int Hp { get; set; }

        [PacketIndex(1)]
        public int MaxHp { get; set; }

        [PacketIndex(2)]
        public short Mp { get; set; }

        [PacketIndex(3)]
        public short MaxMp { get; set; }
    }
}