using NtCore.API.Enums;
using NtCore.API.Events.Character;
using NtCore.API.Events.Entity;
using NtCore.API.Extensions;
using NtCore.API.Game.Battle;
using NtCore.API.Game.Entities;
using NtCore.API.Plugins;

namespace NtCore.Example
{
    public class ExampleListener : IListener
    {
        [Handler]
        public void OnTargetChange(TargetChangeEvent e)
        {
            ITarget target = e.Target;
            ILivingEntity entity = e.Target.Entity;
            
            e.Character.ShowBubbleMessage($"{entity.Id} / {entity.EntityType} / Lv.{entity.Level}");
            e.Character.ShowChatMessage($"Hp: {target.Hp}", ChatMessageColor.GRAY);
            e.Character.ShowChatMessage($"Mp: {target.Mp}", ChatMessageColor.GRAY);
        }

        [Handler]
        public void OnEntitySpawn(EntitySpawnEvent e)
        {
            if (e.Entity.EntityType == EntityType.MONSTER)
            {
                var monster = e.Entity.As<IMonster>();
                
                e.Character.ShowChatMessage($"Monster {monster.Vnum} spawned at {monster.Position} on map {monster.Map.Id}", ChatMessageColor.RED);
            }
        }
    }
}