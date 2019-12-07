using NtCore.API;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;

namespace NtCore.Game.Entities
{
    public class Drop : IDrop
    {
        public Drop()
        {
            EntityType = EntityType.DROP;
        }

        public int Id { get; set; }
        public int Vnum { get; set; }
        public int Amount { get; set; }
        public IMap Map { get; set; }
        public Position Position { get; set; }
        public EntityType EntityType { get; }
    }
}