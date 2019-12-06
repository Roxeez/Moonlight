using NtCore.API.Enums;

namespace NtCore.API.Game.Entities
{
    public interface IPlayer : ILivingEntity
    {
        /// <summary>
        /// Player name
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Player reputation
        /// </summary>
        int Reputation { get; }
        
        /// <summary>
        /// Player dignity
        /// </summary>
        int Dignity { get; }
        
        ClassType Class { get; }
        Gender Gender { get; }
        
        byte Level { get; }
        
        /// <summary>
        /// Player faction
        /// </summary>
        Faction Faction { get; }
    }
}