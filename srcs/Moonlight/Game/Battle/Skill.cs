using Moonlight.Core.Enums;
using Moonlight.Database.Dto;

namespace Moonlight.Game.Battle
{
    public sealed class Skill
    {
        private readonly SkillDto _skillDto;

        public int Id => _skillDto.Id;
        public string Name { get; }
        public short Range => _skillDto.Range;
        public int Cooldown => _skillDto.Cooldown;
        public SkillType SkillType => _skillDto.SkillType;
        public int MpCost => _skillDto.MpCost;
        public int CastId => _skillDto.CastId;
        public TargetType TargetType => _skillDto.TargetType;

        internal Skill(string name, SkillDto skillDto)
        {
            _skillDto = skillDto;
            Name = name;
        }
    }
}