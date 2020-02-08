using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly ObservableCollection<Monster> _monsters;
        private readonly ObservableCollection<Npc> _npcs;
        private readonly ObservableCollection<Drop> _drops;
        private readonly ObservableCollection<Player> _players;
        private readonly ObservableCollection<Portal> _portals;

        private byte this[int x, int y] => Grid.Skip(4 + y * Width + x).Take(1).FirstOrDefault();
        
        internal Map(int id, string name, byte[] grid)
        {
            Id = id;
            Name = name;
            Grid = grid;
            Width = BitConverter.ToInt16(Grid.Take(2).ToArray(), 0);
            Height = BitConverter.ToInt16(Grid.Skip(2).Take(2).ToArray(), 0);
            
            _monsters = new ObservableCollection<Monster>();
            _npcs = new ObservableCollection<Npc>();
            _drops = new ObservableCollection<Drop>();
            _players = new ObservableCollection<Player>();
            _portals = new ObservableCollection<Portal>();
            
            Monsters = new ReadOnlyObservableCollection<Monster>(_monsters);
            Npcs = new ReadOnlyObservableCollection<Npc>(_npcs);
            Drops = new ReadOnlyObservableCollection<Drop>(_drops);
            Players = new ReadOnlyObservableCollection<Player>(_players);
            Portals = new ReadOnlyObservableCollection<Portal>(_portals);
        }

        public int Id { get; }
        public string Name { get; }
        public byte[] Grid { get; }
        
        public short Width { get; }
        public short Height { get; }
        
        public ReadOnlyObservableCollection<Monster> Monsters { get; }
        public ReadOnlyObservableCollection<Npc> Npcs { get; }
        public ReadOnlyObservableCollection<Player> Players { get; }
        public ReadOnlyObservableCollection<Drop> Drops { get; }
        public ReadOnlyObservableCollection<Portal> Portals { get; }

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
            _portals.Add(portal);
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