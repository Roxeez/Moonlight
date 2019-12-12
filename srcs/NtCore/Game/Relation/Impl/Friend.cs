using NtCore.Clients;

namespace NtCore.Game.Relation.Impl
{
    public class Friend : IFriend
    {
        private readonly IClient _client;
        
        public int Id { get; }
        public string Name { get; }
        public bool IsConnected { get; set; }

        public Friend(IClient client, int id, string name)
        {
            _client = client;
            
            Id = id;
            Name = name;
        }

        public bool Equals(IFriend other) => other != null && other.Id == Id;
        
        public void SendPrivateMessage(string message)
        {
            _client.SendPacket($"btk {Id} {message}");
        }
    }
}