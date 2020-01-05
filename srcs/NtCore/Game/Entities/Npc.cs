using NtCore.Enums;

namespace NtCore.Game.Entities
{
    public class Npc : LivingEntity
    {
        public Npc() => EntityType = EntityType.NPC;

        public int Vnum { get; internal set; }
    }
}