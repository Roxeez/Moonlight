using Moonlight.Database.Dto;
using Moonlight.Game.Battle;
using NFluent;
using Xunit;

namespace Moonlight.Tests.Handling
{
    public class BattleHandlingTests : PacketHandlingTest
    {
        [Fact]
        public void Sr_Packet_Reset_Skill_Cooldown()
        {
            var skill = new Skill("dummy", new SkillDto { CastId = 5 })
            {
                IsOnCooldown = true
            };


            Character.Skills.Add(skill);
            
            Client.ReceivePacket("sr 5");

            Check.That(skill.IsOnCooldown).IsFalse();
        }
    }
}