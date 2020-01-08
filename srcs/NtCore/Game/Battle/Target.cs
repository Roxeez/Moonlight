using System;
using NtCore.Game.Entities;

namespace NtCore.Game.Battle
{
    public class Target : IEquatable<Target>
    {
        internal Target(LivingEntity entity) => Entity = entity;

        /// <summary>
        /// Target entity
        /// </summary>
        public LivingEntity Entity { get; }

        /// <summary>
        /// Hp of the target
        /// </summary>
        public int Hp { get; internal set; }
        
        /// <summary>
        /// Mp of the target
        /// </summary>
        public int Mp { get; internal set; }
        
        public bool Equals(Target other) => other != null && Entity.Equals(other.Entity);
    }
}