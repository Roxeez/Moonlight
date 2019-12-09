using JetBrains.Annotations;
using NtCore.Game.Entities;
using NtCore.Game.Maps;

namespace NtCore.Events.Entity
{
    /// <summary>
    ///     Event called when an Entity Spawn (player, monster, npc, drop)
    ///     It's basically called when you receive in packet
    /// </summary>
    public class EntitySpawnEvent : Event
    {
        public EntitySpawnEvent([NotNull] IEntity entity, [NotNull] IMap map)
        {
            Entity = entity;
            Map = map;
        }

        /// <summary>
        ///     Entity involved in this event
        /// </summary>
        public IEntity Entity { get; }

        /// <summary>
        ///     Map where this entity spawned
        /// </summary>
        public IMap Map { get; }
    }
}