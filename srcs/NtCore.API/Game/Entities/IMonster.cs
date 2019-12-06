namespace NtCore.API.Game.Entities
{
    public interface IMonster : ILivingEntity
    {
        /// <summary>
        /// Monster vnum
        /// </summary>
        int Vnum { get; }
    }
}