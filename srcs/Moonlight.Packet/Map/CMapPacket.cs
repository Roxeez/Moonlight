using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Map
{
    [PacketHeader("c_map")]
    public class CMapPacket : Packet
    {
        [PacketIndex(1)]
        public short MapId { get; set; }

        [PacketIndex(2)]
        public bool IsBaseMap { get; set; }
    }
}