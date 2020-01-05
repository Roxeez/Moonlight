using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using NtCore.Core;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;

namespace NtCore.Game.Maps
{
    public class Map
    {
        private static readonly IDictionary<Type, EntityType> _mapping = new Dictionary<Type, EntityType>
        {
            [typeof(Monster)] = EntityType.MONSTER,
            [typeof(Npc)] = EntityType.NPC,
            [typeof(Player)] = EntityType.PLAYER,
            [typeof(Drop)] = EntityType.DROP
        };

        private readonly IDictionary<int, Drop> _drops;
        private readonly IDictionary<int, Monster> _monsters;
        private readonly IDictionary<int, Npc> _npcs;
        private readonly IDictionary<int, Player> _players;

        public Map(int id, byte[] data)
        {
            Id = id;
            Data = data;
            Width = BitConverter.ToInt16(Data.Take(2).ToArray(), 0);
            Height = BitConverter.ToInt16(Data.Skip(2).Take(2).ToArray(), 0);

            _monsters = new ConcurrentDictionary<int, Monster>();
            _npcs = new ConcurrentDictionary<int, Npc>();
            _drops = new ConcurrentDictionary<int, Drop>();
            _players = new ConcurrentDictionary<int, Player>();
        }

        private byte this[Position position] => Data.Skip(4 + position.Y * Width + position.X).Take(1).FirstOrDefault();

        public int Id { get; }
        public string Name { get; set; }
        public byte[] Data { get; }
        public short Height { get; }
        public short Width { get; }

        public IEnumerable<Monster> Monsters => _monsters.Values;
        public IEnumerable<Npc> Npcs => _npcs.Values;
        public IEnumerable<Drop> Drops => _drops.Values;
        public IEnumerable<Player> Players => _players.Values;

        [CanBeNull]
        public T GetEntity<T>(int id) where T : Entity
        {
            EntityType entityType = _mapping.GetValueOrDefault(typeof(T));
            return GetEntity<T>(entityType, id);
        }

        [CanBeNull]
        public T GetEntity<T>(EntityType entityType, int id) where T : Entity => GetEntity(entityType, id) as T;

        [CanBeNull]
        public Entity GetEntity(EntityType entityType, int id)
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
            if (position.X > Width || position.X < 0 || position.Y > Height || position.Y < 0)
            {
                Trace.WriteLine("false");
                return false;
            }

            byte value = this[position];

            return value == 0 || value == 2 || value >= 16 && value <= 19;
        }

        internal void AddEntity(Entity entity)
        {
            switch (entity.EntityType)
            {
                case EntityType.PLAYER:
                    var player = (Player)entity;
                    player.Map = this;
                    _players[entity.Id] = player;
                    break;
                case EntityType.MONSTER:
                    var monster = (Monster)entity;
                    monster.Map = this;
                    _monsters[entity.Id] = monster;
                    break;
                case EntityType.NPC:
                    var npc = (Npc)entity;
                    npc.Map = this;
                    _npcs[entity.Id] = npc;
                    break;
                case EntityType.DROP:
                    var drop = (Drop)entity;
                    drop.Map = this;
                    _drops[entity.Id] = drop;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal void RemoveEntity(Entity entity)
        {
            RemoveEntity(entity.EntityType, entity.Id);
        }

        internal void RemoveEntity(EntityType entityType, int entityId)
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