using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;

namespace NtCore.Clients
{
    public class Communication : ICommunication
    {
        private readonly IClient _client;
        
        public Communication(IClient client)
        {
            _client = client;
        }
        
        public void ReceiveChatMessage(string message, ChatMessageType messageType)
        {
            _client.ReceivePacket($"say {(byte)_client.Character.EntityType} {_client.Character.Id} {(byte)messageType} {message}");
        }

        public void ShowBubble(string message)
        {
            ShowBubble(message, _client.Character);
        }

        public void ShowBubble(string message, ILivingEntity sender)
        {
            _client.ReceivePacket($"say {(byte)sender.EntityType} {sender.Id} 1 {message}");
        }

        public void ReceiveMessage(string message, MessageType messageType)
        {
            _client.ReceivePacket($"msg {(byte)messageType} {message}");
        }
    }
}