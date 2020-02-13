using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character
{
    [PacketHeader("sp")]
    public class SpPacket : Packet
    {
        [PacketIndex(0)]
        public int AdditionalPoints { get; set; }

        [PacketIndex(1)]
        public int MaximumAdditionalPoints { get; set; }

        [PacketIndex(2)]
        public int Points { get; set; }

        [PacketIndex(3)]
        public int MaximumPoints { get; set; }
    }
}