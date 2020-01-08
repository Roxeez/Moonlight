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

        /// <summary>
        /// Skill vnum
        /// </summary>
        public int Vnum { get; }
        
        /// <summary>
        /// Name of the skill
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Information about this skill
        /// </summary>
        public SkillInfo Info { get; }

        public bool Equals(Skill other) => other != null && other.Vnum.Equals(Vnum);
    }
}