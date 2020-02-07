using System;
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
            ItemDto itemDto = _itemRepository.Find(vnum);
            if (itemDto == null)
            {
                throw new InvalidOperationException($"Can't find item {vnum} in database");
            }

            string name = _languageService.GetTranslation(RootKey.ITEM, itemDto.NameKey);
            return new Item(vnum, name);
        }
    }
}