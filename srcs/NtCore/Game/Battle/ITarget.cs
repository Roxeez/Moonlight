using NtCore.Game.Entities;

namespace NtCore.Game.Battle
{
    public interface ITarget
    {
        ILivingEntity Entity { get; }
        
        int Hp { get; }
        int Mp { get; }
    }
}