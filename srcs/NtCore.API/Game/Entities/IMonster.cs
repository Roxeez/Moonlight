namespace NtCore.API.Game.Entities
{
    /// <summary>
    ///     Represent a Monster
    /// </summary>
    public interface IMonster : ILivingEntity
    {
        /// <summary>
        ///     Monster vnum
        /// </summary>
        int Vnum { get; }
    }
}