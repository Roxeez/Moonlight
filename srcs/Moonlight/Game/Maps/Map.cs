using System;
using System.Collections.Generic;
using System.Linq;
using Moonlight.Core.Enums;
using Moonlight.Core.Extensions;
using Moonlight.Game.Entities;
using Moonlight.Game.Utility;

namespace Moonlight.Game.Maps
{
    public class Map
    {
        private readonly Dictionary<EntityType, Dictionary<long, Entity>> _entities;

        internal Map(int id, string name, byte[] grid)
        {
            Id = id;
            Name = name;
            Grid = grid;

            _entities = new Dictionary<EntityType, Dictionary<long, Entity>>();
        }

        public int Id { get; }
        public string Name { get; }
        public byte[] Grid { get; }

        public IEnumerable<Entity> Entities => _entities.Values.SelectMany(x => x.Values);
        public IEnumerable<Monster> Monsters => _entities.GetValueOrDefault(EntityType.MONSTER)?.Values.Cast<Monster>() ?? Array.Empty<Monster>();
        public IEnumerable<Npc> Npcs => _entities.GetValueOrDefault(EntityType.NPC)?.Values.Cast<Npc>() ?? Array.Empty<Npc>();
        public IEnumerable<Player> Players => _entities.GetValueOrDefault(EntityType.PLAYER)?.Values.Cast<Player>() ?? Array.Empty<Player>();
        public IEnumerable<Drop> Drops => _entities.GetValueOrDefault(EntityType.DROP)?.Values.Cast<Drop>() ?? Array.Empty<Drop>();

        public Entity GetEntity(EntityType entityType, long entityId) => _entities.GetValueOrDefault(entityType)?.GetValueOrDefault(entityId);

        public T GetEntity<T>(EntityType entityType, long entityId) where T : Entity
        {
            Entity entity = GetEntity(entityType, entityId);
            if (!(entity is T castEntity))
            {
                return default;
            }

            return castEntity;
        }

        public T GetEntity<T>(long entityId) where T : Entity
        {
            EntityType entityType = EntityUtility.GetEntityType<T>();
            return GetEntity<T>(entityType, entityId);
        }

        internal void AddEntity(Entity entity)
        {
            Dictionary<long, Entity> entities = _entities.GetValueOrDefault(entity.EntityType);
            if (entities == null)
            {
                entities = new Dictionary<long, Entity>();
                _entities[entity.EntityType] = entities;
            }

            entities[entity.Id] = entity;
            entity.Map = this;
        }

        internal void RemoveEntity(Entity entity)
        {
            RemoveEntity(entity.EntityType, entity.Id);
        }

        internal void RemoveEntity(EntityType entityType, long entityId)
        {
            Dictionary<long, Entity> entities = _entities.GetValueOrDefault(entityType);
            if (entities == null)
            {
                return;
            }

            entities.Remove(entityId);
        }
    }
}