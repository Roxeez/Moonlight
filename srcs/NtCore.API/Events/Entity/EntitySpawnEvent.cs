using JetBrains.Annotations;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Entity
{
    /// <summary>
    ///     Event called when an Entity Spawn
    /// </summary>
    public class EntitySpawnEvent : Event
    {
        public EntitySpawnEvent([NotNull] ICharacter character, [NotNull] IEntity entity, [NotNull] IMap map)
        {
            Character = character;
            Entity = entity;
            Map = map;
        }

        public ICharacter Character { get; }
        public IEntity Entity { get; }
        public IMap Map { get; }
    }
}