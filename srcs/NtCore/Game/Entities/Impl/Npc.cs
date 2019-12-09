using NtCore.Enums;

namespace NtCore.Game.Entities.Impl
{
    public class Npc : LivingEntity, INpc
    {
        public Npc() => EntityType = EntityType.NPC;

        public int Vnum { get; set; }
    }
}