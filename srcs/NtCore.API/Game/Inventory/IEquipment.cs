namespace NtCore.API.Game.Inventory
{
    public interface IEquipment
    {
        IFairy Fairy { get; }
        ISpecialist Specialist { get; }
    }
}