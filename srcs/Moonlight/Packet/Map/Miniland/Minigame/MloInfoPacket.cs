using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Map.Miniland.Minigame
{
    [PacketHeader("mlo_info")]
    internal class MloInfoPacket : Packet
    {
        [PacketIndex(1)]
        public int Vnum { get; set; }

        [PacketIndex(2)]
        public int ObjectId { get; set; }

        [PacketIndex(3)]
        public short Points { get; set; }
    }
}