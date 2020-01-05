using System;
using NtCore.Registry;

namespace NtCore.Game.Battle
{
    public class Skill : IEquatable<Skill>
    {
        public Skill(SkillInfo skillInfo) => Info = skillInfo;

        public int Vnum { get; internal set; }
        public string Name { get; internal set; }
        public SkillInfo Info { get; }

        public bool Equals(Skill other) => other != null && other.Vnum.Equals(Vnum);
    }
}