using System;

namespace NtCore.API.Game.Entities
{
    public interface ILivingEntity : IEntity, IEquatable<ILivingEntity>
    {
        /// <summary>
        ///     Current movement speed
        /// </summary>
        byte Speed { get; }

        byte Level { get; }
        byte Direction { get; }

        /// <summary>
        ///     Current hp percentage
        /// </summary>
        byte HpPercentage { get; }

        /// <summary>
        ///     Current mp percentage
        /// </summary>
        byte MpPercentage { get; }
    }
}