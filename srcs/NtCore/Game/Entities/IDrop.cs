using System;

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

        /// <summary>
        ///     Name of the item
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Owner of the drop
        /// </summary>
        IPlayer Owner { get; }

        /// <summary>
        ///     Time where drop spawned
        /// </summary>
        DateTime DropTime { get; }

        /// <summary>
        ///     Define if drop is gold or not
        /// </summary>
        bool IsGold { get; }
    }
}