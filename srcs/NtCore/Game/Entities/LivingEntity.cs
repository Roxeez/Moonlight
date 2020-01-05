using System;
using NtCore.Core;
using NtCore.Enums;
using NtCore.Game.Maps;

namespace NtCore.Game.Entities
{
    public abstract class LivingEntity : Entity, IEquatable<LivingEntity>
    {
        public string Name { get; set; }
        public Map Map { get; set; }
        public Position Position { get; set; }
        public byte Speed { get; set; }
        public byte Level { get; set; }
        public byte Direction { get; set; }
        public byte HpPercentage { get; set; }
        public byte MpPercentage { get; set; }

        public bool Equals(LivingEntity other) => other != null && other.EntityType == EntityType && other.Id == Id;
    }
}