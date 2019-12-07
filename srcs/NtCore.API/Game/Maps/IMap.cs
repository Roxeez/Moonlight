using System.Collections.Generic;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;

namespace NtCore.API.Game.Maps
{
    public interface IMap
    {
        int Id { get; }

        IEnumerable<IMonster> Monsters { get; }
        IEnumerable<INpc> Npcs { get; }
        IEnumerable<IDrop> Drops { get; }
        IEnumerable<IPlayer> Players { get; }

        T GetEntity<T>(int id) where T : IEntity;

        IEntity GetEntity(EntityType entityType, int id);
    }
}