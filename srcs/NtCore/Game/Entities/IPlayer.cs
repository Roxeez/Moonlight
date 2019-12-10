using NtCore.Enums;

namespace NtCore.Game.Entities
{
    /// <summary>
    ///     Represent a player
    /// </summary>
    public interface IPlayer : ILivingEntity
    {
        
        /// <summary>
        ///     Player reputation
        /// </summary>
        int Reputation { get; }

        /// <summary>
        ///     Player dignity
        /// </summary>
        int Dignity { get; }

        /// <summary>
        ///     Player class
        /// </summary>
        ClassType Class { get; }

        /// <summary>
        ///     Player gender
        /// </summary>
        Gender Gender { get; }

        /// <summary>
        ///     Player faction
        /// </summary>
        Faction Faction { get; }
    }
}