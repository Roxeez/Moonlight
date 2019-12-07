using JetBrains.Annotations;
using NtCore.API.Clients;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Maps
{
    /// <summary>
    ///     Event called when an Entity Spawn
    /// </summary>
    public class EntitySpawnEvent : Event
    {
        public EntitySpawnEvent([NotNull] IClient client, [NotNull] IEntity entity, [NotNull] IMap map)
        {
            Client = client;
            Entity = entity;
            Map = map;
        }

        public IClient Client { get; }
        public IEntity Entity { get; }
        public IMap Map { get; }
    }
}