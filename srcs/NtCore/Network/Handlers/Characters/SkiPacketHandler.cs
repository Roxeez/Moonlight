using NtCore.Clients;
using NtCore.Enums;
using NtCore.Game.Battle;
using NtCore.Game.Entities;
using NtCore.Game.Factory;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class SkiPacketHandler : PacketHandler<SkiPacket>
    {
        private readonly ISkillFactory _skillFactory;

        public SkiPacketHandler(ISkillFactory skillFactory) => _skillFactory = skillFactory;

        public override void Handle(IClient client, SkiPacket packet)
        {
            Character character = client.Character;

            character.Skills.Clear();

            foreach (int skillVnum in packet.Skills)
            {
                Skill skill = _skillFactory.CreateSkill(skillVnum);
                if (skill.Info.SkillType != SkillType.PLAYER) // Filter upgrades & skill upgrades
                {
                    continue;
                }

                character.Skills.Add(skill);
            }
        }
    }
}