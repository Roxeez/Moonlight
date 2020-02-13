using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Map.Miniland.Minigame
{
    [PacketHeader("mlo_rw")]
    public class MloRwPacket : Packet
    {
        [PacketIndex(0)]
        public int ItemVnum { get; set; }

        [PacketIndex(1)]
        public int Count { get; set; }
    }
}