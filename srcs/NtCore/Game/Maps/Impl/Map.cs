using System;
using System.Collections.Generic;
using System.Linq;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;

namespace NtCore.Game.Maps.Impl
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

        public Map(int id, byte[] data)
        {
            Id = id;
            Data = data;
            Height = BitConverter.ToInt16(Data.Take(2).ToArray(), 0);
            Width = BitConverter.ToInt16(Data.Skip(2).Take(2).ToArray(), 0);
            
            _monsters = new Dictionary<int, IMonster>();
            _npcs = new Dictionary<int, INpc>();
            _drops = new Dictionary<int, IDrop>();
            _players = new Dictionary<int, IPlayer>();
        }

        private byte this[Position position] => Data.Skip(4 + position.Y * Height + position.X).FirstOrDefault();
        
        public int Id { get; }
        public byte[] Data { get; }
        public short Height { get; }
        public short Width { get; }

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

        public bool IsWalkable(Position position)
        {
            if (position.X > Height || position.X < 0 || (position.Y > Width) || (position.Y < 0))
            {
                return false;
            }

            byte value = this[position];
            
            return value == 0 || value == 2 || value >= 16 && value <= 19;
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void RemoveEntity(IEntity entity)
        {
            RemoveEntity(entity.EntityType, entity.Id);
        }

        public void RemoveEntity(EntityType entityType, int entityId)
        {
            switch (entityType)
            {
                case EntityType.PLAYER:
                    _players.Remove(entityId);
                    break;
                case EntityType.MONSTER:
                    _monsters.Remove(entityId);
                    break;
                case EntityType.NPC:
                    _npcs.Remove(entityId);
                    break;
                case EntityType.DROP:
                    _drops.Remove(entityId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
            }
        }
    }
}