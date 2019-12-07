namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("lev", PacketType.Recv)]
    public class LevPacket : Packet
    {
        [PacketIndex(1)]
        public byte Level { get; set; }

        [PacketIndex(3)]
        public byte JobLevel { get; set; }
    }
}