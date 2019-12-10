using System;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Game.Battle;

namespace NtCore.Game.Entities.Impl
{
    public class Character : Player, ICharacter
    {
        public Character(IClient client)
        {
            Client = client;
            LastMapChange = DateTime.Now;
        }

        public IClient Client { get; }
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
            Client.SendPacket($"walk {position.X} {position.Y} 0 {Speed}");

            if (Client.Type == ClientType.LOCAL) // Trick for moving player (need to find something better)
            {
                Client.ReceivePacket($"tp {(byte)EntityType} {Id} {position.X} {position.Y} 0");
            }
        }

        public void ReceiveMessage(string message, MessageType messageType)
        {
            Client.ReceivePacket($"msg {(byte)messageType} {message}");
        }

        public void ReceiveChatMessage(string message, ChatMessageColor messageColor)
        {
            Client.ReceivePacket($"say {(byte)EntityType} {Id} {(byte)messageColor} {message}");
        }

        public void ShowBubbleMessage(string message)
        {
            Client.ReceivePacket($"say {(byte)EntityType} {Id} 1 {message}");
        }

        public void ShowBubbleMessage(string message, ILivingEntity entity)
        {
            Client.ReceivePacket($"say {(byte)entity.EntityType} {entity.Id} 1 {message}");
        }
    }
}