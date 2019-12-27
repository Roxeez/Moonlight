using NtCore.Core;

namespace NtCore.Game.Maps
{
    /// <summary>
    ///     Represent a miniland object
    /// </summary>
    public interface IMinilandObject
    {
        /// <summary>
        ///     Object vnum
        /// </summary>
        int Vnum { get; }

        /// <summary>
        ///     Object id
        /// </summary>
        int Id { get; }

        /// <summary>
        ///     Position in miniland
        /// </summary>
        Position Position { get; }
    }
}