using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Battle
{
    [PacketHeader("sr")]
    public class SrPacket : Packet
    {
        [PacketIndex(0)]
        public int CastId { get; set; }
    }
}