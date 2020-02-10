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

namespace Moonlight.Game.Maps
{
    public class Map
    {
        private byte this[int x, int y] => Grid.Skip(4 + y * Width + x).Take(1).FirstOrDefault();
        
        internal Map(int id, string name, byte[] grid)
        {
            Id = id;
            Name = name;
            Grid = grid;
            Width = BitConverter.ToInt16(Grid.Take(2).ToArray(), 0);
            Height = BitConverter.ToInt16(Grid.Skip(2).Take(2).ToArray(), 0);
            
            Monsters = new SafeObservableCollection<Monster>();
            Npcs = new SafeObservableCollection<Npc>();
            Drops = new SafeObservableCollection<Drop>();
            Players = new SafeObservableCollection<Player>();
            Portals = new SafeObservableCollection<Portal>();
        }

        public int Id { get; }
        public string Name { get; }
        public byte[] Grid { get; }
        public short Width { get; }
        public short Height { get; }
        
        public SafeObservableCollection<Monster> Monsters { get; }
        public SafeObservableCollection<Npc> Npcs { get; }
        public SafeObservableCollection<Player> Players { get; }
        public SafeObservableCollection<Drop> Drops { get; }
        public SafeObservableCollection<Portal> Portals { get; }

        public Entity GetEntity(EntityType entityType, long entityId)
        {
            switch (entityType)
            {
                case EntityType.NPC:
                    return Npcs.FirstOrDefault(x => x.Id == entityId);
                case EntityType.MONSTER:
                    return Monsters.FirstOrDefault(x => x.Id == entityId);
                case EntityType.PLAYER:
                    return Players.FirstOrDefault(x => x.Id == entityId);
                case EntityType.DROP:
                    return Drops.FirstOrDefault(x => x.Id == entityId);
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
            return Portals.FirstOrDefault(x => x.Id == id);
        }

        internal void AddPortal(Portal portal)
        {
            Portals.Add(portal);
        }

        internal void AddEntity(Entity entity)
        {
            switch (entity.EntityType)
            {
                case EntityType.NPC:
                    Npcs.Add((Npc)entity);
                    break;
                case EntityType.MONSTER:
                    Monsters.Add((Monster)entity);
                    break;
                case EntityType.PLAYER:
                    Players.Add((Player)entity);
                    break;
                case EntityType.DROP:
                    Drops.Add((Drop)entity);
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
                    Npcs.Remove((Npc)entity);
                    break;
                case EntityType.MONSTER:
                    Monsters.Remove((Monster)entity);
                    break;
                case EntityType.PLAYER:
                    Players.Remove((Player)entity);
                    break;
                case EntityType.DROP:
                    Drops.Remove((Drop)entity);
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