using System.Collections.Generic;
using NtCore.Enums;
using NtCore.Game.Entities;

namespace NtCore.Game.Maps
{
    /// <summary>
    ///     Represent a map
    /// </summary>
    public interface IMap
    {
        /// <summary>
        ///     Map Id
        /// </summary>
        int Id { get; }

        string Name { get; }
        byte[] Data { get; }
        
        short Height { get; }
        short Width { get; }
        
        /// <summary>
        ///     Contains all monsters currently in map
        /// </summary>
        IEnumerable<IMonster> Monsters { get; }

        /// <summary>
        ///     Contains all npcs currently in map
        /// </summary>
        IEnumerable<INpc> Npcs { get; }

        /// <summary>
        ///     Contains all drops currently in map
        /// </summary>
        IEnumerable<IDrop> Drops { get; }

        /// <summary>
        ///     Contains all players currently in map
        /// </summary>
        IEnumerable<IPlayer> Players { get; }

        /// <summary>
        ///     Get entity using class type and id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <typeparam name="T">Type implementing IEntity</typeparam>
        /// <returns>Entity found or null if none</returns>
        T GetEntity<T>(int id) where T : IEntity;

        /// <summary>
        ///     Get entity by entity type and id
        /// </summary>
        /// <param name="entityType">Entity type</param>
        /// <param name="id">Entity id</param>
        /// <returns>Entity found or null if none</returns>
        IEntity GetEntity(EntityType entityType, int id);

        bool IsWalkable(Position position);
    }
}