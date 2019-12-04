using NtCore.API.Game.Inventory;

namespace NtCore.Game.Inventory
{
    public class Equipment : IEquipment
    {
        public IFairy Fairy { get; set; }
        public ISpecialist Specialist { get; set; }
    }
}