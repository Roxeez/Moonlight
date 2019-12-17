using NtCore.Game.Items;

namespace NtCore.Game.Inventories
{
    public interface IEquipment
    {
        IWeapon MainWeapon { get; }
        IArmor Armor { get; }
        IWeapon SecondaryWeapon { get; }
        IFairy Fairy { get; }
    }
}