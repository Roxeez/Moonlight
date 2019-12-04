using NtCore.Game.Maps;

namespace NtCore.Game.Entities.Impl
{
    public class Character : ICharacter
    {
        public long Id { get; set; }
        public IMap Map { get; set; }
        public Position Position { get; set; }
        
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }
    }
}