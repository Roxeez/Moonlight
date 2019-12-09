using NtCore.Game.Entities;

namespace NtCore.Game.Battle
{
    public interface ITarget
    {
        /// <summary>
        ///     Target entity
        /// </summary>
        ILivingEntity Entity { get; }

        /// <summary>
        ///     Hp of target
        /// </summary>
        int Hp { get; }

        /// <summary>
        ///     Mp of target
        /// </summary>
        int Mp { get; }
    }
}