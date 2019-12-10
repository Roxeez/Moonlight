using System;

namespace NtCore.Game.Entities
{
    /// <summary>
    ///     Represent a LivingEntity
    /// </summary>
    public interface ILivingEntity : IEntity, IEquatable<ILivingEntity>
    {
        /// <summary>
        ///     Current movement speed
        /// </summary>
        byte Speed { get; }

        /// <summary>
        ///     Current level
        /// </summary>
        byte Level { get; }

        /// <summary>
        ///     Current direction
        /// </summary>
        byte Direction { get; }

        /// <summary>
        ///     Current hp percentage
        /// </summary>
        byte HpPercentage { get; }

        /// <summary>
        ///     Current mp percentage
        /// </summary>
        byte MpPercentage { get; }
        
        string Name { get; }
    }
}