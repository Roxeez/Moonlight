using System.Collections.Generic;

namespace NtCore.API.Game.Maps
{
    /// <summary>
    ///     Represent a miniland map
    /// </summary>
    public interface IMiniland : IMap
    {
        /// <summary>
        ///     Miniland owner name
        /// </summary>
        string Owner { get; }

        /// <summary>
        ///     Amount of visitor (today)
        /// </summary>
        int Visitor { get; }

        /// <summary>
        ///     Amount of total visitor
        /// </summary>
        int TotalVisitor { get; }

        /// <summary>
        ///     Welcome message
        /// </summary>
        string Message { get; }

        /// <summary>
        ///     All objects in miniland
        /// </summary>
        IEnumerable<IMinilandObject> MinilandObjects { get; }

        /// <summary>
        ///     Get a miniland object by id
        /// </summary>
        /// <param name="id">Miniland object id</param>
        /// <returns>Miniland found or null if none</returns>
        IMinilandObject GetMinilandObject(int id);
    }
}