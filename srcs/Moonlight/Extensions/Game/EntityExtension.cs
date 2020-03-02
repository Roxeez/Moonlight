using Moonlight.Core;
using Moonlight.Game.Entities;

namespace Moonlight.Extensions.Game
{
    public static class EntityExtension
    {
        public static bool IsInRange(this Entity entity, Position position, int range) => entity.Position.IsInRange(position, range);
        public static bool IsInRange(this Entity entity, Entity target, int range) => entity.IsInRange(target.Position, range);
        
        public static int GetDistance(this Entity entity, Position position) => entity.Position.GetDistance(position);
        public static int GetDistance(this Entity entity, Entity target) => entity.GetDistance(target.Position);
        
        public static bool IsAlive(this LivingEntity entity) => entity.HpPercentage > 0;
        public static bool IsDead(this LivingEntity entity) => entity.HpPercentage <= 0;
    }
}