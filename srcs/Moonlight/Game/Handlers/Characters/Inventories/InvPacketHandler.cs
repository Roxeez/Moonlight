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
            foreach (IvnSubPacket sub in packet.SubPackets)
            {
                InventoryItem inventoryItem;
                Item item = _itemFactory.CreateItem(sub.VNum);

                if (packet.BagType == BagType.EQUIPMENT)
                {
                    inventoryItem = new Equipment(item, sub.Slot)
                    {
                        Rarity = (RarityType)sub.RareAmount,
                        Upgrade = sub.UpgradeDesign
                    };
                }
                else
                {
                    inventoryItem = new InventoryItem(item, packet.BagType, sub.Slot)
                    {
                        Amount = sub.RareAmount == 0 ? 1 : sub.RareAmount
                    };
                }

                client.Character.Inventory.GetBag(packet.BagType)?.AddItem(inventoryItem);
            }
            
            _logger.Info($"{packet.BagType} bag initialized.");
        }
    }
}