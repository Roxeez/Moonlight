using System;
using NtCore.Enums;

namespace NtCore.Game.Battle
{
    public interface ISkill : IEquatable<ISkill>
    {
        int Vnum { get; }
        string Name { get; }
        int MpCost { get; }
        SkillType SkillType { get; }
        int Cooldown { get; }
    }
}