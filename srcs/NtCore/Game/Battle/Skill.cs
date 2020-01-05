using System;
using NtCore.Registry;

namespace NtCore.Game.Battle
{
    public class Skill : IEquatable<Skill>
    {
        public Skill(int vnum, string name, SkillInfo skillInfo)
        {
            Vnum = vnum;
            Info = skillInfo;
            Name = name;
        }

        public int Vnum { get; }
        public string Name { get; }
        public SkillInfo Info { get; }

        public bool Equals(Skill other) => other != null && other.Vnum.Equals(Vnum);
    }
}