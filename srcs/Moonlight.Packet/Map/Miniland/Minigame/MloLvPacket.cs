using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Map.Miniland.Minigame
{
    [PacketHeader("mlo_lv")]
    internal class MloLvPacket : Packet
    {
        [PacketIndex(0)]
        public int Level { get; set; }
    }
}