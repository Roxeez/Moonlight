namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("lev", PacketType.Recv)]
    public class LevPacket : Packet
    {
        [PacketIndex(0)]
        public byte Level { get; set; }

        [PacketIndex(2)]
        public byte JobLevel { get; set; }
    }
}