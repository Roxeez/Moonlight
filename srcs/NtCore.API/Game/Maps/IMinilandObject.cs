namespace NtCore.API.Game.Maps
{
    public interface IMinilandObject
    {
        int Vnum { get; }
        int Id { get; }
        Position Position { get; }
    }
}