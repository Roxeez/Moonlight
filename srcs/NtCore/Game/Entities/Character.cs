using System;
using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;

namespace NtCore.Game.Entities
{
    public class Character : Player, ICharacter
    {
        private readonly IClient _client;
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

        public Character(IClient client)
        {
            _client = client;
        }
        
        public void ShowMessage(string message, MessageType messageType)
        {
            _client.SendPacket($"msg {(byte)messageType} {message}");
        }

        public void ShowChatMessage(string message, ChatMessageType messageType)
        {
            _client.SendPacket($"say {(byte)EntityType} {Id} {(byte)messageType} {message}");
        }

        public void ShowBubbleMessage(string message)
        {
            _client.SendPacket($"say {(byte)EntityType} {Id} 1 {message}");
        }

        public void ShowBubbleMessage(string message, ILivingEntity entity)
        {
            _client.SendPacket($"say {(byte)entity.EntityType} {entity.EntityType} 1 {message}");
        }
        
    }
}