using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character
{
    [PacketHeader("lev")]
    public class LevPacket : Packet
    {
        [PacketIndex(0)]
        public byte Level { get; set; }

        [PacketIndex(2)]
        public byte JobLevel { get; set; }
    }
}