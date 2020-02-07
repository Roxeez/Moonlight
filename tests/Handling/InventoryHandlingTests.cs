using Moonlight.Core.Enums;
using Moonlight.Game.Inventories;
using Moonlight.Tests.Extensions;
using NFluent;
using Xunit;

namespace Moonlight.Tests.Handling
{
    public class InventoryHandlingTests : PacketHandlingTest
    {
        [Fact]
        public void Gold_Packet_Initialize_Character_Gold()
        {
            Client.ReceivePacket("gold 7268858 0");

            Check.That(Character.Inventory.Gold).Is(7268858);
        }

        [Fact]
        public void Inv_Packet_Initialize_Equipment_Bag()
        {
            Client.ReceivePacket("inv 0 0.8112.0.0.0.0 1.8114.0.0.0.0 2.8111.0.0.0.0 3.8112.0.0.0.0 4.8105.0.0.0.0 6.221.0.0.0.0 12.148.4.0.0.0 59.8306.0.0.0.0");

            Bag bag = Character.Inventory.GetBag(BagType.EQUIPMENT);

            Check.That(bag).IsNotNull();

            ItemStack itemStack = bag.GetItem(0);

            Check.That(itemStack).IsNotNull();
            Check.That(itemStack.Item.Vnum).Is(8112);
            Check.That(itemStack.Amount).Is(1);
        }

        [Fact]
        public void Inv_Packet_Initialize_Main_Bag()
        {
            Client.ReceivePacket(
                "inv 1 0.1012.23 1.1027.480 4.1211.17 5.1030.1 18.9042.47 19.9017.50 20.1007.4 23.5834.1 24.9009.25 25.1010.19 26.1004.1 35.5119.1 41.9033.5 42.9020.3 43.9021.2 44.9022.10 47.9040.1 48.1246.2 49.1247.1 50.1248.4 51.9023.8 53.9039.1 54.1285.6 55.9041.20 56.9074.9 57.1362.19 59.9110.2");

            Bag bag = Character.Inventory.GetBag(BagType.MAIN);

            Check.That(bag).IsNotNull();

            ItemStack itemStack = bag.GetItem(0);

            Check.That(itemStack).IsNotNull();
            Check.That(itemStack.Item.Vnum).Is(1012);
            Check.That(itemStack.Amount).Is(23);
        }
    }
}