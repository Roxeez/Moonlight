using System;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;

namespace NtCore.API.Client
{
    public interface IClient : IDisposable
    {
        ICharacter Character { get; }

        void SendPacket(string packet);
        void ReceivePacket(string packet);
    }
}