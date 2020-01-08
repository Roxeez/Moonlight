using NtCore.Enums;

namespace NtCore.Game.Entities
{
    public class Npc : LivingEntity
    {
        public Npc() => EntityType = EntityType.NPC;

        /// <summary>
        /// Vnum of the npc
        /// </summary>
        public int Vnum { get; internal set; }
    }
}