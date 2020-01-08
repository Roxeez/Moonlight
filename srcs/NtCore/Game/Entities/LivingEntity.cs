using System;

namespace NtCore.Game.Entities
{
    public abstract class LivingEntity : Entity, IEquatable<LivingEntity>
    {
        /// <summary>
        /// Name of the entity
        /// </summary>
        public string Name { get; internal set; }
        
        /// <summary>
        /// Speed of the entity
        /// </summary>
        public byte Speed { get; internal set; }
        
        /// <summary>
        /// Level of the entity
        /// </summary>
        public byte Level { get; internal set; }
        
        /// <summary>
        /// Direction of the entity
        /// </summary>
        public byte Direction { get; internal set; }
        
        /// <summary>
        /// Hp percentage of the entity
        /// </summary>
        public byte HpPercentage { get; internal set; }
        
        /// <summary>
        /// Mp percentage of the entity
        /// </summary>
        public byte MpPercentage { get; internal set; }
        
        /// <summary>
        /// Define if this entity is resting or not
        /// </summary>
        public bool IsResting { get; internal set; }

        public bool Equals(LivingEntity other) => other != null && other.EntityType == EntityType && other.Id == Id;
    }
}