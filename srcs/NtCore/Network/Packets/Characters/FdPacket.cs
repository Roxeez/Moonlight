namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("fd", PacketType.Recv)]
    public class FdPacket : Packet
    {
        [PacketIndex(0)]
        public int Reputation { get; set; }
        
        [PacketIndex(2)]
        public int Dignity { get; set; }
    }
}