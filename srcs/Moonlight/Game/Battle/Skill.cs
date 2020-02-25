using System;
using Moonlight.Core.Enums;
using Moonlight.Database.Dto;

namespace Moonlight.Game.Battle
{
    public sealed class Skill : IEquatable<Skill>
    {
        private readonly SkillDto _skillDto;

        internal Skill(string name, SkillDto skillDto)
        {
            _skillDto = skillDto;
            Name = name;
        }

        public int Id => _skillDto.Id;
        public string Name { get; }
        public short Range => _skillDto.Range;
        public short ZoneRange => _skillDto.ZoneRange;
        public int CastTime => _skillDto.CastTime;
        public int Cooldown => _skillDto.Cooldown;
        public SkillType SkillType => _skillDto.SkillType;
        public int MpCost => _skillDto.MpCost;
        public int CastId => _skillDto.CastId;
        public TargetType TargetType => _skillDto.TargetType;
        public HitType HitType => _skillDto.HitType;
        public bool IsOnCooldown { get; internal set; }

        public bool Equals(Skill other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * 397;
            }
        }
    }
}