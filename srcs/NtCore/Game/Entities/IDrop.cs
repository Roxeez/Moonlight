namespace NtCore.Game.Entities
{
    /// <summary>
    ///     Represent a Drop
    /// </summary>
    public interface IDrop : IEntity
    {
        /// <summary>
        ///     Vnum of the item
        /// </summary>
        int Vnum { get; }

        /// <summary>
        ///     Amount of item drop
        /// </summary>
        int Amount { get; }
        
        string Name { get; }
    }
}