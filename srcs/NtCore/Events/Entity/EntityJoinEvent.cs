using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;
using NtCore.Game.Maps;

namespace NtCore.Events.Entity
{
    /// <summary>
    ///     Event called when an Entity Spawn (player, monster, npc, drop)
    ///     It's basically called when you receive in packet
    /// </summary>
    public class EntityJoinEvent : Event
    {
        public EntityJoinEvent([NotNull] IClient client, [NotNull] ILivingEntity entity, [NotNull] IMap map) : base(client)
        {
            Entity = entity;
            Map = map;
        }

        /// <summary>
        ///     Entity involved in this event
        /// </summary>
        public ILivingEntity Entity { get; }

        /// <summary>
        ///     Map where this entity spawned
        /// </summary>
        public IMap Map { get; }
    }
}