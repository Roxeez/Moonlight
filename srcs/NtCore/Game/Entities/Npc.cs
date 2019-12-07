using NtCore.API.Enums;
using NtCore.API.Game.Entities;

namespace NtCore.Game.Entities
{
    public class Npc : LivingEntity, INpc
    {
        public Npc() => EntityType = EntityType.NPC;

        public int Vnum { get; set; }
    }
}