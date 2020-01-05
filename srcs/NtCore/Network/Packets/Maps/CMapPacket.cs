namespace NtCore.Network.Packets.Maps
{
    [PacketInfo("c_map", PacketType.Recv)]
    public class CMapPacket : Packet
    {
        [PacketIndex(1)]
        public short MapId { get; set; }

        [PacketIndex(2)]
        public bool IsBaseMap { get; set; }
    }
}