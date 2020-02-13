using System.Collections.Generic;
using Moonlight.Core.Enums.Game;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character.Inventory
{
    [PacketHeader("inv")]
    public class InvPacket : Packet
    {
        public BagType BagType { get; set; }
        public List<IvnSubPacket> SubPackets { get; set; }
    }
}