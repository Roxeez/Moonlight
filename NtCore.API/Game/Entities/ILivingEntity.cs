namespace NtCore.Game.Entities
{
    public interface ILivingEntity : IEntity
    {
        int Level { get; set; }
        int Hp { get; set; }
        int Mp { get; set; }
        
        int MaxHp { get; set; }
        int MaxMp { get; set; }
    }
}