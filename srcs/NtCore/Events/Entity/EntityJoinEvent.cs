using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Entity
{
    /// <summary>
    ///     Event called when an entity join the map (player, monster, npc, drop)
    ///     It's basically called when you receive in packet
    /// </summary>
    public class EntityJoinEvent : Event
    {
        public EntityJoinEvent([NotNull] IClient client, [NotNull] LivingEntity entity, [NotNull] Game.Maps.Map map) : base(client)
        {
            Entity = entity;
            Map = map;
        }

        /// <summary>
        ///     Entity involved in this event
        /// </summary>
        [NotNull]
        public LivingEntity Entity { get; }

        /// <summary>
        ///     Map where this entity spawned
        /// </summary>
        [NotNull]
        public Game.Maps.Map Map { get; }
    }
}