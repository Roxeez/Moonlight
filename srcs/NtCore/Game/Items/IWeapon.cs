namespace NtCore.Game.Items
{
    public interface IWeapon : IItem
    {
        byte Rarity { get; }
        byte Upgrade { get; }
    }
}