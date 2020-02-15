using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Battle
{
    [PacketHeader("sr")]
    internal class SrPacket : Packet
    {
        [PacketIndex(0)]
        public int CastId { get; set; }
    }
}