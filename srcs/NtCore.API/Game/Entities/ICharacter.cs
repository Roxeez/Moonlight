using NtCore.API.Enums;

namespace NtCore.API.Game.Entities
{
    public interface ICharacter : ILivingEntity
    {
        string Name { get; }
        int Reputation { get; }
        int Dignity { get; }
        Faction Faction { get; }
        int SpPoint { get; }
        int AdditionalSpPoint { get; }
        int Gold { get; }
    }
}