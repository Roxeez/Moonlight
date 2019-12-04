using System.Collections.Generic;
using NtCore.API.Extensions;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;
using NtCore.Game.Entities;

namespace NtCore.Game.Maps
{
    public class Map : IMap
    {
        private readonly IDictionary<long, IMonster> _monsters;
        private readonly IDictionary<long, INpc> _npcs;
        private readonly IDictionary<long, IDrop> _drops;
        
        public int Id { get; }

        public IEnumerable<IMonster> Monsters => _monsters.Values;
        public IEnumerable<INpc> Npcs => _npcs.Values;
        public IEnumerable<IDrop> Drops => _drops.Values;

        public Map(int id)
        {
            Id = id;
            
            _monsters = new Dictionary<long, IMonster>();
            _npcs = new Dictionary<long, INpc>();
            _drops = new Dictionary<long, IDrop>();
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
        
        public IMonster GetMonster(long id)
        {
            return _monsters.GetValueOrDefault(id);
        }

        public INpc GetNpc(long id)
        {
            return _npcs.GetValueOrDefault(id);
        }

        public IDrop GetDrop(long id)
        {
            return _drops.GetValueOrDefault(id);
        }
    }
}