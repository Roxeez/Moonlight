namespace NtCore.Game.Entities
{
    /// <summary>
    ///     Represent a NPC
    /// </summary>
    public interface INpc : ILivingEntity
    {
        /// <summary>
        ///     Npc vnum
        /// </summary>
        int Vnum { get; }
    }
}