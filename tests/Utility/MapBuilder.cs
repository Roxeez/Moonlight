using System;
using System.Collections.Generic;
using System.Linq;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities.Impl;
using NtCore.Game.Maps;
using NtCore.Game.Maps.Impl;
using NtCore.Resources;

namespace NtCore.Tests.Utility
{
    public class MapBuilder
    {
        private IEnumerable<Drop> _drops = new List<Drop>();
        private int _id;
        private bool _isMiniland;
        private IEnumerable<MinilandObject> _minilandObjects = new List<MinilandObject>();
        private IEnumerable<Monster> _monsters = new List<Monster>();
        private IEnumerable<Npc> _npcs = new List<Npc>();
        private IEnumerable<Player> _players = new List<Player>();
        
        public MapBuilder()
        {
            
        }
        
        public MapBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public MapBuilder WithMonsters(params int[] monsters)
        {
            _monsters = monsters.Select(x => new Monster
            {
                Id = x
            });
            return this;
        }

        public MapBuilder WithEntity(EntityType entityType, int id)
        {
            switch (entityType)
            {
                case EntityType.NPC:
                    return WithNpcs(id);
                case EntityType.DROP:
                    return WithDrops(id);
                case EntityType.MONSTER:
                    return WithMonsters(id);
                case EntityType.PLAYER:
                    return WithPlayers(id);
                default:
                    throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
            }
        }

        public MapBuilder WithMinilandObjects(params MinilandObject[] objects)
        {
            _minilandObjects = objects;
            return this;
        }

        public MapBuilder WithNpcs(params int[] npcs)
        {
            _npcs = npcs.Select(x => new Npc
            {
                Id = x
            });
            return this;
        }

        public MapBuilder WithPlayers(params int[] players)
        {
            _players = players.Select(x => new Player
            {
                Id = x
            });
            return this;
        }

        public MapBuilder WithDrops(params int[] drops)
        {
            _drops = drops.Select(x => new Drop
            {
                Id = x
            });
            return this;
        }

        public MapBuilder AsMiniland()
        {
            _isMiniland = true;
            return this;
        }

        public Map Create()
        {
            Map map = _isMiniland ? new Miniland(Resource.Read($"maps.20001")) : new Map(_id, Resource.Read($"maps.{_id}"));

            foreach (Monster monster in _monsters)
            {
                map.AddEntity(monster);
            }

            foreach (Npc npc in _npcs)
            {
                map.AddEntity(npc);
            }

            foreach (Player player in _players)
            {
                map.AddEntity(player);
            }

            foreach (Drop drop in _drops)
            {
                map.AddEntity(drop);
            }

            if (map is Miniland miniland)
            {
                foreach (MinilandObject obj in _minilandObjects)
                {
                    miniland.AddMinilandObject(obj);
                }
            }

            return map;
        }
    }
}