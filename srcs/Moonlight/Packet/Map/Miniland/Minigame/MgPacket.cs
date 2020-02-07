using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Map.Miniland.Minigame
{
    [PacketHeader("mg")]
    internal class MgPacket : Packet
    {
        [PacketIndex(0)]
        public byte Type { get; set; }
    }
}