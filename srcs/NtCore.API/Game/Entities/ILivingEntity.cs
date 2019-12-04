namespace NtCore.API.Game.Entities
{
    public interface ILivingEntity : IEntity
    {
        int Level { get; }
        byte Speed { get; }
        
        int Hp { get; }
        int Mp { get; }
        
        int MaxHp { get; }
        int MaxMp { get; }
        
        byte HpPercentage { get; }
        byte MpPercentage { get; }
    }
}