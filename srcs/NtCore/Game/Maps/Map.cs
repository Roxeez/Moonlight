using System;
using System.Collections.Generic;
using NtCore.API.Enums;
using NtCore.API.Extensions;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;
using NtCore.Game.Entities;

namespace NtCore.Game.Maps
{
    public class Map : IMap
    {
        private readonly IDictionary<int, IDrop> _drops;

        private readonly IDictionary<Type, EntityType> _mapping = new Dictionary<Type, EntityType>
        {
            [typeof(IMonster)] = EntityType.MONSTER,
            [typeof(Monster)] = EntityType.MONSTER,
            [typeof(INpc)] = EntityType.NPC,
            [typeof(Npc)] = EntityType.NPC,
            [typeof(IPlayer)] = EntityType.PLAYER,
            [typeof(Player)] = EntityType.PLAYER,
            [typeof(IDrop)] = EntityType.DROP,
            [typeof(Drop)] = EntityType.DROP
        };

        private readonly IDictionary<int, IMonster> _monsters;
        private readonly IDictionary<int, INpc> _npcs;
        private readonly IDictionary<int, IPlayer> _players;

        public Map(int id)
        {
            Id = id;

            _monsters = new Dictionary<int, IMonster>();
            _npcs = new Dictionary<int, INpc>();
            _drops = new Dictionary<int, IDrop>();
            _players = new Dictionary<int, IPlayer>();
        }

        public int Id { get; }

        public IEnumerable<IMonster> Monsters => _monsters.Values;
        public IEnumerable<INpc> Npcs => _npcs.Values;
        public IEnumerable<IDrop> Drops => _drops.Values;
        public IEnumerable<IPlayer> Players => _players.Values;

        public T GetEntity<T>(int id) where T : IEntity
        {
            EntityType entityType = _mapping.GetValueOrDefault(typeof(T));
            return (T)GetEntity(entityType, id);
        }

        public IEntity GetEntity(EntityType entityType, int id)
        {
            switch (entityType)
            {
                case EntityType.PLAYER:
                    return _players.GetValueOrDefault(id);
                case EntityType.DROP:
                    return _drops.GetValueOrDefault(id);
                case EntityType.MONSTER:
                    return _monsters.GetValueOrDefault(id);
                case EntityType.NPC:
                    return _npcs.GetValueOrDefault(id);
                default:
                    throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
            }
        }

        public void AddEntity(IEntity entity)
        {
            switch (entity.EntityType)
            {
                case EntityType.PLAYER:
                    var player = entity.As<Player>();
                    player.Map = this;
                    _players[entity.Id] = player;
                    break;
                case EntityType.MONSTER:
                    var monster = entity.As<Monster>();
                    monster.Map = this;
                    _monsters[entity.Id] = monster;
                    break;
                case EntityType.NPC:
                    var npc = entity.As<Npc>();
                    npc.Map = this;
                    _npcs[entity.Id] = npc;
                    break;
                case EntityType.DROP:
                    var drop = entity.As<Drop>();
                    drop.Map = this;
                    _drops[entity.Id] = drop;
                    break;
            }
        }

        public void RemoveEntity(IEntity entity)
        {
            switch (entity.EntityType)
            {
                case EntityType.PLAYER:
                    _players.Remove(entity.Id);
                    break;
                case EntityType.MONSTER:
                    _monsters.Remove(entity.Id);
                    break;
                case EntityType.NPC:
                    _npcs.Remove(entity.Id);
                    break;
                case EntityType.DROP:
                    _drops.Remove(entity.Id);
                    break;
            }
        }
    }
}