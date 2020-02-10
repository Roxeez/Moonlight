using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character.Inventory
{
    [PacketHeader("ivn")]
    internal class IvnPacket : Packet
    {
        [PacketIndex(0)]
        public BagType BagType { get; set; }

        public IvnSubPacket SubPacket { get; set; }
    }

    public class IvnSubPacket
    {
        public int Slot { get; set; }

        public int VNum { get; set; }

        public int RareAmount { get; set; }

        public int UpgradeDesign { get; set; }

        public int SecondUpgrade { get; set; }
    }
}