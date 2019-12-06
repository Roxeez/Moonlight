using NtCore.API.Enums;
using NtCore.API.Game.Entities;

namespace NtCore.API.Client
{
    public interface ICommunication
    {
        void ShowMessage(string message, ChatMessageType messageType);
        void ShowDialogBubble(string message);
        void ShowDialogBubble(string message, ILivingEntity sender);
        void ShowMessage(string message, MessageType messageType);
    }
}