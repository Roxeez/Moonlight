namespace NtCore.Network.Packets.Character
{
    [PacketInfo("stat", PacketType.Recv)]
    public class StatPacket : Packet
    {
        [PacketIndex(1)]
        public int Hp { get; set; }

        [PacketIndex(2)]
        public int MaxHp { get; set; }

        [PacketIndex(3)]
        public short Mp { get; set; }

        [PacketIndex(4)]
        public short MaxMp { get; set; }
    }
}