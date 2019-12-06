namespace NtCore.API.Game.Entities
{
    public interface INpc : ILivingEntity
    {
        /// <summary>
        /// Npc vnum
        /// </summary>
        int Vnum { get; }
    }
}