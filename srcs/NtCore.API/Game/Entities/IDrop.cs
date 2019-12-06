namespace NtCore.API.Game.Entities
{
    public interface IDrop : IEntity
    {
        /// <summary>
        /// Vnum of the item
        /// </summary>
        int Vnum { get; }
        
        /// <summary>
        /// Amount of item drop
        /// </summary>
        int Amount { get; }
    }
}