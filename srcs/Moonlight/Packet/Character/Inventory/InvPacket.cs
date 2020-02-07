using System.Collections.Generic;
using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character.Inventory
{
    [PacketHeader("inv")]
    internal class InvPacket : Packet
    {
        public BagType BagType { get; set; }
        public List<IvnSubPacket> SubPackets { get; set; }
    }
}