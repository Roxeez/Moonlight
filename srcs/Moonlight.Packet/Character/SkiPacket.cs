using System.Collections.Generic;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character
{
    [PacketHeader("ski")]
    public class SkiPacket : Packet
    {
        public List<int> Skills { get; set; }
    }
}