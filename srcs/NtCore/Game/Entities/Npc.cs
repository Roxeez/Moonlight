using NtCore.API;
using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;

namespace NtCore.Game.Entities
{
    public class Npc : LivingEntity, INpc
    {
        public int Vnum { get; set; }

        public Npc()
        {
            EntityType = EntityType.NPC;
        }
    }
}