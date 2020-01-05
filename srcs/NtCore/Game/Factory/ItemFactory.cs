using NtCore.Game.Items;
using NtCore.I18N;
using NtCore.Registry;

namespace NtCore.Game.Factory
{
    public class ItemFactory : IItemFactory
    {
        private readonly ILanguageService _languageService;
        private readonly IRegistry _registry;

        public ItemFactory(IRegistry registry, ILanguageService languageService)
        {
            _registry = registry;
            _languageService = languageService;
        }

        public Item CreateItem(int vnum)
        {
            ItemInfo info = _registry.GetItemInfo(vnum);
            string name = _languageService.GetTranslation(LanguageKey.ITEM, info?.NameKey ?? $"{vnum}");

            return new Item(vnum, name, info);
        }

        public Weapon CreateWeapon(int vnum, byte rarity, byte upgrade)
        {
            ItemInfo info = _registry.GetItemInfo(vnum);
            string name = _languageService.GetTranslation(LanguageKey.ITEM, info?.NameKey ?? $"{vnum}");

            return new Weapon(vnum, name, info)
            {
                Rarity = rarity,
                Upgrade = upgrade
            };
        }

        public Armor CreateArmor(int vnum, byte rarity, byte upgrade)
        {
            ItemInfo info = _registry.GetItemInfo(vnum);
            string name = _languageService.GetTranslation(LanguageKey.ITEM, info?.NameKey ?? $"{vnum}");

            return new Armor(vnum, name, info)
            {
                Rarity = rarity,
                Upgrade = upgrade
            };
        }

        public Fairy CreateFairy(int vnum)
        {
            ItemInfo info = _registry.GetItemInfo(vnum);
            string name = _languageService.GetTranslation(LanguageKey.ITEM, info?.NameKey ?? $"{vnum}");

            return new Fairy(vnum, name, info);
        }
    }
}