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

        IMonster GetMonster(long id);
        INpc GetNpc(long id);
        IDrop GetDrop(long id);
    }
}