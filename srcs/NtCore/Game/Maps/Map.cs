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
        private readonly IDictionary<int, IMonster> _monsters;
        private readonly IDictionary<int, INpc> _npcs;
        private readonly IDictionary<int, IDrop> _drops;
        private readonly IDictionary<int, IPlayer> _players;

        public int Id { get; }

        public IEnumerable<IMonster> Monsters => _monsters.Values;
        public IEnumerable<INpc> Npcs => _npcs.Values;
        public IEnumerable<IDrop> Drops => _drops.Values;
        public IEnumerable<IPlayer> Players => _players.Values;

        public Map(int id)
        {
            Id = id;
            
            _monsters = new Dictionary<int, IMonster>();
            _npcs = new Dictionary<int, INpc>();
            _drops = new Dictionary<int, IDrop>();
            _players = new Dictionary<int, IPlayer>();
        }

        public void AddMonster(Monster monster)
        {
            _monsters[monster.Id] = monster;
            monster.Map = this;
        }

        public void AddNpc(Npc npc)
        {
            _npcs[npc.Id] = npc;
            npc.Map = this;
        }

        public void AddDrop(Drop drop)
        {
            _drops[drop.Id] = drop;
            drop.Map = this;
        }

        public void AddPlayer(Player player)
        {
            _players[player.Id] = player;
            player.Map = this;
        }
        
        public IMonster GetMonster(int id)
        {
            return _monsters.GetValueOrDefault(id);
        }

        public INpc GetNpc(int id)
        {
            return _npcs.GetValueOrDefault(id);
        }

        public IDrop GetDrop(int id)
        {
            return _drops.GetValueOrDefault(id);
        }

        public ILivingEntity GetLivingEntity(EntityType entityType, int id)
        {
            switch (entityType)
            {
                case EntityType.Player:
                    return _players.GetValueOrDefault(id);
                case EntityType.Npc:
                    return _npcs.GetValueOrDefault(id);
                case EntityType.Monster:
                    return _monsters.GetValueOrDefault(id);
                case EntityType.Drop:
                    throw new InvalidOperationException("Drop is not a living entity");
                default:
                    throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
            }
        }

        public IPlayer GetPlayer(int id)
        {
            return _players.GetValueOrDefault(id);
        }
    }
}