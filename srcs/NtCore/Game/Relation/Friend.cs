using NtCore.Clients;

namespace NtCore.Game.Relation
{
    public class Friend
    {
        private readonly IClient _client;

        public Friend(IClient client, int id, string name)
        {
            _client = client;

            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
        public bool IsConnected { get; internal set; }

        public bool Equals(Friend other) => other != null && other.Id == Id;

        public void SendPrivateMessage(string message)
        {
            if (!IsConnected)
            {
                return;
            }

            _client.SendPacket($"btk {Id} {message}");
        }

        public void Delete()
        {
            _client.SendPacket($"fdel {Id}");
        }

        public void JoinMiniland()
        {
            if (!IsConnected)
            {
                return;
            }

            _client.SendPacket($"mjoin 1 {Id}");
        }
    }
}