using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Map.Miniland
{
    [PacketHeader("mlinfo")]
    public class MlInfoPacket : Packet
    {
        [PacketIndex(1)]
        public short Points { get; set; }
    }
}