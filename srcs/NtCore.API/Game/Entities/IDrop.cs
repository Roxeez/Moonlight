namespace NtCore.API.Game.Entities
{
    public interface IDrop : IEntity
    {
        int Vnum { get; }
        int Amount { get; }
    }
}