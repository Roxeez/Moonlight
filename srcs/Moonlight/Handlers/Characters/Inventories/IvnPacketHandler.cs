using Moonlight.Clients;
using Moonlight.Game.Entities;
using Moonlight.Game.Factory;
using Moonlight.Game.Inventories;
using Moonlight.Packet.Character.Inventory;

namespace Moonlight.Handlers.Characters.Inventories
{
    internal class IvnPacketHandler : PacketHandler<IvnPacket>
    {
        private readonly IItemInstanceFactory _itemInstanceFactory;

        public IvnPacketHandler(IItemInstanceFactory itemInstanceFactory) => _itemInstanceFactory = itemInstanceFactory;

        protected override void Handle(Client client, IvnPacket packet)
        {
            Character character = client.Character;

            Bag bag = character.Inventory.GetBag(packet.BagType);
            if (bag == null)
            {
                return;
            }

            if (packet.SubPacket.VNum == -1)
            {
                bag.RemoveItem(packet.SubPacket.Slot);
                return;
            }

            ItemInstance item = _itemInstanceFactory.CreateItemInstance(packet.SubPacket.VNum, packet.SubPacket.RareAmount);
            if (item == null)
            {
                return;
            }

            bag.AddItem(packet.SubPacket.Slot, item);
        }
    }
}