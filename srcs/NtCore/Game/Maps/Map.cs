using System;
using System.CodeDom;
using System.Collections.Generic;
using NtCore.API.Enums;
using NtCore.API.Extensions;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;
using NtCore.Extensions;
using NtCore.Game.Entities;

namespace NtCore.Game.Maps
{
    public class Map : IMap
    {
        private readonly IDictionary<int, IMonster> _monsters;
        private readonly IDictionary<int, INpc> _npcs;
        private readonly IDictionary<int, IDrop> _drops;
        private readonly IDictionary<int, IPlayer> _players;

        public int Id { get; }

        public IEnumerable<IMonster> Monsters => _monsters.Values;
        public IEnumerable<INpc> Npcs => _npcs.Values;
        public IEnumerable<IDrop> Drops => _drops.Values;
        public IEnumerable<IPlayer> Players => _players.Values;

        private readonly IDictionary<Type, EntityType> _mapping = new Dictionary<Type, EntityType>()
        {
            [typeof(IMonster)] = EntityType.Monster,
            [typeof(Monster)] = EntityType.Monster,
            [typeof(INpc)] = EntityType.Npc,
            [typeof(Npc)] = EntityType.Npc,
            [typeof(IPlayer)] = EntityType.Player,
            [typeof(Player)] = EntityType.Player,
            [typeof(IDrop)] = EntityType.Drop,
            [typeof(Drop)] = EntityType.Drop,
        };
        
        public T GetEntity<T>(int id) where T : IEntity
        {
            EntityType entityType = _mapping.GetValueOrDefault(typeof(T));
            return (T)GetEntity(entityType, id);
        }

        public IEntity GetEntity(EntityType entityType, int id)
        {
            switch (entityType)
            {
                case EntityType.Player:
                    return _players.GetValueOrDefault(id);
                case EntityType.Drop:
                    return _drops.GetValueOrDefault(id);
                case EntityType.Monster:
                    return _monsters.GetValueOrDefault(id);
                case EntityType.Npc:
                    return _npcs.GetValueOrDefault(id);
                default:
                    throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
            }
        }

        public Map(int id)
        {
            Id = id;
            
            _monsters = new Dictionary<int, IMonster>();
            _npcs = new Dictionary<int, INpc>();
            _drops = new Dictionary<int, IDrop>();
            _players = new Dictionary<int, IPlayer>();
        }
        
        public void AddEntity(IEntity entity)
        {
            switch (entity.EntityType)
            {
                case EntityType.Player:
                    var player = entity.As<Player>();
                    player.Map = this;
                    _players[entity.Id] = player;
                    break;
                case EntityType.Monster:
                    var monster = entity.As<Monster>();
                    monster.Map = this;
                    _monsters[entity.Id] = monster;
                    break;
                case EntityType.Npc:
                    var npc = entity.As<Npc>();
                    npc.Map = this;
                    _npcs[entity.Id] = npc;
                    break;
                case EntityType.Drop:
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
                case EntityType.Player:
                    _players.Remove(entity.Id);
                    break;
                case EntityType.Monster:
                    _monsters.Remove(entity.Id);
                    break;
                case EntityType.Npc:
                    _npcs.Remove(entity.Id);
                    break;
                case EntityType.Drop:
                    _drops.Remove(entity.Id);
                    break;
            }
        }
    }
}