using System;
using NtCore.API;
using NtCore.API.Clients;
using NtCore.API.Enums;
using NtCore.API.Game.Battle;
using NtCore.API.Game.Entities;

namespace NtCore.Game.Entities
{
    public class Character : Player, ICharacter
    {
        private readonly IClient _client;

        public Character(IClient client)
        {
            _client = client;
            LastMapChange = DateTime.Now;
        }

        public int SpPoints { get; set; }
        public int AdditionalSpPoints { get; set; }
        public int MaximumSpPoints { get; set; }
        public int MaximumAdditionalSpPoints { get; set; }
        public int Gold { get; set; }
        public DateTime LastMapChange { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Mp { get; set; }
        public int MaxMp { get; set; }
        public ITarget Target { get; set; }
        public byte JobLevel { get; set; }

        public void Move(Position position)
        {
            _client.SendPacket($"walk {position.X} {position.Y} 0 {Speed}");

            if (_client.Type == ClientType.LOCAL) // Trick for moving player (need to find something better)
            {
                _client.ReceivePacket($"tp {(byte)EntityType} {Id} {position.X} {position.Y} 0");
            }
        }

        public void ShowMessage(string message, MessageType messageType)
        {
            _client.ReceivePacket($"msg {(byte)messageType} {message}");
        }

        public void ShowChatMessage(string message, ChatMessageColor messageColor)
        {
            _client.ReceivePacket($"say {(byte)EntityType} {Id} {(byte)messageColor} {message}");
        }

        public void ShowBubbleMessage(string message)
        {
            _client.ReceivePacket($"say {(byte)EntityType} {Id} 1 {message}");
        }

        public void ShowBubbleMessage(string message, ILivingEntity entity)
        {
            _client.ReceivePacket($"say {(byte)entity.EntityType} {entity.Id} 1 {message}");
        }
    }
}