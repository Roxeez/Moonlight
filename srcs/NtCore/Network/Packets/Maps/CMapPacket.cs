namespace NtCore.Network.Packets.Maps
{
    [PacketInfo("c_map", PacketType.Recv)]
    public class CMapPacket : Packet
    {
        [PacketIndex(2)]
        public short MapId { get; set; }

        [PacketIndex(3)]
        public bool IsJoining { get; set; }
    }
}