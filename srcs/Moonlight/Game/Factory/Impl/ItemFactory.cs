using System;
using System.Linq;
using Moonlight.Core.Enums;
using Moonlight.Database.DAL;
using Moonlight.Database.Dto;
using Moonlight.Game.Inventories.Items;
using Moonlight.Translation;

namespace Moonlight.Game.Factory.Impl
{
    internal class ItemFactory : IItemFactory
    {
        private readonly IRepository<ItemDto> _itemRepository;
        private readonly ILanguageService _languageService;

        public ItemFactory(IRepository<ItemDto> itemRepository, ILanguageService languageService)
        {
            _itemRepository = itemRepository;
            _languageService = languageService;
        }

        public Item CreateItem(int vnum)
        {
            ItemDto itemDto = _itemRepository.Select(vnum);
            if (itemDto == null)
            {
                throw new InvalidOperationException($"Can't find item {vnum} in database");
            }

            string name = _languageService.GetTranslation(RootKey.ITEM, itemDto.NameKey);
            return new Item(itemDto.Id, name)
            {
                BagType = itemDto.BagType,
                Type = itemDto.Type,
                SubType = itemDto.SubType,
                Data = itemDto.Data.Split('|').Select(x => Convert.ToInt16(x)).ToArray()
            };
        }
    }
}