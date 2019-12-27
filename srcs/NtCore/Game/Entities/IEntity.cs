using NtCore.Core;
using NtCore.Enums;
using NtCore.Game.Maps;

namespace NtCore.Game.Entities
{
    /// <summary>
    ///     Represent an Entity
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        ///     Id of entity
        /// </summary>
        int Id { get; }

        /// <summary>
        ///     Map of entity
        /// </summary>
        IMap Map { get; }

        /// <summary>
        ///     Position in map
        /// </summary>
        Position Position { get; }

        /// <summary>
        ///     Type of entity
        /// </summary>
        EntityType EntityType { get; }
    }
}