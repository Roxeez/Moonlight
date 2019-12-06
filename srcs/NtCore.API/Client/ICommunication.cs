using NtCore.API.Enums;
using NtCore.API.Game.Entities;

namespace NtCore.API.Client
{
    public interface ICommunication
    {
        void ReceiveChatMessage(string message, ChatMessageType messageType);
        void ShowBubble(string message);
        void ShowBubble(string message, ILivingEntity sender);
        void ReceiveMessage(string message, MessageType messageType);
    }
}