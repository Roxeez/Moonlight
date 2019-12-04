namespace NtCore.Network.Packets.Character
{
    [PacketInfo("gold", PacketType.Recv)]
    public class GoldPacket : Packet
    {
        [PacketIndex(1)]
        public int Gold { get; set; }
    }
}