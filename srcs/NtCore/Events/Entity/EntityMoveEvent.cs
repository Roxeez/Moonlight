using JetBrains.Annotations;
using NtCore.Clients;
using NtCore.Core;
using NtCore.Game.Entities;

namespace NtCore.Events.Entity
{
    /// <summary>
    ///     Event called when a living entity move (player, monster, npc)
    /// </summary>
    public class EntityMoveEvent : Event
    {
        public EntityMoveEvent([NotNull] IClient client, [NotNull] LivingEntity entity, Position from) : base(client)
        {
            Entity = entity;
            From = from;
        }

        /// <summary>
        ///     Entity involved in this event
        /// </summary>
        [NotNull]
        public LivingEntity Entity { get; }

        /// <summary>
        ///     Position of entity before movement
        /// </summary>
        public Position From { get; }
    }
}