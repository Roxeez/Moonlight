namespace NtCore.Game.Relation.Impl
{
    public class Friend : IFriend
    {
        public int Id { get; }
        public string Name { get; }

        public Friend(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool Equals(IFriend other) => other != null && other.Id == Id;
    }
}