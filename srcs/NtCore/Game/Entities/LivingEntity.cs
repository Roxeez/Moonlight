using System;

namespace NtCore.Game.Entities
{
    public abstract class LivingEntity : Entity, IEquatable<LivingEntity>
    {
        public string Name { get; internal set; }
        public byte Speed { get; internal set; }
        public byte Level { get; internal set; }
        public byte Direction { get; internal set; }
        public byte HpPercentage { get; internal set; }
        public byte MpPercentage { get; internal set; }
        public bool IsResting { get; internal set; }

        public bool Equals(LivingEntity other) => other != null && other.EntityType == EntityType && other.Id == Id;
    }
}