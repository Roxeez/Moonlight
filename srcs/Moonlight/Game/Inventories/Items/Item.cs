using Moonlight.Core.Enums;
using Moonlight.Database.Dto;

namespace Moonlight.Game.Inventories.Items
{
    public class Item
    {
        private readonly ItemDto _itemDto;

        internal Item(string name, ItemDto itemDto)
        {
            Name = name;
            _itemDto = itemDto;
        }

        public int Vnum => _itemDto.Id;
        public string Name { get; }

        public int Type => _itemDto.Type;
        public int SubType => _itemDto.SubType;
        public BagType BagType => _itemDto.BagType;
    }
}