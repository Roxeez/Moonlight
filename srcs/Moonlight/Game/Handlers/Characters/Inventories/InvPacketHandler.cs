using Moonlight.Clients;
using Moonlight.Core.Enums;
using Moonlight.Core.Logging;
using Moonlight.Game.Factory;
using Moonlight.Game.Inventories;
using Moonlight.Game.Inventories.Items;
using Moonlight.Packet.Character.Inventory;

namespace Moonlight.Game.Handlers.Characters.Inventories
{
    internal class InvPacketHandler : PacketHandler<InvPacket>
    {
        private readonly IItemFactory _itemFactory;
        private readonly ILogger _logger;

        public InvPacketHandler(ILogger logger, IItemFactory itemFactory)
        {
            _logger = logger;
            _itemFactory = itemFactory;
        }

        protected override void Handle(Client client, InvPacket packet)
        {
            var bag = new Bag();
            foreach (IvnSubPacket sub in packet.SubPackets)
            {
                ItemStack itemStack;
                Item item = _itemFactory.CreateItem(sub.VNum);

                if (packet.BagType == BagType.EQUIPMENT)
                {
                    itemStack = new Equipment(item)
                    {
                        Rarity = (RarityType)sub.RareAmount,
                        Upgrade = sub.UpgradeDesign
                    };
                }
                else
                {
                    itemStack = new ItemStack(item)
                    {
                        Amount = sub.RareAmount == 0 ? 1 : sub.RareAmount
                    };
                }

                bag.SetItem(sub.Slot, itemStack);
            }

            client.Character.Inventory.AddBag(packet.BagType, bag);
            _logger.Info($"{packet.BagType} bag initialized.");
        }
    }
}