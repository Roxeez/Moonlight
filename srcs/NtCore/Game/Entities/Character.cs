using NtCore.API;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;

namespace NtCore.Game.Entities
{
    public class Character : ICharacter
    {
        public int Id { get; set; }
        public IMap Map { get; set; }
        public Position Position { get; set; }
        public string Name { get; set; }
        public int Reputation { get; set; }
        public int Dignity { get; set; }
        public Faction Faction { get; set; }
        public int SpPoints { get; set; }
        public int AdditionalSpPoints { get; set; }
        public int MaximumSpPoints { get; set; }
        public int MaximumAdditionalSpPoints { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }
        public byte HpPercentage { get; set; }
        public byte MpPercentage { get; set; }
    }
}