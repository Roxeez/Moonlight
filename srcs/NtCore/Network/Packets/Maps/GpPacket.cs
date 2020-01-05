using NtCore.Enums;

namespace NtCore.Network.Packets.Maps
{
    [PacketInfo("gp", PacketType.Recv)]
    public class GpPacket : Packet
    {
        [PacketIndex(0)]
        public short SourceX { get; set; }

        [PacketIndex(1)]
        public short SourceY { get; set; }

        [PacketIndex(2)]
        public short DestinationId { get; set; }

        [PacketIndex(3)]
        public PortalType PortalType { get; set; }

        [PacketIndex(4)]
        public int PortalId { get; set; }
    }
}