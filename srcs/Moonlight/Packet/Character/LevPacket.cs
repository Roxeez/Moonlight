using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character
{
    [PacketHeader("lev")]
    internal class LevPacket : Packet
    {
        [PacketIndex(0)]
        public byte Level { get; set; }

        [PacketIndex(2)]
        public byte JobLevel { get; set; }
    }
}