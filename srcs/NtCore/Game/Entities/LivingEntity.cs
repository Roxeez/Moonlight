using NtCore.API;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;

namespace NtCore.Game.Entities
{
    public abstract class LivingEntity : ILivingEntity
    {
        public int Id { get; set; }
        public IMap Map { get; set; }
        public Position Position { get; set; }
        public EntityType EntityType { get; protected set; }
        public byte Speed { get; set; }
        public byte Level { get; set; }
        public byte Direction { get; set; }
        public byte HpPercentage { get; set; }
        public byte MpPercentage { get; set; }
        
        public bool Equals(ILivingEntity other)
        {
            return other != null && other.EntityType == EntityType && other.Id == Id;
        }
    }
}