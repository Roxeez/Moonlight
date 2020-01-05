using System;
using NtCore.Game.Entities;

namespace NtCore.Game.Battle.Impl
{
    public class Target : ITarget
    {
        public ILivingEntity Entity { get; }
        
        public int Hp { get; set; }
        public int Mp { get; set; }

        public Target(ILivingEntity entity)
        {
            Entity = entity;
        }
    }
}