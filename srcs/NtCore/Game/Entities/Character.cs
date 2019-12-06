using System;
using NtCore.API;
using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Inventory;
using NtCore.API.Game.Maps;
using NtCore.Game.Inventory;
using NtCore.Game.Maps;

namespace NtCore.Game.Entities
{
    public class Character : Player, ICharacter
    {
        private readonly IClient _client;
        
        public int SpPoints { get; set; }
        public int AdditionalSpPoints { get; set; }
        public int MaximumSpPoints { get; set; }
        public int MaximumAdditionalSpPoints { get; set; }
        public IEquipment Equipment { get; }
        public int Gold { get; set; }
        public DateTime LastMapChange { get; set; }

        public Character(IClient client)
        {
            _client = client;
            
            LastMapChange = DateTime.MinValue;
            Equipment = new Equipment();
        }
        
        public void ShowMessage(string message, ChatMessageType messageType)
        {
            _client.ReceivePacket($"say {(byte)_client.Character.EntityType} {_client.Character.Id} {(byte)messageType} {message}");
        }

        public void ShowDialogBubble(string message)
        {
            ShowDialogBubble(message, _client.Character);
        }

        public void ShowDialogBubble(string message, ILivingEntity sender)
        {
            _client.ReceivePacket($"say {(byte)sender.EntityType} {sender.Id} 1 {message}");
        }

        public void ShowMessage(string message, MessageType messageType)
        {
            _client.ReceivePacket($"msg {(byte)messageType} {message}");
        }
    }
}