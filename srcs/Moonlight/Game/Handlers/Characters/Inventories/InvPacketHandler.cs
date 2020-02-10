using Moonlight.Clients;
using Moonlight.Core.Logging;
using Moonlight.Game.Factory;
using Moonlight.Game.Inventories;
using Moonlight.Packet.Character.Inventory;

namespace Moonlight.Game.Handlers.Characters.Inventories
{
    internal class InvPacketHandler : PacketHandler<InvPacket>
    {
        private readonly ILogger _logger;
        private readonly IItemInstanceFactory _itemInstanceFactory;

        public InvPacketHandler(ILogger logger, IItemInstanceFactory itemInstanceFactory)
        {
            _logger = logger;
            _itemInstanceFactory = itemInstanceFactory;
        }

        protected override void Handle(Client client, InvPacket packet)
        {
            Bag bag = client.Character.Inventory.GetBag(packet.BagType);

            if (bag == null)
            {
                _logger.Error($"Can't found bad {packet.BagType}");
                return;
            }
            
            foreach (IvnSubPacket sub in packet.SubPackets)
            {
                ItemInstance item = _itemInstanceFactory.CreateItemInstance(sub.VNum, sub.RareAmount);
                if (item == null)
                {
                    _logger.Error($"Can't create item instance for {sub.VNum}");
                    return;
                }

                bag.AddItem(sub.Slot, item);
            }
            
            _logger.Info($"{packet.BagType} bag initialized.");
        }
    }
}