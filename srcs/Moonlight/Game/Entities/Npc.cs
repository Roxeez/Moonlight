using Moonlight.Core.Enums;
using Moonlight.Database.Dto;

namespace Moonlight.Game.Entities
{
    /// <summary>
    ///     Represent any kind of Npc
    ///     It can be game npc but also player pets
    /// </summary>
    public class Npc : LivingEntity
    {
        internal Npc(long id, string name) : base(id, name, EntityType.NPC)
        {
            
        }

        public int Vnum { get; internal set; }
    }
}