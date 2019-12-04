using System.Collections.Generic;
using NtCore.API.Game.Entities;

namespace NtCore.API.Game.Maps
{
    public interface IMap
    {
        int Id { get; }
        
        IEnumerable<IMonster> Monsters { get; }
        IEnumerable<INpc> Npcs { get; }
        IEnumerable<IDrop> Drops { get; }

        IMonster GetMonster(int id);
        INpc GetNpc(int id);
        IDrop GetDrop(int id);
    }
}