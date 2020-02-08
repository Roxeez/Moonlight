using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Moonlight.Core;
using Moonlight.Core.Enums;
using Moonlight.Core.Extensions;
using Moonlight.Game.Entities;
using Moonlight.Game.Utility;
using Swordfish.NET.Collections;

namespace Moonlight.Game.Maps
{
    public class Map
    {
        private readonly ConcurrentObservableCollection<Monster> _monsters;
        private readonly ConcurrentObservableCollection<Npc> _npcs;
        private readonly ConcurrentObservableCollection<Drop> _drops;
        private readonly ConcurrentObservableCollection<Player> _players;
        private readonly ConcurrentObservableCollection<Portal> _portals;

        private byte this[int x, int y] => Grid.Skip(4 + y * Width + x).Take(1).FirstOrDefault();
        
        internal Map(int id, string name, byte[] grid)
        {
            Id = id;
            Name = name;
            Grid = grid;
            Width = BitConverter.ToInt16(Grid.Take(2).ToArray(), 0);
            Height = BitConverter.ToInt16(Grid.Skip(2).Take(2).ToArray(), 0);
            
            _monsters = new ConcurrentObservableCollection<Monster>();
            _npcs = new ConcurrentObservableCollection<Npc>();
            _drops = new ConcurrentObservableCollection<Drop>();
            _players = new ConcurrentObservableCollection<Player>();
            _portals = new ConcurrentObservableCollection<Portal>();
        }

        public int Id { get; }
        public string Name { get; }
        public byte[] Grid { get; }
        
        public short Width { get; }
        public short Height { get; }

        public IEnumerable<Monster> Monsters => _monsters;
        public IEnumerable<Npc> Npcs => _npcs;
        public IEnumerable<Player> Players => _players;
        public IEnumerable<Drop> Drops => _drops;
        public IEnumerable<Portal> Portals => _portals;

        public Entity GetEntity(EntityType entityType, long entityId)
        {
            switch (entityType)
            {
                case EntityType.NPC:
                    return _npcs.FirstOrDefault(x => x.Id == entityId);
                case EntityType.MONSTER:
                    return _monsters.FirstOrDefault(x => x.Id == entityId);
                case EntityType.PLAYER:
                    return _players.FirstOrDefault(x => x.Id == entityId);
                case EntityType.DROP:
                    return _drops.FirstOrDefault(x => x.Id == entityId);
                default:
                    throw new InvalidOperationException();
            }
        }

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

        public Portal GetPortal(int id)
        {
            return _portals.FirstOrDefault(x => x.Id == id);
        }

        internal void AddPortal(Portal portal)
        {
            _portals[portal.Id] = portal;
        }

        internal void AddEntity(Entity entity)
        {
            switch (entity.EntityType)
            {
                case EntityType.NPC:
                    _npcs.Add((Npc)entity);
                    break;
                case EntityType.MONSTER:
                    _monsters.Add((Monster)entity);
                    break;
                case EntityType.PLAYER:
                    _players.Add((Player)entity);
                    break;
                case EntityType.DROP:
                    _drops.Add((Drop)entity);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            entity.Map = this;
        }

        internal void RemoveEntity(Entity entity)
        {
            switch (entity.EntityType)
            {
                case EntityType.NPC:
                    _npcs.Remove((Npc)entity);
                    break;
                case EntityType.MONSTER:
                    _monsters.Remove((Monster)entity);
                    break;
                case EntityType.PLAYER:
                    _players.Remove((Player)entity);
                    break;
                case EntityType.DROP:
                    _drops.Remove((Drop)entity);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        internal void RemoveEntity(EntityType entityType, long entityId)
        {
            Entity entity = GetEntity(entityType, entityId);
            if (entity == null)
            {
                return;
            }
            
            RemoveEntity(entity);
        }
        
        public bool IsWalkable(Position position)
        {
            if (position.X > Width || position.X < 0 || position.Y > Height || position.Y < 0)
            {
                return false;
            }

            byte value = this[position.X, position.Y];

            return value == 0 || value == 2 || value >= 16 && value <= 19;
        }
    }
}