using NtCore.API.Client;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Maps
{
    public class EntitySpawnEvent : Event
    {
        public IClient Client { get; }
        public IEntity Entity { get; }
        public IMap Map { get; }

        public EntitySpawnEvent(IClient client, IEntity entity, IMap map)
        {
            Client = client;
            Entity = entity;
            Map = map;
        }
    }
}