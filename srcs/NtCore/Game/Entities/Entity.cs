using JetBrains.Annotations;
using NtCore.Core;
using NtCore.Enums;
using NtCore.Game.Maps;

namespace NtCore.Game.Entities
{
    public abstract class Entity
    {
        /// <summary>
        /// Id of the entity
        /// </summary>
        public int Id { get; internal set; }
        
        /// <summary>
        /// Type of entity
        /// </summary>
        public EntityType EntityType { get; internal set; }
        
        /// <summary>
        /// Current map of this entity
        /// </summary>
        [NotNull]
        public Map Map { get; internal set; }
        
        /// <summary>
        /// Current position in the map
        /// </summary>
        public Position Position { get; internal set; }
    }
}