using NtCore.API;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Inventory;
using NtCore.API.Game.Maps;
using NtCore.Game.Inventory;
using NtCore.Game.Maps;

namespace NtCore.Game.Entities
{
    public class Character : Player, ICharacter
    {
        public int SpPoints { get; set; }
        public int AdditionalSpPoints { get; set; }
        public int MaximumSpPoints { get; set; }
        public int MaximumAdditionalSpPoints { get; set; }
        public IEquipment Equipment { get; }
        public int Gold { get; set; }

        public Character()
        {
            Equipment = new Equipment();
        }
    }
}