using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NtCore.Clients;

namespace NtCore.Game.Relation
{
    public class Friend : IEquatable<Friend>
    {
        private readonly IClient _client;

        public Friend(IClient client, int id, string name)
        {
            _client = client;

            Id = id;
            Name = name;
        }

        /// <summary>
        /// Friend player id
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// Friend Name
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Define if friend is connected or not
        /// </summary>
        public bool IsConnected { get; internal set; }

        public bool Equals(Friend other) => other != null && other.Id == Id;

        /// <summary>
        /// Send a private message
        /// </summary>
        /// <param name="message">Message to send</param>
        public async Task SendPrivateMessage([NotNull] string message)
        {
            if (!IsConnected)
            {
                return;
            }

            await _client.SendPacket($"btk {Id} {message}");
        }

        /// <summary>
        /// Delete friend from friendlist
        /// </summary>
        /// <returns></returns>
        public async Task Delete()
        {
            await _client.SendPacket($"fdel {Id}");
        }

        /// <summary>
        /// Join miniland of this friend
        /// </summary>
        /// <returns></returns>
        public async Task JoinMiniland()
        {
            if (!IsConnected)
            {
                return;
            }

            await _client.SendPacket($"mjoin 1 {Id}");
        }
    }
}