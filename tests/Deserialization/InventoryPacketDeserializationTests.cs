using Moonlight.Core.Enums;
using Moonlight.Packet.Character.Inventory;
using Moonlight.Tests.Extensions;
using NFluent;
using Xunit;

namespace Moonlight.Tests.Deserialization
{
    public class InventoryPacketDeserializationTests : DeserializationTests
    {
        [Fact]
        public void Inv_Packet_Costume_Bag()
        {
            InvPacket packet = Deserialize<InvPacket>("inv 7 0.8101.0.72.0 1.8117.0.0.0 2.8192.0.0.0");

            Check.That(packet.BagType).Is(BagType.COSTUME);
        }

        [Fact]
        public void Inv_Packet_Equipment_Bag()
        {
            InvPacket packet = Deserialize<InvPacket>("inv 0 0.8112.0.0.0.0 1.8114.0.0.0.0 2.8111.0.0.0.0 3.8112.0.0.0.0 4.8105.0.0.0.0 6.221.0.0.0.0 12.148.4.0.0.0 59.8306.0.0.0.0");

            Check.That(packet.BagType).Is(BagType.EQUIPMENT);
        }

        [Fact]
        public void Inv_Packet_Etc_Bag()
        {
            InvPacket packet = Deserialize<InvPacket>(
                "inv 2 0.2801.9 1.2800.2 4.2070.29 5.2072.42 6.10027.1 7.2083.11 10.10010.19 11.2071.42 16.10028.10 17.10048.20 18.2155.95 54.10016.14 55.10050.40 59.10059.5");

            Check.That(packet.BagType).Is(BagType.ETC);
        }

        [Fact]
        public void Inv_Packet_Main_Bag()
        {
            InvPacket packet = Deserialize<InvPacket>(
                "inv 1 0.1012.23 1.1027.480 4.1211.17 5.1030.1 18.9042.47 19.9017.50 20.1007.4 23.5834.1 24.9009.25 25.1010.19 26.1004.1 35.5119.1 41.9033.5 42.9020.3 43.9021.2 44.9022.10 47.9040.1 48.1246.2 49.1247.1 50.1248.4 51.9023.8 53.9039.1 54.1285.6 55.9041.20 56.9074.9 57.1362.19 59.9110.2");

            Check.That(packet.BagType).Is(BagType.MAIN);
        }

        [Fact]
        public void Inv_Packet_Miniland_Bag()
        {
            InvPacket packet = Deserialize<InvPacket>("inv 3 0.3104.1 1.3157.1 2.3194.1 3.3203.1 4.3171.1 5.3181.1 6.3185.1 7.3120.1 8.3138.1");

            Check.That(packet.BagType).Is(BagType.MINILAND);
        }

        [Fact]
        public void Inv_Packet_Specialist_Bag()
        {
            InvPacket packet = Deserialize<InvPacket>("inv 6");

            Check.That(packet.BagType).Is(BagType.SPECIALIST);
        }
    }
}