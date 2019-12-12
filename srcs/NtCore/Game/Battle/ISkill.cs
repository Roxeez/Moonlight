using System;
using NtCore.Registry;

namespace NtCore.Game.Battle
{
    public interface ISkill : IEquatable<ISkill>
    {
        int Vnum { get; }
        string Name { get; }
        SkillInfo Info { get; }
    }
}