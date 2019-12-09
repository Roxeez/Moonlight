using JetBrains.Annotations;
using NtCore.Game.Entities;

namespace NtCore.Events.Entity
{
    /// <summary>
    ///     Event called when a living entity move (player, monster, npc)
    /// </summary>
    public class EntityMoveEvent : Event
    {
        public EntityMoveEvent([NotNull] ILivingEntity entity, Position from)
        {
            Entity = entity;
            From = from;
        }

        /// <summary>
        ///     Entity involved in this event
        /// </summary>
        public ILivingEntity Entity { get; }

        /// <summary>
        ///     Position of entity before movement
        /// </summary>
        public Position From { get; }
    }
}