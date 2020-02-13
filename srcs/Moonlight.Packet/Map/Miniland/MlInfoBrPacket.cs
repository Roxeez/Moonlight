using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Map.Miniland
{
    [PacketHeader("mlinfobr")]
    public class MlInfoBrPacket : Packet
    {
        [PacketIndex(1)]
        public string Owner { get; set; }
    }
}