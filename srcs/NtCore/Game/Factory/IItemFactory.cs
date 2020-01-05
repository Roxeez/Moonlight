using NtCore.Game.Items;

namespace NtCore.Game.Factory
{
    public interface IItemFactory
    {
        Item CreateItem(int vnum);
        Weapon CreateWeapon(int vnum, byte rarity, byte upgrade);
        Armor CreateArmor(int vnum, byte rarity, byte upgrade);
        Fairy CreateFairy(int vnum);
    }
}