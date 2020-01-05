using System;
using NtCore.Game.Entities;

namespace NtCore.Game.Battle
{
    public class Target : IEquatable<Target>
    {
        public LivingEntity Entity { get; }
        
        public int Hp { get; set; }
        public int Mp { get; set; }

        public Target(LivingEntity entity)
        {
            Entity = entity;
        }


        public bool Equals(Target other) => other != null && Entity.Equals(other.Entity);
    }
}