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
        private readonly IDictionary<int, Portal> _portals;

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
            _portals = new ConcurrentDictionary<int, Portal>();
        }

        private byte this[int x, int y] => Data.Skip(4 + y * Width + x).Take(1).FirstOrDefault();

        /// <summary>
        /// Id of the map
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// Name of the map
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Data of the map
        /// </summary>
        public byte[] Data { get; }
        
        /// <summary>
        /// Height of the map
        /// </summary>
        public short Height { get; }
        
        /// <summary>
        /// Width of the map
        /// </summary>
        public short Width { get; }

        /// <summary>
        /// Monsters in the map
        /// </summary>
        public IEnumerable<Monster> Monsters => _monsters.Values;
        
        /// <summary>
        /// Npcs in the map
        /// </summary>
        public IEnumerable<Npc> Npcs => _npcs.Values;
        
        /// <summary>
        /// Drops in the map
        /// </summary>
        public IEnumerable<Drop> Drops => _drops.Values;
        
        /// <summary>
        /// Players in the map
        /// </summary>
        public IEnumerable<Player> Players => _players.Values;
        
        /// <summary>
        /// Portals in the map
        /// </summary>
        public IEnumerable<Portal> Portals => _portals.Values;

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <returns>Entity found or null if none</returns>
        [CanBeNull]
        public T GetEntity<T>(int id) where T : Entity
        {
            EntityType entityType = _mapping.GetValueOrDefault(typeof(T));
            return GetEntity<T>(entityType, id);
        }

        /// <summary>
        /// Get entity by id & entity type
        /// </summary>
        /// <param name="entityType">Type of entity</param>
        /// <param name="id">Id of the entity</param>
        /// <typeparam name="T">Type of easy casting</typeparam>
        /// <returns>Found entity casted to T</returns>
        [CanBeNull]
        public T GetEntity<T>(EntityType entityType, int id) where T : Entity => GetEntity(entityType, id) as T;

        /// <summary>
        /// Find entity by entity type & id
        /// </summary>
        /// <param name="entityType">Type of entity</param>
        /// <param name="id">Id of the entity</param>
        /// <returns>Entity found</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        internal Portal GetPortal(int id)
        {
            return _portals.GetValueOrDefault(id);
        }
        
        /// <summary>
        /// Check if a position is walkable in the map
        /// </summary>
        /// <param name="position">Position to check</param>
        /// <returns>True if walkable false if not walkable</returns>
        public bool IsWalkable(Position position)
        {
            if (position.X > Width || position.X < 0 || position.Y > Height || position.Y < 0)
            {
                return false;
            }

            byte value = this[position.X, position.Y];

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

        internal void AddPortal(Portal portal)
        {
            _portals[portal.Id] = portal;
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