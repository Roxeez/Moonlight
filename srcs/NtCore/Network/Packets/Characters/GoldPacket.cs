namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("gold", PacketType.Recv)]
    public class GoldPacket : Packet
    {
        [PacketIndex(0)]
        public int Gold { get; set; }
    }
}