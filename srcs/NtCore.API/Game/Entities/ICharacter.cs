using NtCore.API.Enums;

namespace NtCore.API.Game.Entities
{
    public interface ICharacter : ILivingEntity
    {
        string Name { get; }
        int Reputation { get; }
        int Dignity { get; }
        Faction Faction { get; }
        int SpPoints { get; }
        int AdditionalSpPoints { get; }
        int MaximumSpPoints { get; }
        int MaximumAdditionalSpPoints { get; }
        int Gold { get; }
    }
}